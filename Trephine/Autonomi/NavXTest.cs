using Base;

namespace Trephine.Autonomi
{
    public class NavXTest : Autonomous
    {
        protected override void main()
        {
            while (true)
            {
                FrameworkCommunication.Instance.SendData("ANGLE", NavX.Instance.GetAngle());
            }
        }
    }
}
