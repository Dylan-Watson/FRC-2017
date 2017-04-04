using WPILib;
using Base;
using System;

namespace Trephine.Autonomi
{
    internal class GearCenterGyro : Autonomous
    {

        #region variables

        private readonly double distance = 8000, power = .45, error = .05, inc = .00005;

        #endregion variables


        protected override void main()
        {
            baseCalls.GyroEncDrive(distance, power);

            baseCalls.DeliverGear(.75, -power);

            baseCalls.RoboReset();

            done();

        }
    }
}
