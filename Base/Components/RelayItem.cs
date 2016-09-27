/****************************** Header ******************************\
Class Name: DoubleSolenoidItem inherits IComponent
Summary: Abstractio017
Copyright (c) n for the WPIlib Relay that extends to include
some helper and control methods.
Project:     FRC2BroncBotz.
All rights reserved.

Author(s): Dylan Watson, Ryan S. Cooper
Email: dylantrwatson@gmail.com, cooper.ryan@centaurisoft.org
\********************************************************************/


using System;
using WPILib;
namespace Base.Components
{
    /// <summary>
    /// Class to handle WPI Relays
    /// </summary>
    public sealed class RelayItem : IComponent
    {
        #region Private Fields

        private readonly Relay relay;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Constructor for RelayItem
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

        #region Public Events

        /// <summary>
        /// Event used for VirtualControlEvents
        /// </summary>
        public event EventHandler ValueChanged;

        #endregion Public Events

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

        #endregion Public Properties

        #region Private Properties

        private Relay.Value Default { get; }

        #endregion Private Properties

        #region Protected Methods

        /// <summary>
        /// Method to fire value changes for set/get values and InUse values
        /// </summary>
        /// <param name="e">VirtualControlEventArgs</param>
        private void onValueChanged(VirtualControlEventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        #endregion Protected Methods

        /// <summary>
        /// Releases managed and native resources
        /// </summary>
        /// <param name="disposing"></param>
        private void dispose(bool disposing)
        {
            if (!disposing) return;
            lock (relay)
            {
                relay?.Dispose();
            }
        }

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <param name="sender"></param>
        public void Set(Relay.Value val, object sender)
        {
            Sender = sender;
            InUse = true;
            lock (relay)
            {
                relay.Set(val);
                onValueChanged(new VirtualControlEventArgs(Convert.ToDouble(val), InUse));
            }

            Sender = null;
            InUse = false;
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
        /// Sets the Relay to its default position
        /// </summary>
        public void DefaultSet() { lock (relay) { relay.Set(Default); } }

        /// <summary>
        /// Gets the raw Relay object representing the Relay
        /// </summary>
        /// <returns></returns>
        public object GetRawComponent() => relay;

        /// <summary>
        /// Gets the current position that the relay is supposed to be in
        /// </summary>
        /// <returns></returns>
        public Relay.Value GetCurrentPosition() { lock (relay) { return relay.Get(); } }

        /// <summary>
        /// Sets the Relay to the Off Position
        /// </summary>
        private void setOff()
        {
            relay.Set(Relay.Value.Off);
            onValueChanged(new VirtualControlEventArgs(0, InUse));
        }

        /// <summary>
        /// Sets the Relay to the On Position
        /// </summary>
        private void setOn()
        {
            relay.Set(Relay.Value.On);
            onValueChanged(new VirtualControlEventArgs(1, InUse));
        }

        /// <summary>
        /// Sets the Relay to the Forward Position
        /// </summary>
        private void setForward()
        {
            relay.Set(Relay.Value.Forward);
            onValueChanged(new VirtualControlEventArgs(2, InUse));
        }

        /// <summary>
        /// Sets the Relay to the Reverse Position
        /// </summary>
        private void setReverse()
        {
            relay.Set(Relay.Value.Reverse);
            onValueChanged(new VirtualControlEventArgs(3, InUse));
        }


        #endregion Public Methods
    }
}
