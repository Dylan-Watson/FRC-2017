using WPILib;

namespace Tourniquet.ControlItems
{
    /// <summary>
    /// Control that uses two butons to perform an action
    /// </summary>
    public class DualButtonControl : ControlItem
    {
        #region Public Constructors

        /// <summary>
        ///     Constructor
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

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Updates the control
        /// </summary>
        public override void Update()
        {
            if (joystick.GetRawButton(buttonA))
            {
                ButtonA = true;
                ButtonB = false;
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
                    set(1*multiplier);
                    IsRunning = true;
                }
            }
        }

        #endregion Public Methods

        #region Private Fields

        private readonly int buttonA;

        private readonly int buttonB;

        private readonly double multiplier;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Button a of the control
        /// </summary>
        public bool ButtonA { get; private set; }

        /// <summary>
        /// Button b of the control
        /// </summary>
        public bool ButtonB { get; private set; }

        #endregion Public Properties
    }
}