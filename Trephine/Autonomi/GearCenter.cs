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
    /// <summary>
    ///     Drives strait for x amount of time
    /// </summary>
    internal class GearCenter : Autonomous
    {
        #region Private Fields

        private readonly double driveTime =2, power = .5;

        #endregion Private Fields

        #region Protected Methods

        protected override void main()
        {
            //shift into low gear
            baseCalls.ShiftGears(DoubleSolenoid.Value.Reverse, this);

            //drive up to the peg
            baseCalls.SetLeftDrive(power);
            baseCalls.SetRightDrive(power+.025);
            Timer.Delay(driveTime);

            //slow stop
            baseCalls.SlowStop();

            //start outtake before dropping gear
            baseCalls.SetIntake(.25,this);

            //drop gear
            baseCalls.SetMani(DoubleSolenoid.Value.Forward, this);
            Timer.Delay(1);

            //drive back to wall
            baseCalls.SlowStart(-power);
            Timer.Delay(.5);

            //stop outtake
            baseCalls.FullStop();

            //return manipulator to down position
            baseCalls.SetMani(DoubleSolenoid.Value.Reverse, this);

            //shift into high gear
            baseCalls.ShiftGears(DoubleSolenoid.Value.Forward, this);

            //report that we are ALMOST done
            Report.Warning(" GearCenter Completed");

            //done
            done();
            

        }

        #endregion Protected Methods
    }
}