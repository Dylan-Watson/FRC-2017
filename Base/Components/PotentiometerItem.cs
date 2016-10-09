using System;
using WPILib;

namespace Base.Components
{
    /// <summary>
    /// Class to handle the Potentiometer Components
    /// </summary>
    public class PotentiometerItem : InputComponent, IComponent
    {
        #region Private Fields

        private readonly AnalogPotentiometer apt;

        #endregion Private Fields

        #region Public Fields

        /// <summary>
        /// Defines whether the component is in use or not
        /// </summary>
        public bool InUse { get; } = false;

        /// <summary>
        /// Name of the component
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Defines the object issuing the commands
        /// </summary>
        public object Sender { get; } = null;
        
        #endregion Public Fields

        #region Public Events

        /// <summary>
        /// Event used for VirtualControlEvents
        /// </summary>
        public event EventHandler ValueChanged;

        #endregion Public Events

        #region Public Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="channel">The analog channel this potentiometer is plugged into.</param>
        /// <param name="commonName">CommonName the component will have</param>
        public PotentiometerItem(int channel, string commonName)
        {
            apt = new AnalogPotentiometer(channel);
        }

        #endregion Public Constructors

        #region Private Methods

        /// <summary>
        /// Releases managed and native resources
        /// </summary>
        /// <param name="disposing"></param>
        private void dispose(bool disposing)
        {
            if (!disposing) return;
            lock (apt)
            {
                apt?.Dispose();
            }
        }

        #endregion Private Methods

        #region Public Methods
        
        /// <summary>
        /// Return the WPILib AnalogPotentiometer
        /// </summary>
        /// <returns>WPILib.AnalogPotentiometer apt</returns>
        public object GetRawComponent()
        {
            return apt;
        }

        /// <summary>
        /// Disposes of this IComponent and its managed resources
        /// </summary>
        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets the current value of the AnalogPotentiometer
        /// </summary>
        /// <returns></returns>
        public override double Get()
        {
            lock (apt)
            {
                return apt.Get();
            }
        }

        #endregion Public Methods
    }
}
