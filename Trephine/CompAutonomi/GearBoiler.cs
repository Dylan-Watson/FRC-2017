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
    internal class GearBoiler : Autonomous
    {
        #region Private Fields

        private readonly double driveTime = 1.0, power = .7;

        #endregion Private Fields

        #region Protected Methods

        protected override void main()
        {
            //shift into low gear
            baseCalls.ShiftGears(DoubleSolenoid.Value.Reverse, this);

            //drive forward
            baseCalls.SlowStart(power);
            Timer.Delay(driveTime);

            //stop drivetrain
            baseCalls.SlowStop();

            //turn
            baseCalls.SlowTurn(-power, power);
            Timer.Delay(.5);

            //stop

            //drive forward just a little bit

            //start intake

            //stop drive train

            //done
            done();
        }

        #endregion Protected Methods
    }
}

