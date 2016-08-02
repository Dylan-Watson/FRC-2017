using Base.Config;

namespace Trephine
{
    /// <summary>
    ///     Initializes the autonomous period
    /// </summary>
    public class Initialize
    {
        private readonly BaseCalls baseCalls;
        private readonly Config config;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="config">instance of the config</param>
        public Initialize(Config config)
        {
            this.config = config;
            baseCalls = new BaseCalls(config);
        }

        /// <summary>
        ///     Run autonomous
        /// </summary>
        public void Run()
        {
            new DriveStrait(baseCalls, .5, 1).Start();
        }
    }
}