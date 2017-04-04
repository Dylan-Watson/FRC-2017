using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib;
using Base;

namespace Trephine
{
   internal class EncoderOffset : Autonomous
    {
        protected override void main()
        {
            baseCalls.LeftMotor().ResetEncoder();
            baseCalls.RightMotor().ResetEncoder();

            baseCalls.SetLeftDrive(1);
            baseCalls.SetRightDrive(1);

            Timer.Delay(10);

            double right = baseCalls.RightMotor().GetEncoderValue() / 10;
            double left = baseCalls.LeftMotor().GetEncoderValue() / 10;

            Report.General($"left enc {left} /n right enc {right}");
            

        }
    }
}
