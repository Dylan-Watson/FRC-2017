using Base;
using WPILib;

namespace Trephine
{
    internal class Encodre : Autonomous
    {

        #region Private Fields

        double driveTime = 1000;

        #endregion Private Fields

        #region Protected Methods

        protected override void main()
        {
            var wd = new WatchDog(driveTime);
            wd.Start();

            var left = .5;
            var right = .5;

            baseCalls.SetLeftDrive(left);
            baseCalls.SetRightDrive(right);

            baseCalls.RightMotor().ResetEncoder();
            baseCalls.LeftMotor().ResetEncoder();

            while (wd.State == WatchDog.WatchDogState.Running)
            {
                if (baseCalls.LeftMotor().GetEncoderValue() > baseCalls.RightMotor().GetEncoderValue())
                    left -= .0001;
                if (baseCalls.RightMotor().GetEncoderValue() > baseCalls.LeftMotor().GetEncoderValue())
                    left += .0001;

                baseCalls.SetLeftDrive(left);
                baseCalls.SetRightDrive(right);
                
            }

            baseCalls.SlowStop();
            done();
        }

        #endregion Protected Methods

    }
}