/****************************** Header ******************************\
Interface Name: IComponent
Summary: Interface used to create all component items that map to
physical components on the robot.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using System;

namespace Base
{
    /// <summary>
    ///     Interface that all components [physical devices on the robot] implement.
    /// </summary>
    public interface IComponent
    {
        #region Public Methods

        /// <summary>
        ///     Returns the WPI object that this component wraps.
        /// </summary>
        /// <returns></returns>
        [Obsolete("Not thread safe! Be sure to lock if you are using this method.")]
        object GetRawComponent();

        #endregion Public Methods

        #region Public Properties

        /// <summary>
        ///     The name of the component
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Boolean flag to determin if the component is in use
        /// </summary>
        bool InUse { get; }

        /// <summary>
        ///     Returns the object that is currently using the component
        /// </summary>
        object Sender { get; }

        #endregion Public Properties
    }
}