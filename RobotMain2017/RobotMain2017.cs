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
using Tourniquet;
using Trephine;
using WPILib;

namespace RobotMain2017
{
    public class RobotMain2017 : SampleRobot
    {
        private const string CONFIG_FILE = @"robot.xml";
        private readonly Config config = new Config();

        public RobotMain2017()
        {
            Log.ClearSessionLog();
            config.Load(CONFIG_FILE);
        }

        protected override void RobotInit() => Initialize.BuildControlSchema(config);

        public override void Autonomous()
        {
            QuickLoad();

            if (config.AutonEnabled)
                new Autonomous(config).Start();
        }

        public override void OperatorControl()
        {
            QuickLoad();

            new Driver().Start();
            new Operator().Start();

            //while (IsOperatorControl && IsEnabled) { }
        }

        public override void Test() {}

        private void QuickLoad()
        {
            if (!config.QuickLoad) return;

            config.Load(CONFIG_FILE);
            Initialize.BuildControlSchema(config);
        }
    }
}