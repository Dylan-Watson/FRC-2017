using Base;
using WPILib;
using static Base.CommunicationFrames;

namespace Trephine.Autonomi
{
    internal class Align : Autonomous
    {
        public override void Start()
        {
            while(true)
            {
                var target = VisionMonitor.Instance.GetLatestTargetData(0).Target;

                if (!target.HasTarget)
                    continue;

                //TODO: try to make the robot align (aka get 0 returned from getOffset(target))
                //remember you use these two methods to move the train baseCalls.SetRightDrive(0-1); and baseCalls.SetLeftDrive(0-1);

                Timer.Delay(.05);
            }
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
