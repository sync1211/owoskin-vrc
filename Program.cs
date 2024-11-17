// See https://aka.ms/new-console-template for more information
using OWOVRC.Classes;
using OWOVRC.Classes.OSC;
using OWOVRC.Classes.OWOSuit;
using OWOVRC.Classes.Effects;
using OWOVRC.Classes.Settings;
using Serilog;
using Serilog.Core;

//TODO: Create an UI for this

// Logger
LoggingLevelSwitch logLevel = Logging.SetUpLogger();
logLevel.MinimumLevel = Serilog.Events.LogEventLevel.Debug;

Log.Information("Starting up...");

// Get Settings
ConnectionSettings settings = new();
VelocitySensationSettings velocitySettings = new();
CollisionSensationSettings collisionSettings = new();

// Prepare OWOHelper
OWOHelper owo = new(settings.OWOAddress);

// Prepare sensation processors
OSCSensationBase[] sensations = [
    new Collision(owo, collisionSettings),
    new Velocity(owo, velocitySettings)
];

// Start OSC listener
OSCReceiver receiver = new(settings.OSCPort);
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