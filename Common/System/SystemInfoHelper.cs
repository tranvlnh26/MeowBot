using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MeowBot.Common.System;

public static class SystemInfoHelper
{
    private static readonly DateTime StartTime = DateTime.UtcNow;

    public static string GetUptime() => (DateTime.UtcNow - StartTime).ToString(@"d\d\ h\h\ m\m");

    public static string GetRamUsage()
    {
        var process = Process.GetCurrentProcess();
        var usedMemoryBytes = process.PrivateMemorySize64;
        var usedMemory = Math.Round(usedMemoryBytes / (1024.0 * 1024.0), 2);
        
        return $"{usedMemory} MB";
    }

    public static string GetOsPlatform() => 
        $"{RuntimeInformation.OSDescription} ({RuntimeInformation.OSArchitecture})";

    public static string GetRuntime() => 
        RuntimeInformation.FrameworkDescription;
}