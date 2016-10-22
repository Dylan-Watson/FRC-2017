namespace Base
{
    /// <summary>
    ///     Class to define common program wide constants
    /// </summary>
    public static class Constants
    {
        #region Public Fields

        /// <summary>
        ///     Constant used during communications with the dashboard
        /// </summary>
        public const string AUTON = @"AUTON";

        /// <summary>
        ///     Constant used during communications with the dashboard
        /// </summary>
        public const string DISABLED = @"DISABLED";

        /// <summary>
        ///     The minimum value before we start accepting joystick input values
        /// </summary>
        public const double MINUMUM_JOYSTICK_RETURN = .04;

        /// <summary>
        ///     The name of the network table to communicate witht he dashboard
        /// </summary>
        public const string DASHBOARD_NETWORK_TABLE = @"DASHBOARD_2017";

        /// <summary>
        ///     The name of the network table to communicate with vision co-processor
        /// </summary>
        public const string VISION_NETWORK_TABLE = @"NTRELAY_2017";

        /// <summary>
        ///     Constant used during communications with the dashboard
        /// </summary>
        public const string TELEOP = @"TELEOP";

        #endregion Public Fields
    }
}