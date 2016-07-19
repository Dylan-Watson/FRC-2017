/****************************** Header ******************************\
Class Name: Operator inherits ControllerLoop
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
    public class Operator : ControllerLoop
    {
        #region Protected Methods

        protected override void Main()
        {
            foreach (var control in ControlCollection.Instance.GetOperatorControls())
                control.Update();
        }

        #endregion Protected Methods
    }
}