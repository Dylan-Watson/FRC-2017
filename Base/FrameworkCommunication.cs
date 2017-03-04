/****************************** Header ******************************\
Class Name: FrameworkCommunication
Summary: Class to handle communication with the smart dashboard.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using NetworkTables;
using NetworkTables.Tables;
using System;

namespace Base
{
    /// <summary>
    ///     Singleton for handling communication with the dashboard
    /// </summary>
    public sealed class FrameworkCommunication
    {
        #region Private Constructors

        /// <summary>
        ///     Default constructor, this is only called by the class itself.
        /// </summary>
        private FrameworkCommunication()
        {
            RobotStatus.Instance.RobotStatusChanged += Instance_RobotStatusChanged;
            nTRelayComm.AddConnectionListener(new NtRelayListener(), true);
        }

        #endregion Private Constructors

        #region Public Properties

        /// <summary>
        ///     The instance of the singleton
        /// </summary>
        public static FrameworkCommunication Instance => _lazy.Value;

        #endregion Public Properties

        #region Private Classes

        private class NtRelayListener : IRemoteConnectionListener
        {
            #region Public Methods

            public void Connected(IRemote remote, ConnectionInfo info)
            {
                Instance.GetDashboardComm().PutValue(@"NTRELAY_CONNECTION", Value.MakeValue(true));
            }

            public void Disconnected(IRemote remote, ConnectionInfo info)
            {
                Instance.GetDashboardComm().PutValue(@"NTRELAY_CONNECTION", Value.MakeValue(false));
            }

            #endregion Public Methods
        }

        #endregion Private Classes

        #region Private Fields

        private static readonly Lazy<FrameworkCommunication> _lazy =
            new Lazy<FrameworkCommunication>(() => new FrameworkCommunication());

        /// <summary>
        ///     The name of the network table to communicate with the dashboard used for this class.
        /// </summary>
        private readonly NetworkTable dashboardComm = NetworkTable.GetTable(Constants.DASHBOARD_NETWORK_TABLE);

        /// <summary>
        ///     The name of the network table to communicate with vision co-processor. See <see cref="Constants" />.
        ///     <see cref="Constants.VISION_NETWORK_TABLE" />
        /// </summary>
        private readonly NetworkTable nTRelayComm = NetworkTable.GetTable(Constants.VISION_NETWORK_TABLE);

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        ///     Initialize the singleton
        /// </summary>
        public static void Initialize()
        {
            var tmp = Instance;
        }

        /// <summary>
        ///     Method to get the dashboardComm
        /// </summary>
        /// <returns>NetworkTable dashboardComm</returns>
        public NetworkTable GetDashboardComm() => dashboardComm;

        /// <summary>
        ///     Method to getg the VisionRelayComm
        /// </summary>
        /// <returns>NetworkTable nTRelayComm</returns>
        public NetworkTable GetVisonRelayComm() => nTRelayComm;

        /// <summary>
        ///     Sends data to the dashboard
        /// </summary>
        /// <param name="key">network table key</param>
        /// <param name="value">double value to send</param>
        /// <param name="announcePeriod">should the key be prefixed with the current robot state? (auton/teleop)</param>
        public void SendData(string key, double value, bool announcePeriod = true)
        {
            if (LoopCheck._IsTeleoporated() && announcePeriod)
                dashboardComm.PutNumber($"TELEOP_{key}", value);
            else if (LoopCheck._IsAutonomous() && announcePeriod)
                dashboardComm.PutNumber($"AUTON_{key}", value);
            else
                dashboardComm.PutNumber($"{key}", value);
        }

        /// <summary>
        ///     Sends data to the dashboard
        /// </summary>
        /// <param name="key">network table key</param>
        /// <param name="value">double value to send</param>
        /// <param name="announcePeriod">should the key be prefixed with the current robot state? (auton/teleop)</param>
        public void SendData(string key, object value, bool announcePeriod = true)
        {
            if (LoopCheck._IsTeleoporated() && announcePeriod)
                dashboardComm.PutValue($"TELEOP_{key}", Value.MakeValue(value));
            else if (LoopCheck._IsAutonomous() && announcePeriod)
                dashboardComm.PutValue($"AUTON_{key}", Value.MakeValue(value));
            else
                dashboardComm.PutValue($"{key}", Value.MakeValue(value));
        }

        /// <summary>
        ///     Sends data to the dashboard
        /// </summary>
        /// <param name="key">network table key</param>
        /// <param name="value">object value to send</param>
        public void SendHealthData(string key, object value)
        {
            dashboardComm.PutValue($"H_{key}", Value.MakeValue(value));
        }

        #endregion Public Methods

        #region Private Methods

        private void Instance_RobotStatusChanged(object sender, RobotStatusChangedEventArgs e)
        {
            switch (e.CurrentRobotState)
            {
                case RobotState.Auton:
                    notifyRobotState(Constants.AUTON);
                    break;

                case RobotState.Teleop:
                    notifyRobotState(Constants.TELEOP);
                    break;

                default:
                    notifyRobotState(Constants.DISABLED);
                    break;
            }
        }

        /// <summary>
        ///     Sends data to the dashboard regarding the state of the robot
        /// </summary>
        /// <param name="value">the state of the robot (string)</param>
        private void notifyRobotState(object value)
        {
            dashboardComm.PutValue(@"ROBOT_STATE", Value.MakeValue(value));
        }

        #endregion Private Methods
    }
}