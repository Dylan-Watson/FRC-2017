/****************************** Header ******************************\
Class Name: RioAccelerometerItem, inherits IComponent and WPI BuiltinAccelerometer, this is a singleton
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
    public class RioAccelerometerItem : BuiltInAccelerometer, IComponent
    {
        #region Private Fields

        private static readonly Lazy<RioAccelerometerItem> _lazy =
            new Lazy<RioAccelerometerItem>(() => new RioAccelerometerItem());

        #endregion Private Fields

        #region Private Constructors

        private RioAccelerometerItem()
        {
        }

        #endregion Private Constructors

        #region Public Events

        /// <summary>
        ///     Event used for VirtualControlEvents
        /// </summary>
        [Obsolete("This event is never used")]
        public event EventHandler ValueChanged;

        #endregion Public Events

        #region Public Properties

        /// <summary>
        ///     Gets the instance (if any) of the builtin accelerometer
        /// </summary>
        public static RioAccelerometerItem Instance => _lazy?.Value;

        /// <summary>
        ///     Defines whether the component is in use or not
        /// </summary>
        public bool InUse { get; } = false;

        /// <summary>
        ///     Name of the component
        /// </summary>
        public string Name { get; } = "BuiltInAccelerometer";

        /// <summary>
        ///     Defines the object issuing the commands
        /// </summary>
        public object Sender { get; } = null;

        #endregion Public Properties

        #region Public Methods

        [Obsolete("This object does not need to be disposed of, as it is a standard singleton.")]
        public void Dispose()
        {
        }

        public object GetRawComponent()
        {
            return this;
        }

        #endregion Public Methods
    }
}