using System;
using WPILib;

namespace Base.Components
{
    /// <summary>
    /// Class to handle Analog Input Components
    /// </summary>
    public class AnalogInputItem: InputComponent, IComponent
    {
        private readonly AnalogInput ain;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="channel">pwm channel the AIO is plugged into</param>
        /// <param name="commonName">CommonName the component will have</param>
        public AnalogInputItem(int channel, string commonName)
        {
            ain = new AnalogInput(channel);
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
        ///     returns ain
        /// </summary>
        /// <returns>ain</returns>
        public object GetRawComponent()
        {
            return ain;
        }

        private double previousVoltage;
        /// <summary>
        ///     Gets the input voltage from the AnalogInput
        /// </summary>
        /// <returns>Double</returns>
        public override double Get()
        {
            lock (ain)
            {
                var voltage = ain.GetVoltage();

                if (Math.Abs(previousVoltage - voltage) <= Math.Abs(previousVoltage * .00001))
                    onValueChanged(new VirtualControlEventArgs(voltage, InUse));

                previousVoltage = voltage;
                return previousVoltage;
            }
        }
    }
}
