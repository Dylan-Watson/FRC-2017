using System;
using WPILib;

namespace Base.Components
{
    /// <summary>
    /// Class to handle Digital Input Components
    /// </summary>
    public class DigitalInputItem: InputComponent, IComponent
    {
        private readonly DigitalInput din;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="channel">pwm channel the DIO is plugged into</param>
        /// <param name="commonName">CommonName the component will have</param>
        public DigitalInputItem(int channel, string commonName)
        {
            din = new DigitalInput(channel);
            Name = commonName;
        }

        /// <summary>
        ///     Defines whether the component is in use or not
        /// </summary>
        public bool InUse { get; } = false;

        /// <summary>
        ///     Name of the component
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Defines the object issuing the commands
        /// </summary>
        public object Sender { get; } = null;

        /// <summary>
        ///     returns din
        /// </summary>
        /// <returns>din</returns>
        public object GetRawComponent()
        {
            return din;
        }

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

        private bool previousBool;
        /// <summary>
        ///     Gets the Input Value from the DigitalInput
        /// </summary>
        /// <returns>Boolean</returns>
        public bool GetBool()
        {
            lock (din)
            {
                var value = din.Get();

                if(previousBool!=value)
                    onValueChanged(new VirtualControlEventArgs(Convert.ToDouble(value), InUse));

                previousBool = value;
                return value;
            }
        }

        private double previousInput;
        /// <summary>
        ///     Gets the Input Value from the DigitalInput
        /// </summary>
        /// <returns>Boolean</returns>
        public override double Get()
        {
            lock (din)
            {
                var input = Convert.ToDouble(din.Get());

                if (Math.Abs(previousInput - input) <= Math.Abs(previousInput * .00001))
                    onValueChanged(new VirtualControlEventArgs(input, InUse));

                previousInput = input;
                return Convert.ToDouble(din.Get());
            }
        }
    }
}
