/****************************** Header ******************************\
Class Name: Constants
Summary: Class to house all of the constant variables used throughout
the program
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

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
        ///     The val to cut by when in brownout
        /// </summary>
        public const double BROWNOUT_MULT = 0.5;

        /// <summary>
        ///     Threshold for when the robot enters brownout protection mode 
        /// </summary>
        public const double BROWNOUT_THRESHOLD = 6.0;

        /// <summary>
        ///     The name of the network table to communicate witht he dashboard
        /// </summary>
        public const string DASHBOARD_NETWORK_TABLE = @"DASHBOARD_2017";

        /// <summary>
        ///     Constant used during communications with the dashboard
        /// </summary>
        public const string DISABLED = @"DISABLED";

        /// <summary>
        ///     Value to use with epsilon comparison of floating points
        /// </summary>
        public const double EPSILON_MIN = .00001;

        /// <summary>
        ///     The minimum value before we start accepting joystick input values
        /// </summary>
        public const double MINUMUM_JOYSTICK_RETURN = .04;

        /// <summary>
        ///     Constant used during communications with the dashboard
        /// </summary>
        public const string TELEOP = @"TELEOP";

        /// <summary>
        ///     The name of the network table to communicate with vision co-processor
        /// </summary>
        public const string VISION_NETWORK_TABLE = @"NTRELAY_2017";

        #endregion Public Fields
    }
}