/****************************** Header ******************************\
Class Name: Autonomous
Summary: Entry point of autonomous.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using System.Threading.Tasks;
using Base;

namespace Trephine
{
    /// <summary>
    ///     Abstract class to define an atonomous program
    /// </summary>
    public abstract class Autonomous : ControlLoop
    {
        #region Protected Properties

        /// <summary>
        ///     Instance of BaseCalls
        /// </summary>
        protected BaseCalls baseCalls { get; } = BaseCalls.Instance;

        /// <summary>
        /// Call at th end of an autonomous routine
        /// </summary>
        protected void done()
        {
            Cancel();
            // ReSharper disable once EmptyEmbeddedStatement
            while (Status() == TaskStatus.Running) ;
            Dispose();
        }
        #endregion Protected Properties
    }
}