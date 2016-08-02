﻿using Base;
using Base.Config;

namespace Trephine
{
    /// <summary>
    ///     Instance based utility class for calles to Base
    /// </summary>
    public class BaseCalls
    {
        private readonly Config config;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="config">instance of the config</param>
        public BaseCalls(Config config)
        {
            this.config = config;
        }

        /// <summary>
        ///     Gets the instance of the config
        /// </summary>
        /// <returns></returns>
        public Config GetConfig() => config;

        /// <summary>
        ///     Sets the right drive of the robot to a specified value
        /// </summary>
        /// <param name="value">value to set</param>
        public void SetRightDrive(double value)
            => config.ActiveCollection.GetRightDriveMotors.ForEach(s => ((Motor) s).Set(value, this));

        /// <summary>
        ///     Sets the left drive of the robot to a specified value
        /// </summary>
        /// <param name="value">value to set</param>
        public void SetLeftDrive(double value)
            => config.ActiveCollection.GetLeftDriveMotors.ForEach(s => ((Motor) s).Set(value, this));

        //TODO: complete full stop of all components
        /// <summary>
        ///     Full stop of the robot
        /// </summary>
        public void FullStop()
        {
            config.ActiveCollection.GetRightDriveMotors.ForEach(s => ((Motor) s).Stop());
            config.ActiveCollection.GetLeftDriveMotors.ForEach(s => ((Motor) s).Stop());
        }

        /// <summary>
        ///     Full stop of the drive train
        /// </summary>
        public void FullDriveStop()
        {
            config.ActiveCollection.GetRightDriveMotors.ForEach(s => ((Motor) s).Stop());
            config.ActiveCollection.GetLeftDriveMotors.ForEach(s => ((Motor) s).Stop());
        }
    }
}