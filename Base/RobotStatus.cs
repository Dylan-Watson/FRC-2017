using System;

namespace Base
{
    public enum RobotState
    {
        Teleop,
        Auton,
        Test,
        Disabled
    }

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

        public void NotifyState(RobotState state)
        {
            CurrentRobotState = state;
            RobotStatusChanged?.Invoke(this, new RobotStatusChangedEventArgs(CurrentRobotState));
        }

        #endregion Public Methods

        #region Public Properties

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