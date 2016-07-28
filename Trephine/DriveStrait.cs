namespace Trephine
{
    using WPILib;

    /// <summary>
    ///     Drives strait for x amount of time
    /// </summary>
    internal class DriveStrait : Autonomous
    {
        private readonly double driveTime, power;

        public DriveStrait(BaseCalls baseCalls, double power = .5, double seconds = .5) : base(baseCalls)
        {
            this.power = power;
            driveTime = seconds;
        }

        public override void Start()
        {
            baseCalls.SetRightDrive(power);
            baseCalls.SetLeftDrive(power);
            Timer.Delay(driveTime);
            baseCalls.FullStop();
        }
    }
}