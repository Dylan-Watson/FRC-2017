using Base;
using WPILib;

namespace Trephine.Autonomi
{
    /// <summary>
    /// sends the  dahsboard navx values
    /// </summary>
    public class NavXTest : Autonomous
    {

        #region Private Fields

        #endregion private fields

        private readonly double driveTime = 5, driveBackTime = 2.5, power = .6;

        #region Protected Methods

        protected override void main()
        {
            NavX.Instance.Reset();
            /*while (true)
            {
                FrameworkCommunication.Instance.SendData("-NAVX_PROY", NavX.Instance.GetAngle());
            }*/

            double straight = NavX.Instance.GetAngle();

            //baseCalls.DeliverGear(driveTime, driveBackTime, power);

            double current = NavX.Instance.GetAngle();

            /*while (current > straight + .05 || current < straight - .05)
            {
                if (current > straight + .05)
                {
                    baseCalls.SetLeftDrive(-power);
                    baseCalls.SetRightDrive(power);
                }
                if (current < straight - .05)
                {
                    baseCalls.SetLeftDrive(power);
                    baseCalls.SetRightDrive(-power);
                }
            }
            baseCalls.FullDriveStop();*/

            //current = NavX.Instance.GetAngle();
            

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
                 baseCalls.FullDriveStop();
            }
            baseCalls.FullDriveStop();
            done();
        }
        #endregion Protected Methods
    }
}
