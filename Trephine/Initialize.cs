using Base;

namespace Trephine
{
    /// <summary>
    /// Initializes the autonomous period
    /// </summary>
    public class Initialize
    {
        #region Public Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="config">instance of the config</param>
        public Initialize(Config config)
        {
            this.config = config;
            baseCalls = new BaseCalls(config);
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Run autonomous
        /// </summary>
        public void Run()
        {
            new DriveStrait(baseCalls, .5, 1).Start();
        }

        #endregion Public Methods

        #region Private Fields

        private readonly BaseCalls baseCalls;
        private readonly Config config;

        #endregion Private Fields
    }
}