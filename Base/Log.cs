/****************************** Header ******************************\
Class Name: Log
Summary: Simple loggin class to log exceptions and other data.
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
    public static class Log
    {
        #region Public Methods

        private const string SESSION_LOG_FILE = @"session_log.txt";
        private const string FULL_LOG_FILE = @"full_log.txt";

        /// <summary>
        ///     Writes a string to the log file
        /// </summary>
        /// <param name="msg">A string for dubugging info</param>
        public static void Str(string msg)
        {
            var sessionLog = File.AppendText(SESSION_LOG_FILE);
            var fullLog = File.AppendText(FULL_LOG_FILE);

            sessionLog.WriteLine(msg + "   ::" + DateTime.Now.ToString("dd/MM/yy HH:mm:ss"));
            fullLog.WriteLine(msg + "   ::" + DateTime.Now.ToString("dd/MM/yy HH:mm:ss"));
            sessionLog.Close();
            fullLog.Close();
        }

        /// <summary>
        ///     Writes and exception to the log file
        /// </summary>
        /// <param name="ex">Exception</param>
        public static void Write(Exception ex)
        {
            var sessionLog = File.AppendText(SESSION_LOG_FILE);
            var fullLog = File.AppendText(FULL_LOG_FILE);

            sessionLog.WriteLine(ex + "   ::" + DateTime.Now.ToString("dd/MM/yy HH:mm:ss"));
            fullLog.WriteLine(ex + "   ::" + DateTime.Now.ToString("dd/MM/yy HH:mm:ss"));
            sessionLog.Close();
            fullLog.Close();
        }

        /// <summary>
        ///     Clears the session log
        /// </summary>
        public static void ClearSessionLog()
        {
            if (File.Exists(SESSION_LOG_FILE))
                File.Delete(SESSION_LOG_FILE);
        }

        /// <summary>
        ///     Clears the full log
        /// </summary>
        public static void ClearFullLog()
        {
            if (File.Exists(FULL_LOG_FILE))
                File.Delete(FULL_LOG_FILE);
        }

        #endregion Public Methods
    }
}