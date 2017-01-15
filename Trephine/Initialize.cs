﻿/****************************** Header ******************************\
Class Name: Initialize
Summary: Class used to initialize the autonomous period
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using Base;
using Base.Config;
using NetworkTables;
using NetworkTables.Tables;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Trephine
{
    /// <summary>
    ///     Initializes the autonomous period
    /// </summary>
    public class Initialize
    {
        private Autonomous auton;

        #region Public Methods

        /// <summary>
        ///     Run autonomous
        /// </summary>
        public void Run()
        {
            thread = new Thread(() => auton.Start());

            thread.Start();
        }

        #endregion Public Methods

        private class TableActivityListener : ITableListener
        {
            private readonly Initialize parent;

            public TableActivityListener(Initialize parent)
            {
                this.parent = parent;
            }

            public void ValueChanged(ITable source, string key, Value value, NotifyFlags flags)
            {
                if (key != @"AUTON_SELECT") return;
                foreach (
                    var auton in Assembly.GetExecutingAssembly().GetTypes().Where(t => string.Equals(t.Namespace,
                        @"Trephine.Autonomi", StringComparison.Ordinal)))
                {
                    if (auton.Name != value.GetString()) continue;
                    parent.auton = (Autonomous) Activator.CreateInstance(auton);
                    return;
                }
            }
        }

        #region Public Constructors

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="config">instance of the config</param>
        public Initialize(Config config)
        {
            baseCalls = BaseCalls.Instance;
            baseCalls.SetConfig(config);

            RobotStatus.Instance.RobotStatusChanged += Instance_RobotStatusChanged;

            FrameworkCommunication.Instance.SendData(@"AUTON_LIST",
                Assembly.GetExecutingAssembly().GetTypes().Where(t => string.Equals(t.Namespace,
                    @"Trephine.Autonomi", StringComparison.Ordinal)).Select(x => x.Name).ToList(), false);

            FrameworkCommunication.Instance.GetDashboardComm().AddTableListener(new TableActivityListener(this), true);
        }

        /// <summary>
        ///     Aborts the autonomous thread if the robot state changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Instance_RobotStatusChanged(object sender, RobotStatusChangedEventArgs e)
        {
            baseCalls.FullStop();
            thread?.Abort();

            if (thread != null)
                Report.Warning("Autonomous thread was aborted before completion");

            RobotStatus.Instance.RobotStatusChanged -= Instance_RobotStatusChanged;
        }

        #endregion Public Constructors

        #region Private Fields

        private readonly BaseCalls baseCalls;

        private Thread thread;

        #endregion Private Fields
    }
}