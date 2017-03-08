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
using Base.Components;
using Base.Config;
using System;
using System.Linq;
using WPILib;

namespace Trephine
{
    /// <summary>
    ///     Instance based utility class for calles to Base
    /// </summary>
    public sealed class BaseCalls
    {
        #region Private Constructors

        private BaseCalls()
        {
        }

        #endregion Private Constructors

        #region Public Properties

        /// <summary>
        /// </summary>
        public static BaseCalls Instance => _lazy.Value;

        private static CommonName gmMani = new CommonName("gm_mani");

        private static CommonName intake = new CommonName("intake");

        #endregion Public Properties

        #region Private Fields

        private static readonly Lazy<BaseCalls> _lazy =
            new Lazy<BaseCalls>(() => new BaseCalls());

        private Config config;

        #endregion Private Fields

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
        ///     Full stop of the robot
        /// </summary>
        public void FullStop()
        {
            config.ActiveCollection.GetRightDriveMotors.ForEach(s => ((Motor) s).Stop());
            config.ActiveCollection.GetLeftDriveMotors.ForEach(s => ((Motor) s).Stop());
            config.ActiveCollection.GetMotorItem(intake).Stop();
        }

        /// <summary>
        ///     Gets the instance of the config
        /// </summary>
        /// <returns></returns>
        public Config GetConfig() => config;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="config">instance of the config</param>
        public void SetConfig(Config config)
        {
            this.config = config;
        }

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

        /// <summary>
        ///     sets the mani to forward or back position
        /// </summary>
        /// <param name="value">value to set</param>
        /// <param name="sender">specifies whats sending it</param>
        public void SetMani(DoubleSolenoid.Value value, object sender)
        {
            var tmp = (DoubleSolenoidItem)(config.ActiveCollection.Get(gmMani));
            tmp.Set(value, sender);
        }

        /// <summary>
        ///     sets intake to spin forward/backward 
        /// </summary>
        /// <param name="value">value to set</param>
        /// <param name="sender">speifies whats sending it</param>
        public void SetIntake(double value, object sender)
        {
            var tmp = (Motor)(config.ActiveCollection.Get(intake));
            tmp.Set(value, sender);
        }

        /// <summary>
        ///     slow stop dt
        /// </summary>
        public void SlowStop()
        {
            var rightPow = config.ActiveCollection.GetRightDriveMotors.Select(s => ((Motor) s).Get()).ToList()[0];
            var leftPow = config.ActiveCollection.GetLeftDriveMotors.Select(s => ((Motor) s).Get()).ToList()[0];

            while (Math.Abs(rightPow) > .05 && Math.Abs(leftPow) > .05)
            {
                rightPow /= 1.02;
                leftPow /= 1.02;

                SetLeftDrive(leftPow);
                SetRightDrive(rightPow);

                Timer.Delay(.005);
            }

            FullDriveStop();
        }
        /// <summary>
        ///     slow start DT
        /// </summary>
        /// <param name="finalPower"></param>
        /*
         * I AM GONNA DO A THING WHERE U CAN SLOW SPIN L8R
         */
        public void SlowStart(double finalPower)
        {
            if (finalPower < 0)
            {
                for (double d = -.05; d < finalPower; d *= 1.02)
                {
                    SetLeftDrive(d);
                    SetRightDrive(d);
                    Timer.Delay(.005);
                }
            }

            if (finalPower > 0)
            {
                for (double d = .05; d < finalPower; d *= 1.02)
                {
                    SetLeftDrive(d);
                    SetRightDrive(d);
                    Timer.Delay(.005);
                }
            }
        }

        #endregion Public Methods
    }
}