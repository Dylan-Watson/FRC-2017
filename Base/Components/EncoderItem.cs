﻿/****************************** Header ******************************\
Class Name: EncoderItem inherits IComponent and InputComponent
Summary: Abstraction for the WPIlib Encoder that extends to include
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
    ///     Class to handle Encoder Components
    /// </summary>
    public sealed class EncoderItem : InputComponent, IComponent
    {
        #region Public Constructors

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="commonName">CommonName the EncoderItem will have</param>
        /// <param name="aChannel">aChannel that the encoder is plugged into on the roboRIO</param>
        /// <param name="bChannel">bChannel that the encoder is plugged into on the roboRIO</param>
        /// <param name="isReversed">Whether or not the encoder is reversed - defaults to false</param>
        public EncoderItem(string commonName, int aChannel, int bChannel, bool isReversed = false)
        {
            encoder = new Encoder(aChannel, bChannel, isReversed);
            Name = commonName;
            IsReversed = isReversed;
        }

        #endregion Public Constructors

        #region Public Events

        /// <summary>
        ///     Event used for VirtualControlEvents
        /// </summary>
        public event EventHandler ValueChanged;

        #endregion Public Events

        #region Private Fields

        private readonly Encoder encoder;

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

        /// <summary>
        ///     Defines whether or not the encoder is reversed
        /// </summary>
        public object IsReversed { get; }

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
        ///     Gets the current value of the encoder
        /// </summary>
        /// <returns>Double Input</returns>
        public override double Get()
        {
            lock (encoder)
            {
                var input = Convert.ToDouble(encoder.Get());

                if (Math.Abs(previousInput - input) > Constants.EPSILON_MIN)
                    onValueChanged(new VirtualControlEventArgs(input, true));

                previousInput = input;
                return input;
            }
        }

        /// <summary>
        ///     returns encoder
        /// </summary>
        /// <returns>encoder</returns>
        public object GetRawComponent() => encoder;

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        ///     Releases managed and native resources
        /// </summary>
        /// <param name="disposing"></param>
        private void dispose(bool disposing)
        {
            if (!disposing) return;
            lock (encoder)
            {
                encoder?.Dispose();
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