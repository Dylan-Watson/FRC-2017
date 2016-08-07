/****************************** Header ******************************\
Class Name: ControlItem [abstract]
Summary: Abstract class used to create objects that control physical
componentRange in like ways.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using Base.Components;
using WPILib;

namespace Tourniquet.ControlItems
{
    /// <summary>
    ///     Defines a control type
    /// </summary>
    public enum ControlItemType
    {
        /// <summary>
        ///     Button based control
        /// </summary>
        ButtonControl,

        /// <summary>
        ///     Toggle based control
        /// </summary>
        ToggleControl,

        /// <summary>
        ///     Axis based control
        /// </summary>
        AxisControl
    }

    /// <summary>
    ///     Abstract class to define different types of controls
    /// </summary>
    public abstract class ControlItem
    {
        #region Private Fields

        private readonly List<IComponent> components = new List<IComponent>();

        #endregion Private Fields

        #region Protected Properties

        /// <summary>
        ///     Joystick the control is to use
        /// </summary>
        protected Joystick joystick { get; set; }

        #endregion Protected Properties

        #region Public Properties

        /// <summary>
        ///     The control's name
        /// </summary>
        public string ControlName { get; protected set; }

        /// <summary>
        ///     The control type
        /// </summary>
        public ControlItemType ControlType { get; protected set; }

        /// <summary>
        ///     Defines if the control is running (outputting values)
        /// </summary>
        public bool IsRunning { get; protected set; }

        /// <summary>
        ///     Defines if the control is enabled
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///     Overrides ToString to return the control's name
        /// </summary>
        public override string ToString() => ControlName;

        /// <summary>
        ///     Adds a IComponent to the control
        /// </summary>
        /// <param name="component"></param>
        public void AddComponent(IComponent component) => components.Add(component);

        /// <summary>
        ///     Adds multiple IComponents to the control
        /// </summary>
        /// <param name="componentRange"></param>
        public void AddComponents(List<IComponent> componentRange) => components.AddRange(componentRange);

        /// <summary>
        ///     Sets a solenoid value if there is a solenoid in the controls bindings
        /// </summary>
        /// <param name="value">solenoid position</param>
        protected void set(DoubleSolenoid.Value value)
        {
            if (!IsEnabled) return;
        }

        /// <summary>
        ///     The control implements to update its state and values
        /// </summary>
        public abstract void Update();

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        ///     Sets a double value to components
        /// </summary>
        /// <param name="val">value</param>
        protected void set(double val)
        {
            if (!IsEnabled)
            {
                stopMotors();
                return;
            }

            foreach (
                var motor in
                    components.OfType<Motor>()
                        .Where(motor => !((IComponent) motor).InUse || (((IComponent) motor).Sender == this)))
                motor?.Set(val, this);

            foreach (
                var output in
                    components.OfType<OutputComponent>()
                        .Where(output => !((IComponent)output).InUse || (((IComponent)output).Sender == this)))
                output?.Set(Math.Abs(val*5), this);//times five to compensate for analog output, which has upto 5v output.
        }

        //TODO: Implement for button controls
        /// <summary>
        ///     NotImplementedException
        /// </summary>
        /// <param name="val"></param>
        protected void set(bool val)
        {
            throw new NotImplementedException();
            /*
                        if (IsEnabled) return;
                        StopMotors(); return;
            */
        }

        /// <summary>
        ///     Sets the control's motor bindings to only forward motion
        /// </summary>
        protected void setOnlyForward()
        {
            foreach (var motor in components.OfType<Motor>()) motor?.SetAllowCc(false);
        }

        /// <summary>
        ///     Sets the control's motor bindings to only reverse motion
        /// </summary>
        protected void setOnlyReverse()
        {
            foreach (var motor in components.OfType<Motor>()) motor?.SetAllowCc(false);
        }

        /// <summary>
        ///     Sets all the motor controllers in the control's bindings to reverse or not
        /// </summary>
        /// <param name="val">if the controllers should be reversed</param>
        protected void setReversed(bool val)
        {
            foreach (var motor in components.OfType<Motor>())
                if (val)
                    motor.ReverseDirection();
                else
                    motor.RestoreDirection();
        }

        /// <summary>
        /// Stops or zeros all components this control is bound to
        /// </summary>
        protected void stop()
        {
            stopMotors();
            zeroOutputComponents();
        }

        /// <summary>
        ///     Stopps all motors in the control's bindings
        /// </summary>
        protected void stopMotors()
        {
            foreach (
                var motor in
                    components.OfType<Motor>()
                        .Where(motor => ((IComponent) motor).Sender == this))
                motor?.Stop();
        }

        /// <summary>
        ///     Sets outputcomponents to zero
        /// </summary>
        protected void zeroOutputComponents()
        {
            foreach (
                var output in
                    components.OfType<OutputComponent>()
                        .Where(output => !((IComponent)output).InUse || (((IComponent)output).Sender == this)))
                output?.Set(0, this);
        }

        #endregion Protected Methods
    }
}