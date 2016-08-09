using NetworkTables;
using System;

namespace Base
{
    /// <summary>
    /// Singleton for handling communication with the dashboard
    /// </summary>
    public sealed class DashboardComms
    {
        #region Private Constructors

        private DashboardComms()
        {
        }

        #endregion Private Constructors

        #region Public Properties

        /// <summary>
        /// The instance of the singleton
        /// </summary>
        public static DashboardComms Instance => _lazy.Value;

        #endregion Public Properties

        #region Private Fields

        private static readonly Lazy<DashboardComms> _lazy =
            new Lazy<DashboardComms>(() => new DashboardComms());

        private readonly NetworkTable dashboard = NetworkTable.GetTable(Constants.PRIMARY_NETWORK_TABLE);

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        /// Sends data to the dashboard regarding the state of the robot
        /// </summary>
        /// <param name="value">the state of the robot (string)</param>
        public void NotifyRobotState(object value)
        {
            dashboard.PutValue(@"ROBOT_STATE", value);
        }

        /// <summary>
        /// Sends data to the dashboard
        /// </summary>
        /// <param name="key">network table key</param>
        /// <param name="value">object value to send</param>
        public void SendData(string key, object value)
        {
            if (LoopCheck._IsTeleoporated())
                dashboard.PutValue($"TELEOP_{key}", value);
            else if (LoopCheck._IsAutonomous())
                dashboard.PutValue($"AUTON_{key}", value);
            else
                dashboard.PutValue($"{key}", value);
        }

        #endregion Public Methods
    }
}