/****************************** Header ******************************\
Class Name: ControllerLoop inherits IDisposable
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
    public abstract class ControllerLoop
    {
        private bool kill;
        private Task thread;

        public void Kill() => kill = true;

        public void Start()
        {
            Report.General($"Spinning up the {GetType()} system.");
            thread = new Task(Loop);
            thread.Start();
        }

        public TaskStatus Status() => thread.Status;

        protected abstract void Main();

        private void Loop()
        {
            while (!kill && (LoopCheck._IsAutonomous() || LoopCheck._IsTeleoporated()))
            {
                Main();
                Timer.Delay(.005);
            }
        }
    }
}