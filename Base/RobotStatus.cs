using System;

namespace Base
{
    /// <summary>
    /// Defines the current state of the robot
    /// </summary>
    public enum RobotState
    {
        /// <summary>
        /// Code is currently in the teleoperational period
        /// </summary>
        Teleop,
        /// <summary>
        /// Code is currently in the autonomous period
        /// </summary>
        Auton,
        /// <summary>
        /// Code is currently in the test period
        /// </summary>
        Test,
        /// <summary>
        /// Code is currently disabled
        /// </summary>
        Disabled
    }

    /// <summary>
    /// Singleton to manage and notify the current status of the robot and robot code
    /// </summary>
    public sealed class RobotStatus
    {
        #region Private Fields

        private static readonly Lazy<RobotStatus> _lazy =
            new Lazy<RobotStatus>(() => new RobotStatus());

        #endregion Private Fields

        #region Private Constructors

        private RobotStatus()
        {
        }

        #endregion Private Constructors

        #region Public Events

        /// <summary>
        /// Event used for RobotStatusChanged
        /// </summary>
        public event EventHandler<RobotStatusChangedEventArgs> RobotStatusChanged;

        #endregion Public Events

        #region Public Methods

        /// <summary>
        /// Method used to change the robot state and fire respective events
        /// </summary>
        /// <param name="state">the robot state</param>
        public void NotifyState(RobotState state)
        {
            CurrentRobotState = state;
            RobotStatusChanged?.Invoke(this, new RobotStatusChangedEventArgs(CurrentRobotState));
        }

        #endregion Public Methods

        #region Public Properties

        /// <summary>
        /// Holds the current robot state
        /// </summary>
        public RobotState CurrentRobotState { get; private set; }

        /// <summary>
        /// Instance of the singleton
        /// </summary>
        public static RobotStatus Instance => _lazy.Value;

        #endregion Public Properties
    }

    /// <summary>
    /// EventArgs for the EventHandler of IComponents
    /// </summary>
    public class RobotStatusChangedEventArgs : EventArgs
    {
        #region Public Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="state">current state of the robot passed to the event</param>
        public RobotStatusChangedEventArgs(RobotState state)
        {
            CurrentRobotState = state;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Defines the RobotState to pass to registered events
        /// </summary>
        public RobotState CurrentRobotState { get; }

        #endregion Public Properties
    }
}