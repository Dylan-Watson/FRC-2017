/****************************** Header ******************************\
Class Name: UltrasonicItem inherits InputComponent and IComponent
Summary: Abstraction for the WPIlib Ultrasonic that extends to include
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
    ///     Class to handle UltrasonicItem Sensor Components
    /// </summary>
    public sealed class UltrasonicItem : InputComponent, IComponent
    {

        #region Public Constructor

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="commonName">CommonName the component will have</param>
        /// <param name="pingChannel"></param>
        /// <param name="echoChannel"></param>
        /// <param name="unit"></param>
        public UltrasonicItem(string commonName, int pingChannel, int echoChannel, Ultrasonic.Unit unit = 
                  Ultrasonic.Unit.Millimeters)
        {
            u = new Ultrasonic(pingChannel, echoChannel, unit);
            Name = commonName;
            Unit = unit;
        }

        #endregion Public Constructor

        #region Private Fields

        private Ultrasonic u;

        #endregion Private Fields 

        #region Public Events

        /// <summary>
        ///     Event used for VirtualControlEvents
        /// </summary>
        public event EventHandler ValueChanged;

        #endregion Public Events

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
        ///     Defines the Units the Ultrasonic will measure in -> Inches or MM
        /// </summary>
        public Ultrasonic.Unit Unit { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///     returns ain
        /// </summary>
        /// <returns>ain</returns>
        public object GetRawComponent()
        {
            return u;
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
        ///     Gets the current value of the Ultrasonic Sensor in Inches or MM
        /// </summary>
        /// <returns>double rangeInches or double rangeMM</returns>
        public override double Get()
        {
            lock (u)
            {
                if (Unit == Ultrasonic.Unit.Inches)
                {
                    return u.GetRangeInches();
                }
                else
                {
                    return u.GetRangeMM();
                }
            }
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
            lock (u)
            {
                u?.Dispose();
            }
        }

        #endregion Private Methods

    }
}
