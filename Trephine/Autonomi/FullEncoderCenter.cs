using Base;
using WPILib;

namespace Trephine.Autonomi
{
    internal class FullEncoderCenter: Autonomous
    {
        #region Private Fields

        private readonly double power = 0.6;
        private readonly double forwardEnc = 8750;
        
        #endregion Private Fields

        #region Protected Methods

        protected override void main()
        {
            //shift into high gear
            baseCalls.ShiftGears(DoubleSolenoid.Value.Forward, this);

            //drive forward
            baseCalls.driveFullEncoder(forwardEnc, power);

            //stop 
            baseCalls.SlowStop();

            //start intake
            baseCalls.SetIntake(.25, this);

            //stop
            baseCalls.SlowStop();

            //drop gear
            baseCalls.SetMani(DoubleSolenoid.Value.Forward, this);
            Timer.Delay(1);

            //back up
            //baseCalls.SlowStart(-power);
            //Timer.Delay(1);
            baseCalls.driveFullEncoder(forwardEnc, -power);

            //return manipulator to down position
            baseCalls.SetMani(DoubleSolenoid.Value.Reverse, this);

            //stop
            baseCalls.SlowStop();

            //stop outtake
            baseCalls.FullStop();

            //report that we are ALMOST done
            Report.Warning(" Full Encoder GearLeft Completed");

            //done
            done();
        }

        #endregion Protected Methods
    }
}
