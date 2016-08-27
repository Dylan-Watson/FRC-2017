﻿using WPILib;

namespace Trephine
{
    /// <summary>
    /// Drives strait for x amount of time
    /// </summary>
    internal class DriveStrait : Autonomous
    {
        #region Private Fields

        private readonly double driveTime, power;

        #endregion Private Fields

        #region Public Constructors

        public DriveStrait(BaseCalls baseCalls, double power = .5, double seconds = .5) : base(baseCalls)
        {
            this.power = power;
            driveTime = seconds;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Start()
        {
            baseCalls.SetRightDrive(power);
            baseCalls.SetLeftDrive(power);
            Timer.Delay(driveTime);
            baseCalls.FullStop();
        }

        #endregion Public Methods
    }
}