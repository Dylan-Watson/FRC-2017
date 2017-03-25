using Base;
using WPILib;

namespace Trephine
{
    public class NavXCenterShoot : Autonomous
    {

        #region Private Fields

        private readonly double driveTime = 2, power = .5;

        #endregion private fields

        #region Protected Methods

        protected override void main()
        {

            //This loop displays the gyro values on the debug part of dashboard, used for testing
            while (true)
            {
                FrameworkCommunication.Instance.SendData("-NAVX_PROY", NavX.Instance.GetAngle());

                //reset the encoder
                NavX.Instance.Reset();

                /*
                 * use OGCenterBoio to score gear and back up
                 */
                baseCalls.SetLeftDrive(power);
                baseCalls.SetRightDrive(power + .025);
                Timer.Delay(driveTime);
                //start outtake before dropping gear
                baseCalls.SetIntake(.6, this);
                //slow stop
                baseCalls.SlowStop();
                //drop gear
                baseCalls.SetMani(DoubleSolenoid.Value.Forward, this);
                Timer.Delay(1);
                //drive back to wall
                baseCalls.SlowStart(-power);
                Timer.Delay(.5);
                //stop outtake
                baseCalls.FullStop();
                //return manipulator to down position
                baseCalls.SetMani(DoubleSolenoid.Value.Reverse, this);

                //ok now turn and face the boiler

            
                //drive up to boiler


                //align

                
                //rev up shooter and wait half second

                
                //agitate


                //stop everything
            }

        }

        #endregion Protected Methods
    }
}
