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

using System.Threading.Tasks;
using WPILib;

namespace Base
{
    /// <summary>
    /// Abstract class to create and manage a loop for robot functions
    /// </summary>
    public abstract class ControlLoop
    {
        private double cycleTime = .005;
        private bool kill;
        private Task thread;

        /// <summary>
        /// Kills or aborts the loop at next possible time
        /// </summary>
        public void Kill() => kill = true;

        /// <summary>
        /// Starts the loop in a new thread
        /// </summary>
        public void Start()
        {
            Report.General($"Spinning up the {GetType()} system.");
            thread = new Task(Loop);
            thread.Start();
        }

        /// <summary>
        /// Returns the status of the thread that the loop is in
        /// </summary>
        /// <returns></returns>
        public TaskStatus Status() => thread.Status;

        /// <summary>
        /// Sets the time in miliseconds that the loop will wait each iteration,
        /// the default is .005 seconds
        /// </summary>
        /// <param name="seconds">time in seconds that the loop will wait each iteration</param>
        public void OverrideCycleTime(double seconds) => cycleTime = seconds;

        /// <summary>
        /// Sets the time in miliseconds that the loop will wait to its default value, .005 seconds
        /// </summary>
        public void DefaultCycleTime() => cycleTime = .005;

        /// <summary>
        /// Method for the implimentor to implement, this is what is called withing the loop
        /// </summary>
        protected abstract void Main();

        private void Loop()
        {
            while (!kill && (LoopCheck._IsAutonomous() || LoopCheck._IsTeleoporated()))
            {
                Main();
                Timer.Delay(cycleTime);
            }
        }
    }
}