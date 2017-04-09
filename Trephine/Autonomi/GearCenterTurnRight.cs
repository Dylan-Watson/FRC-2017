using WPILib;
using Base;
using System;

namespace Trephine.Autonomi
{
    internal class GearCenterTurnRight : Autonomous
    {
        #region variables

        private readonly double distance = 8250, power = .4, error = .05, inc = .00005;

        #endregion variables

        protected override void main()
        {
            baseCalls.ShiftGears(DoubleSolenoid.Value.Reverse, this);

            baseCalls.SetIntake(.5, this);

            baseCalls.GyroEncDrive(distance, power);

            baseCalls.SetMani(DoubleSolenoid.Value.Forward, this);

            baseCalls.GyroEncDrive(5000, -power);

            baseCalls.RoboReset();

            baseCalls.GyroTurn(NavX.Instance.GetAngle(), 80);

            baseCalls.GyroEncDrive(13000, power);

            baseCalls.SetLeftDrive(power);
            Timer.Delay(2);

            baseCalls.FullDriveStop();

            baseCalls.Shoot();

            baseCalls.RoboReset();

            done();
        }
    }
}
