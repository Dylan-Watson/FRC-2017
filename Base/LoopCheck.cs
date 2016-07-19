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

using WPILib;

namespace Base
{
    public static class LoopCheck
    {
        #region Public Methods

        public static bool _IsAutonomous() => DriverStation.Instance.Autonomous && DriverStation.Instance.Enabled;

        public static bool _IsTeleoporated() => DriverStation.Instance.OperatorControl && DriverStation.Instance.Enabled;

        #endregion Public Methods
    }
}