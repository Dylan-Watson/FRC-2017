using Base.Components;
using System;
using System.Collections.Generic;

namespace Base
{
    /// <summary>
    /// Class to handle VirtualControlEvents; events driven by IComponents inputs/outputs.
    /// </summary>
    public class VirtualControlEvent
    {
        #region Public Enums

        /// <summary>
        /// The way to pass values to the actors
        /// </summary>
        public enum VirtualControlEventSetMethod
        {
            #region Public Fields

            /// <summary>
            /// Direct pass, no changes to the values
            /// </summary>
            Passthrough

            #endregion Public Fields

            ,

            /// <summary>
            /// Adjusted to compensate for range differences
            /// Example: If you want to run a motor based off of an analog voltage input, it adjusts
            ///          the 0-5 range from the analog voltage to a 0-1 range for the motor
            /// </summary>
            Adjusted
        }

        #endregion Public Enums

        /// <summary>
        /// The type of value to pass to the actors
        /// </summary>
        public enum VirtualControlEventType
        {
            /// <summary>
            /// A numeric or boolean value
            /// </summary>
            Value,

            /// <summary>
            /// The usage state of the component
            /// </summary>
            Usage
        }

        #region Private Fields

        /// <summary>
        /// Defines a list of actors; the IComponents affected by the drivers/event
        /// </summary>
        private readonly List<IComponent> actors = new List<IComponent>();

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="eventType">type of VirtualControlEvent</param>
        /// <param name="setMethod">the SetMethod to use</param>
        /// <param name="drivers">the drivers; IComponents that fire this event</param>
        public VirtualControlEvent(VirtualControlEventType eventType, VirtualControlEventSetMethod setMethod,
            params IComponent[] drivers)
        {
            EventType = eventType;
            SetMethod = setMethod;

            foreach (var driver in drivers)
                driver.ValueChanged += VirtualControlEvent_ValueChanged;
        }

        #endregion Public Constructors

        /// <summary>
        /// Defines the type of VirtualControlEvent this is
        /// </summary>
        public VirtualControlEventType EventType { get; }

        /// <summary>
        /// Defines the type of VirtualControlEventSetMethod to use
        /// </summary>
        public VirtualControlEventSetMethod SetMethod { get; }

        /// <summary>
        /// Adds an IComponents to the actors list
        /// </summary>
        /// <param name="component">IComponent</param>
        public void AddActionComponent(IComponent component)
        {
            actors.Add(component);
        }

        /// <summary>
        /// Adds an array of IComponents to the actors list
        /// </summary>
        /// <param name="components">array of IComponents</param>
        public void AddActionComponents(params IComponent[] components)
        {
            foreach (var component in components)
                actors.Add(component);
        }

        private void VirtualControlEvent_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                var param = e as VirtualControlEventArgs;

                if (param == null)
                    return;

                foreach (var component in actors)
                {
                    var actor = component;

                    if ((EventType == VirtualControlEventType.Value) &&
                        (SetMethod == VirtualControlEventSetMethod.Passthrough))
                    {
                        (actor as Motor)?.Set(param.Value, this);
                        (actor as OutputComponent)?.Set(Math.Abs(param.Value), this);
                        (actor as DoubleSolenoidItem)?.Set(Math.Abs(param.Value), this);
                    }
                    else if ((EventType == VirtualControlEventType.Value) &&
                             (SetMethod == VirtualControlEventSetMethod.Adjusted))
                    {
                        if (sender is AnalogInputItem)
                        {
                            (actor as Motor)?.Set(param.Value/5, this);
                            (actor as OutputComponent)?.Set(Math.Abs(param.Value), this);
                            (actor as DoubleSolenoidItem)?.Set(Math.Abs(param.Value), this);
                        }
                        else
                        {
                            (actor as Motor)?.Set(param.Value, this);
                            (actor as DigitalOutputItem)?.Set(param.Value, this);
                            (actor as AnalogOutputItem)?.Set(Math.Abs(param.Value)*5, this);
                            (actor as DoubleSolenoidItem)?.Set(Math.Abs(param.Value), this);
                        }
                    }
                    else if ((EventType == VirtualControlEventType.Usage) &&
                             (SetMethod == VirtualControlEventSetMethod.Passthrough))
                    {
                        (actor as Motor)?.Set(Convert.ToDouble(param.InUse), this);
                        (actor as OutputComponent)?.Set(param.InUse, this);
                        (actor as DoubleSolenoidItem)?.Set(param.InUse, this);
                    }
                    else
                    {
                        (actor as Motor)?.Set(Convert.ToDouble(param.InUse), this);
                        (actor as OutputComponent)?.Set(param.InUse, this);
                        (actor as DoubleSolenoidItem)?.Set(param.InUse, this);
                    }
                }
            }
            catch (Exception ex)
            {
                Report.Error(ex.Message);
                Log.Write(ex);
            }
        }
    }

    /// <summary>
    /// EventArgs for the EventHandler of IComponents
    /// </summary>
    public class VirtualControlEventArgs : EventArgs
    {
        #region Public Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="value">double value to pass to actors during the event</param>
        /// <param name="inUse">InUse value to pass to actors during the event</param>
        public VirtualControlEventArgs(double value, bool inUse)
        {
            InUse = inUse;
            Value = value;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Defines the InUse value to pass to actors during the event
        /// </summary>
        public bool InUse { get; }

        /// <summary>
        /// Defines the double value to pass to actors during the event
        /// </summary>
        public double Value { get; }

        #endregion Public Properties
    }
}