/****************************** Header ******************************\
Class Name: Sensing inherits ControlLoop
Summary: Loop that handles all sensor activities.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using Base;
using Base.Components;
using Base.Config;

namespace Tourniquet
{
    using System;

    /// <summary>
    ///     Class to handle specific sensor functionality on the robot, this is a ControlLoop, see ControlLoop in Base
    /// </summary>
    public class Sensing : ControlLoop
    {
        #region Public Constructors

        /// <summary>
        /// Defult constructor
        /// </summary>
        /// <param name="config">instance of the config</param>
        public Sensing(Config config)
        {
            preassurePad = (AnalogInputItem) config.ActiveCollection.Get(new CommonName("p_pad"));
        }

        #endregion Public Constructors

        #region Protected Methods

        /// <summary>
        ///     Instruction to execute within the loop
        /// </summary>
        protected override void main()
        {

        }

        #endregion Protected Methods

        #region Private Fields

        private readonly AnalogInputItem preassurePad;

        #endregion Private Fields
    }
}