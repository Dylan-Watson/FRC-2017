/****************************** Header ******************************\
Interface Name: IComponent
Summary: Interface used to create all component items that map to
physical components on the robot.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using System;

namespace Base
{
    public interface IComponent
    {
        #region Public Methods

        [Obsolete("Not thread safe! Be sure to lock if you are using this method.")]
        object GetRawComponent();

        #endregion Public Methods

        #region Public Properties

        string Name { get; }

        bool InUse { get; }

        object Sender { get; }

        #endregion Public Properties
    }

    public abstract class Motor
    {
        #region Public Enums

        public enum Side
        {
            Left,
            Right
        }

        #endregion Public Enums

        protected Motor()
        {
            AllowC = true;
            AllowCc = true;
            IsReversed = false;
        }

        #region Public Properties

        public bool AllowC { get; private set; }
        public bool AllowCc { get; private set; }

        public Side DriveSide { get; protected set; }
        public bool IsDrivetrainMotor { get; protected set; }
        public bool IsReversed { get; protected set; }

        #endregion Public Properties

        #region Public Methods

        public int GetPolarity
        {
            get
            {
                if (IsReversed) return -1;

                return 1;
            }
        }

        public void RestoreDirection() => IsReversed = false;

        public void ReverseDirection() => IsReversed = true;

        public abstract void Set(double val, object sender);

        public void SetAllowC(bool val) => AllowC = val;

        //!< Sets allowC value

        public void SetAllowCc(bool val) => AllowCc = val;

        //!< Sets allowCC value

        public abstract void Stop();

        #endregion Public Methods
    }
}