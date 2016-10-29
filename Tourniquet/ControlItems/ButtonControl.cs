﻿using WPILib;

namespace Tourniquet.ControlItems
{
    /// <summary>
    /// Defines a control that uses a button on the controller
    /// </summary>
    public class ButtonControl : ControlItem
    {
        #region Public Constructors

        /// <summary>
        ///     Constructor
        /// </summary>
        public ButtonControl(string name, Joystick joystick, int buttonA, bool reversed,
            bool isEnabled, double multiplier = 1)
        {
            IsEnabled = isEnabled;
            ControlName = name;
            ControlType = ControlItemType.ButtonControl;
            this.joystick = joystick;
            IsReversed = reversed;
            this.multiplier = multiplier;
            button = buttonA;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Update the control
        /// </summary>
        public override void Update()
        {
            if (joystick.GetRawButton(button))
                if (!IsReversed)
                {
                    set(1*multiplier);
                    IsRunning = true;
                }
                else
                {
                    set(0);
                    stop();
                    IsRunning = false;
                }
            else if (IsRunning)
                if (!IsReversed)
                {
                    set(0);
                    stop();
                    IsRunning = false;
                }
                else
                {
                    set(1*multiplier);
                    IsRunning = true;
                }
        }

        #endregion Public Methods

        #region Private Fields

        private readonly int button;

        private readonly double multiplier;

        #endregion Private Fields
    }
}