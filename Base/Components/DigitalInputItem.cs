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
        ///     Gets the Input Value from the DigitalInput
        /// </summary>
        /// <returns>Boolean</returns>
        public bool GetBool()
        {
            lock (din)
                return din.Get();
        }

        /// <summary>
        ///     Gets the Input Value from the DigitalInput
        /// </summary>
        /// <returns>Boolean</returns>
        public override double Get()
        {
            lock (din)
                return Convert.ToDouble(din.Get());
        }
    }
}
