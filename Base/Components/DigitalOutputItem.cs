/****************************** Header ******************************\
Class Name: DigitalOutputItem inherits OutputComponent and IComponent
Summary: Abstraction for the WPIlib DigitalOutput that extends to include
some helper and control methods.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using System;
using WPILib;

namespace Base.Components
{
    /// <summary>
    ///     Class to handle Digital Output Components
    /// </summary>
    public sealed class DigitalOutputItem : OutputComponent, IComponent
    {
        #region Private Fields

        private readonly DigitalOutput dout;

        #endregion Private Fields

        #region Public Constructors

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

        #endregion Public Constructors

        #region Public Events

        /// <summary>
        ///     Event used for VirtualControlEvents
        /// </summary>
        public event EventHandler ValueChanged;

        #endregion Public Events

        #region Protected Methods

        /// <summary>
        ///     Sets a value to the DigitalOutput
        /// </summary>
        /// <param name="val">value to set the controller to</param>
        /// <param name="sender">the caller of this method</param>
        protected override void set(double val, object sender)
        {
            var value = false;
            Sender = sender;
#if USE_LOCKING
            lock (dout)
#endif
            {
                if (Math.Abs(val - 0) <= Math.Abs(val*.00001))
                {
                    InUse = true;
                    dout.Set(false);
                    onValueChanged(new VirtualControlEventArgs(0, InUse));
                }
                else
                {
                    InUse = true;
                    dout.Set(true);
                    value = true;
                    onValueChanged(new VirtualControlEventArgs(1, InUse));
                }
                /*else
                {
                    Report.Error(
                        $"The valid range for DigitalOutput is 0 or 1 (false or true). {sender} tried to set a value not in this range.");
                    throw new ArgumentOutOfRangeException(nameof(val),
                        $"The valid range for DigitalOutput is 0 or 1 (false or true). {sender} tried to set a value not in this range.");
                }*/
            }

            Sender = null;
            InUse = false;
            onValueChanged(new VirtualControlEventArgs(Convert.ToDouble(value), InUse));
        }

        #endregion Protected Methods

        #region Public Properties

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
        ///     returns dout
        /// </summary>
        /// <returns>dout</returns>
        public object GetRawComponent()
        {
            return dout;
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
            lock (dout)
#endif
            {
                dout?.Dispose();
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