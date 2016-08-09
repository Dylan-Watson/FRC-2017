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

using System;
using WPILib;

namespace Base.Components
{
    /// <summary>
    /// Defines the type of Victor
    /// </summary>
    public enum VictorType
    {
        /// <summary>
        /// Victor 888
        /// </summary>
        EightEightEight,

        /// <summary>
        /// VictorSP
        /// </summary>
        Sp
    }

    /// <summary>
    /// Class to handle Victor motor controllers
    /// </summary>
    public class VictorItem : Motor, IComponent
    {
        #region Private Fields

        private readonly PWMSpeedController victor;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Name of the component
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Type of victor
        /// </summary>
        public VictorType VictorType { get; }

        /// <summary>
        /// Defines wether the component is in use or not
        /// </summary>
        public bool InUse { get; private set; }

        /// <summary>
        /// Defines the object issuing the commands
        /// </summary>
        public object Sender { get; private set; }

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">type of victor</param>
        /// <param name="channel">pwm channel the victor is plugged into</param>
        /// <param name="commonName">CommonName the component will have</param>
        /// <param name="isReversed">if the controller output should be reversed</param>
        /// <param name="upperLimit">Limit switch to prevent the motor from moving forward</param>
        ///<param name="lowerLimit">Limit switch to prevent the motor from moving reverse</param>
        public VictorItem(VictorType type, int channel, string commonName, bool isReversed = false, DigitalInputItem upperLimit = null, DigitalInputItem lowerLimit = null)
        {
            VictorType = type;
            if (type == VictorType.Sp)
                victor = new VictorSP(channel);
            else
                victor = new Victor(channel);

            Name = commonName;
            IsReversed = isReversed;
            UpperLimit = upperLimit;
            LowerLimit = lowerLimit;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">type of victor</param>
        /// <param name="channel">pwm channel the victor is plugged into</param>
        /// <param name="commonName">CommonName the component will have</param>
        /// <param name="side">side of the drive train the cotnroller is on</param>
        /// <param name="isReversed">if the controller output should be reversed</param>
        public VictorItem(VictorType type, int channel, string commonName, Side side, bool isReversed = false)
        {
            VictorType = type;
            if (type == VictorType.Sp)
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

        /// <summary>
        /// Gets the raw WPI PWMSpeedController object representing the victor
        /// </summary>
        /// <returns></returns>
        public object GetRawComponent() => victor;

        /// <summary>
        /// Event used for VirtualControlEvents
        /// </summary>
        public event EventHandler ValueChanged;

        /// <summary>
        /// Method to fire value changes for set/get values and InUse values
        /// </summary>
        /// <param name="e">VirtualControlEventArgs</param>
        protected virtual void onValueChanged(VirtualControlEventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Sets a value to the victor
        /// </summary>
        /// <param name="val">value to set the controller to</param>
        /// <param name="sender">the caller of this method</param>
        public override void Set(double val, object sender)
        {
            Sender = sender;
            SetAllowC(UpperLimit?.GetBool() ?? true);
            SetAllowCc(LowerLimit?.GetBool() ?? true);
            lock (victor)
            {
                if ((val < -Constants.MINUMUM_JOYSTICK_RETURN) && AllowCc)
                {
                    InUse = true;
                    if (IsReversed)
                    {
                        victor.Set(-val);
                        onValueChanged(new VirtualControlEventArgs(-val, InUse));
                    }
                    else
                    {
                        victor.Set(val);
                        onValueChanged(new VirtualControlEventArgs(val, InUse));
                    }
                }
                else if ((val > Constants.MINUMUM_JOYSTICK_RETURN) && AllowC)
                {
                    InUse = true;
                    if (IsReversed)
                    {
                        victor.Set(-val);
                        onValueChanged(new VirtualControlEventArgs(-val, InUse));
                    }
                    else
                    {
                        victor.Set(val);
                        onValueChanged(new VirtualControlEventArgs(val, InUse));
                    }
                }
                else if (InUse)
                {
                    victor.StopMotor();
                    InUse = false;
                    onValueChanged(new VirtualControlEventArgs(val, InUse));
                }
            }
        }

        /// <summary>
        /// Stops the controller
        /// </summary>
        public override void Stop()
        {
            lock (victor)
            {
                victor.StopMotor();
                InUse = false;
                Sender = null;
                onValueChanged(new VirtualControlEventArgs(0, InUse));
            }
        }

        #endregion Public Methods
    }
}