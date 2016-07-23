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

using Base;
using Base.Config;
using WPILib;

namespace RobotMain2017
{
    /// <summary>
    /// Class called by WPI for robot states (teleop, auto, test...)
    /// </summary>
    public class RobotMain2017 : SampleRobot
    {
        private const string CONFIG_FILE = @"robot.xml";
        private readonly Config config = new Config();

        /// <summary>
        /// Constructor
        /// </summary>
        public RobotMain2017()
        {
            Log.ClearSessionLog();
            config.Load(CONFIG_FILE);
        }

        /// <summary>
        /// Called by WPI on robot initialization
        /// </summary>
        protected override void RobotInit() => Tourniquet.Initialize.BuildControlSchema(config);

        /// <summary>
        /// Called when auton starts
        /// </summary>
        public override void Autonomous()
        {
            QuickLoad();

            if (config.AutonEnabled)
                new Trephine.Initialize(config).Run();
        }

        /// <summary>
        /// Called with teleop starts
        /// </summary>
        public override void OperatorControl()
        {
            QuickLoad();

            new Tourniquet.Driver().Start();
            new Tourniquet.Operator().Start();

            //while (IsOperatorControl && IsEnabled) { }
        }

        /// <summary>
        /// Called when Test is run
        /// </summary>
        public override void Test() {}

        private void QuickLoad()
        {
            if (!config.QuickLoad) return;

            config.Load(CONFIG_FILE);
            Tourniquet.Initialize.BuildControlSchema(config);
        }
    }
}