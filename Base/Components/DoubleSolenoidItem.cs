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
        ///     Name of the component
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Defines wether the component is in use or not
        /// </summary>
        public bool InUse { get; private set; }

        /// <summary>
        ///     Defines the object issuing the commands
        /// </summary>
        public object Sender { get; private set; }

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
        ///     Constructor
        /// </summary>
        /// <param name="channel">pwm channel the DoubleSolenoid is plugged into</param>
        /// <param name="commonName">CommonName the DoubleSolenoid will have</param>
        /// <param name="forwardChannel">The forward channel number on the PCM [0..7]</param>
        /// <param name="reverseChannel">The reverse channel number on the PCM [0..7]</param>
        /// <param name="Default">Default position for when the Robot is initialized</param>
        public DoubleSolenoidItem(int channel, string commonName, int forwardChannel, int reverseChannel, int Default)
        {

        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///      Gets the raw DoubleSolenoid object representing the Solenoid
        /// </summary>
        /// <returns></returns>
        public object GetRawComponent() => solenoid;

        public void Set(double val, object sender)
        {
            Sender = sender;
            lock (solenoid)
            {

            }
        }

        #endregion Public Methods
    }
}
