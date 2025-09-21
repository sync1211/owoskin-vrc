using Serilog;

namespace OWOVRC.Classes.Effects.OWI
{
    public class LogWatcher
    {
        public string LogPath;
        private Thread? readerThread;
        private CancellationTokenSource? cancellationTokenSource;
        public int SleepMillis = 1000;

        public bool IsRunning
        {
            get
            {
                return readerThread?.IsAlive == true;
            }
        }

        public EventHandler<string>? OnLogLineRead;

        public LogWatcher(string logPath, int SleepMillis = 1000)
        {
            this.LogPath = logPath;
            this.SleepMillis = SleepMillis;
        }

        public void Start()
        {
            if (readerThread != null)
            {
                Log.Warning("Another reader thread may already be running, stopping existing thread before creating a new one!");
                Stop();
            }
            cancellationTokenSource = new CancellationTokenSource();
            readerThread = new Thread(ReadLog);
            readerThread.Start();
            Log.Debug("Log reader thread started!");
        }

        public void Stop()
        {
            if (readerThread == null)
            {
                Log.Warning("Unable to stop log reader thread: readerThread is null!");
                return;
            }

            if (cancellationTokenSource == null)
            {
                Log.Warning("Unable to stop log reader thread: cancellationTokenSource is null!");
                return;
            }

            cancellationTokenSource.Cancel();
            Log.Debug("Requested cancellation of log reader thread!");
            readerThread.Join(); // Wait for thread to close

            // Cleanup
            readerThread = null;
            cancellationTokenSource = null;
        }

        //this will run in a separate thread
        private void ReadLog()
        {
            if (cancellationTokenSource == null)
            {
                Log.Error("Failed to start log reader thread, CancellationTokenSource is null!");
                return;
            }

            CancellationToken cancellationToken = cancellationTokenSource.Token;
            using (FileStream fileStream = new(LogPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader reader = new(fileStream))
                {
                    reader.ReadToEnd();

                    while (true)
                    {
                        try
                        {
                            string? line = reader.ReadLine()?.Trim();
                            if (!String.IsNullOrEmpty(line))
                            {
                                Log.Verbose("New log line: '{Line}'", line);
                                OnLogLineRead?.Invoke(this, line);
                            }
                        }
                        catch (IOException e)
                        {
                            Log.Error(e, "Error reading log file!");
                        }

                        if (cancellationToken.IsCancellationRequested)
                        {
                            Log.Debug("Log reader thread cancelled!");
                            cancellationTokenSource.Dispose();
                            cancellationTokenSource = null;
                            return;
                        }

                        Thread.Sleep(SleepMillis);
                    }
                }
            }
        }
    }
}
