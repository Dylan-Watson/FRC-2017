using WPILib;
using Base;
using System;

namespace Trephine.AutyInProgress
{
    internal class GearCenterShootLeft : Autonomous
    {

        #region variables

        private readonly double distance = 8250, power = .4, error = .05, inc = .00005;

        #endregion variables


        protected override void main()
        {
            baseCalls.SetIntake(.5, this);

            baseCalls.GyroEncDrive(distance, power);

            baseCalls.SetMani(DoubleSolenoid.Value.Forward, this);

            baseCalls.GyroEncDrive(3500, -power);

            baseCalls.RoboReset();

            baseCalls.GyroTurn(NavX.Instance.GetAngle(), -100);
            Report.General($"Gyro0: {NavX.Instance.GetAngle()}");

            baseCalls.GyroEncDrive(10000, power);
           // baseCalls.GyroEncDrive(7500, power);
            Report.General($"Gyro1: {NavX.Instance.GetAngle()}");

            //baseCalls.Straighten(power, 0);

            baseCalls.Shoot();

            done();

        }
    }
}
