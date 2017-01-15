/****************************** Header ******************************\
Class Name: ControlCollection [singleton]
Summary: Stores and manages all ControlItems for the teleop period.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Tourniquet.ControlItems;

namespace Tourniquet
{
    internal class ControlCollection
    {
        #region Private Constructors

        private ControlCollection()
        {
        }

        #endregion Private Constructors

        #region Public Properties

        public static ControlCollection Instance => _lazy.Value;

        #endregion Public Properties

        #region Private Fields

        private static readonly Lazy<ControlCollection> _lazy =
            new Lazy<ControlCollection>(() => new ControlCollection(), true);

        private readonly List<ControlItem> driverControlItems = new List<ControlItem>();

        private readonly List<ControlItem> operatorControlItems = new List<ControlItem>();

        #endregion Private Fields

        #region Public Methods

        public void AddDriverControl(ControlItem item) => driverControlItems.Add(item);

        public void AddOperatorControl(ControlItem item) => operatorControlItems.Add(item);

        public void CleanCollection()
        {
            driverControlItems.Clear();
            operatorControlItems.Clear();
        }

        public ControlItem GetDriverControl(string name)
        {
#if USE_LOCKING
            lock (driverControlItems)
#endif
            {
                return driverControlItems.FirstOrDefault(x => x.ControlName == name);
            }
        }

        public List<ControlItem> GetDriverControls()
        {
#if USE_LOCKING
            lock (driverControlItems)
#endif
            {
                return driverControlItems;
            }
        }

        public ControlItem GetOperatorControl(string name)
        {
#if USE_LOCKING
            lock (operatorControlItems)
#endif
            {
                return operatorControlItems.FirstOrDefault(x => x.ControlName == name);
            }
        }

        public List<ControlItem> GetOperatorControls()
        {
#if USE_LOCKING
            lock (operatorControlItems)
#endif
            {
                return operatorControlItems;
            }
        }

        #endregion Public Methods
    }
}