// See https://aka.ms/new-console-template for more information
using OWOVRC.Classes;
using OWOVRC.Classes.OSC;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Sensations;
using Serilog;
using Serilog.Core;

//TODO: Create an UI for this

// Logger
LoggingLevelSwitch logLevel = Logging.SetUpLogger();
logLevel.MinimumLevel = Serilog.Events.LogEventLevel.Debug;
Log.Information("Starting up...");

// Prepare OWOHelper
OWOHelper owo = new();

// Prepare sensation processors
OSCSensationBase[] sensations = [
    new Collision(owo),
    new Velocity(owo)
];

// Start OSC listener
OSCReceiver receiver = new();
RegisterSensations();
receiver.Start();

// Start main task
Task.Run(Main).Wait();

// Clean up
receiver.Dispose();
owo.Dispose();
UnregisterSensations();

async Task Main()
{
    await owo.Connect();
    try
    {
        await Task.Delay(-1);
    }
    finally
    {
        Log.Information("Quit");
    }
}

void RegisterSensations()
{
    foreach (OSCSensationBase sensation in sensations)
    {
        receiver.OnMessageReceived += sensation.OnOSCMessageReceived;
    }
}

void UnregisterSensations()
{
    foreach (OSCSensationBase sensation in sensations)
    {
        receiver.OnMessageReceived -= sensation.OnOSCMessageReceived;
    }
}
