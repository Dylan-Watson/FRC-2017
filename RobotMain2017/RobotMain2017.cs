/****************************** Header ******************************\
Class Name: RobotMain2017 inherits SampleRobot
Summary: Entry point from WPIlib, main class where robot routines are
started.
Project: FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using System;
using Base;
using Base.Components;
using Tourniquet;
using WPILib;
using RobotState = Base.RobotState;

namespace RobotMain2017
{
    /// <summary>
    /// Class called by WPI for robot states (teleop, auto, test...)
    /// </summary>
    public class RobotMain2017 : SampleRobot
    {
        #region Public Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public RobotMain2017()
        {
            Log.ClearSessionLog();
            config.Load(CONFIG_FILE);
        }

        #endregion Public Constructors

        #region Protected Methods

        /// <summary>
        /// Called by WPI on robot initialization
        /// </summary>
        protected override void RobotInit() => Initialize.BuildControlSchema(config);

        #endregion Protected Methods

        #region Private Methods

        private void quickLoad()
        {
            if (!config.QuickLoad) return;

            config.Load(CONFIG_FILE);
            Initialize.BuildControlSchema(config);
        }

        #endregion Private Methods

        #region Private Fields

        private const string CONFIG_FILE = @"robot.xml";
        private readonly Config config = new Config();

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        /// Called when auton starts
        /// </summary>
        public override void Autonomous()
        {
            quickLoad();
            RobotStatus.Instance.NotifyState(RobotState.Auton);
            if (!config.AutonEnabled) return;
            new Trephine.Initialize(config).Run();
        }

        /// <summary>
        /// Called with teleop starts
        /// </summary>
        public override void OperatorControl()
        {
            quickLoad();
            RobotStatus.Instance.NotifyState(RobotState.Teleop);
            new Driver().Start();
            new Operator().Start();
            //while (IsOperatorControl && IsEnabled) { }
        }

        /// <summary>
        /// Called when Test is run
        /// </summary>
        public override void Test()
        {
            RobotStatus.Instance.NotifyState(RobotState.Test);
            foreach (var component in config.ActiveCollection.GetActiveCollection())
            {

                Report.General($"Testing {component.Value.Name}");

                if (component.Value is Motor)
                    (component.Value as Motor).Set(1, this);
                else if (component.Value is OutputComponent)
                    (component.Value as OutputComponent).Set(1, this);
                else if (component.Value is DoubleSolenoidItem)
                {
                    if ((component.Value as DoubleSolenoidItem).GetCurrentPosition() == DoubleSolenoid.Value.Forward)
                        (component.Value as DoubleSolenoidItem).Set(DoubleSolenoid.Value.Reverse, this);
                    else
                        (component.Value as DoubleSolenoidItem).Set(DoubleSolenoid.Value.Forward, this);
                }

                Timer.Delay(.5);

                if (component.Value is Motor)
                    (component.Value as Motor).Stop();
                else if (component.Value is OutputComponent)
                    (component.Value as OutputComponent).Set(0, this);
                else if (component.Value is DoubleSolenoidItem)
                {
                    if ((component.Value as DoubleSolenoidItem).GetCurrentPosition() == DoubleSolenoid.Value.Forward)
                        (component.Value as DoubleSolenoidItem).Set(DoubleSolenoid.Value.Reverse, this);
                    else
                        (component.Value as DoubleSolenoidItem).Set(DoubleSolenoid.Value.Forward, this);
                }

                Timer.Delay(.5);
            }

            Disabled();
        }

        /// <summary>
        /// Called when the robot is disabled
        /// </summary>
        protected override void Disabled()
        {
            base.Disabled();
            RobotStatus.Instance.NotifyState(RobotState.Disabled);
        }

        #endregion Public Methods
    }
}