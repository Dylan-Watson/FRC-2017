/****************************** Header ******************************\
Class Name: WatchDog
Summary: An expiration timer used to check if a specified amount
of time has passed.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Dylan Watson, Ryan S. Cooper
Email: dylantrwatson@gmail.com, cooper.ryan@centaurisoft.org
\********************************************************************/

using System;
using System.Timers;

namespace Base
{
    /// <summary>
    ///     An expiration timer used to check if a specified amount of time has passed
    /// </summary>
    public sealed class WatchDog : IDisposable
    {
        #region Public Enums

        /// <summary>
        ///     Enum used to define the states of the timer
        /// </summary>
        public enum WatchDogState
        {
            /// <summary>
            ///     The timer is running currently
            /// </summary>
            Running,

            /// <summary>
            ///     The timer is stopped, but not necessarily expired
            /// </summary>
            Stopped,

            /// <summary>
            ///     The timer expired
            /// </summary>
            Expired
        }

        #endregion Public Enums

        #region Private Properties

        /// <summary>
        ///     Instantiation of the native C# DateTime class used for timing
        /// </summary>
        private Timer timer { get; }

        #endregion Private Properties

        #region Public Events

        /// <summary>
        ///     Event to handle when the timer expires
        /// </summary>
        public event ElapsedEventHandler IsExpired;

        #endregion Public Events

        #region Public Constructors

        /// <summary>
        ///     Contructor for the timer using seconds
        /// </summary>
        /// <param name="seconds">The amount of time (interval) for the timer to go for in seconds</param>
        public WatchDog(int seconds)
        {
            timer = new Timer(seconds * 1000);
            timer.Elapsed += Timer_Expired;
            RobotStatus.Instance.RobotStatusChanged += Instance_RobotStatusChanged;
        }

        /// <summary>
        ///     Constructor for the timer using milliseconds
        /// </summary>
        /// <param name="milliseconds">The amount of time (interval) for the timer to go for in milliseconds</param>
        public WatchDog(double milliseconds)
        {
            timer = new Timer(milliseconds);
            timer.Elapsed += Timer_Expired;
            RobotStatus.Instance.RobotStatusChanged += Instance_RobotStatusChanged;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///     Method to reset the timer and restart
        /// </summary>
        public void Reset()
        {
            Stop();
            Start();
        }

        /// <summary>
        ///     Start the timer
        /// </summary>
        public void Start()
        {
            timer.Start();
            State = WatchDogState.Running;
        }

        /// <summary>
        ///     Stop the timer
        /// </summary>
        public void Stop()
        {
            timer.Stop();
            State = WatchDogState.Stopped;
        }

        /// <summary>
        ///     Method to edit the timer interval in seconds
        /// </summary>
        /// <param name="seconds">The new interval in seconds</param>
        public void SetInSeconds(int seconds)
        {
            timer.Interval = seconds * 1000;
        }

        /// <summary>
        ///     Method to edit the timer interval in milliseconds
        /// </summary>
        /// <param name="milliseconds">The new interval in milliseconds</param>
        public void SetInMilliseconds(double milliseconds)
        {
            timer.Interval = milliseconds;
        }

        /// <summary>
        ///     Disposes of this class and its managed resources
        /// </summary>
        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        ///     Event handler for the timer.Elapsed event
        /// </summary>
        /// <param name="sender">The object invoking the method</param>
        /// <param name="e">ElapsedEvent Arguments Passed</param>
        private void Timer_Expired(object sender, ElapsedEventArgs e)
        {
            State = WatchDogState.Expired;
            IsExpired?.Invoke(this, e);
            Report.Warning("WatchDog Timer Expired Invoked!");
        }

        /// <summary>
        ///     Disposes of the instance if the robot state changes
        /// </summary>
        /// <param name="sender">The object invoking the method</param>
        /// <param name="e">RobotStatusChanged Arguments passed</param>
        private void Instance_RobotStatusChanged(object sender, RobotStatusChangedEventArgs e)
        {
            Report.Warning("WatchDog disposed due to robot state change.");
            Dispose();
        }

        /// <summary>
        ///     Releases managed and native resources
        /// </summary>
        /// <param name="disposing"></param>
        private void dispose(bool disposing)
        {
            if (!disposing) return;
#if USE_LOCKING
            lock (timer)
#endif
            {
                timer?.Dispose();
            }
        }

        #endregion Private Methods

        #region Public Properties

        /// <summary>
        ///     Defines the amount of time before the timer expires
        /// </summary>
        public double ExTime => timer.Interval;

        /// <summary>
        ///     Defines the current state of the timer
        /// </summary>
        public WatchDogState State { get; private set; } = WatchDogState.Stopped;

        #endregion Public Properties
    }
}