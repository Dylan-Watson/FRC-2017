using Base;
using static Base.CommunicationFrames;

namespace Trephine.Autonomi
{
    internal class Align : Autonomous
    {
        protected override void main()
        {
            var target = VisionMonitor.Instance.GetLatestTargetData(0).Target;

            if (!target.HasTarget)
            {
                baseCalls.FullStop();
                return;
            }


            if (getOffset(target) > 20)
            {
                baseCalls.SetLeftDrive(-.25);
                baseCalls.SetRightDrive(.25);
            }

            else if (getOffset(target) < -20)
            {
                baseCalls.SetLeftDrive(.25);
                baseCalls.SetRightDrive(-.25);
            }
            else if (getOffset(target) < 20 && getOffset(target) > -20)
                baseCalls.FullStop();
        }

        private int getOffset(Target target)
        {
            var centre = target.Width / 2;

            if (target.X > centre)
                return target.X - centre;
            if (target.X < centre)
                return -(centre - target.X);

            return 0;
        }
    }
}
