using System;
using WPILib;

namespace Base.Components
{
    /// <summary>
    ///     Class to handle Digital Input Components
    /// </summary>
    public sealed class DigitalInputItem : InputComponent, IComponent
    {
        #region Public Constructors

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

        #endregion Public Constructors

        #region Public Events

        /// <summary>
        ///     Event used for VirtualControlEvents
        /// </summary>
        public event EventHandler ValueChanged;

        #endregion Public Events

        #region Private Fields

        private readonly DigitalInput din;

        private bool previousBool;

        private double previousInput;

        #endregion Private Fields

        #region Public Properties

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

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///     Disposes of this IComponent and its managed resources
        /// </summary>
        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Gets the Input Value from the DigitalInput
        /// </summary>
        /// <returns>Boolean</returns>
        public override double Get()
        {
            lock (din)
            {
                var input = Convert.ToDouble(din.Get());

                if (previousInput != input)
                    onValueChanged(new VirtualControlEventArgs(input, InUse));

                previousInput = input;
                return input;
            }
        }

        /// <summary>
        ///     Gets the Input Value from the DigitalInput
        /// </summary>
        /// <returns>Boolean</returns>
        public bool GetBool()
        {
            lock (din)
            {
                var value = din.Get();

                if (previousBool != value)
                    onValueChanged(new VirtualControlEventArgs(Convert.ToDouble(value), InUse));

                previousBool = value;
                return value;
            }
        }

        /// <summary>
        ///     returns din
        /// </summary>
        /// <returns>din</returns>
        public object GetRawComponent()
        {
            return din;
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        ///     Releases managed and native resources
        /// </summary>
        /// <param name="disposing"></param>
        private void dispose(bool disposing)
        {
            if (!disposing) return;
            lock (din)
            {
                din?.Dispose();
            }
        }

        /// <summary>
        ///     Method to fire value changes for set/get values and InUse values
        /// </summary>
        /// <param name="e">VirtualControlEventArgs</param>
        private void onValueChanged(VirtualControlEventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        #endregion Private Methods
    }
}