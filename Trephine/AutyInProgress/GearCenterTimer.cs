/****************************** Header ******************************\
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

namespace Trephine.AutyInProgress
{
    /// <summary>
    ///     Drives strait for x amount of time
    /// </summary>
    internal class GearCenterTimer : Autonomous
    {
        #region Private Fields

        private readonly double driveTime = 5, power = .25;

        #endregion Private Fields

        #region Protected Methods

        protected override void main()
        {
            //shift into low gear and wait for a second 
            baseCalls.ShiftGears(DoubleSolenoid.Value.Reverse, this);
            Timer.Delay(.25);

            //drive up to the peg
            baseCalls.SetLeftDrive(power);
            baseCalls.SetRightDrive(power+.025);
            Timer.Delay(driveTime);

            //start outtake before dropping gear
            baseCalls.SetIntake(.6, this);

            //slow stop
            baseCalls.SlowStop();

            //drop gear
            baseCalls.SetMani(DoubleSolenoid.Value.Forward, this);
            Timer.Delay(.25);

            //drive back to wall
            baseCalls.SlowStart(-power);
            Timer.Delay(driveTime);

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