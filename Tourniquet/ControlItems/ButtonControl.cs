/****************************** Header ******************************\
Class Name: ButtonControl inherits ControlItem
Summary: Used to control physical component with an button (not a toggle).
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using WPILib;

namespace Tourniquet.ControlItems
{
    using Base;

    /// <summary>
    ///     Defines a control that uses a button on the controller
    /// </summary>
    public class ButtonControl : ControlItem
    {
        #region Public Constructors

        /// <summary>
        ///     Constructor
        /// </summary>
        public ButtonControl(string name, Joystick joystick, int buttonA, bool reversed,
            bool isEnabled, bool actOnRelease, double multiplier = 1)
        {
            IsEnabled = isEnabled;
            ControlName = name;
            ControlType = ControlItemType.ButtonControl;
            this.joystick = joystick;
            IsReversed = reversed;
            this.multiplier = multiplier;
            button = buttonA;
            this.actOnRelease = actOnRelease;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///     Update the control
        /// </summary>
        public override void Update()
        {
            if (joystick.GetRawButton(button))
                if (!IsReversed)
                {
                    set(1 * multiplier);

                    if (Debug)
                        FrameworkCommunication.Instance.SendData($"{ControlName}", 1 * multiplier);

                    IsRunning = true;
                }
                else
                {
                    set(0);

                    if (Debug)
                        FrameworkCommunication.Instance.SendData($"{ControlName}", 0);

                    stop();
                    IsRunning = false;
                }
            else if (IsRunning && actOnRelease)
                if (!IsReversed)
                {
                    set(0);

                    if (Debug)
                        FrameworkCommunication.Instance.SendData($"{ControlName}", 0);

                    stop();
                    IsRunning = false;
                }
                else
                {
                    set(1 * multiplier);

                    if (Debug)
                        FrameworkCommunication.Instance.SendData($"{ControlName}", 1 * multiplier);

                    IsRunning = true;
                }
        }

        #endregion Public Methods

        #region Private Fields

        private readonly int button;

        private readonly double multiplier;

        private readonly bool actOnRelease;

        #endregion Private Fields
    }
}