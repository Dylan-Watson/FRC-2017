using WPILib;

namespace Tourniquet.ControlItems
{
    internal class ToggleButtonControl : ControlItem
    {
        /// <summary>
        /// Constructor
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

        private readonly double multiplier;
        private readonly int buttonA;

        private bool toggle;
        private bool previousState, currentState;
        public override void Update()
        {
            currentState = joystick.GetRawButton(buttonA);

            if (currentState != previousState)
            {
                toggle ^= currentState;
                if (toggle)
                {
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
    }
}
