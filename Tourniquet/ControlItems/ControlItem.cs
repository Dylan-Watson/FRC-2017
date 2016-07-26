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

namespace Tourniquet.ControlItems
{
    using Base;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using WPILib;

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
        protected Joystick Joystick { get; set; }

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
        public bool IsEnabled { get; set; }

        #endregion Public Properties

        #region Public Methods

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
        protected void Set(DoubleSolenoid.Value value)
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
        ///     Sets a value to motor controllers in the control's bindings
        /// </summary>
        /// <param name="val">motor controller speed</param>
        protected void Set(double val)
        {
            if (!IsEnabled)
            {
                StopMotors();
                return;
            }

            foreach (
                var motor in
                    components.Select(component => component as Motor)
                        .Where(motor => !((IComponent)motor).InUse || (((IComponent)motor).Sender == this)))
                motor?.Set(val, this);
        }

        //TODO: Implement for button controls
        /// <summary>
        ///     NotImplementedException
        /// </summary>
        /// <param name="val"></param>
        protected void Set(bool val)
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
        protected void SetOnlyForward()
        {
            foreach (var motor in components.Select(component => component as Motor)) motor?.SetAllowCc(false);
        }

        /// <summary>
        ///     Sets the control's motor bindings to only reverse motion
        /// </summary>
        protected void SetOnlyReverse()
        {
            foreach (var motor in components.Select(component => component as Motor)) motor?.SetAllowCc(false);
        }

        /// <summary>
        ///     Sets all the motor controllers in the control's bindings to reverse or not
        /// </summary>
        /// <param name="val">if the controllers should be reversed</param>
        protected void SetReversed(bool val)
        {
            foreach (var motor in components.OfType<Motor>())
                if (val)
                    motor.ReverseDirection();
                else
                    motor.RestoreDirection();
        }

        /// <summary>
        ///     Stopps all motors in the control's bindings
        /// </summary>
        protected void StopMotors()
        {
            foreach (
                var motor in
                    components.Select(component => component as Motor)
                        .Where(motor => ((IComponent)motor).Sender == this))
                motor?.Stop();
        }

        #endregion Protected Methods
    }
}