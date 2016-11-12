/****************************** Header ******************************\
Class Name: RelayItem inherits IComponent
Summary: Abstraction for the WPIlib Relay that extends to include
some helper and control methods.
Project:     FRC2017.
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
    ///     Class to handle WPI Relays
    /// </summary>
    public sealed class RelayItem : IComponent
    {
        #region Private Fields

        private readonly Relay relay;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///     Constructor for RelayItem
        /// </summary>
        /// <param name="channel">The channel number on the roboRIO</param>
        /// <param name="commonName">Common Name the RelayItem will have</param>
        /// <param name="_default">Default position for when the robot is initialized</param>
        public RelayItem(int channel, string commonName, Relay.Value _default = Relay.Value.Off)
        {
            relay = new Relay(channel);
            Name = commonName;
            Default = _default;
            relay.Set(_default);
        }

        #endregion Public Constructors

        #region Private Properties

        private Relay.Value Default { get; }

        #endregion Private Properties

        #region Public Events

        /// <summary>
        ///     Event used for VirtualControlEvents
        /// </summary>
        public event EventHandler ValueChanged;

        #endregion Public Events

        #region Public Properties

        /// <summary>
        ///     Defines wether the component is in use or not
        /// </summary>
        public bool InUse { get; private set; }

        /// <summary>
        ///     Defines if the output is reversed for forward and reverse states
        /// </summary>
        public bool IsReversed { get; private set; }

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
        /// Sets if the relay is reversed or not
        /// </summary>
        /// <param name="val">boolean value to represent reversion</param>
        public void SetReverse(bool val)
        {
            IsReversed = val;
        }

        /// <summary>
        ///     Sets the Relay to its default position
        /// </summary>
        public void DefaultSet()
        {
#if USE_LOCKING
            lock (relay)
#endif
            {
                relay.Set(Default);
            }
        }

        /// <summary>
        ///     Disposes of this IComponent and its managed resources
        /// </summary>
        public void Dispose()
        {
            dispose(true);
            //GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Gets the current position that the relay is supposed to be in
        /// </summary>
        /// <returns></returns>
        public Relay.Value GetCurrentPosition()
        {
#if USE_LOCKING
            lock (relay)
#endif
            {
                return relay.Get();
            }
        }

        /// <summary>
        ///     Gets the raw Relay object representing the Relay
        /// </summary>
        /// <returns></returns>
        public object GetRawComponent() => relay;

        /// <summary>
        /// </summary>
        /// <param name="val"></param>
        /// <param name="sender"></param>
        public void Set(Relay.Value val, object sender)
        {
            Sender = sender;
            InUse = true;
#if USE_LOCKING
            lock (relay)
#endif
            {
                relay.Set(val);
                onValueChanged(new VirtualControlEventArgs(Convert.ToDouble(val), InUse));
            }

            Sender = null;
            InUse = false;
        }

        /// <summary>
        /// Sets the relay to the given value
        /// </summary>
        /// <param name="val">double value 0-2</param>
        /// <param name="sender"></param>
        public void Set(double val, object sender)
        {
            if ((val >= 0) && (val <= 2))
            {
                InUse = true;
                if (Math.Abs(val - 2) <= Math.Abs(val*Constants.EPSILON_MIN))
                    Set(Relay.Value.Off, sender);
                else if (Math.Abs(val - 0) <= Math.Abs(val* Constants.EPSILON_MIN))
                    Set(!IsReversed ? Relay.Value.Forward : Relay.Value.Reverse, sender);
                else if (Math.Abs(val - 1) <= Math.Abs(val* Constants.EPSILON_MIN))
                    Set(!IsReversed ? Relay.Value.Reverse : Relay.Value.Forward, sender);
            }
            else
            {
                Report.Error(
                    $"The valid arguments for Relay {Name} is Off, Forward, and Reverse (2, 0, 1). {sender} tried to set a value not in this range.");
                throw new ArgumentOutOfRangeException(nameof(val),
                    $"The valid arguments for Relay {Name} is Off, Forward, and Reverse (2, 0, 1). {sender} tried to set a value not in this range.");
            }
            Sender = null;
            InUse = false;
            onValueChanged(new VirtualControlEventArgs(-1, InUse));
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
            lock (relay)
#endif
            {
                relay?.Dispose();
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

        /// <summary>
        ///     Sets the Relay to the Forward Position
        /// </summary>
        private void setForward()
        {
#if USE_LOCKING
            lock (relay)
#endif
            {
                relay.Set(Relay.Value.Forward);
            }
            onValueChanged(new VirtualControlEventArgs(2, InUse));
        }

        /// <summary>
        ///     Sets the Relay to the Off Position
        /// </summary>
        private void setOff()
        {
#if USE_LOCKING
            lock (relay)
#endif
            {
                relay.Set(Relay.Value.Off);
            }
            onValueChanged(new VirtualControlEventArgs(0, InUse));
        }

        /// <summary>
        ///     Sets the Relay to the On Position
        /// </summary>
        private void setOn()
        {
#if USE_LOCKING
            lock (relay)
#endif
            {
                relay.Set(Relay.Value.On);
            }
            onValueChanged(new VirtualControlEventArgs(1, InUse));
        }

        /// <summary>
        ///     Sets the Relay to the Reverse Position
        /// </summary>
        private void setReverse()
        {
#if USE_LOCKING
            lock (relay)
#endif
            {
                relay.Set(Relay.Value.Reverse);
            }
            onValueChanged(new VirtualControlEventArgs(3, InUse));
        }

        #endregion Private Methods
    }
}