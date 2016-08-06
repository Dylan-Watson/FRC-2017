using System;
using WPILib;

namespace Base.Components
{
    /// <summary>
    ///     Defines the type of AIO
    /// </summary>
    public enum AioType
    {
        /// <summary>
        ///     Analog Input
        /// </summary>
        Input,

        /// <summary>
        ///     Analog Output
        /// </summary>
        Output
    }

    /// <summary>
    ///     Class to handle Analog Input and Analog Output Components
    /// </summary>
    public class AioItem : IO, IComponent
    {
        private readonly AnalogSource aio;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="type">type of AIO</param>
        /// <param name="channel">pwm channel the AIO is plugged into</param>
        /// <param name="commonName">CommonName the component will have</param>
        public AioItem(AioType type, int channel, string commonName)
        {
            if (type == AioType.Input)
                aio = new AnalogInput(channel);
            else
                aio = new AnalogOutput(channel);

            Name = commonName;
        }

        /// <summary>
        ///     Defines whether the component is in use or not
        /// </summary>
        public bool InUse { get; private set; }

        /// <summary>
        ///     Name of the component
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Defines the object issuing the commands
        /// </summary>
        public object Sender { get; private set; }

        /// <summary>
        ///     returns aio
        /// </summary>
        /// <returns>aio</returns>
        public object GetRawComponent()
        {
            return aio;
        }

        /// <summary>
        ///     Gets the Input Value from the AnalogInput
        /// </summary>
        /// <returns>Double</returns>
        public bool Get()
        {
            return ((AnalogInput) aio).Get();
        }

        /// <summary>
        ///     Sets a value to the AnalogInput
        /// </summary>
        /// <param name="val">value to set the controller to</param>
        /// <param name="sender">the caller of this method</param>
        protected override void set(double val, object sender)
        {
            Sender = sender;
            lock (aio)
            {
                if (aio is AnalogInput)
                {
                    Report.Error("This is a analog input, you cannot output to this.");
                    return;
                }

                if ((val >= -1) && (val <= 5))
                {
                    InUse = true;
                    ((AnalogOutput) aio).Set(val);
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }

            Sender = null;
            InUse = false;
        }
    }
}
