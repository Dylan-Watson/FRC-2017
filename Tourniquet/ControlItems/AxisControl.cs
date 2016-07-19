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

using System;
using Base;
using WPILib;

namespace Tourniquet.ControlItems
{
    internal delegate double FitFunction(double x, double y, double z);

    public class AxisControl : ControlItem
    {
        #region Public Constructors

        public AxisControl(string name, Joystick joystick, int axis, MotorControlFitFunction fitFunction, bool reversed,
            double deadZone, double multiplier = 1, double power = 2)
        {
            ControlName = name;
            ControlType = ControlItemType.AxisControl;
            this.deadZone = deadZone;
            this.multiplier = multiplier;
            this.power = power;
            this.axis = axis;
            Joystick = joystick;
            SetReversed(reversed);

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

        public override void Update()
        {
            var raw = Joystick.GetRawAxis(axis);
            if ((raw > deadZone) || (raw < -deadZone))
            {
                Set(fitFunction(raw, deadZone, power)*multiplier);
                IsRunning = true;
            }
            else if (IsRunning)
            {
                StopMotors();
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