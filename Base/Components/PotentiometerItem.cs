/****************************** Header ******************************\
Class Name: PotentiometerItem, inherits IComponent and InputComponent
Summary: Abstraction for the WPIlib AnalogPot. that extends to include
some helper and reading methods.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Dylan Watson, Ryan S. Cooper
Email: dylantrwatson@gmail.com, cooper.ryan@centaurisoft.org
\********************************************************************/

using System;
using WPILib;

namespace Base.Components
{
    /// <summary>
    ///     Class to handle the Potentiometer Components
    /// </summary>
    public class PotentiometerItem : InputComponent, IComponent
    {
        #region Public Constructors

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="channel">The analog channel this potentiometer is plugged into.</param>
        /// <param name="commonName">CommonName the component will have</param>
        public PotentiometerItem(int channel, string commonName)
        {
            apt = new AnalogPotentiometer(channel);
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

        private readonly AnalogPotentiometer apt;

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
        ///     Gets the current value of the AnalogPotentiometer
        /// </summary>
        /// <returns></returns>
        public override double Get()
        {
            lock (apt)
            {
                var input = apt.Get();

                if (previousInput != input)
                    onValueChanged(new VirtualControlEventArgs(input, true));

                previousInput = input;
                return input;
            }
        }


        /// <summary>
        ///     Return the WPILib AnalogPotentiometer
        /// </summary>
        /// <returns>WPILib.AnalogPotentiometer apt</returns>
        public object GetRawComponent()
        {
            return apt;
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
            lock (apt)
            {
                apt?.Dispose();
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