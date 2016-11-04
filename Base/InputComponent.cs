/****************************** Header ******************************\
Class Name: InputComponent [Abstract]
Summary: Abstract Class used to create all items that return an input to the robot.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper, Dylan Watson
Email: cooper.ryan@centaurisoft.org, dylantrwatson@gmail.com
\********************************************************************/

namespace Base
{
    /// <summary>
    ///     Abstract class for creating components that have physical input
    /// </summary>
    public abstract class InputComponent
    {
        #region Public Methods

        /// <summary>
        ///     Gets the primary input value from the component
        /// </summary>
        /// <returns></returns>
        public abstract double Get();

        #endregion Public Methods
    }
}