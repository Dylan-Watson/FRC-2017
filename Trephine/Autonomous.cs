/****************************** Header ******************************\
Class Name: Autonomous
Summary: Entry point of autonomous.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using Base.Config;
using WPILib;

namespace Trephine
{
    public class Autonomous
    {
        private readonly BaseCalls baseCalls;
        private readonly Config config;

        public Autonomous(Config config)
        {
            this.config = config;
            baseCalls = new BaseCalls(config);
        }

        #region Public Methods

        public void Start()
        {
            baseCalls.SetRightDrive(.5);
            baseCalls.SetLeftDrive(.5);

            Timer.Delay(1);

            baseCalls.FullStop();
        }

        #endregion Public Methods
    }
}