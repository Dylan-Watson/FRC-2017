using Base;
using static Base.CommunicationFrames;

namespace Trephine.Autonomi
{
    internal class Align : Autonomous
    {
        const double GAMMA = .011;
        const double ALPHA = .20;

        const double GAMMA2 = .075;
        const double ALPHA2 = .20;

        protected override void main()
        {
            var target = VisionMonitor.Instance.GetLatestTargetData(0).Target;

            if (!target.HasTarget)
            {
                baseCalls.SlowStop();
                return;
            }

            var offset = getOffset(target);

            if (offset > 20)
            {
                baseCalls.SetLeftDrive(-getSpeed(offset, GAMMA, ALPHA));
                baseCalls.SetRightDrive(-getSpeed(offset, GAMMA, ALPHA));
            }

            else if (offset < -20)
            {
                baseCalls.SetLeftDrive(-getSpeed(offset, GAMMA, ALPHA));
                baseCalls.SetRightDrive(-getSpeed(offset, GAMMA, ALPHA));
            }
            else if (offset < 20 && offset > -20)
            {
                /*get the diameter of the target at current position 
                 * assume diameter should be 15 units
                 */
                var diameter = getDistanceOffset(target);
                /*check if the diameter is in the "o.k." range (assumed +/- 5 units), if not, adjust
                 * stop once botty bot gets in the "o.k." range
                 */
                if (diameter > 0)
                {
                    baseCalls.SetLeftDrive(-getSpeed(diameter, GAMMA2, ALPHA2));
                    baseCalls.SetRightDrive(getSpeed(diameter, GAMMA2, ALPHA2));
                }
                else if (diameter < 0)
                {
                    baseCalls.SetLeftDrive(-getSpeed(diameter, GAMMA2, ALPHA2));
                    baseCalls.SetRightDrive(getSpeed(diameter, GAMMA2, ALPHA2));
                }
                else
                    baseCalls.SlowStop();
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

        private int getDistanceOffset(Target target)
        {
            var centre = 60;

            if (target.Radius > centre)
                return target.Radius - centre;
            else if (target.Radius < centre)
                return -(centre - target.Radius);

            return 0;
        }
    }
}
