/****************************** Header ******************************\
Class Name: Report [static]
Summary: Simple reporting class to report general, warning, and error messages to the driverstation.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using System;
using WPILib;

namespace Base
{
    /// <summary>
    ///     Utility class for reporting information to the log and driver station
    /// </summary>
    public static class Report
    {
        #region Public Methods

        /// <summary>
        ///     Reports a error message to the log and driver station
        /// </summary>
        /// <param name="message">message to send</param>
        public static void Error(string message)
        {
            //Console.WriteLine($"ERROR:{message}");
            Log.Str($"ERROR:{message}");
            DriverStation.ReportError($"ERROR:{message}", false);
        }

        /// <summary>
        ///     Reports a general message to the log and or driver station
        /// </summary>
        /// <param name="message">message to send</param>
        /// <param name="sendToDriverstation">wether to send it to the driverstation</param>
        public static void General(string message, bool sendToDriverstation = false)
        {
            Console.WriteLine(message);
            Log.Str(message);
            if (sendToDriverstation)
                DriverStation.ReportError(message, false);
        }

        /// <summary>
        ///     Reports a warning message to the log and driver station
        /// </summary>
        /// <param name="message">message to send</param>
        public static void Warning(string message)
        {
            //Console.WriteLine($"WARNING:{message}");
            Log.Str($"WARNING:{message}");
            DriverStation.ReportError($"WARNING:{message}", false);
        }

        #endregion Public Methods
    }
}