﻿/****************************** Header ******************************\
Class Name: DriveStrait
Summary: Practice autonomous made to drive straight
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using Base;
using WPILib;

namespace Trephine.Autonomi
{
    internal class DriveStraight : Autonomous
    {
        #region Private Fields

        private readonly double driveTime = 1, power = .5;

        #endregion Private Fields

        #region Protected Methods

        protected override void main()
        {
            //low gear
            baseCalls.ShiftGears(DoubleSolenoid.Value.Forward, this);

            //drive
            baseCalls.SetLeftDrive(power);
            baseCalls.SetRightDrive(power);
            Timer.Delay(driveTime);

            //slow stop
            baseCalls.SlowStop();

            //shift into high gear
            //baseCalls.ShiftGears(DoubleSolenoid.Value.Forward, this);

            //report that we are ALMOST done
            Report.Warning(" DriveStraight Completed");

            //done
            done();


        }

        #endregion Protected Methods
    }
}