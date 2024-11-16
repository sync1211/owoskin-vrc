﻿using BlobHandles;
using BuildSoft.OscCore;
using Serilog;

namespace OWOVRC.Classes.OSC
{
    public partial class OSCReceiver(int port = 9001): IDisposable
    {
        private bool disposed;

        private const string OSC_ADDRESS = "/avatar/parameters/";
        private readonly OscServer receiver = new(port);

        public EventHandler<OSCMessage>? OnMessageReceived;

        public void Start()
        {
            receiver.AddMonitorCallback(MessageReceived);
            receiver.Start();
            Log.Information("OSC listener started on port {port}!", port);
        }

        private void MessageReceived(BlobString address, OscMessageValues values)
        {
            string addressString = address.ToString();
            if (!addressString.StartsWith(OSC_ADDRESS, StringComparison.CurrentCultureIgnoreCase))
            {
                Log.Verbose("Ignoring non-vrchat message at {address}", addressString);
                return;
            }

            if (values.ElementCount == 0)
            {
                Log.Verbose("Message at {address} does not include any values, ignoring.", addressString);
                return;
            }

            OnMessageReceived?.Invoke(this, new OSCMessage(addressString.Remove(0, OSC_ADDRESS.Length), values));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed || !disposing)
            {
                return;
            }

            receiver.Dispose();
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
