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
            NavX.Instance.Reset();
            while (true)
            {
                FrameworkCommunication.Instance.SendData("-NAVX_PROY", NavX.Instance.GetAngle());
            }
        }
    }
}
