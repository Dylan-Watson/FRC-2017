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
    /// <summary>
    ///     Drives strait for x amount of time
    /// </summary>
    internal class GearCenter : Autonomous
    {
        #region Private Fields

        private readonly double driveTime = 1000/*, power = .5*/;

        #endregion Private Fields

        #region Protected Methods

        protected override void main()
        {

            //shift into high gear
            baseCalls.ShiftGears(DoubleSolenoid.Value.Forward, this);

            //drive up to the peg
            var wd = new WatchDog(driveTime);
            wd.Start();

            var left = .5;
            var right = .5;

            baseCalls.SetLeftDrive(left);
            baseCalls.SetRightDrive(right);

            baseCalls.RightMotor().ResetEncoder();
            baseCalls.LeftMotor().ResetEncoder();

            while (wd.State == WatchDog.WatchDogState.Running)
            {
                if (baseCalls.LeftMotor().GetEncoderValue() > baseCalls.RightMotor().GetEncoderValue())
                    left -= .0001;
                if (baseCalls.RightMotor().GetEncoderValue() > baseCalls.LeftMotor().GetEncoderValue())
                    left += .0001;

                baseCalls.SetLeftDrive(left);
                baseCalls.SetRightDrive(right);

            }

            //slow stop
            baseCalls.SlowStop();

            //start outtake before dropping gear
            baseCalls.SetIntake(.25,this);

            //drop gear
            baseCalls.SetMani(DoubleSolenoid.Value.Forward, this);
            Timer.Delay(1);

            //back up
            wd.SetInMilliseconds(driveTime);
            wd.Reset();

            baseCalls.SetLeftDrive(-left);
            baseCalls.SetRightDrive(-right);

            baseCalls.RightMotor().ResetEncoder();
            baseCalls.LeftMotor().ResetEncoder();

            while (wd.State == WatchDog.WatchDogState.Running)
            {
                if (baseCalls.LeftMotor().GetEncoderValue() > baseCalls.RightMotor().GetEncoderValue())
                    left += .0001;
                if (baseCalls.RightMotor().GetEncoderValue() > baseCalls.LeftMotor().GetEncoderValue())
                    left -= .0001;

                baseCalls.SetLeftDrive(-left);
                baseCalls.SetRightDrive(-right);

            }

            //stop outtake
            baseCalls.FullStop();

            //return manipulator to down position
            baseCalls.SetMani(DoubleSolenoid.Value.Reverse, this);

            //shift into high gear
            //baseCalls.ShiftGears(DoubleSolenoid.Value.Forward, this);

            //report that we are ALMOST done
            Report.Warning(" GearCenter Completed");

            //done
            done();
            

        }

        #endregion Protected Methods
    }
}