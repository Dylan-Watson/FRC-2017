using WPILib;

namespace Tourniquet.ControlItems
{
    public class DualButtonControl : ControlItem
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DualButtonControl(string name, Joystick joystick, int buttonA, int buttonB, bool reversed,
            bool isEnabled, double multiplier = 1)
        {
            IsEnabled = isEnabled;
            ControlName = name;
            ControlType = ControlItemType.DualButtonControl;
            this.joystick = joystick;
            IsReversed = reversed;
            this.multiplier = multiplier;
            this.buttonA = buttonA;
            this.buttonB = buttonB;
        }

        public bool ButtonA { get; private set; }
        public bool ButtonB { get; private set; }

        private readonly double multiplier;
        private readonly int buttonA;
        private readonly int buttonB;

        public override void Update()
        {
            if (joystick.GetRawButton(buttonA))
            {
                ButtonA = true;
                ButtonB = false;
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
            else if (joystick.GetRawButton(buttonB))
            {
                ButtonB = true;
                ButtonA = false;
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
    }
}