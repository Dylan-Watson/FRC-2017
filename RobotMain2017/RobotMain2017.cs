﻿/****************************** Header ******************************\
Class Name: RobotMain2017 inherits SampleRobot
Summary: Entry point from WPIlib, main class where robot routines are
started.
Project: FRC2017
Robot Name: Orange Juice
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using Base;
using Base.Config;
using System.Diagnostics;
using System.Reflection;
using Tourniquet;
using WPILib;
using Initialize = Trephine.Initialize;
using RobotState = Base.RobotState;

namespace RobotMain2017
{
    /// <summary>
    ///     Class called by WPI for robot states (teleop, auto, test...)
    /// </summary>
    public class RobotMain2017 : SampleRobot
    {
        #region Public Constructors

        /// <summary>
        ///     Constructor
        /// </summary>
        public RobotMain2017()
        {
            Log.ClearSessionLog();
            config.Load(CONFIG_FILE);
            FrameworkCommunication.Initialize();
            auton = new Initialize(config);
            var hm = new HealthMonitor();
            hm.OverrideCycleTime(1);
            hm.Start(true);

            Report.General($"\n\nOrange Juice Ready! Version {Assembly.GetExecutingAssembly().GetName().Version}\n\n");
        }

        #endregion Public Constructors

        #region Private Methods

        private void quickLoad()
        {
            if (!config.QuickLoad) return;

            config.Load(CONFIG_FILE);
            Tourniquet.Initialize.BuildControlSchema(config);
        }

        #endregion Private Methods

        #region Private Fields

        private const string CONFIG_FILE = @"config.xml";
        private readonly Initialize auton;

        private readonly Config config = new Config();

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        ///     Called when auton starts
        /// </summary>
        public override void Autonomous()
        {
            quickLoad();
            RobotStatus.Instance.NotifyState(RobotState.Auton);
            if (!config.AutonEnabled) return;
            auton.Run();
        }

        /// <summary>
        ///     Called with teleop starts
        /// </summary>
        public override void OperatorControl()
        {
            quickLoad();
            RobotStatus.Instance.NotifyState(RobotState.Teleop);
            new Sensing(config).Start();
            new Driver().Start();
            new Operator().Start();
        }

        /// <summary>
        ///     Called when Test is run
        /// </summary>
        public override void Test()
        {
            RobotStatus.Instance.NotifyState(RobotState.Test);

            Disabled();
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        ///     Called when the robot is disabled
        /// </summary>
        protected override void Disabled()
        {
            base.Disabled();
            RobotStatus.Instance.NotifyState(RobotState.Disabled);
        }

        /// <summary>
        ///     Called by WPI on robot initialization
        /// </summary>
        protected override void RobotInit() => Tourniquet.Initialize.BuildControlSchema(config);

        #endregion Protected Methods
    }
}