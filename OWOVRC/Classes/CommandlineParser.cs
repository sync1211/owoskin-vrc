﻿using OWOVRC.Classes.Helpers;
using Serilog;
using Serilog.Events;
using System.Diagnostics;
using System.Globalization;

namespace OWOVRC.Classes
{
    public class CommandlineParser
    {
        private const string AUTOSTART_SWITCH = "--start";
        private const string CPU_AFFINITY_ARG = "--affinity=";
        private const string REVERSE_AFFINITY_ARG = "--vrc-affinity=";
        private const string PROCESS_PRIORITY_ARG = "--process-priority=";
        private const string LOG_LEVEL_ARG = "--log-level=";

        public readonly bool Autostart;
        public readonly nint? CpuAffinity;
        public readonly ProcessPriorityClass? Priority;
        public readonly LogEventLevel? LogLevel;

        private readonly ProcessPriorityClass[] priorityClasses =
        [
            ProcessPriorityClass.Idle,        // -2
            ProcessPriorityClass.BelowNormal, // -1
            ProcessPriorityClass.Normal,      // 0
            ProcessPriorityClass.AboveNormal, // 1
            ProcessPriorityClass.High         // 2
         // ProcessPriorityClass.RealTime     // 3 (shouldn't be used)
        ];

        private readonly Dictionary<string, LogEventLevel> LogLevelMap = Logging.Levels
            .ToDictionary(
                level => level.ToString().ToLower(),
                level => level
            );

        public CommandlineParser(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];

                // Autostart
                if (arg.Equals(AUTOSTART_SWITCH, StringComparison.CurrentCultureIgnoreCase))
                {
                    Autostart = true;
                }

                // CPU affinity
                else if (arg.StartsWith(CPU_AFFINITY_ARG, StringComparison.CurrentCultureIgnoreCase))
                {
                    string argValue = arg.Substring(CPU_AFFINITY_ARG.Length).TrimStart('0', 'x');
                    if (!int.TryParse(argValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int affinity) || affinity <= 0)
                    {
                        Log.Error("Invalid CPU affinity value: {arg}", argValue);
                        continue;
                    }

                    CpuAffinity = new nint(affinity);
                }

                // CPU affinity (inverted)
                else if (arg.StartsWith(REVERSE_AFFINITY_ARG, StringComparison.CurrentCultureIgnoreCase))
                {
                    string argValue = arg.Substring(REVERSE_AFFINITY_ARG.Length).TrimStart('0', 'x');
                    if (!int.TryParse(argValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int affinity) || affinity > CPUHelper.MaxAffinityValue)
                    {
                        Log.Error("Invalid inverse CPU affinity value: {arg}", argValue);
                        continue;
                    }

                    if (CpuAffinity != null)
                    {
                        Log.Warning("CPU affinity already set, ignoring other CPU affinity value of {arg:X}", CpuAffinity);
                    }

                    CpuAffinity = CPUHelper.InvertAffinityValue(affinity);
                    Log.Information("VRChat's CPU affinity is {affinity:X}, setting own affinity to {invertedAffinity:X}", affinity, CpuAffinity);
                }

                // Process priority
                else if (arg.StartsWith(PROCESS_PRIORITY_ARG, StringComparison.CurrentCultureIgnoreCase))
                {
                    string argValue = arg.Substring(PROCESS_PRIORITY_ARG.Length);
                    if (!int.TryParse(argValue, out int priority))
                    {
                        Log.Error("Invalid process priority value: {arg}", argValue);
                        continue;
                    }

                    ProcessPriorityClass? priorityClass = IntToPriorityClass(priority);
                    if (priorityClass == null)
                    {
                        Log.Error("Invalid process priority value: {arg}", argValue);
                        continue;
                    }

                    if (CpuAffinity != null)
                    {
                        Log.Warning("CPU affinity already set, ignoring other CPU affinity value of {arg:X}", CpuAffinity);
                    }

                    Priority = priorityClass.Value;
                }

                // Log level
                else if (arg.StartsWith(LOG_LEVEL_ARG, StringComparison.CurrentCultureIgnoreCase))
                {
                    string argValue = arg.Substring(LOG_LEVEL_ARG.Length).ToLower();
                    if (!LogLevelMap.TryGetValue(argValue, out LogEventLevel logLevel))
                    {
                        Log.Error("Invalid log level value: {arg}", argValue);
                        continue;
                    }
                    LogLevel = logLevel;
                }
                else
                {
                    Log.Warning("Unknown commandline argument: {arg}", arg);
                }
            }
        }

        private ProcessPriorityClass? IntToPriorityClass(int priorityId)
        {
            if (priorityId < -2 || priorityId > 2)
            {
                return null;
            }

            return priorityClasses[priorityId + 2];
        }
    }
}
