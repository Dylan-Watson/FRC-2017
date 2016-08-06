using System;
using WPILib;

namespace Base.Components
{
    /// <summary>
    ///     Defines the type of DIO
    /// </summary>
    public enum DioType
    {
        /// <summary>
        ///     Digital Input
        /// </summary>
        Input,

        /// <summary>
        ///     Digital Output
        /// </summary>
        Output
    }

    /// <summary>
    ///     Class to handle Digital Input and Digital Output Components
    /// </summary>
    public class DioItem : IO, IComponent
    {
        private readonly DigitalSource dio;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="type">type of DIO</param>
        /// <param name="channel">pwm channel the DIO is plugged into</param>
        /// <param name="commonName">CommonName the component will have</param>
        public DioItem(DioType type, int channel, string commonName)
        {
            if (type == DioType.Input)
                dio = new DigitalInput(channel);
            else
                dio = new DigitalOutput(channel);

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
        ///     returns dio
        /// </summary>
        /// <returns>dio</returns>
        public object GetRawComponent()
        {
            return dio;
        }

        /// <summary>
        ///     Gets the Input Value from the DigitalInput
        /// </summary>
        /// <returns>Double</returns>
        public bool Get()
        {
            return ((DigitalInput) dio).Get();
        }

        /// <summary>
        ///     Sets a value to the DigitalOutput
        /// </summary>
        /// <param name="val">value to set the controller to</param>
        /// <param name="sender">the caller of this method</param>
        protected override void set(double val, object sender)
        {
            Sender = sender;
            lock (dio)
            {
                if (dio is DigitalInput)
                {
                    Report.Error("This is a digital input, you cannot output to this.");
                    return;
                }

                if ((val == 1) || (val == 0))
                {
                    InUse = true;
                    ((DigitalOutput) dio).Set(Convert.ToBoolean(val));
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
