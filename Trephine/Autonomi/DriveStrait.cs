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

        private readonly double driveTime = .75, power = 1;

        #endregion Private Fields

        #region Protected Methods

        protected override void main()
        {
            /* 
            baseCalls.SetRightDrive(power);
            baseCalls.SetLeftDrive(power);
            Timer.Delay(driveTime);
            baseCalls.SlowStop();
            done();
            */

            /*
             * drive forward
             */
            baseCalls.SlowStart(power);
            Timer.Delay(driveTime);

            /*
             * stop
             */
            baseCalls.SlowStop();

            /*
             * start outtake
             */
            baseCalls.SetIntake(.25,this);

            /*
             * mani
             */
            baseCalls.SetMani(DoubleSolenoid.Value.Forward, this);
            Timer.Delay(1);

            /*
             * stop outtake
             */
            baseCalls.FullStop();

            /*
             * drive back
             */
            baseCalls.SlowStart(-power);
            Timer.Delay(driveTime);
            baseCalls.SlowStop();

            /*
             * return mani
             */
            baseCalls.SetMani(DoubleSolenoid.Value.Reverse, this);

            /*
             * done
             */
            done();

        }

        #endregion Protected Methods
    }
}