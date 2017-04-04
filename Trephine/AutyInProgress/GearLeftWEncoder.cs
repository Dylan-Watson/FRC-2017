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

namespace Trephine
{
    internal class GearLeftWEncoder : Autonomous
    {
        #region Private Fields

        private readonly double driveTime = 730, power = .7;

        #endregion Private Fields

        #region Protected Methods

        protected override void main()
        {
            //shift into high gear
            baseCalls.ShiftGears(DoubleSolenoid.Value.Forward, this);

            //drive forward
            baseCalls.EncoderDrive(power, power, driveTime);

            //stop 
            baseCalls.SlowStop();

            //turn
            baseCalls.EncoderDrive(power, -power, 425);

            //stop
            baseCalls.SlowStop();

            //drive forward just a little bit
            baseCalls.EncoderDrive(power, power, 300);

            //stop drive train
            baseCalls.SlowStop();

            //start intake
            baseCalls.SetIntake(.25, this);

            //stop
            baseCalls.SlowStop();

            //drop gear
            baseCalls.SetMani(DoubleSolenoid.Value.Forward, this);
            Timer.Delay(1);

            //back up
            baseCalls.EncoderDrive(-power, -power, 100);

            //return manipulator to down position
            baseCalls.SetMani(DoubleSolenoid.Value.Reverse, this);

            //stop
            baseCalls.SlowStop();

            //stop outtake
            baseCalls.FullStop();

            //report that we are ALMOST done
            Report.Warning(" GearLeft Completed");

            //done
            done();
        }

        #endregion Protected Methods
    }
}

