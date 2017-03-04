/****************************** Header ******************************\
Class Name: ControlLoop inherits IDisposable
Summary: Abstract class for creating safe and controlled loops that
run within their own thread.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using System;
using System.Threading;
using Timer = WPILib.Timer;

namespace Base
{
    /// <summary>
    ///     Abstract class to create and manage a loop for robot functions
    /// </summary>
    public abstract class ControlLoop
    {
        #region Private Fields

        private volatile bool cancel;

        private double cycleTime = .05;

        private bool finished;

        private bool overrideSaftey;

        private Thread thread;

        #endregion Private Fields

        #region Public Events

        /// <summary>
        ///     Event raised when the control loop is aborted
        /// </summary>
        public event EventHandler<EventArgs> Aborted;

        /// <summary>
        ///     Event raised when a cancelation request is complete
        /// </summary>
        public event EventHandler<EventArgs> CancelationComplete;

        /// <summary>
        ///     Event raised when a cancelation is requested
        /// </summary>
        public event EventHandler<EventArgs> CancelationRequest;

        /// <summary>
        ///     Event raised when a cancelation request is complete
        /// </summary>
        public event EventHandler<EventArgs> Finished;

        /// <summary>
        ///     Event raised when a cancelation is requested
        /// </summary>
        public event EventHandler<EventArgs> SignaledCompletion;

        /// <summary>
        ///     Event raised when a cancelation request is complete
        /// </summary>
        public event EventHandler<EventArgs> Started;

        #endregion Public Events

        #region Public Methods

        /// <summary>
        ///     Aborts the thread, instantly stopping execution. If possible always try to cancel over abort
        /// </summary>
        public void Abort(object sender)
        {
            if (thread != null && thread.IsAlive)
            {
                thread?.Abort();
                Aborted?.Invoke(sender, new EventArgs());
            }
        }

        /// <summary>
        ///     Cancels the loop asynchronously at the next available time
        /// </summary>
        public void CancelAsync(object sender)
        {
            cancel = true;
            Report.General($"{GetType()} cancellation requested.");
            CancelationRequest?.Invoke(sender, new EventArgs());
        }

        /// <summary>
        ///     Cancels the loop synchronously at the next available time
        /// </summary>
        public void CancelSync(object sender)
        {
            cancel = true;
            Report.General($"{GetType()} cancellation requested.");
            CancelationRequest?.Invoke(sender, new EventArgs());
            while (thread.IsAlive)
                // ReSharper disable once EmptyEmbeddedStatement
                ;
        }

        /// <summary>
        ///     Returns the status of the thread that the loop is in
        /// </summary>
        /// <returns></returns>
        public bool IsAlive() => thread.IsAlive;

        /// <summary>
        ///     Sets the time in miliseconds that the loop will wait each iteration, the default is .005 seconds
        /// </summary>
        /// <param name="seconds">time in seconds that the loop will wait each iteration</param>
        public void OverrideCycleTime(double seconds) => cycleTime = seconds;

        /// <summary>
        ///     Sets the time in miliseconds that the loop will wait to its default value, .005 seconds
        /// </summary>
        public void SetToDefaultCycleTime() => cycleTime = .05;

        /// <summary>
        ///     Starts the loop in a new thread
        /// </summary>
        public void Start(bool overrideSaftey = false)
        {
            this.overrideSaftey = overrideSaftey;
            finished = false;
            cancel = false;
            Report.General($"Spinning up the {GetType()} system.");
            thread = new Thread(loop);
            thread.Start();
            Started?.Invoke(this, new EventArgs());
        }

        /// <summary>
        ///     Starts the loop then the robot is either in Auton or Teleop,
        ///     and kills the loop otherwise.
        /// </summary>
        public void StartWhenReady()
        {
            RobotStatus.Instance.RobotStatusChanged += Instance_RobotStatusChanged;
        }

        /// <summary>
        ///     Returns the status of the thread that the loop is in
        /// </summary>
        /// <returns></returns>
        public ThreadState ThreadState() => thread.ThreadState;

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        ///     Gracefully stops the execution of the loop.
        ///     This should be called when you are finished.
        /// </summary>
        protected void done()
        {
            finished = true;
            SignaledCompletion?.Invoke(this, new EventArgs());
        }

        /// <summary>
        ///     Method for the implimentor to implement, this is what is called within the loop
        /// </summary>
        protected abstract void main();

        #endregion Protected Methods

        #region Private Methods

        private void backgroundLoop()
        {
            try
            {
                while (!cancel && !finished)
                {
                    if (LoopCheck._IsAutonomous() || LoopCheck._IsTeleoporated() || overrideSaftey)
                        main();

                    Timer.Delay(cycleTime);
                }
            }
            catch (ThreadAbortException)
            {
                Report.General($"{GetType()} was forcefully aborted.");
            }

            if (cancel)
            {
                CancelationComplete?.Invoke(this, new EventArgs());
                Report.General($"{GetType()} was canceled.");
            }
            else
            {
                Finished?.Invoke(this, new EventArgs());
                Report.General($"{GetType()} ran to completion.");
            }
        }

        private void Instance_RobotStatusChanged(object sender, RobotStatusChangedEventArgs e)
        {
            if (e.CurrentRobotState == RobotState.Auton || e.CurrentRobotState == RobotState.Teleop)
            {
                finished = false;
                cancel = false;
                thread = new Thread(backgroundLoop);
                thread.Start();
                Started?.Invoke(this, new EventArgs());
            }
            else
            {
                if (thread != null && thread.IsAlive)
                    Abort(RobotStatus.Instance);
            }
        }

        private void loop()
        {
            try
            {
                while (!cancel && !finished && (LoopCheck._IsAutonomous() || LoopCheck._IsTeleoporated() || overrideSaftey))
                {
                    main();
                    Timer.Delay(cycleTime);
                }
            }
            catch (ThreadAbortException)
            {
                Report.General($"{GetType()} was forcefully aborted.");
            }

            if (cancel)
            {
                CancelationComplete?.Invoke(this, new EventArgs());
                Report.General($"{GetType()} was canceled.");
            }
            else
            {
                Finished?.Invoke(this, new EventArgs());
                Report.General($"{GetType()} ran to completion.");
            }
        }

        #endregion Private Methods
    }
}