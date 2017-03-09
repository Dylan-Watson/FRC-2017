using Base;
using static Base.CommunicationFrames;

namespace Trephine.Autonomi
{
    internal class Align : Autonomous
    {
        #region Protected Methods

        protected override void main()
        {
            //variable target gets the current target
            var target = VisionMonitor.Instance.GetLatestTargetData(0).Target;

            //if the target is not in frame, stop
            if (!target.HasTarget)
            {
                baseCalls.SlowStop();
                return;
            }

            //get how far off we are from the target
            var offset = getOffset(target);

            //given that we are more than 20 units off, we will turn until the robot is within the 20 unit range
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

            //now we will get distance using what the diameter should be, and what it is
            else if (offset < 20 && offset > -20)
            {
                /*
                 * get the diameter of the target at current position
                 * assume diameter should be 15 units
                 */
                var diameter = getDistanceOffset(target);
                /*
                 * check if the diameter is in the "o.k." range (assumed +/- 5 units), if not, adjust
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
                //once we are all lined up, stop the robot
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