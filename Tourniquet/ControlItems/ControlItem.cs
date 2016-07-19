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

using System.Collections.Generic;
using System.Linq;
using Base;
using WPILib;

namespace Tourniquet.ControlItems
{
    public enum ControlItemType
    {
        ButtonControl,
        ToggleControl,
        AxisControl
    }

    public abstract class ControlItem
    {
        #region Private Fields

        private readonly List<IComponent> components = new List<IComponent>();

        #endregion Private Fields

        #region Protected Properties

        protected Joystick Joystick { get; set; }

        #endregion Protected Properties

        #region Public Properties

        public string ControlName { get; protected set; }
        public ControlItemType ControlType { get; protected set; }
        public bool IsRunning { get; protected set; }
        public bool IsEnabled { get; set; }

        #endregion Public Properties

        #region Public Methods

        public void AddComponent(IComponent component) => components.Add(component);

        public void AddComponents(List<IComponent> componentRange) => components.AddRange(componentRange);

        protected void Set(DoubleSolenoid.Value value) { if (!IsEnabled) return; }

        public abstract void Update();

        #endregion Public Methods

        #region Protected Methods

        protected void Set(double val)
        {
            if (!IsEnabled) { StopMotors(); return; }

            foreach (
                var motor in
                    components.Select(component => component as Motor)
                        .Where(motor => !((IComponent) motor).InUse || (((IComponent) motor).Sender == this)))
                motor?.Set(val, this);
        }

        protected void Set(bool val)
        {
            if (!IsEnabled) { StopMotors(); return; }
        }

        protected void SetOnlyForward()
        {
            foreach (var motor in components.Select(component => component as Motor)) motor?.SetAllowCc(false);
        }

        protected void SetOnlyReverse()
        {
            foreach (var motor in components.Select(component => component as Motor)) motor?.SetAllowCc(false);
        }

        protected void SetReversed(bool val)
        {
            foreach (var motor in components.OfType<Motor>())
                if (val)
                    motor.ReverseDirection();
                else
                    motor.RestoreDirection();
        }

        protected void StopMotors()
        {
            foreach (
                var motor in
                    components.Select(component => component as Motor)
                        .Where(motor => ((IComponent) motor).Sender == this))
                motor?.Stop();
        }

        #endregion Protected Methods
    }
}