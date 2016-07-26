/****************************** Header ******************************\
Class Name: LoopCheck [static]
Summary: Provides methods to check if the robot is in autonomous mode
or teleop mode.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

namespace Base
{
    using WPILib;

    /// <summary>
    ///     Utility class for custom WPI functions describing match states
    /// </summary>
    public static class LoopCheck
    {
        #region Public Methods

        /// <summary>
        ///     Checks to see if autonomous is in session and if the robot is enabled
        /// </summary>
        /// <returns>state of autonomous</returns>
        public static bool _IsAutonomous() => DriverStation.Instance.Autonomous && DriverStation.Instance.Enabled;

        /// <summary>
        ///     Checks to see if teleop is in session and if the robot is enabled
        /// </summary>
        /// <returns>state of teleop</returns>
        public static bool _IsTeleoporated() => DriverStation.Instance.OperatorControl && DriverStation.Instance.Enabled;

        #endregion Public Methods
    }
}