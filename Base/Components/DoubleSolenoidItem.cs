/****************************** Header ******************************\
Class Name: DoubleSolenoidItem inherits and IComponent
Summary: Abstraction for the WPIlib DoubleSolenoid that extends to include
some helper and control methods.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Dylan Watson, Ryan Cooper
Email: dylantrwatson@gmail.com, cooper.ryan@centaurisoft.org
\********************************************************************/

using System;
using WPILib;

namespace Base.Components
{
    /// <summary>
    ///     Class to handle Double Solenoid Pneumatics
    /// </summary>
    public class DoubleSolenoidItem : IComponent
    {
        #region Private Fields

        private readonly DoubleSolenoid solenoid;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Name of the component
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Defines wether the component is in use or not
        /// </summary>
        public bool InUse { get; private set; }

        /// <summary>
        /// Defines the object issuing the commands
        /// </summary>
        public object Sender { get; private set; }

        /// <summary>
        /// Defines the default position of the solenoid
        /// </summary>
        private DoubleSolenoid.Value Default { get; }

        #endregion Public Properties

        #region Public Events

        /// <summary>
        /// Event used for VirtualControlEvents
        /// </summary>
        public event EventHandler ValueChanged;

        #endregion Public Events

        #region Protected Methods

        /// <summary>
        /// Method to fire value changes for set/get values and InUse values
        /// </summary>
        /// <param name="e">VirtualControlEventArgs</param>
        protected virtual void onValueChanged(VirtualControlEventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="commonName">CommonName the DoubleSolenoid will have</param>
        /// <param name="forwardChannel">The forward channel number on the PCM [0..7]</param>
        /// <param name="reverseChannel">The reverse channel number on the PCM [0..7]</param>
        /// <param name="_default">Default position for when the Robot is initialized</param>
        public DoubleSolenoidItem(string commonName, int forwardChannel, int reverseChannel, DoubleSolenoid.Value _default)
        {
            solenoid = new DoubleSolenoid(forwardChannel, reverseChannel);
            Name = commonName;
            Default = _default;
            solenoid.Set(_default);
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Gets the raw DoubleSolenoid object representing the Solenoid
        /// </summary>
        /// <returns></returns>
        public object GetRawComponent() => solenoid;

        /// <summary>
        /// Sets a value to the solenoid
        /// </summary>
        /// <param name="val">Double, 0, 1, or 2, to set the DoubleSolenoid to Off, Forward, or Reverse, respectively</param>
        /// <param name="sender">the caller of this method</param>
        public void Set(double val, object sender)
        {
            Sender = sender;
            lock (solenoid)
            {
                if ((val >= 0) && (val <= 2))
                {
                    InUse = true;
                    if (val == 0)
                        solenoid.Set(DoubleSolenoid.Value.Off);
                    else if (val == 1)
                        solenoid.Set(DoubleSolenoid.Value.Forward);
                    else if (val == 2)
                        solenoid.Set(DoubleSolenoid.Value.Reverse);                     
                }
                else
                {
                    Report.Error(
                        $"The valid arguments for DoubleSolenoid is Off, Forward, and Reverse. {sender} tried to set a value not in this range.");
                    throw new ArgumentOutOfRangeException(nameof(val),
                        $"The valid arguments for DoubleSolenoid is Off, Forward, and Reverse. {sender} tried to set a value not in this range.");
                }
                Sender = null;
                InUse = false;
            }
        }

        /// <summary>
        /// Calls the method to set the value to the solenoid
        /// </summary>
        /// <param name="value">The enum variable to set the solenoid to</param>
        /// <param name="sender">the caller of this method</param>
        public void Set(DoubleSolenoid.Value value, object sender)
        {
            Set(Convert.ToDouble(value), sender);
        }

        /// <summary>
        /// Sets the DoubleSolenoid to its default position
        /// </summary>
        public void defaultSet()
        {
            solenoid.Set(Default);
        }

        #endregion Public Methods
    }
}
