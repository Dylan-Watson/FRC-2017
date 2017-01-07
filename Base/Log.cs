﻿/****************************** Header ******************************\
Class Name: Log [static]
Summary: Simple logging class to log exceptions and other data.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using System;
using System.IO;

namespace Base
{
    /// <summary>
    ///     Utility class for logging functions
    /// </summary>
    public static class Log
    {
        #region Private Fields

        private const string FULL_LOG_FILE = @"full_log.txt";
        private const string SESSION_LOG_FILE = @"session_log.txt";

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        ///     Clears the full log
        /// </summary>
        public static void ClearFullLog()
        {
            try
            {
                if (File.Exists(FULL_LOG_FILE))
                    File.Delete(FULL_LOG_FILE);
            }
            catch (Exception) { }
        }

        /// <summary>
        ///     Clears the session log
        /// </summary>
        public static void ClearSessionLog()
        {
            try
            {
                if (File.Exists(SESSION_LOG_FILE))
                File.Delete(SESSION_LOG_FILE);
            }
            catch (Exception) { }
        }

        /// <summary>
        ///     Writes a string to the log file
        /// </summary>
        /// <param name="msg">A string for dubugging info</param>
        public static void Str(string msg)
        {
            try
            {
                var sessionLog = File.AppendText(SESSION_LOG_FILE);
                var fullLog = File.AppendText(FULL_LOG_FILE);

                sessionLog.WriteLine(msg + "   ::" + DateTime.Now.ToString("dd/MM/yy HH:mm:ss"));
                fullLog.WriteLine(msg + "   ::" + DateTime.Now.ToString("dd/MM/yy HH:mm:ss"));
                sessionLog.Close();
                fullLog.Close();
            }
            catch (Exception) { }
        }

        /// <summary>
        ///     Writes and exception to the log file
        /// </summary>
        /// <param name="ex">Exception</param>
        public static void Write(Exception ex)
        {
            try
            {
                var sessionLog = File.AppendText(SESSION_LOG_FILE);
                var fullLog = File.AppendText(FULL_LOG_FILE);

                sessionLog.WriteLine(ex + "   ::" + DateTime.Now.ToString("dd/MM/yy HH:mm:ss"));
                fullLog.WriteLine(ex + "   ::" + DateTime.Now.ToString("dd/MM/yy HH:mm:ss"));
                sessionLog.Close();
                fullLog.Close();
            }
            catch (Exception) { }
        }

        #endregion Public Methods
    }
}