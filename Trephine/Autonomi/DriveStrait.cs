/****************************** Header ******************************\
Class Name: DriveStrait
Summary: Practice autonomous made to drive straight
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using WPILib;

namespace Trephine.Autonomi
{
    /// <summary>
    ///     Drives strait for x amount of time
    /// </summary>
    internal class DriveStrait : Autonomous
    {
        #region Private Fields

        private readonly double driveTime = 1, power = 1;

        #endregion Private Fields

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