using System;
using WPILib;

namespace Base.Components
{
    /// <summary>
    /// Class to handle Analog Output Components
    /// </summary>
    public class AnalogOutputItem : OutputComponent, IComponent
    {
        #region Private Fields

        private readonly AnalogOutput aout;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="channel">pwm channel the AIO is plugged into</param>
        /// <param name="commonName">CommonName the component will have</param>
        public AnalogOutputItem(int channel, string commonName)
        {
            aout = new AnalogOutput(channel);
            Name = commonName;
        }

        #endregion Public Constructors

        #region Public Events

        /// <summary>
        /// Event used for VirtualControlEvents
        /// </summary>
        public event EventHandler ValueChanged;

        #endregion Public Events

        #region Public Methods

        /// <summary>
        /// returns aout
        /// </summary>
        /// <returns>aout</returns>
        public object GetRawComponent()
        {
            return aout;
        }

        #endregion Public Methods

        /// <summary>
        /// Disposes of this IComponent and its managed resources
        /// </summary>
        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases managed and native resources
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void dispose(bool disposing)
        {
            if (!disposing) return;
            lock (aout)
            {
                aout?.Dispose();
            }
        }

        #region Public Properties

        /// <summary>
        /// Defines whether the component is in use or not
        /// </summary>
        public bool InUse { get; private set; }

        /// <summary>
        /// Name of the component
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Defines the object issuing the commands
        /// </summary>
        public object Sender { get; private set; }

        #endregion Public Properties

        #region Protected Methods

        /// <summary>
        /// Method to fire value changes for set/get values and InUse values
        /// </summary>
        /// <param name="e">VirtualControlEventArgs</param>
        protected virtual void onValueChanged(VirtualControlEventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Sets a value to the AnalogInput
        /// </summary>
        /// <param name="val">value to set the controller to</param>
        /// <param name="sender">the caller of this method</param>
        protected override void set(double val, object sender)
        {
            Sender = sender;
            lock (aout)
            {
                if ((val >= 0) && (val <= 5))
                {
                    InUse = true;
                    aout.SetVoltage(val);
                    onValueChanged(new VirtualControlEventArgs(val, InUse));
                }
                else
                {
                    Report.Error(
                        $"The valid range for AnalogOutput is 0 to 5. {sender} tried to set a value not in this range.");
                    throw new ArgumentOutOfRangeException(nameof(val),
                        $"The valid range for AnalogOutput is 0 to 5. {sender} tried to set a value not in this range.");
                }
            }

            Sender = null;
            InUse = false;
            onValueChanged(new VirtualControlEventArgs(val, InUse));
        }

        #endregion Protected Methods
    }
}