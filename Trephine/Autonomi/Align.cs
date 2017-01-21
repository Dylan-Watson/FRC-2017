using Base;
using static Base.CommunicationFrames;

namespace Trephine.Autonomi
{
    internal class Align : Autonomous
    {
        const double GAMMA = .011;
        const double ALPHA = .20;

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
                baseCalls.SetLeftDrive(-getSpeed(offset, GAMMA, ALPHA));
                baseCalls.SetRightDrive(getSpeed(offset, GAMMA, ALPHA));
            }

            else if (offset < -20)
            {
                baseCalls.SetLeftDrive(-getSpeed(offset, GAMMA, ALPHA));
                baseCalls.SetRightDrive(getSpeed(offset, GAMMA, ALPHA));
            }
            else if (offset < 20 && getOffset(target) > -20)
            {
                /*get the diameter of the target at current position 
                 * assume diameter should be 15 units
                 */
                var diameter = target.Radius * 2;
                /*check if the diameter is in the "o.k." range (assumed +/- 5 units), if not, adjust
                 * stop once botty bot gets in the "o.k." range
                 */
                if (diameter > 20)
                {
                    baseCalls.SetLeftDrive(-getSpeed(offset, GAMMA, ALPHA));
                    baseCalls.SetRightDrive(-getSpeed(offset, GAMMA, ALPHA));
                }
                else if (diameter < 10)
                {
                    baseCalls.SetLeftDrive(getSpeed(offset, GAMMA, ALPHA));
                    baseCalls.SetRightDrive(getSpeed(offset, GAMMA, ALPHA));
                }
                else
                    baseCalls.FullStop();
            }
        }

        private double getSpeed(int offset, double gamma, double alpha)
        {
            return (offset * gamma * alpha);
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
