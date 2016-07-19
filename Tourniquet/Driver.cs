/****************************** Header ******************************\
Class Name: Driver inherits ControllerLoop
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
    public class Driver : ControllerLoop
    {
        #region Protected Methods

        protected override void Main()
        {
            foreach (var control in ControlCollection.Instance.GetDriverControls())
                control.Update();
        }

        #endregion Protected Methods
    }
}