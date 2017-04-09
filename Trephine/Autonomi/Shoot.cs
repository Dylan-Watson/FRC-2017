using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base;
using WPILib;

namespace Trephine.Autonomi
{
    internal class Shoot : Autonomous
    {
        protected override void main()
        {
            baseCalls.SetRightDrive(.7);
            Timer.Delay(4);

            baseCalls.Shoot();
                           
            baseCalls.RoboReset();

            baseCalls.SetLeftDrive(-.7);
            baseCalls.SetRightDrive(-.7);
            Timer.Delay(3);
            baseCalls.FullDriveStop();
            done();

        }
    }
}
