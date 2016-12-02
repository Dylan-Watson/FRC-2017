/****************************** Header ******************************\
Class Name: Initialize
Summary: Class used to initialize the autonomous period
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using System.Threading;
using Base;
using Base.Config;

namespace Trephine
{
    /// <summary>
    ///     Initializes the autonomous period
    /// </summary>
    public class Initialize
    {
        #region Public Constructors

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="config">instance of the config</param>
        public Initialize(Config config)
        {
            baseCalls = new BaseCalls(config);
            RobotStatus.Instance.RobotStatusChanged += Instance_RobotStatusChanged;
        }


        /// <summary>
        /// Aborts the autonomous thread if the robot state changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Instance_RobotStatusChanged(object sender, RobotStatusChangedEventArgs e)
        {
            baseCalls.FullStop();
            thread?.Abort();

            if(thread != null)
                Report.Warning("Autonomous thread was aborted before completion");

            RobotStatus.Instance.RobotStatusChanged -= Instance_RobotStatusChanged;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///     Run autonomous
        /// </summary>
        public void Run()
        {
            thread = new Thread(() => new DriveStrait(baseCalls, .5, 5).Start());

            thread.Start();
        }

        #endregion Public Methods

        #region Private Fields

        private readonly BaseCalls baseCalls;

        private Thread thread;

        #endregion Private Fields
    }
}