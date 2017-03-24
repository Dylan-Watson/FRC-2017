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
    internal class GearLeftShoot : Autonomous
    {
        #region Private Fields

        private readonly double driveTime = 1500, power = .7;

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
            baseCalls.EncoderDrive(power, -power, 400);

            //stop
            baseCalls.SlowStop();

            //drive forward just a little bit
            baseCalls.EncoderDrive(power, power, 10);

            //stop drive train
            baseCalls.SlowStop();

            //start intake
            baseCalls.SetIntake(.25, this);

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

            //turn around and face boiler
            baseCalls.EncoderDrive(power, -power, 500);

            //scooch
            baseCalls.EncoderDrive(power, power, 2500);


            /*SHOOT
            //rev up shooter
            baseCalls.StartShooter(.85, this);

            //agitate/release balls into the shooter
            baseCalls.StartAgitator(1, this);

            //STOP shooter and agitator
            baseCalls.StopShooter();
            baseCalls.StopAgitator();
            */


            //report that we are done
            Report.Warning(" GearLeft Completed");

            //done
            done();

        }

        #endregion Protected Methods
    }
}