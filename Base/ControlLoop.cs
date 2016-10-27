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
using System.Threading.Tasks;
using WPILib;

namespace Base
{
    /// <summary>
    ///     Abstract class to create and manage a loop for robot functions
    /// </summary>
    public abstract class ControlLoop : IDisposable
    {
        #region Protected Methods

        /// <summary>
        ///     Method for the implimentor to implement, this is what is called withing the loop
        /// </summary>
        protected abstract void main();

        #endregion Protected Methods

        #region Private Fields

        private double cycleTime = .05;

        private bool kill;

        private Task thread;

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        ///     Disposes of this IComponent and its managed resources
        /// </summary>
        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Kills or aborts the loop at next possible time
        /// </summary>
        public void Kill() => kill = true;

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
        public void Start()
        {
            kill = false;
            Report.General($"Spinning up the {GetType()} system.");
            thread = new Task(loop);
            thread.Start();
        }

        /// <summary>
        ///     TODO: Ryan, comment
        /// </summary>
        public void StartWhenReady()
        {
            RobotStatus.Instance.RobotStatusChanged += Instance_RobotStatusChanged;
        }

        /// <summary>
        ///     Returns the status of the thread that the loop is in
        /// </summary>
        /// <returns></returns>
        public TaskStatus Status() => thread.Status;

        #endregion Public Methods

        #region Private Methods

        private void backgroundLoop()
        {
            while (true)
            {
                if (kill)
                    break;

                if (LoopCheck._IsAutonomous() || LoopCheck._IsTeleoporated())
                    main();

                Timer.Delay(cycleTime);
            }
        }

        /// <summary>
        ///     Releases managed and native resources
        /// </summary>
        /// <param name="disposing"></param>
        private void dispose(bool disposing)
        {
            if (!disposing) return;
            lock (thread)
            {
                thread?.Dispose();
            }
        }

        private void Instance_RobotStatusChanged(object sender, RobotStatusChangedEventArgs e)
        {
            if ((e.CurrentRobotState == RobotState.Auton) || (e.CurrentRobotState == RobotState.Teleop))
            {
                kill = false;
                thread = new Task(backgroundLoop);
                thread.Start();
            }
            else
            {
                Kill();
            }
        }

        private void loop()
        {
            while (!kill && (LoopCheck._IsAutonomous() || LoopCheck._IsTeleoporated()))
            {
                main();
                Timer.Delay(cycleTime);
            }
        }

        #endregion Private Methods
    }
}