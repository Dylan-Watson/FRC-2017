using System;
using WPILib;

namespace Base.Components
{/// <summary>
/// Class to handle Digital Output Components
/// </summary>
    public class DigitalOutputItem : OutputComponent, IComponent
    {
        private readonly DigitalOutput dout;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="channel">pwm channel the DIO is plugged into</param>
        /// <param name="commonName">CommonName the component will have</param>
        public DigitalOutputItem(int channel, string commonName)
        {
            dout = new DigitalOutput(channel);
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
        ///     returns dout
        /// </summary>
        /// <returns>dout</returns>
        public object GetRawComponent()
        {
            return dout;
        }

        /// <summary>
        ///     Sets a value to the DigitalOutput
        /// </summary>
        /// <param name="val">value to set the controller to</param>
        /// <param name="sender">the caller of this method</param>
        protected override void set(double val, object sender)
        {
            Sender = sender;
            lock (dout)
            {
                if ((Convert.ToInt32(val) == 1) || (Convert.ToInt32(val) == 0))
                {
                    InUse = true;
                    dout.Set(Convert.ToBoolean(val));
                }
                else
                {
                    Report.Error(
                        $"The valid range for DigitalOutput is 0 or 1 (false or true). {sender} tried to set a value not in this range.");
                    throw new ArgumentOutOfRangeException(nameof(val),
                        $"The valid range for DigitalOutput is 0 or 1 (false or true). {sender} tried to set a value not in this range.");
                }
            }

            Sender = null;
            InUse = false;
        }
    }
}
