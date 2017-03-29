using Base;
using WPILib;

namespace Trephine.Autonomi
{
    internal class SystemsCheck : Autonomous
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
            Timer.Delay(3);
            baseCalls.ShiftGears(DoubleSolenoid.Value.Forward, this);
            Timer.Delay(3);
            baseCalls.FullDriveStop();

            baseCalls.ShiftGears(DoubleSolenoid.Value.Reverse, this);
            baseCalls.SetRightDrive(1);
            Timer.Delay(3);
            baseCalls.ShiftGears(DoubleSolenoid.Value.Forward, this);
            Timer.Delay(3);
            baseCalls.FullDriveStop();

            baseCalls.ShiftGears(DoubleSolenoid.Value.Reverse, this);
            baseCalls.SetLeftDrive(1);
            baseCalls.SetRightDrive(1);
            Timer.Delay(3);
            baseCalls.ShiftGears(DoubleSolenoid.Value.Forward, this);
            Timer.Delay(3);
            baseCalls.FullDriveStop();

            /*
             * GM
             */
            baseCalls.SetIntake(.5, this);
            Timer.Delay(3);
            baseCalls.StopIntake();

            baseCalls.SetMani(DoubleSolenoid.Value.Forward, this);
            Timer.Delay(.5);
            baseCalls.SetMani(DoubleSolenoid.Value.Reverse, this);
            Timer.Delay(.5);
            baseCalls.SetMani(DoubleSolenoid.Value.Forward, this);
            Timer.Delay(.5);
            baseCalls.SetMani(DoubleSolenoid.Value.Reverse, this);
            Timer.Delay(.5);
            baseCalls.SetMani(DoubleSolenoid.Value.Forward, this);
            Timer.Delay(.5);

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
            baseCalls.ShiftHood(DoubleSolenoid.Value.Forward, this);
            Timer.Delay(.25);
            baseCalls.ShiftHood(DoubleSolenoid.Value.Reverse, this);
            Timer.Delay(.25);
            baseCalls.ShiftHood(DoubleSolenoid.Value.Forward, this);
            Timer.Delay(.25);
            baseCalls.ShiftHood(DoubleSolenoid.Value.Reverse, this);
            Timer.Delay(.25);
            baseCalls.ShiftHood(DoubleSolenoid.Value.Forward, this);
            Timer.Delay(.25);

            baseCalls.StartShooter(1, this);
            Timer.Delay(3);
            baseCalls.StopShooter();

            baseCalls.StartAgitator(.75, this);
            Timer.Delay(3);
            baseCalls.StopAgitator();


            for (int i = 100; i>=0; i --)
            {
                Report.General("PULL CLIMBER BREAKERS",true);
            }
            Timer.Delay(10);
            /*
             * climber
             */
            baseCalls.StartCimber(.75, this);
            Timer.Delay(5);
            baseCalls.StopClimber();

        }

        #endregion Protected Methods
    }
}
