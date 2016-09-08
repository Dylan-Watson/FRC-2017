﻿using System;
using WPILib;
namespace Base.Components
{

    /// <summary>
    /// Class to handle Encoder Components
    /// </summary>
    public sealed class EncoderItem : InputComponent, IComponent
    {
       
        #region Public Events

        /// <summary>
        /// Event used for VirtualControlEvents
        /// </summary>
        public event EventHandler ValueChanged;

        #endregion Public Events

        #region Public Constructors

        /// <summary>
        ///  Constructor
        /// </summary>
        /// <param name="commonName"></param>
        /// <param name="aChannel"></param>
        /// <param name="bChannel"></param>
        public EncoderItem(string commonName, int aChannel, int bChannel) {
            encoder = new Encoder(aChannel, bChannel);
            Name = commonName;
        }

        #endregion Public Constructors


        /// <summary>
        /// Releases managed and native resources
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

        #region Private Fields

        private readonly Encoder encoder;

        #endregion Provate Fields

        #region Public Properties

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
        
        #endregion Public Properties

        #region Public Methods 

        /// <summary>
        /// Disposes of this IComponent and its managed resources
        /// </summary>
        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets the current value of the encoder
        /// </summary>
        /// <returns></returns>
        public override double Get()
        {
            lock (encoder)
            {
                int encoderVal = encoder.Get();
                return encoderVal;
            }
        }

        /// <summary>
        /// returns encoder
        /// </summary>
        /// <returns>encoder</returns>
        public object GetRawComponent() => encoder;

        #endregion Public Methods
    }
}
