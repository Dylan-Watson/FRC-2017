using Base;
using WPILib;

namespace Trephine.Autonomi
{
    /// <summary>
    /// sends the  dahsboard navx values
    /// </summary>
    public class NavXTest : Autonomous
    {
        protected override void main()
        {
            baseCalls.LeftMotor().ResetEncoder();
            baseCalls.RightMotor().ResetEncoder();
            NavX.Instance.Reset();
            while (true)
            {
                FrameworkCommunication.Instance.SendData("-ENCLEFT", baseCalls.LeftMotor());
                FrameworkCommunication.Instance.SendData("-ENCRIGHT", baseCalls.RightMotor());
                FrameworkCommunication.Instance.SendData("-NAVX_PROY", NavX.Instance.GetAngle());
            }
        }
    }
}
