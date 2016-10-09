/****************************** Header ******************************\
Class Name: AccelerometerItem, inherits IComponent
Summary: Abstraction for the WPIlib BuiltInAccelerometer that extends to include
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
    ///     Class to handle the RoboRio's Built In Accelerometer
    /// </summary>
    public class AccelerometerItem : IComponent
    {
        #region Public Constructors

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="commonName">CommonName the component will have</param>
        public AccelerometerItem(string commonName)
        {
            aa = new BuiltInAccelerometer();
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

        private readonly BuiltInAccelerometer aa;

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

        public void Dispose() {}

        /// <summary>
        ///     Return the WPILib BuiltInAccelerometer
        /// </summary>
        /// <returns>WPILib.BuiltInAccelerometer aa</returns>
        public object GetRawComponent()
        {
            return aa;
        }

        /// <summary>
        ///     Return the X, Y, Z axes of the BuiltInAccelerometer
        /// </summary>
        /// <returns>WPILib.Interfaces.AllAxes</returns>
        public WPILib.Interfaces.AllAxes GetAllAxes()
        {
            lock (aa)
            {
                return aa.GetAllAxes();
            }
        }

        /// <summary>
        ///     Gets the X Axis of the BuiltInAccelerometer
        /// </summary>
        /// <returns></returns>
        public double GetX()
        {
            lock (aa)
            {
                return aa.GetX();
            }
        }

        /// <summary>
        ///     Gets the Y Axis of the BuiltInAccelerometer
        /// </summary>
        /// <returns></returns>
        public double GetY()
        {
            lock (aa)
            {
                return aa.GetY();
            }
        }

        /// <summary>
        ///     Gets the Z Axis of the BuiltInAccelerometer
        /// </summary>
        /// <returns></returns>
        public double GetZ()
        {
            lock (aa)
            {
                return aa.GetZ();
            }
        }

        #endregion Public Methods
    }
}
