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
    /// Class to handle Double Solenoid Pneumatics
    /// </summary>
    public class DoubleSolenoidItem : IComponent
    {
        #region Private Fields

        private readonly DoubleSolenoid solenoid;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="commonName">CommonName the DoubleSolenoid will have</param>
        /// <param name="forwardChannel">The forward channel number on the PCM [0..7]</param>
        /// <param name="reverseChannel">The reverse channel number on the PCM [0..7]</param>
        /// <param name="_default">Default position for when the Robot is initialized</param>
        /// <param name="reversed">If the output is reversed for the forward and reversed states</param>
        public DoubleSolenoidItem(string commonName, int forwardChannel, int reverseChannel,
            DoubleSolenoid.Value _default, bool reversed = false)
        {
            solenoid = new DoubleSolenoid(forwardChannel, reverseChannel);
            Name = commonName;
            Default = _default;
            IsReversed = reversed;
            solenoid.Set(_default);
        }

        #endregion Public Constructors

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

        #endregion Protected Methods

        #region Public Properties

        /// <summary>
        /// Defines wether the component is in use or not
        /// </summary>
        public bool InUse { get; private set; }

        /// <summary>
        /// Defines if the output is reversed for forward and reverse states
        /// </summary>
        public bool IsReversed { get; private set; }

        /// <summary>
        /// Name of the component
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Defines the object issuing the commands
        /// </summary>
        public object Sender { get; private set; }

        /// <summary>
        /// Defines the default position of the solenoid
        /// </summary>
        private DoubleSolenoid.Value Default { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Sets the DoubleSolenoid to its default position
        /// </summary>
        public void DefaultSet()
        {
            lock (solenoid)
            {
                solenoid.Set(Default);
            }
        }

        /// <summary>
        /// Gets the raw DoubleSolenoid object representing the Solenoid
        /// </summary>
        /// <returns></returns>
        public object GetRawComponent() => solenoid;

        /// <summary>
        /// Sets the IsReversed flag/property to false
        /// </summary>
        public void RestoreDirection() => IsReversed = false;

        /// <summary>
        /// Sets the IsReversed flag/property to true
        /// </summary>
        public void ReverseDirection() => IsReversed = true;

        /// <summary>
        /// Sets a value to the solenoid
        /// </summary>
        /// <param name="val">
        /// Double, 0, 1, or 2, to set the DoubleSolenoid to Off, Forward, or Reverse, respectively
        /// </param>
        /// <param name="sender">the caller of this method</param>
        public void Set(double val, object sender)
        {
            Sender = sender;
            lock (solenoid)
            {
                if ((val >= 0) && (val <= 2))
                {
                    InUse = true;
                    if (Math.Abs(val - 0) <= Math.Abs(val*.00001))
                    {
                        solenoid.Set(DoubleSolenoid.Value.Off);
                        onValueChanged(new VirtualControlEventArgs(-1, InUse));
                    }
                    else if (Math.Abs(val - 2) <= Math.Abs(val*.00001))
                    {
                        if (!IsReversed)
                            setForward();
                        else
                            setReverse();
                    }
                    else if (Math.Abs(val - 1) <= Math.Abs(val*.00001))
                    {
                        if (!IsReversed)
                            setReverse();
                        else
                            setForward();
                    }
                }
                else
                {
                    Report.Error(
                        $"The valid arguments for DoubleSolenoid {Name} is Off, Forward, and Reverse (-1, 1, 0). {sender} tried to set a value not in this range.");
                    throw new ArgumentOutOfRangeException(nameof(val),
                        $"The valid arguments for DoubleSolenoid {Name} is Off, Forward, and Reverse (-1, 1, 0). {sender} tried to set a value not in this range.");
                }
                Sender = null;
                InUse = false;
                onValueChanged(new VirtualControlEventArgs(-1, InUse));
            }
        }

        /// <summary>
        /// Calls the method to set the value to the solenoid
        /// </summary>
        /// <param name="value">The enum variable to set the solenoid to</param>
        /// <param name="sender">the caller of this method</param>
        public void Set(DoubleSolenoid.Value value, object sender)
        {
            Set(Convert.ToDouble(value) - 1, sender);
        }

        /// <summary>
        /// Boolean setter, true = forward, false = reverse
        /// </summary>
        /// <param name="value">boolean to set the solenoid position</param>
        /// <param name="sender">the caller of this method</param>
        public void Set(bool value, object sender)
        {
            Set(Convert.ToDouble(value), sender);
        }

        private void setForward()
        {
            solenoid.Set(DoubleSolenoid.Value.Forward);
            onValueChanged(new VirtualControlEventArgs(1, InUse));
        }

        private void setReverse()
        {
            solenoid.Set(DoubleSolenoid.Value.Reverse);
            onValueChanged(new VirtualControlEventArgs(0, InUse));
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
            lock (solenoid)
            {
                solenoid?.Dispose();
            }
        }
    }
}