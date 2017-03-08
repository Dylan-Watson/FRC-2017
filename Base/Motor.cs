/****************************** Header ******************************\
Class Name: Motor [Abstract]
Summary: Abstract class for motor components
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using Base.Components;

namespace Base
{
    /// <summary>
    ///     Abstract class for wrapper/container classes representing motor/speed controller components
    ///     from the WPI(victors, talons...)
    /// </summary>
    public abstract class Motor
    {
        #region Public Enums

        /// <summary>
        ///     Represents the side the motor is on, if the motor is a drive motor
        /// </summary>
        public enum Side
        {
            /// <summary>
            ///     Left side of the drive train
            /// </summary>
            Left,

            /// <summary>
            ///     Right side of the drive train
            /// </summary>
            Right,

            /// <summary>
            ///     Default, not a member of the drive train.
            /// </summary>
            Na
        }

        #endregion Public Enums

        #region Protected Constructors

        /// <summary>
        ///     Default constructor
        /// </summary>
        protected Motor()
        {
            AllowC = true;
            AllowCc = true;
            IsReversed = false;
        }

        #endregion Protected Constructors

        #region Public Properties

        /// <summary>
        ///     Boolean flag to set rule on clockwise movment
        /// </summary>
        public bool AllowC { get; private set; }

        /// <summary>
        ///     Boolean flag to set rule on counter-clockwise movment
        /// </summary>
        public bool AllowCc { get; private set; }

        /// <summary>
        ///     Boolean flag to set if we are currently in brownout mode
        /// </summary>
        public bool BrownOut { get; private set; }

        /// <summary>
        ///     Side of the drive train the motor is on, if it is on the drive train
        /// </summary>
        public Side DriveSide { get; protected set; } = Side.Na;

        /// <summary>
        ///     Utility function that returns the polarity of the motor 1 forward, -1 reversed
        /// </summary>
        public int GetPolarity
        {
            get
            {
                if (IsReversed) return -1;
                return 1;
            }
        }

        /// <summary>
        ///     Boolean flag to determine if the motor is a drive train motor
        /// </summary>
        public bool IsDrivetrainMotor { get; protected set; }

        /// <summary>
        ///     Boolean flag to determine if the motor is reversed
        /// </summary>
        public bool IsReversed { get; protected set; }

        #endregion Public Properties

        #region Protected Properties

        /// <summary>
        ///     Encoder to read the value of the motor
        /// </summary>
        protected EncoderItem encoder { get; set; }

        /// <summary>
        ///     DigitalInput to stop the motor from going reverse when true
        /// </summary>
        protected DigitalInputItem lowerLimit { get; set; }

        /// <summary>
        ///     DigitalInput to stop the motor from going forward when true
        /// </summary>
        protected DigitalInputItem upperLimit { get; set; }

        #endregion Protected Properties

        #region Public Methods

        /// <summary>
        ///     Method to return the last value set to the motor
        /// </summary>
        /// <returns>double value</returns>
        public abstract double Get();

        /// <summary>
        ///     Sets the IsReversed flag/property to false
        /// </summary>
        public void RestoreDirection() => IsReversed = false;

        /// <summary>
        ///     Sets the IsReversed flag/property to true
        /// </summary>
        public void ReverseDirection() => IsReversed = true;

        /// <summary>
        ///     Abstract method to set the motor/speed controller to a specific speed
        /// </summary>
        /// <param name="val">Speed 0-1 to set the motor controller to</param>
        /// <param name="sender">The caller of this function</param>
        public abstract void Set(double val, object sender);

        /// <summary>
        ///     Sets the BrownOut flag/property to the passed value
        /// </summary>
        /// <param name="val"></param>
        public void setBrownOut(bool val) => BrownOut = val;

        /// <summary>
        ///     Sets the AllowC flag/property to the passed value
        /// </summary>
        /// <param name="val">Bool to set AllowC to</param>
        public void SetAllowC(bool val) => AllowC = val;

        /// <summary>
        ///     Sets the AllowCc flag/property to the passed value
        /// </summary>
        /// <param name="val">Bool to set AllowCc to</param>
        public void SetAllowCc(bool val) => AllowCc = val;

        /// <summary>
        ///     Abstract method to stop the motor/speed cotnroller
        /// </summary>
        public abstract void Stop();

        #endregion Public Methods
    }
}