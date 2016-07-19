/****************************** Header ******************************\
Class Name: VictorItem inherits Motor and IComponent
Summary: Abstraction for the WPIlib Victor that extends to include
some helper and control methods.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using WPILib;

namespace Base.Components
{
    public enum VictorType
    {
        EightEightEight,
        SP
    }

    public class VictorItem : Motor, IComponent
    {
        #region Private Fields

        private readonly PWMSpeedController victor;

        #endregion Private Fields

        #region Public Properties

        public string Name { get; }

        public VictorType VictorType { get; }

        public bool InUse { get; private set; }

        public object Sender { get; private set; }

        #endregion Public Properties

        #region Public Constructors

        public VictorItem(VictorType type, int channel, string commonName, bool isReversed = false)
        {
            VictorType = type;
            if (type == VictorType.SP)
                victor = new VictorSP(channel);
            else
                victor = new Victor(channel);

            Name = commonName;
            IsReversed = isReversed;
        }

        public VictorItem(VictorType type, int channel, string commonName, Side side, bool isReversed = false)
        {
            VictorType = type;
            if (type == VictorType.SP)
                victor = new VictorSP(channel);
            else
                victor = new Victor(channel);

            Name = commonName;
            IsReversed = isReversed;
            IsDrivetrainMotor = true;
            DriveSide = side;
        }

        #endregion Public Constructors

        #region Public Methods

        public object GetRawComponent() => victor;

        public override void Set(double val, object sender)
        {
            Sender = sender;
            lock (victor)
            {
                if ((val < -Constants.MINUMUM_JOYSTICK_RETURN) && AllowCc)
                {
                    InUse = true;
                    if (IsReversed) victor.Set(-val);
                    else victor.Set(val);
                }
                else if ((val > Constants.MINUMUM_JOYSTICK_RETURN) && AllowC)
                {
                    InUse = true;
                    if (IsReversed) victor.Set(-val);
                    else victor.Set(val);
                }
                else if (InUse)
                {
                    victor.StopMotor();
                    InUse = false;
                }
            }
        }

        public override void Stop()
        {
            lock (victor)
            {
                victor.StopMotor();
                InUse = false;
            }
        }

        #endregion Public Methods
    }
}