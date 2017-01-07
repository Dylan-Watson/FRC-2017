/****************************** Header ******************************\
Class Name: AnalogInputItem inherits InputComponent and IComponent
Summary: Abstraction for the WPIlib AnalogInput that extends to include
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
    ///     Class to handle Analog Input Components
    /// </summary>
    public sealed class AnalogInputItem : InputComponent, IComponent
    {
        #region Public Constructors

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="channel">Analog Input channel the AnalogInput is plugged into</param>
        /// <param name="commonName">CommonName the component will have</param>
        public AnalogInputItem(int channel, string commonName)
        {
            ain = new AnalogInput(channel);
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

        private readonly AnalogInput ain;

        private double previousVoltage;

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
            //GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Gets the input voltage from the AnalogInput
        /// </summary>
        /// <returns>Double</returns>
        public override double Get()
        {
#if USE_LOCKING
            lock (ain)
#endif
            {
                var voltage = ain.GetVoltage();

                if (Math.Abs(previousVoltage - voltage) <= Math.Abs(previousVoltage*.00001))
                    onValueChanged(new VirtualControlEventArgs(voltage, InUse));

                previousVoltage = voltage;
                return previousVoltage;
            }
        }

        /// <summary>
        ///     returns ain
        /// </summary>
        /// <returns>ain</returns>
        public object GetRawComponent()
        {
            return ain;
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
#if USE_LOCKING
            lock (ain)
#endif
            {
                ain?.Dispose();
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