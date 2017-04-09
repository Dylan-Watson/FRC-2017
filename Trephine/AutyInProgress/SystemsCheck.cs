using Base;
using WPILib;

namespace Trephine.AutyInProgress
{
    public class SystemsCheck : Autonomous
    {
        #region Private Fields

        #endregion Private Fields

        #region Protected Methods

        protected override void main()
        {
            /*
             * drive train
             */

            baseCalls.ShiftGears(DoubleSolenoid.Value.Reverse, this);
            baseCalls.SetLeftDrive(1);
            Timer.Delay(1.5);
            baseCalls.ShiftGears(DoubleSolenoid.Value.Forward, this);
            Timer.Delay(1.5);
            baseCalls.FullDriveStop();

            baseCalls.ShiftGears(DoubleSolenoid.Value.Reverse, this);
            baseCalls.SetRightDrive(1);
            Timer.Delay(1.5);
            baseCalls.ShiftGears(DoubleSolenoid.Value.Forward, this);
            Timer.Delay(1.5);
            baseCalls.FullDriveStop();

            baseCalls.ShiftGears(DoubleSolenoid.Value.Reverse, this);
            baseCalls.SetLeftDrive(1);
            baseCalls.SetRightDrive(1);
            Timer.Delay(1.5);
            baseCalls.ShiftGears(DoubleSolenoid.Value.Forward, this);
            Timer.Delay(1.5);
            baseCalls.FullDriveStop();

            /*
             * GM
             */
            baseCalls.SetIntake(.5, this);
            Timer.Delay(1.5);
            baseCalls.StopIntake();

            baseCalls.SetMani(DoubleSolenoid.Value.Reverse, this);
            Timer.Delay(.5);
            baseCalls.SetMani(DoubleSolenoid.Value.Forward, this);
            Timer.Delay(.5);
            baseCalls.SetMani(DoubleSolenoid.Value.Reverse, this);
            Timer.Delay(.5);
            baseCalls.SetMani(DoubleSolenoid.Value.Forward, this);
            Timer.Delay(.5);
            baseCalls.SetMani(DoubleSolenoid.Value.Reverse, this);
            Timer.Delay(.5);
            Timer.Delay(2);

            baseCalls.SetRamp(DoubleSolenoid.Value.Forward, this);
            Timer.Delay(.5);
            baseCalls.SetRamp(DoubleSolenoid.Value.Reverse, this);
            Timer.Delay(.5);
            baseCalls.SetRamp(DoubleSolenoid.Value.Forward, this);
            Timer.Delay(.5);
            baseCalls.SetRamp(DoubleSolenoid.Value.Reverse, this);
            Timer.Delay(.5);
            baseCalls.SetRamp(DoubleSolenoid.Value.Forward, this);
            Timer.Delay(.5);

            /*
             * shooter
             */

            baseCalls.StartShooter(1, this);
            Timer.Delay(3);
            baseCalls.StopShooter();

            baseCalls.StartAgitator(.75, this);
            Timer.Delay(3);
            baseCalls.StopAgitator();

            /*
             * climber
             */
            baseCalls.StartCimber(.75, this);
            Timer.Delay(3);
            baseCalls.StopClimber();

            /*
             * report
             */
            Report.General("Systems Check Completed", true);

            done();

        }

        #endregion Protected Methods
    }
}
