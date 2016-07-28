/****************************** Header ******************************\
Class Name: Autonomous
Summary: Entry point of autonomous.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

namespace Trephine
{
    /// <summary>
    ///     Abstract class to define an atonomous program
    /// </summary>
    public abstract class Autonomous
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="baseCalls">auton base calls instance</param>
        protected Autonomous(BaseCalls baseCalls)
        {
            this.baseCalls = baseCalls;
        }

        /// <summary>
        ///     Instance of BaseCalls
        /// </summary>
        protected BaseCalls baseCalls { get; }

        #region Public Methods

        /// <summary>
        ///     Runs the auton program
        /// </summary>
        public abstract void Start();

        #endregion Public Methods
    }
}