/****************************** Header ******************************\
Class Name: VirtualControlEvent
Summary: Class to handle VirtualControlEvents; events driven by IComponents inputs/outputs
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using Base.Components;
using System;
using System.Collections.Generic;
using WPILib;

namespace Base
{
    /// <summary>
    ///     Class to handle VirtualControlEvents; events driven by IComponents inputs/outputs.
    /// </summary>
    public class VirtualControlEvent
    {
        #region Private Fields

        /// <summary>
        ///     Defines a list of actors; the IComponents affected by the drivers/event
        /// </summary>
        private readonly List<IComponent> actors = new List<IComponent>();

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///     Default constructor
        /// </summary>
        /// <param name="eventType">type of VirtualControlEvent</param>
        /// <param name="setMethod">the SetMethod to use</param>
        /// <param name="drivers">the drivers; IComponents that fire this event</param>
        /// <param name="enabledDuringAuton">determins whether action will be taken during auton</param>
        /// <param name="enabledDuringTeleop">determins whether action will be taken during teleop</param>
        public VirtualControlEvent(VirtualControlEventType eventType,
            VirtualControlEventSetMethod setMethod, bool enabledDuringAuton, bool enabledDuringTeleop,
            params IComponent[] drivers)
        {
            EventType = eventType;
            SetMethod = setMethod;
            EnabledDuringAuton = enabledDuringAuton;
            EnabledDuringTeleop = enabledDuringTeleop;

            /*if ((eventType == VirtualControlEventType.Value) &&
                (drivers.Where(d => d is InputComponent).ToList().Count != 0))
                config.ActiveCollection.AddVirutalControlEventStatusLoop(new VirutalControlEventStatusLoop(drivers));*/

            foreach (var driver in drivers)
                driver.ValueChanged += VirtualControlEvent_ValueChanged;
        }

        #endregion Public Constructors

        #region Private Methods

        private void VirtualControlEvent_ValueChanged(object sender, EventArgs e)
        {
            if ((!EnabledDuringAuton || !LoopCheck._IsAutonomous()) &&
                (!EnabledDuringTeleop || !LoopCheck._IsTeleoporated())) return;
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
                        (actor as RelayItem)?.Set((Relay.Value) Math.Abs(param.Value), this);
                    }
                    else if ((EventType == VirtualControlEventType.Value) &&
                             (SetMethod == VirtualControlEventSetMethod.Adjusted))
                    {
                        if (sender is AnalogInputItem || sender is PotentiometerItem)
                        {
                            (actor as Motor)?.Set(param.Value/5, this);
                            (actor as OutputComponent)?.Set(Math.Abs(param.Value), this);
                            (actor as DoubleSolenoidItem)?.Set(Math.Abs(param.Value), this);
                            (actor as RelayItem)?.Set((Relay.Value) Math.Abs(param.Value), this);
                        }
                        else if (sender is EncoderItem)
                        {
                            var tmp = (EncoderItem) sender;
                            (actor as Motor)?.Set(param.Value/((Encoder) tmp.GetRawComponent()).EncodingScale, this);
                            (actor as OutputComponent)?.Set(
                                Math.Abs(param.Value/((Encoder) tmp.GetRawComponent()).EncodingScale), this);
                            (actor as DoubleSolenoidItem)?.Set(
                                Math.Abs(param.Value/((Encoder) tmp.GetRawComponent()).EncodingScale), this);
                            (actor as RelayItem)?.Set(
                                (Relay.Value) (Math.Abs(param.Value)/((Encoder) tmp.GetRawComponent()).EncodingScale),
                                this);
                        }
                        else
                        {
                            (actor as Motor)?.Set(param.Value, this);
                            (actor as DigitalOutputItem)?.Set(param.Value, this);
                            (actor as AnalogOutputItem)?.Set(Math.Abs(param.Value)*5, this);
                            (actor as DoubleSolenoidItem)?.Set(Math.Abs(param.Value), this);
                            (actor as RelayItem)?.Set((Relay.Value) Math.Abs(param.Value), this);
                        }
                    }
                    else if ((EventType == VirtualControlEventType.Usage) &&
                             (SetMethod == VirtualControlEventSetMethod.Passthrough))
                    {
                        (actor as Motor)?.Set(Convert.ToDouble(param.InUse), this);
                        (actor as OutputComponent)?.Set(param.InUse, this);
                        (actor as DoubleSolenoidItem)?.Set(param.InUse, this);
                        (actor as RelayItem)?.Set((Relay.Value) Convert.ToDouble(param.InUse), this);
                    }
                    else
                    {
                        (actor as Motor)?.Set(Convert.ToDouble(param.InUse), this);
                        (actor as OutputComponent)?.Set(param.InUse, this);
                        (actor as DoubleSolenoidItem)?.Set(param.InUse, this);
                        (actor as RelayItem)?.Set((Relay.Value) Convert.ToDouble(param.InUse), this);
                    }
                }
            }
            catch (Exception ex)
            {
                Report.Error(ex.Message);
                Log.Write(ex);
            }
        }

        #endregion Private Methods

        #region Public Enums

        /// <summary>
        ///     The way to pass values to the actors
        /// </summary>
        public enum VirtualControlEventSetMethod
        {
            #region Public Fields

            /// <summary>
            ///     Direct pass, no changes to the values
            /// </summary>
            Passthrough

            #endregion Public Fields

            ,

            /// <summary>
            ///     Adjusted to compensate for range differences
            ///     Example: If you want to run a motor based off of an analog voltage input, it adjusts
            ///     the 0-5 range from the analog voltage to a 0-1 range for the motor
            /// </summary>
            Adjusted
        }

        /// <summary>
        ///     The type of value to pass to the actors
        /// </summary>
        public enum VirtualControlEventType
        {
            /// <summary>
            ///     A numeric or boolean value
            /// </summary>
            Value,

            /// <summary>
            ///     The usage state of the component
            /// </summary>
            Usage
        }

        #endregion Public Enums

        #region Public Properties

        /// <summary>
        ///     Stores whether this VCE should act during auton
        /// </summary>
        public bool EnabledDuringAuton { get; }

        /// <summary>
        ///     Stores whether this VCE should act during teleop
        /// </summary>
        public bool EnabledDuringTeleop { get; }

        /// <summary>
        ///     Defines the type of VirtualControlEvent this is
        /// </summary>
        public VirtualControlEventType EventType { get; }

        /// <summary>
        ///     Defines the type of VirtualControlEventSetMethod to use
        /// </summary>
        public VirtualControlEventSetMethod SetMethod { get; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///     Adds an IComponents to the actors list
        /// </summary>
        /// <param name="component">IComponent</param>
        public void AddActionComponent(IComponent component)
        {
            actors.Add(component);
        }

        /// <summary>
        ///     Adds an array of IComponents to the actors list
        /// </summary>
        /// <param name="components">array of IComponents</param>
        public void AddActionComponents(params IComponent[] components)
        {
            foreach (var component in components)
                actors.Add(component);
        }

        #endregion Public Methods
    }

    /// <summary>
    ///     EventArgs for the EventHandler of IComponents
    /// </summary>
    public class VirtualControlEventArgs : EventArgs
    {
        #region Public Constructors

        /// <summary>
        ///     Default constructor
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
        ///     Defines the InUse value to pass to actors during the event
        /// </summary>
        public bool InUse { get; }

        /// <summary>
        ///     Defines the double value to pass to actors during the event
        /// </summary>
        public double Value { get; }

        #endregion Public Properties
    }

    /*/// <summary>
    /// Loop to check status of input components (value based) </summary>
    public sealed class VirutalControlEventStatusLoop : ControlLoop
    {
        private readonly List<IComponent> components;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="valueDrivers">all drivers for the event</param>
        public VirutalControlEventStatusLoop(IEnumerable<IComponent> valueDrivers)
        {
            components = valueDrivers.Where(d => d is InputComponent).ToList();
            OverrideCycleTime(.05);
            //StartWhenReady();
        }

        /// <summary>
        /// Method for the implimentor to implement, this is what is called withing the loop
        /// </summary>
        protected override void main()
        {
            foreach (var driver in components)
                ((InputComponent) driver).Get();
        }
    }*/
}