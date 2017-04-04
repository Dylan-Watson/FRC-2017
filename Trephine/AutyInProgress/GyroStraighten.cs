using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib;
using Base;

namespace Trephine.AutyInProgress
{
    public class GyroStraighten : Autonomous
    {
        private readonly double driveTime = 5, driveBackTime = 2.5, power = .6;

        protected override void main()
        {

            NavX.Instance.Reset();

            double straight = NavX.Instance.GetAngle();

            double current = NavX.Instance.GetAngle();


            while (true)
            {
                current = NavX.Instance.GetAngle();
                FrameworkCommunication.Instance.SendData("-NAVX_PROY", NavX.Instance.GetAngle());
                if (current > straight + 5)
                {
                    baseCalls.SetLeftDrive(-power);
                    baseCalls.SetRightDrive(power);
                }
                if (current < straight - 5)
                {
                    baseCalls.SetLeftDrive(power);
                    baseCalls.SetRightDrive(-power);
                }
                else
                    baseCalls.SlowStop();
            }
            done();
        }
    }
}
