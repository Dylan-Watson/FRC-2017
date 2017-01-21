/****************************** Header ******************************\
Class Name: Base Calls
Summary: Class to handle all of the calls to the base broject to
gather groups of components
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

//TODO: Ryan, check this header to make sure it is correct

using Base;
using Base.Config;
using System;
using System.Linq;

namespace Trephine
{
    /// <summary>
    ///     Instance based utility class for calles to Base
    /// </summary>
    public sealed class BaseCalls
    {
        private static readonly Lazy<BaseCalls> _lazy =
            new Lazy<BaseCalls>(() => new BaseCalls());

        #region Private Fields

        private Config config;

        #endregion Private Fields

        private BaseCalls()
        {
        }

        /// <summary>
        /// </summary>
        public static BaseCalls Instance => _lazy.Value;

        #region Public Constructors

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="config">instance of the config</param>
        public void SetConfig(Config config)
        {
            this.config = config;
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///     Full stop of the drive train
        /// </summary>
        public void FullDriveStop()
        {
            config.ActiveCollection.GetRightDriveMotors.ForEach(s => ((Motor) s).Stop());
            config.ActiveCollection.GetLeftDriveMotors.ForEach(s => ((Motor) s).Stop());
        }
        /// <summary>
        ///     slowly stops drive train yeet
        /// </summary>
        public void SlowStop()
        {
            var rightPow = config.ActiveCollection.GetRightDriveMotors.Select(s => ((Motor)s).Get()).ToList()[0];
            var leftPow = config.ActiveCollection.GetLeftDriveMotors.Select(s => ((Motor)s).Get()).ToList()[0];

            for (double i = rightPow; i > 0; i-= .005)
            {
                BaseCalls.Instance.SetRightDrive(i);
            }
            for (double i = leftPow; i > 0; i -= .005)
            {
                BaseCalls.Instance.SetLeftDrive(i);
            }
        }

        /// <summary>
        ///     Full stop of the robot
        /// </summary>
        public void FullStop()
        {
            config.ActiveCollection.GetRightDriveMotors.ForEach(s => ((Motor) s).Stop());
            config.ActiveCollection.GetLeftDriveMotors.ForEach(s => ((Motor) s).Stop());
        }

        /// <summary>
        ///     Gets the instance of the config
        /// </summary>
        /// <returns></returns>
        public Config GetConfig() => config;

        /// <summary>
        ///     Sets the left drive of the robot to a specified value
        /// </summary>
        /// <param name="value">value to set</param>
        public void SetLeftDrive(double value)
            => config.ActiveCollection.GetLeftDriveMotors.ForEach(s => ((Motor) s).Set(value, this));

        /// <summary>
        ///     Sets the right drive of the robot to a specified value
        /// </summary>
        /// <param name="value">value to set</param>
        public void SetRightDrive(double value)
            => config.ActiveCollection.GetRightDriveMotors.ForEach(s => ((Motor) s).Set(value, this));

        #endregion Public Methods
    }
}