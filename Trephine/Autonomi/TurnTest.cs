using Base;
using WPILib;

namespace Trephine.Autonomi
{
    internal class TurnTest : Autonomous
    {
        #region Private Fields

        private readonly double power = 0.45;
        private readonly double enc = 1850;
        
        #endregion Private Fields

        #region Protected Methods

        protected override void main()
        {
            //shift into high gear
            baseCalls.ShiftGears(DoubleSolenoid.Value.Forward, this);

            baseCalls.turnLeftFullEncoder(enc, power);
            
            //done
            done();
        }

        #endregion Protected Methods
    }
}
