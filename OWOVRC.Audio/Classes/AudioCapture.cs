using NAudio.CoreAudioApi;
using NAudio.Dsp;
using NAudio.Wave;
using Serilog;

namespace OWOVRC.Audio.Classes
{
    public class AudioCapture : IDisposable
    {
        public EventHandler<AnalyzedAudioSample>? OnSampleRead;
        public bool IsListening
        {
            get
            {
                return capture.CaptureState == CaptureState.Capturing;
            }
        }
        public CaptureState CaptureState
        {
            get
            {
                return capture.CaptureState;
            }
        }

        public readonly AnalyzedAudioSample Analyzer;

        private readonly WasapiRecorder capture;

        private int fftWritePos = 0;
        private readonly Complex[] bufferR;
        private readonly Complex[] bufferL;
        private readonly int bytesPerSampleChannel;
        private readonly int bytesPerSample;

        private readonly int bufferLength;

        //private Func<ReadOnlySpan<byte>, Complex> CreateComplexFromBuffer = null!;

        public AudioCapture(MMDevice? device = null, uint? processID = null, int amplification = 100, bool useHammingWindow = true)
        {
            WasapiRecorderBuilder recorderBuilder = new WasapiRecorderBuilder()
                .WithLowLatency()
                .WithEventSync()                            // default: event sync (vs WithPollingSync)
                //.WithBufferLength(bufferLength)             // default: 100ms
                .WithRawMode()
                .WithFormat(new WaveFormat(44100, 16, 2));  // default: device mix format;

            if (processID != null)
            {
                recorderBuilder = recorderBuilder.WithProcessLoopback(processID.Value, ProcessLoopbackMode.IncludeTargetProcessTree);
            }
            else if (device != null)
            {
                recorderBuilder = recorderBuilder.WithDevice(device);
            }
            else
            {
                recorderBuilder = recorderBuilder.WithLoopbackCapture();
            }

            capture = recorderBuilder.Build();

            this.bufferLength = 1024;
            bufferR = new Complex[this.bufferLength];
            bufferL = new Complex[this.bufferLength];

            bytesPerSampleChannel = capture.WaveFormat.BitsPerSample / 8;
            bytesPerSample = bytesPerSampleChannel * capture.WaveFormat.Channels;

            double fftPeriod = (double)capture.WaveFormat.SampleRate / this.bufferLength;
            Analyzer = new(bufferR, bufferL, fftPeriod, amplification = 100, useHammingWindow);

            //SelectFormatProcessor(capture.WaveFormat.Encoding);

            capture.DataAvailable += OnDataAvailable;
        }

        //private void SelectFormatProcessor(WaveFormatEncoding waveEncoding)
        //{
        //    if (waveEncoding == WaveFormatEncoding.Pcm)
        //    {
        //        if (capture.WaveFormat.BitsPerSample == 16)
        //        {
        //            // 16-bit PCM
        //            Log.Debug("Audio mode: 16-bit PCM");
        //            CreateComplexFromBuffer = CreateComplexFromBuffer_PCM16;
        //        }
        //        else if (capture.WaveFormat.BitsPerSample == 32)
        //        {
        //            // 32-bit PCM
        //            Log.Debug("Audio mode: 32-bit PCM");
        //            CreateComplexFromBuffer = CreateComplexFromBuffer_PCM32;
        //        }
        //        else
        //        {
        //            // Not supported!
        //            throw new FormatException("Unsupported PCM format!");
        //        }
        //    }
        //    else if (waveEncoding == WaveFormatEncoding.IeeeFloat && (bytesPerSampleChannel == 4))
        //    {
        //        // 32-bit IEEE float
        //        Log.Debug("Audio mode: 32-bit IEEE float");
        //        CreateComplexFromBuffer = CreateComplexFromBuffer_IeeeFloat32;
        //    }
        //    else
        //    {
        //        // Not supported!
        //        throw new FormatException("Unsupported encoding!");
        //    }
        //}

        public bool Start()
        {
            try
            {
                capture.StartRecording();
            }
            catch (NAudio.CoreAudioApi.CoreAudioException e)
            {
                Log.Error("Exception when starting audio capture.", e);
                return false;
            }

            Log.Information("Audio capture started!");
            return true;
        }

        public void Stop()
        {
            capture.StopRecording();
            Log.Information("Audio capture stopped!");
        }

        private void OnDataAvailable(ReadOnlySpan<byte> bufferSpan, AudioClientBufferFlags flags, long devicePosition, long qpcPosition)
        {
            int sampleCount = bufferSpan.Length / bytesPerSample;
            int maxRead = Math.Min(this.bufferLength - fftWritePos, sampleCount);
            for (int i = 0; i < maxRead; i++)
            {
                int idx = fftWritePos + i;
                bufferL[idx] = CreateComplexFromBuffer_PCM16(bufferSpan.Slice(i * bytesPerSample, bytesPerSampleChannel));
                bufferR[idx] = CreateComplexFromBuffer_PCM16(bufferSpan.Slice((i * bytesPerSample) + bytesPerSampleChannel, bytesPerSampleChannel));
            }
            fftWritePos += maxRead;

            if (fftWritePos == this.bufferLength)
            {
                OnSampleRead?.Invoke(this, AnalyzeAudio());
                fftWritePos = 0;
            }

            for (int i = maxRead; i < sampleCount; i++)
            {
                int idx = fftWritePos + (i - maxRead);
                bufferL[idx] = CreateComplexFromBuffer_PCM16(bufferSpan.Slice(i * bytesPerSample, bytesPerSampleChannel));
                bufferR[idx] = CreateComplexFromBuffer_PCM16(bufferSpan.Slice((i * bytesPerSample) + bytesPerSampleChannel, bytesPerSampleChannel));
            }
        }

        //// 32-bit IEEE float
        //private Complex CreateComplexFromBuffer_IeeeFloat32(ReadOnlySpan<byte> sliceX)
        //{
        //    return new Complex()
        //    {
        //        X = BitConverter.ToSingle(sliceX) / 2147483648f,
        //        Y = 0
        //    };
        //}

        // 16-bit PCM
        private Complex CreateComplexFromBuffer_PCM16(ReadOnlySpan<byte> sliceX)
       {
            return new Complex()
            {
                X = BitConverter.ToInt16(sliceX) / 32768f,
                Y = 0
            };
        }

        //// 32-bit PCM
        //private Complex CreateComplexFromBuffer_PCM32(ReadOnlySpan<byte> sliceX)
        //{
        //    return new Complex()
        //    {
        //        X = BitConverter.ToInt32(sliceX),
        //        Y = 0
        //    };
        //}

        public AnalyzedAudioSample AnalyzeAudio()
        {
            FastFourierTransform.FFT(true, (int)Math.Log2(bufferL.Length), bufferR);
            FastFourierTransform.FFT(true, (int)Math.Log2(bufferL.Length), bufferL);

            return Analyzer;
        }

        private async Task DisposeAsync()
        {
            // Wait for audio capture to stop without blocking the thread used by the audio capture.
            while (capture.CaptureState == CaptureState.Stopping)
            {
                Log.Information("DISPOSE: Waiting for audio capture to stop...");
                await Task.Delay(1000)
                    .ConfigureAwait(false);
            }

            Dispose();
        }

        public void Dispose()
        {
            capture.DataAvailable -= OnDataAvailable;

            if (capture.CaptureState != CaptureState.Stopped)
            {
                capture.StopRecording();
            }

            if (capture.CaptureState == CaptureState.Stopping)
            {
                // Wait asynchronously for audio capture to stop and call Dispose again.
                // Can't be done via Thread.Sleep as it blocks the thread used by the audio capture.
                _ = Task.Run(DisposeAsync);
                return;
            }

            capture.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}