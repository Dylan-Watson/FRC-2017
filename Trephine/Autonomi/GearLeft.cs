using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib;
using Base;
namespace Trephine.Autonomi
{
    internal class GearLeft : Autonomous
    {
        private readonly double distance = 9000, power = .45;
        protected override void main()
        {
            baseCalls.GyroEncDrive(distance, power);

            Timer.Delay(.5);

            baseCalls.GyroTurn(NavX.Instance.GetAngle(), 55);
            Report.General("Angle: " + NavX.Instance.GetAngle());

            baseCalls.SetIntake(.5, this);

            baseCalls.GyroEncDrive(1000, power);



            baseCalls.SetMani(DoubleSolenoid.Value.Forward, this);

            baseCalls.SlowStart(-power);
            Timer.Delay(1);

            baseCalls.RoboReset();

            done();
        }
    }
}
