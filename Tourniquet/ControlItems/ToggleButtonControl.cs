/****************************** Header ******************************\
Class Name: ToggleButtonControl inherits ControlItem
Summary: Used to control physical component with a single button toggle.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using WPILib;

namespace Tourniquet.ControlItems
{
    internal class ToggleButtonControl : ControlItem
    {
        #region Public Constructors

        /// <summary>
        ///     Constructor
        /// </summary>
        public ToggleButtonControl(string name, Joystick joystick, int buttonA, bool reversed,
            bool isEnabled, double multiplier = 1)
        {
            IsEnabled = isEnabled;
            ControlName = name;
            ControlType = ControlItemType.ButtonControl;
            this.joystick = joystick;
            IsReversed = reversed;
            this.multiplier = multiplier;
            this.buttonA = buttonA;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Update()
        {
            currentState = joystick.GetRawButton(buttonA);

            if (currentState != previousState)
            {
                toggle ^= currentState;
                if (!currentState)
                    if (toggle)
                    {
                        if (!IsReversed)
                        {
                            set(1 * multiplier);
                            IsRunning = true;
                        }
                        else
                        {
                            set(0);
                            stop();
                            IsRunning = false;
                        }
                    }
                    else
                    {
                        if (!IsReversed)
                        {
                            set(0);
                            stop();
                            IsRunning = false;
                        }
                        else
                        {
                            set(1 * multiplier);
                            IsRunning = true;
                        }
                    }
            }

            previousState = currentState;
        }

        #endregion Public Methods

        #region Private Fields

        private readonly int buttonA;

        private readonly double multiplier;

        private bool previousState, currentState;

        private bool toggle;

        #endregion Private Fields
    }
}