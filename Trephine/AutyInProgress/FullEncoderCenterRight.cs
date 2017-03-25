using Base;
using WPILib;

namespace Trephine
{
    internal class FullEncoderCenterRight : Autonomous
    {
        #region Private Fields

        private readonly double power = 0.45;
        private readonly double forwardEnc = 8425;
        //JOSE SAID USE BACK LENGTH FOR FORWARD
        private readonly double backEnc = 4375;
        private readonly double turn = 1750;
        private readonly double shootPower = 1.0;
        private readonly double agitatPower = 1.0;


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
            baseCalls.driveFullEncoder(backEnc, -power);

            //return manipulator to down position
            baseCalls.SetMani(DoubleSolenoid.Value.Reverse, this);

            //stop
            baseCalls.SlowStop();

            //stop outtake
            baseCalls.FullStop();

            Timer.Delay(0.35);

            baseCalls.turnRightFullEncoder(turn, power);

            Timer.Delay(0.35);

            baseCalls.driveFullEncoder(backEnc, power);

            baseCalls.StartShooter(shootPower, this);

            Timer.Delay(0.75);

            baseCalls.StartAgitator(agitatPower, this);

            //report that we are ALMOST done
            Report.Warning(" Full Encoder GearLeft Completed");

            //done
            done();
        }

        #endregion Protected Methods
    }
}
