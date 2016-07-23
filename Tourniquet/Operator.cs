/****************************** Header ******************************\
Class Name: Operator inherits ControlLoop
Summary: Loop that handles all Operator activities.
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
    /// Class to handle the operators controls,
    /// this is a ControlLoop, see ControlLoop in Base
    /// </summary>
    public class Operator : ControlLoop
    {
        #region Protected Methods
        /// <summary>
        /// Instruction to execute within the loop
        /// </summary>
        protected override void Main()
        {
            foreach (var control in ControlCollection.Instance.GetOperatorControls())
                control.Update();
        }

        #endregion Protected Methods
    }
}