using Base;
using static Base.CommunicationFrames;

namespace Trephine.Autonomi
{
    internal class Align : Autonomous
    {
        #region Protected Methods

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

        #endregion Protected Methods

        #region Private Fields

        private const double ALPHA = .20;
        private const double ALPHA2 = .20;
        private const double GAMMA = .011;
        private const double GAMMA2 = .075;

        #endregion Private Fields

        #region Private Methods

        private int getDistanceOffset(Target target)
        {
            var centre = 60;

            if (target.Radius > centre)
                return target.Radius - centre;
            if (target.Radius < centre)
                return -(centre - target.Radius);

            return 0;
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

        private double getSpeed(int offset, double gamma, double alpha)
        {
            return offset * gamma * alpha;
        }

        #endregion Private Methods
    }
}