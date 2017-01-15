/****************************** Header ******************************\
Class Name: Filters [static]
Summary: Provides a number of methods designed to filter inputs.
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
    ///     Defines types of fit functions for drive trains
    /// </summary>
    public enum MotorControlFitFunction
    {
        /// <summary>
        ///     Linear fit for axis to deadzone proportion
        /// </summary>
        Linear,

        /// <summary>
        ///     Quadratic fit for axis to deadzone proportion
        /// </summary>
        Quadratic,

        /// <summary>
        ///     Exponential fit for axis to deadzone proportion
        /// </summary>
        Exponential,

        /// <summary>
        ///     Polynomial fit for axis to deadzone proportion
        /// </summary>
        Polynomial
    }

    /// <summary>
    ///     Utility class to define filters
    /// </summary>
    public static class Filters
    {
        #region Public Methods

        /// <summary>
        ///     Function that returns an output proportional (exponentially) to the axis and deadzone inputs
        /// </summary>
        /// <param name="x">input from axis/controller</param>
        /// <param name="dz">deadzone value</param>
        /// <param name="power">the power to use in the fit function</param>
        /// <returns>the filtered value</returns>
        public static double _ExponentialValueEstimator(double x, double dz, double power = 2)
        {
            dz = dz + Constants.MINUMUM_JOYSTICK_RETURN;
            var value = (Math.Pow(Math.Pow(2, 1 / (1 - dz)), Math.Abs(x) - dz) - 1) * Math.Sign(x);
            if (value > 1)
                return 1;
            if (value < -1)
                return -1;

            return value;
        }

        /// <summary>
        ///     Function that returns an output proportional (linearly) to the axis and deadzone inputs
        /// </summary>
        /// <param name="x">input from axis/controller</param>
        /// <param name="dz">deadzone value</param>
        /// <param name="na">not used; however must be listed due to use by a delegate</param>
        /// <returns>the filtered value</returns>
        public static double _LinearValueEstimator(double x, double dz, double na = 0)
        {
            dz = dz + Constants.MINUMUM_JOYSTICK_RETURN;
            return (Math.Abs(x) - dz) * Math.Pow(1 - dz, -1) * Math.Sign(x);
        }

        /// <summary>
        ///     Function that returns an output proportional (polynomial) to the axis and deadzone inputs
        /// </summary>
        /// <param name="x">input from axis/controller</param>
        /// <param name="dz">deadzone value</param>
        /// <param name="power">the power to use in the fit function</param>
        /// <returns>the filtered value</returns>
        public static double _PolynomialValueEstimator(double x, double dz, double power)
        {
            dz = dz + Constants.MINUMUM_JOYSTICK_RETURN;
            var value = Math.Pow(Math.Abs(x) - dz, power) * Math.Pow(1 - dz, -power) * Math.Sign(x);
            if (value > 1)
                return 1;
            if (value < -1)
                return -1;

            return value;
        }

        /// <summary>
        ///     Function that returns an output proportional (quadratically) to the axis and deadzone inputs
        /// </summary>
        /// <param name="x">input from axis/controller</param>
        /// <param name="dz">deadzone value</param>
        /// <param name="na">not used; however must be listed due to use by a delegate</param>
        /// <returns>the filtered value</returns>
        public static double _QuadraticValueEstimator(double x, double dz, double na = 0)
            => _PolynomialValueEstimator(x, dz, 2);

        #endregion Public Methods
    }
}