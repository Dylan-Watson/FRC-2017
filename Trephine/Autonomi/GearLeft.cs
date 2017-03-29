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

namespace Trephine.Autonomi
{
    internal class GearLeft : Autonomous
    {
        #region Private Fields

        private readonly double driveTime = 1.325, power = .7;

        #endregion Private Fields

        #region Protected Methods

        protected override void main()
        {
            //shift into low gear
            baseCalls.ShiftGears(DoubleSolenoid.Value.Reverse, this);

            //drive forward
            baseCalls.SetLeftDrive(power);
            baseCalls.SetRightDrive(power);
            Timer.Delay(driveTime);

            //stop 
            baseCalls.SlowStop();

            //turn
            baseCalls.SlowTurn(power, -power);
            Timer.Delay(.8);

            //stop
            baseCalls.SlowStop();

            //drive forward just a little bit
            baseCalls.SetLeftDrive(power);
            baseCalls.SetRightDrive(power);
            Timer.Delay(.4);

            //stop drive train
            baseCalls.SlowStop();

            /*start intake
            baseCalls.SetIntake(.25, this);

            //drop gear
            baseCalls.SetMani(DoubleSolenoid.Value.Forward, this);
            //baseCalls.SetRamp(DoubleSolenoid.Value.Reverse, this);
            Timer.Delay(1);

            //back up
            baseCalls.SlowStart(-power);
            Timer.Delay(.1);

            //return manipulator to down position
            baseCalls.SetMani(DoubleSolenoid.Value.Reverse, this);
            //baseCalls.SetRamp(DoubleSolenoid.Value.Forward, this);

            //stop
            baseCalls.SlowStop();

            //stop outtake
            baseCalls.FullStop();

            //shift into high gear
            baseCalls.ShiftGears(DoubleSolenoid.Value.Forward, this);
            */
            //report that we are ALMOST done
            Report.Warning(" GearLeft Completed");

            //done
            done();
        }

        #endregion Protected Methods
    }
}

