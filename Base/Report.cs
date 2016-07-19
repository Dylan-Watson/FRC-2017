using System;
using WPILib;

namespace Base
{
    public static class Report
    {
        public static void General(string message, bool sendToDriverstation = false)
        {
            Console.WriteLine(message);
            Log.Str(message);
            if (sendToDriverstation)
                DriverStation.ReportError(message, false);
        }

        public static void Warning(string message)
        {
            //Console.WriteLine($"WARNING:{message}");
            Log.Str($"WARNING:{message}");
            DriverStation.ReportError($"WARNING:{message}", false);
        }

        public static void Error(string message)
        {
            //Console.WriteLine($"ERROR:{message}");
            Log.Str($"ERROR:{message}");
            DriverStation.ReportError($"ERROR:{message}", false);
        }
    }
}