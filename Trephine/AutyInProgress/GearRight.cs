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
    internal class GearRight : Autonomous
    {
        #region Private Fields

        private readonly double driveTime = .85, power = .7;

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
            Timer.Delay(.48);//.475

            //stop
            baseCalls.SlowStop();

            //drive forward just a little bit
            baseCalls.SlowStart(power);
            Timer.Delay(.04);

            //stop drive train
            baseCalls.SlowStop();

            //start intake
            baseCalls.SetIntake(.5, this);

            //drop gear
            baseCalls.SetMani(DoubleSolenoid.Value.Forward, this);
            Timer.Delay(.5);

            //back up
            baseCalls.SlowStart(-power);
            Timer.Delay(.25);

            //stop outtake
            baseCalls.FullStop();

            //return manipulator to down position
            baseCalls.SetMani(DoubleSolenoid.Value.Reverse, this);

            //stop
            baseCalls.SlowStop();

            //shift into high gear
            baseCalls.ShiftGears(DoubleSolenoid.Value.Forward, this);
            /*
            //baseCalls.GyroTurn(NavX.Instance.GetAngle(), -145);

            baseCalls.TestGyroTurn(NavX.Instance.GetAngle(), -145);

            baseCalls.SlowStart(power);
            Timer.Delay(3);

            baseCalls.Shoot();
            Timer.Delay(1);
            baseCalls.RoboReset();*/

            //report that we are ALMOST done
            Report.Warning(" GearRight Completed");

            //done
            done();
        }

        #endregion Protected Methods
    }
}

