/****************************** Header ******************************\
Class Name: Driver inherits ControlLoop
Summary: Loop that handles all Driver activities.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using Base;

namespace Tourniquet
{
    /// <summary>
    /// Class to handle the drivers controls, this is a ControlLoop, see ControlLoop in Base
    /// </summary>
    public class Driver : ControlLoop
    {
        #region Protected Methods

        /// <summary>
        /// Instruction to execute within the loop
        /// </summary>
        protected override void main()
        {
            foreach (var control in ControlCollection.Instance.GetDriverControls())
                control.Update();
        }

        #endregion Protected Methods
    }
}