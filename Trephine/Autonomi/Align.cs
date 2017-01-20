using Base;
using static Base.CommunicationFrames;

namespace Trephine.Autonomi
{
    internal class Align : Autonomous
    {
        const double GAMMA = .15;

        protected override void main()
        {
            var target = VisionMonitor.Instance.GetLatestTargetData(0).Target;

            if (!target.HasTarget)
            {
                baseCalls.FullStop();
                return;
            }

            var offset = getOffset(target);

            if (offset > 20)
            {
                baseCalls.SetLeftDrive(-getSpeed(offset, GAMMA));
                baseCalls.SetRightDrive(getSpeed(offset, GAMMA));
            }

            else if (offset < -20)
            {
                baseCalls.SetLeftDrive(-getSpeed(offset, GAMMA));
                baseCalls.SetRightDrive(getSpeed(offset, GAMMA));
            }
            else if (offset < 20 && getOffset(target) > -20)
                baseCalls.FullStop();
        }

        private double getSpeed(int offset, double gamma)
        {
            return (offset * gamma);
        }

        private int getOffset(Target target)
        {
            var centre = target.Width / 2;

            if (target.X > centre)
                return target.X - centre;
            else if (target.X < centre)
                return -(centre - target.X);

            return 0;
        }
    }
}
