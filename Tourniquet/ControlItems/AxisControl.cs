/****************************** Header ******************************\
Class Name: AxisControl inherits ControlItem
Summary: Used to control physical component with an axis (joystick axis
on controller...).
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using Base;
using System;
using WPILib;

namespace Tourniquet.ControlItems
{
    internal delegate double FitFunction(double x, double y, double z);

    /// <summary>
    /// Class to handle an axis control
    /// </summary>
    public class AxisControl : ControlItem
    {
        #region Public Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">name of the control</param>
        /// <param name="joystick">WPI joysick the control will use</param>
        /// <param name="axis">the axis to use</param>
        /// <param name="fitFunction">the fit function to use, see Filters class in Base</param>
        /// <param name="reversed">if the control is reversed</param>
        /// <param name="deadZone">the deadzone of the axis</param>
        /// <param name="multiplier">the output multiplier</param>
        /// <param name="power">the power for the fit function, see Filters class in Base</param>
        /// <param name="isEnabled">determins whether the control is enabled or not</param>
        public AxisControl(string name, Joystick joystick, int axis, MotorControlFitFunction fitFunction, bool reversed,
            double deadZone, bool isEnabled, double multiplier = 1, double power = 2)
        {
            IsEnabled = isEnabled;
            ControlName = name;
            ControlType = ControlItemType.AxisControl;
            this.deadZone = deadZone;
            this.multiplier = multiplier;
            this.power = power;
            this.axis = axis;
            this.joystick = joystick;
            IsReversed = reversed;

            switch (fitFunction)
            {
                case MotorControlFitFunction.Linear:
                    this.fitFunction = Filters._LinearValueEstimator;
                    break;

                case MotorControlFitFunction.Quadratic:
                    this.fitFunction = Filters._QuadraticValueEstimator;
                    break;

                case MotorControlFitFunction.Polynomial:
                    this.fitFunction = Filters._PolynomialValueEstimator;
                    break;

                case MotorControlFitFunction.Exponential:
                    this.fitFunction = Filters._ExponentialValueEstimator;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(fitFunction), fitFunction, null);
            }
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Update the control and its bindings
        /// </summary>
        public override void Update()
        {
            var raw = joystick.GetRawAxis(axis);
            if ((raw > deadZone) || (raw < -deadZone))
            {
                set(fitFunction(raw, deadZone, power)*multiplier);
                IsRunning = true;
            }
            else if (IsRunning)
            {
                stop();
                IsRunning = false;
            }
        }

        #endregion Public Methods

        #region Private Fields

        private readonly int axis;
        private readonly double deadZone;
        private readonly FitFunction fitFunction;
        private readonly double multiplier;
        private readonly double power;

        #endregion Private Fields
    }
}