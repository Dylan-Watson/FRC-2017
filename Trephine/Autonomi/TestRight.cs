using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib;
using Base;

namespace Trephine.Autonomi
{
    internal class TestRight : Autonomous
    {
        private readonly double distance = 8250, power = .4, error = .05, inc = .00005;

        protected override void main()
        {
            baseCalls.SetIntake(.5, this);

            baseCalls.GyroEncDrive(distance, power);

            baseCalls.SetMani(DoubleSolenoid.Value.Forward, this);

            baseCalls.GyroEncDrive(5000, -power);

            baseCalls.RoboReset();

            baseCalls.GyroTurn(NavX.Instance.GetAngle(), 80);

            baseCalls.SetLeftDrive(power);
            baseCalls.SetRightDrive(power);
            Timer.Delay(5);

            baseCalls.StartShooter(.85, this);

            Timer.Delay(1.5);
            baseCalls.StartAgitator(.5, this);

            baseCalls.RoboReset();

            done();
        }
    }
}
