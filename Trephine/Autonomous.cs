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
        #region Protected Properties

        /// <summary>
        ///     Instance of BaseCalls
        /// </summary>
        protected BaseCalls baseCalls { get; } = BaseCalls.Instance;

        #endregion Protected Properties

        #region Public Methods

        /// <summary>
        ///     Runs the auton program
        /// </summary>
        public abstract void Start();

        #endregion Public Methods
    }
}