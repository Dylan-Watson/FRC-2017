using Base;
using WPILib;

namespace Trephine.Autonomi
{
    internal class DriveStraight : Autonomous
    {
        #region Private Fields

        #region Powers

        private readonly double power = 0.4;

        #endregion Powers

        #region Distance

        private readonly double forwardEnc = 8425;
        //JOSE SAID USE BACK LENGTH FOR FORWARD
        private readonly double backEnc = 4375;
        private readonly double turn = 1750;

        #endregion Distance

        #endregion Private Fields

        #region Protected Methods

        protected override void main()
        {
            baseCalls.GyroEncDrive(7500, power);


            //report that we are ALMOST done
            Report.Warning(" Full Encoder GearLeft ");
            //done
            done();
        }

        #endregion Protected Methods
    }
}
