using Base;
using Base.Config;

namespace Trephine
{
    public class BaseCalls
    {
        private readonly Config config;

        public BaseCalls(Config config)
        {
            this.config = config;
        }

        public void SetRightDrive(double value)
            => config.ActiveCollection.GetRightDriveMotors.ForEach(s => ((Motor) s).Set(value, this));

        public void SetLeftDrive(double value)
            => config.ActiveCollection.GetLeftDriveMotors.ForEach(s => ((Motor) s).Set(value, this));

        public void FullStop()
        {
            config.ActiveCollection.GetRightDriveMotors.ForEach(s => ((Motor) s).Stop());
            config.ActiveCollection.GetLeftDriveMotors.ForEach(s => ((Motor) s).Stop());
        }
    }
}