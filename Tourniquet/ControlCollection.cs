/****************************** Header ******************************\
Class Name: ControlCollection [singleton]
Summary: Stores and manages all ControlItems for the teleop period.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

namespace Tourniquet
{
    using System;
    using System.Collections.Generic;
    using Tourniquet.ControlItems;

    internal class ControlCollection
    {
        #region Private Constructors

        private ControlCollection()
        {
        }

        #endregion Private Constructors

        #region Private Fields

        private static readonly Lazy<ControlCollection> _lazy =
            new Lazy<ControlCollection>(() => new ControlCollection(), true);

        private readonly List<ControlItem> driverControlItems = new List<ControlItem>();

        private readonly List<ControlItem> operatorControlItems = new List<ControlItem>();

        #endregion Private Fields

        #region Public Methods

        public static ControlCollection Instance => _lazy.Value;

        public void AddDriverControl(ControlItem item) => driverControlItems.Add(item);

        public void AddOperatorControl(ControlItem item) => operatorControlItems.Add(item);

        public void CleanCollection()
        {
            driverControlItems.Clear();
            operatorControlItems.Clear();
        }

        public List<ControlItem> GetDriverControls()
        {
            lock (driverControlItems)
                return driverControlItems;
        }

        public List<ControlItem> GetOperatorControls()
        {
            lock (operatorControlItems)
                return operatorControlItems;
        }

        #endregion Public Methods
    }
}