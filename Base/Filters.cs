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
    public enum MotorControlFitFunction
    {
        Linear,
        Quadratic,
        Exponential,
        Polynomial
    }

    public static class Filters
    {
        #region Public Methods

        public static double _ExponentialValueEstimator(double x, double dz, double power = 2)
        {
            dz = dz + Constants.MINUMUM_JOYSTICK_RETURN;
            var value = (Math.Pow(Math.Pow(2, 1/(1 - dz)), Math.Abs(x) - dz) - 1)*Math.Sign(x);
            if (value > 1)
                return 1;
            if (value < -1)
                return -1;

            return value;
        }

        public static double _LinearValueEstimator(double x, double dz, double na = 0)
        {
            dz = dz + Constants.MINUMUM_JOYSTICK_RETURN;
            return (Math.Abs(x) - dz)*Math.Pow(1 - dz, -1)*Math.Sign(x);
        }

        public static double _PolynomialValueEstimator(double x, double dz, double power)
        {
            dz = dz + Constants.MINUMUM_JOYSTICK_RETURN;
            var value = Math.Pow(Math.Abs(x) - dz, power)*Math.Pow(1 - dz, -power)*Math.Sign(x);
            if (value > 1)
                return 1;
            if (value < -1)
                return -1;

            return value;
        }

        public static double _QuadraticValueEstimator(double x, double dz, double na = 0)
            => _PolynomialValueEstimator(x, dz, 2);

        #endregion Public Methods

        /*! Function that returns an output proportional (quadratically) to the axis and deadzone inputs */
        /*! Function that returns an output proportional (exponentially) to the axis and deadzone inputs */
    }
}