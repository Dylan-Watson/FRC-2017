using WPILib;

namespace Tourniquet.ControlItems
{

    public class ButtonControl : ControlItem
    {
        /// <summary>
        /// Constructor
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
            this.button = buttonA;
        }

        private readonly double multiplier;
        private readonly int button;

        public override void Update()
        {
            if (joystick.GetRawButton(button))
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
            else if (IsRunning)
            {
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
        }
    }
}
