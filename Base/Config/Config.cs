﻿/****************************** Header ******************************\
Class Name: Config [singleton]
Summary: Manages and loads the configuration file from XML.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoftware.co
\********************************************************************/

using Base.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using WPILib;
using static Base.Motor;
using static Base.Schemas;

namespace Base
{
    /// <summary>
    /// Manages and loads the configuration file from XML.
    /// </summary>
    public sealed class Config : IDisposable
    {
        #region Public Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Config()
        {
            ActiveCollection = new ActiveCollection();
            //ActiveCollection = ActiveCollection.Instance;
        }

        #endregion Public Constructors

        /// <summary>
        /// Disposes of this IComponent and its managed resources
        /// </summary>
        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases managed and native resources
        /// </summary>
        /// <param name="disposing"></param>
        private void dispose(bool disposing)
        {
            if (!disposing) return;
            lock (ActiveCollection)
            {
                ActiveCollection?.Dispose();
            }
        }
        #region Public Methods

        /// <summary>
        /// Loads the config.
        /// </summary>
        /// <param name="fileName">name of config on disk</param>
        public void Load(string fileName)
        {
            try
            {
                doc = XDocument.Load(fileName);
                load();
            }
            catch (Exception ex)
            {
                Log.Write(ex);
            }
        }

        #endregion Public Methods

        #region Private Fields

        private readonly List<CommonName> componentNames = new List<CommonName>();
        private XDocument doc;

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Instance of the ActiveCollection to be used thoughout the program.
        /// </summary>
        public ActiveCollection ActiveCollection { get; }

        /// <summary>
        /// Boolean flag to determin if autonomous should be enabled in any sence.
        /// </summary>
        public bool AutonEnabled { get; private set; }

        /// <summary>
        /// Instance of the driver's control schema to be used thoughout the program.
        /// </summary>
        public DriverConfig DriverConfig { get; private set; }

        /// <summary>
        /// Instance of the operators's control schema to be used thoughout the program.
        /// </summary>
        public OperatorConfig OperatorConfig { get; private set; }

        /// <summary>
        /// Boolean flag to set QuickLoad mode, see reference manule for details.
        /// </summary>
        public bool QuickLoad { get; private set; }

        #endregion Public Properties

        #region Private Methods

        /// <summary>
        /// Defines if all exception messages should be output to the console in addition to the log.
        /// </summary>
        public bool VerboseOutput { get; private set; }

        private void allocateComponents()
        {
            #region channel asignments

            if (QuickLoad)
                ActiveCollection.ReleaseActiveCollection();

            QuickLoad = Convert.ToBoolean(getAttributeValue("value", "QuickLoad"));
            if (QuickLoad) Report.Warning("I see QuickLoad is turned... This should only be used during practice!");

            //TODO: allow different ports to be used when initializing NavX
            if (Convert.ToBoolean(getAttributeValue("value", "UseNavX")))
                ActiveCollection.AddComponent(NavX.InitializeNavX(SPI.Port.MXP));

            #region Encoders

            try
            {
                foreach (var element in getElements("RobotConfig", "Encoders"))
                {
                    try
                    {
                        componentNames.Add(new CommonName(element.Name.ToString()));
                        Report.General(
                            $"Added Encoder {element.Name}, aChannel = {Convert.ToInt32(element.Attribute("aChannel").Value)}, bChannel = {Convert.ToInt32(element.Attribute("bChannel").Value)}");
                        ActiveCollection.AddComponent(
                            new EncoderItem(element.Name.ToString(), Convert.ToInt32(element.Attribute("aChannel").Value), Convert.ToInt32(element.Attribute("bChannel").Value)));
                    }
                    catch (Exception ex)
                    {
                        Report.Error(
                            $"Failed to load Encoder {element?.Name}. This may cause a fatal runtime error! See log for details.");
                        Log.Write(ex);
                        if (VerboseOutput)
                            Report.Error(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Report.Error(
                    "There was an error loading one or more encoders. This may cause a fatal runtime error! See log for details.");
                Log.Write(ex);
                if (VerboseOutput)
                    Report.Error(ex.Message);
            }

            #endregion Encoders

            #region DI

            try
            {
                foreach (var element in getElements("RobotConfig", "DI"))
                    try
                    {
                        componentNames.Add(new CommonName(element.Name.ToString()));
                        Report.General(
                            $"Added Digital Input {element.Name}, channel {Convert.ToInt32(element.Attribute("channel").Value)}");
                        ActiveCollection.AddComponent(
                            new DigitalInputItem(Convert.ToInt32(element.Attribute("channel").Value),
                                element.Name.ToString()));
                    }
                    catch (Exception ex)
                    {
                        Report.Error(
                            $"Failed to load DigitalInput {element?.Name}. This may cause a fatal runtime error! See log for details.");
                        Log.Write(ex);
                        if (VerboseOutput)
                            Report.Error(ex.Message);
                    }
            }
            catch (Exception ex)
            {
                Report.Error(
                    "There was an error loading one or more digital inputs. This may cause a fatal runtime error! See log for details.");
                Log.Write(ex);
                if (VerboseOutput)
                    Report.Error(ex.Message);
            }

            #endregion DI

            #region DO

            try
            {
                foreach (var element in getElements("RobotConfig", "DO"))
                    try
                    {
                        componentNames.Add(new CommonName(element.Name.ToString()));
                        Report.General(
                            $"Added Digital Output {element.Name}, channel {Convert.ToInt32(element.Attribute("channel").Value)}");
                        ActiveCollection.AddComponent(
                            new DigitalOutputItem(Convert.ToInt32(element.Attribute("channel").Value),
                                element.Name.ToString()));
                    }
                    catch (Exception ex)
                    {
                        Report.Error(
                            $"Failed to load DigitalOutput {element?.Name}. This may cause a fatal runtime error! See log for details.");
                        Log.Write(ex);
                        if (VerboseOutput)
                            Report.Error(ex.Message);
                    }
            }
            catch (Exception ex)
            {
                Report.Error(
                    "There was an error loading one or more digital outputs. This may cause a fatal runtime error! See log for details.");
                Log.Write(ex);
                if (VerboseOutput)
                    Report.Error(ex.Message);
            }

            #endregion DO

            #region AI

            try
            {
                foreach (var element in getElements("RobotConfig", "AI"))
                    try
                    {
                        componentNames.Add(new CommonName(element.Name.ToString()));
                        Report.General(
                            $"Added Analog Input {element.Name}, channel {Convert.ToInt32(element.Attribute("channel").Value)}");
                        ActiveCollection.AddComponent(
                            new AnalogInputItem(Convert.ToInt32(element.Attribute("channel").Value),
                                element.Name.ToString()));
                    }
                    catch (Exception ex)
                    {
                        Report.Error(
                            $"Failed to load AnalogInput {element?.Name}. This may cause a fatal runtime error! See log for details.");
                        Log.Write(ex);
                        if (VerboseOutput)
                            Report.Error(ex.Message);
                    }
            }
            catch (Exception ex)
            {
                Report.Error(
                    "There was an error loading one or more analog inputs. This may cause a fatal runtime error! See log for details.");
                Log.Write(ex);
                if (VerboseOutput)
                    Report.Error(ex.Message);
            }

            #endregion AI

            #region AO

            try
            {
                foreach (var element in getElements("RobotConfig", "AO"))
                    try
                    {
                        componentNames.Add(new CommonName(element.Name.ToString()));
                        Report.General(
                            $"Added Analog Output {element.Name}, channel {Convert.ToInt32(element.Attribute("channel").Value)}");
                        ActiveCollection.AddComponent(
                            new AnalogOutputItem(Convert.ToInt32(element.Attribute("channel").Value),
                                element.Name.ToString()));
                    }
                    catch (Exception ex)
                    {
                        Report.Error(
                            $"Failed to load AnalogOutput {element?.Name}. This may cause a fatal runtime error! See log for details.");
                        Log.Write(ex);
                        if (VerboseOutput)
                            Report.Error(ex.Message);
                    }
            }
            catch (Exception ex)
            {
                Report.Error(
                    "There was an error loading one or more analog outputs. This may cause a fatal runtime error! See log for details.");
                Log.Write(ex);
                if (VerboseOutput)
                    Report.Error(ex.Message);
            }

            #endregion AO

            #region Victors

            try
            {
                foreach (var element in getElements("RobotConfig", "Victors"))
                    try
                    {
                        var type = element.Attribute("type").Value;

                        var t = VictorType.Sp;
                        if (type == "888")
                            t = VictorType.EightEightEight;

                        DigitalInputItem upperLimit = null;
                        DigitalInputItem lowerLimit = null;
                        if (element.Attribute("upperLimit") != null)
                            upperLimit =
                                (DigitalInputItem)
                                    ActiveCollection.Get(toBindCommonName(element.Attribute("upperLimit"))[0]);
                        if (element.Attribute("lowerLimit") != null)
                            lowerLimit =
                                (DigitalInputItem)
                                    ActiveCollection.Get(toBindCommonName(element.Attribute("lowerLimit"))[0]);
                        EncoderItem motorEncoder = null;
                        if (element.Attribute("encoder") != null)
                        {
                            motorEncoder =
                                (EncoderItem)
                                    ActiveCollection.Get(toBindCommonName(element.Attribute("encoder"))[0]);
                        }

                        componentNames.Add(new CommonName(element.Name.ToString()));
                        Report.General(
                            $"Added Victor{type} {element.Name}, channel {Convert.ToInt32(element.Attribute("channel").Value)}, is reversed = {Convert.ToBoolean(element.Attribute("reversed").Value)}");
                        if (!Convert.ToBoolean(element.Attribute("drive").Value))
                        {
                            var temp = new VictorItem(t, Convert.ToInt32(element.Attribute("channel").Value),
                                    element.Name.ToString(), Convert.ToBoolean(element.Attribute("reversed").Value));

                            ActiveCollection.AddComponent(temp);
                            temp.setUpperLimit(upperLimit);
                            temp.setLowerLimit(lowerLimit);
                            temp.setEncoder(motorEncoder);

                        }
                        else
                            switch (element.Attribute("side").Value)
                            {
                                case "right":
                                    var temp =
                                        new VictorItem(t, Convert.ToInt32(element.Attribute("channel").Value),
                                            element.Name.ToString(), Side.Right,
                                            Convert.ToBoolean(element.Attribute("reversed").Value));

                                    ActiveCollection.AddComponent(temp);
                                    temp.setUpperLimit(upperLimit);
                                    temp.setLowerLimit(lowerLimit);
                                    temp.setEncoder(motorEncoder);

                                    break;

                                case "left":
                                    temp =
                                       new VictorItem(t, Convert.ToInt32(element.Attribute("channel").Value),
                                           element.Name.ToString(), Side.Left,
                                           Convert.ToBoolean(element.Attribute("reversed").Value));

                                    ActiveCollection.AddComponent(temp);
                                    temp.setUpperLimit(upperLimit);
                                    temp.setLowerLimit(lowerLimit);
                                    temp.setEncoder(motorEncoder);
                                    break;
                            }
                    }
                    catch (Exception ex)
                    {
                        Report.Error(
                            $"Failed to load Victor {element?.Name}. This may cause a fatal runtime error! See log for details.");
                        Log.Write(ex);
                        if (VerboseOutput)
                            Report.Error(ex.Message);
                    }
            }
            catch (Exception ex)
            {
                Report.Error(
                    "There was an error loading one or more victors. This may cause a fatal runtime error! See log for details.");
                Log.Write(ex);
                if (VerboseOutput)
                    Report.Error(ex.Message);
            }

            #endregion Victors

            #region Talons

            try
            {
                foreach (var element in getElements("RobotConfig", "Talons"))
                    try
                    {
                        DigitalInputItem upperLimit = null;
                        DigitalInputItem lowerLimit = null;
                        if (element.Attribute("upperLimit") != null)
                            upperLimit =
                                (DigitalInputItem)
                                    ActiveCollection.Get(toBindCommonName(element.Attribute("upperLimit"))[0]);
                        if (element.Attribute("lowerLimit") != null)
                            lowerLimit =
                                (DigitalInputItem)
                                    ActiveCollection.Get(toBindCommonName(element.Attribute("lowerLimit"))[0]);
                        EncoderItem motorEncoder = null;
                        if (element.Attribute("encoder") != null)
                        {
                            motorEncoder =
                                (EncoderItem)
                                    ActiveCollection.Get(toBindCommonName(element.Attribute("encoder"))[0]);
                        }

                        componentNames.Add(new CommonName(element.Name.ToString()));
                        Report.General(
                            $"Added Talon {element.Name}, channel {Convert.ToInt32(element.Attribute("channel").Value)}, is reversed = {Convert.ToBoolean(element.Attribute("reversed").Value)}");

                        if (element.Attribute("type").Value == "pwm")
                        {
                            var temp = new CanTalonItem(Convert.ToInt32(element.Attribute("channel").Value), element.Name.ToString());
                            ActiveCollection.AddComponent(temp);
                            temp.setUpperLimit(upperLimit);
                            temp.setLowerLimit(lowerLimit);
                            temp.setEncoder(motorEncoder);
                        }
                        else
                            switch (element.Attribute("type").Value)
                            {
                                case "master":
                                    double p = .5, i = .001, d = 0;

                                    try
                                    {
                                        p = Convert.ToDouble(element.Attribute("p").Value);
                                    }
                                    catch
                                    {
                                        Report.Warning($"Failed to set P for {element.Name}");
                                    }
                                    try
                                    {
                                        i = Convert.ToDouble(element.Attribute("i").Value);
                                    }
                                    catch
                                    {
                                        Report.Warning($"Failed to set I for {element.Name}");
                                    }
                                    try
                                    {
                                        d = Convert.ToDouble(element.Attribute("d").Value);
                                    }
                                    catch
                                    {
                                        Report.Warning($"Failed to set D for {element.Name}");
                                    }


                                    var temp = new CanTalonItem(Convert.ToInt32(element.Attribute("channel").Value),
                                            element.Name.ToString(), p, i, d,
                                            Convert.ToBoolean(element.Attribute("reversed").Value));
                                    ActiveCollection.AddComponent(temp);
                                    temp.setUpperLimit(upperLimit);
                                    temp.setLowerLimit(lowerLimit);
                                    temp.setEncoder(motorEncoder);
                                    Report.General($"{element.Name} is a master with PID set to {p}, {i}, {d}");
                                    break;

                                case "slave":
                                    try
                                    {
                                        var master = element.Attribute("master").Value;

                                        ActiveCollection.AddComponent(
                                            new CanTalonItem(
                                                Convert.ToInt32(element.Attribute("channel").Value),
                                                element.Name.ToString(),
                                                (CanTalonItem)ActiveCollection.Get(new CommonName(master)),
                                                Convert.ToBoolean(element.Attribute("reversed").Value)));
                                        Report.General($"{element.Name} is a slave whose master is {master}");
                                    }
                                    catch (Exception ex)
                                    {
                                        Report.Error(
                                            "Error binding slave CANTalon to its master, check spelling in the config.");
                                        Log.Write(ex);
                                    }
                                    break;
                            }
                    }
                    catch (Exception ex)
                    {
                        Report.Error(
                            $"Failed to load Talon {element?.Name}. This may cause a fatal runtime error! See log for details.");
                        Log.Write(ex);
                        if (VerboseOutput)
                            Report.Error(ex.Message);
                    }
            }
            catch (Exception ex)
            {
                Report.Error(
                    "There was an error loading one or more talons. This may cause a fatal runtime error! See log for details.");
                Log.Write(ex);
                if (VerboseOutput)
                    Report.Error(ex.Message);
            }

            #endregion Talons

            #region DoubleSolenoids

            try
            {
                foreach (var element in getElements("RobotConfig", "Solenoids"))
                    try
                    {
                        var _default = element.Attribute("default")?.Value;

                        var d = DoubleSolenoid.Value.Off;
                        if (_default == "forward")
                            d = DoubleSolenoid.Value.Forward;
                        else if (_default == "reverse")
                            d = DoubleSolenoid.Value.Reverse;

                        componentNames.Add(new CommonName(element.Name.ToString()));
                        Report.General(
                            $"Added Double Solenoid {element.Name}, forward channel {Convert.ToInt32(element.Attribute("forward").Value)}, reverse channel {Convert.ToInt32(element.Attribute("reverse").Value)}, default position = {d.ToString()}, is reversed = {Convert.ToBoolean(element.Attribute("reversed").Value)}");
                        ActiveCollection.AddComponent(
                            new DoubleSolenoidItem(element.Name.ToString(),
                                Convert.ToInt32(element.Attribute("forward").Value),
                                Convert.ToInt32(element.Attribute("reverse").Value), d,
                                Convert.ToBoolean(element.Attribute("reversed").Value)));
                    }
                    catch (Exception ex)
                    {
                        Report.Error(
                            $"Failed to load DoubleSolenoid {element?.Name}. This may cause a fatal runtime error! See log for details.");
                        Log.Write(ex);
                        if (VerboseOutput)
                            Report.Error(ex.Message);
                    }
            }
            catch (Exception ex)
            {
                Report.Error(
                    "There was an error loading one or more DoubleSolenoids. This may cause a fatal runtime error! See log for details.");
                Log.Write(ex);
                if (VerboseOutput)
                    Report.Error(ex.Message);
            }

            #endregion DoubleSolenoids

            #region Relays

            try
            {
                foreach (var element in getElements("RobotConfig", "Relays"))
                    try
                    {
                        componentNames.Add(new CommonName(element.Name.ToString()));

                        var _default = element.Attribute("default")?.Value;
                        var d = Relay.Value.Off;
                        if (_default == "on")
                            d = Relay.Value.On;
                        else if (_default == "forward")
                            d = Relay.Value.Forward;
                        else if (_default == "reverse")
                            d = Relay.Value.Reverse;

                        Report.General(
                            $"Added Relay {element.Name}, channel {Convert.ToInt32(element.Attribute("channel").Value)}, default position {d.ToString()}");
                        ActiveCollection.AddComponent(
                            new RelayItem(Convert.ToInt32(element.Attribute("channel").Value),
                                element.Name.ToString(), d));
                    }
                    catch (Exception ex)
                    {
                        Report.Error(
                            $"Failed to load Relay {element?.Name}. This may cause a fatal runtime error! See log for details.");
                        Log.Write(ex);
                        if (VerboseOutput)
                            Report.Error(ex.Message);
                    }
            }
            catch (Exception ex)
            {
                Report.Error(
                    "There was an error loading one or more relays. This may cause a fatal runtime error! See log for details.");
                Log.Write(ex);
                if (VerboseOutput)
                    Report.Error(ex.Message);
            }


            #endregion Relays

            #region Potentiometers

            try
            {
                foreach (var element in getElements("RobotConfig","Potentiometers"))
                    try
                    {
                        componentNames.Add(new CommonName(element.Name.ToString()));

                        Report.General(
                            $"Added Potentiometer {element.Name}, channel {Convert.ToInt32(element.Attribute("channel").Value)}" );
                        ActiveCollection.AddComponent(
                            new PotentiometerItem(Convert.ToInt32(element.Attribute("channel").Value), element.Name.ToString()));
                    }
                    catch (Exception ex)
                    {
                        Report.Error(
                            $"Failed to load Potentiometer {element?.Name}. This may cause a fatal runtime error! See log for details.");
                        Log.Write(ex);
                        if (VerboseOutput)
                            Report.Error(ex.Message);
                    } 
            }
            catch (Exception ex)
            {
                Report.Error(
                    "There was an error loading one or more potentiometers. This may cause a fatal runtime error! See log for details.");
                Log.Write(ex);
                if (VerboseOutput)
                    Report.Error(ex.Message);
            }

    #endregion Potentiometers

    #endregion channel asignments
}

        private void constructVirtualControlEvents()
        {
            try
            {
                foreach (var element in getElements("VirtualControlEvents"))
                    try
                    {
                        var type = VirtualControlEvent.VirtualControlEventType.Value;
                        var setMethod = VirtualControlEvent.VirtualControlEventSetMethod.Passthrough;

                        switch (element.Attribute("type")?.Value)
                        {
                            case "value":
                                type = VirtualControlEvent.VirtualControlEventType.Value;
                                break;

                            case "usage":
                                type = VirtualControlEvent.VirtualControlEventType.Usage;
                                break;
                        }

                        switch (element.Attribute("setMethod")?.Value)
                        {
                            case "passthrough":
                                setMethod = VirtualControlEvent.VirtualControlEventSetMethod.Passthrough;
                                break;

                            case "adjusted":
                                setMethod = VirtualControlEvent.VirtualControlEventSetMethod.Adjusted;
                                break;
                        }

                        var enInAuton = Convert.ToBoolean(element.Attribute("auton")?.Value);
                        var enInTeleop = Convert.ToBoolean(element.Attribute("teleop")?.Value);
                        var drivers = toBindCommonName(element.Attribute("drivers"));
                        var actors = toBindCommonName(element.Attribute("actions"));

                        var tmp = new VirtualControlEvent(this, type, setMethod, enInAuton, enInTeleop,
                            drivers.Select(driver => ActiveCollection.Get(driver)).ToArray());
                        tmp.AddActionComponents(actors.Select(actor => ActiveCollection.Get(actor)).ToArray());
                    }
                    catch (Exception ex)
                    {
                        Report.Error(
                            $"There was an error creating the VirtualControlEvent {element.Name}, see log for details.");
                        Log.Write(ex);
                        if (VerboseOutput)
                            Report.Error(ex.Message);
                    }
            }
            catch (Exception ex)
            {
                Report.Error("There was an error loading one or more Events from the config.");
                Log.Write(ex);
                if (VerboseOutput)
                    Report.Error(ex.Message);
            }
        }

        /// <summary>
        /// Returns the attribute of an XElement.
        /// </summary>
        /// <param name="attribute">The attribute from which to obtain the value.</param>
        /// <param name="elements">Name of elements to navigate.</param>
        /// <returns>The value from the attribute as a string.</returns>
        private XAttribute getAttribute(string attribute, params string[] elements)
        {
            try
            {
                return getNode(elements).Attribute(attribute);
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                Report.Error($"There was an error obtaining the attribute '{attribute}'. See log for details.");
                if (VerboseOutput)
                    Report.Error(ex.Message);
            }

            return null;
        }

        /// <summary>
        /// Returns the attribute value of an XElement.
        /// </summary>
        /// <param name="attribute">The attribute from which to obtain the value.</param>
        /// <param name="elements">Name of elements to navigate.</param>
        /// <returns>The value from the attribute as a string.</returns>
        private string getAttributeValue(string attribute, params string[] elements)
        {
            try
            {
                return getNode(elements).Attribute(attribute).Value;
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                Report.Error(
                    $"There was an error obtaining the value from the attribute '{attribute}'. See log for details.");
                if (VerboseOutput)
                    Report.Error(ex.Message);
            }

            return null;
        }

        /// <summary>
        /// Returns the last nodes Elements from a path of XElements.
        /// </summary>
        /// <param name="elements">Name of elements to navigate.</param>
        /// <returns></returns>
        private IEnumerable<XElement> getElements(params string[] elements) => getNode(elements).Elements();

        /// <summary>
        /// Returns the last nodes from a path of XElements.
        /// </summary>
        /// <param name="elements">Name of elements to navigate.</param>
        /// <returns></returns>
        private XElement getNode(params string[] elements)
        {
            var node = doc.Root;

            try
            {
                node = elements.Aggregate(node, (current, value) => current.Element(value));
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                Report.Error(
                    $"There was an error navigating to the specified node '{elements[elements.Length]}'. See log for details.");
                if (VerboseOutput)
                    Report.Error(ex.Message);
            }
            return node;
        }

        /// <summary>
        /// Loads all relevant attributes and values from the config file.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        private void load()
        {
            try
            {
                Report.General($"Version: {Convert.ToDouble(getAttributeValue("version", "Version"))}");
                Report.General($"Comment: {getAttributeValue("comment", "Comment")}");
                VerboseOutput = Convert.ToBoolean(getAttributeValue("value", "VerboseOutput"));
                if (VerboseOutput)
                    Report.General("Verbose error reporting enabled.");
                AutonEnabled = Convert.ToBoolean(getAttributeValue("value", "EnableAuton"));
                Report.General($"Auton Enabled: {AutonEnabled}");
                allocateComponents();
                retrieveDriverSchema();
                retrieveOperatorSchema();
                constructVirtualControlEvents();
            }
            catch (Exception ex)
            {
                Report.Error("The config file could not be found or there was an error reading it. See log for details.");
                Log.Write(ex);
                if (VerboseOutput)
                    Report.Error(ex.Message);
            }
        }

        private void retrieveDriverSchema()
        {
            #region driver control schema

            //drive controls are not dynamic, they will always be axis based controls.
            try
            {
                var controllerSlot = Convert.ToInt32(getAttributeValue("controllerSlot", "Controls", "Driver", "slot"));
                var driveFit = Convert.ToInt32(getAttributeValue("driveFit", "Controls", "Driver", "drive"));
                var driveFitPower = Convert.ToDouble(getAttributeValue("power", "Controls", "Driver", "drive"));
                var multiplier =
                    Convert.ToDouble(getAttributeValue("powerMultiplier", "Controls", "Driver", "powerMultiplier"));

                var leftAxis = Convert.ToInt32(getAttributeValue("axis", "Controls", "Driver", "leftDrive"));
                var rightAxis = Convert.ToInt32(getAttributeValue("axis", "Controls", "Driver", "rightDrive"));
                var leftReversed = Convert.ToBoolean(getAttributeValue("reversed", "Controls", "Driver", "leftDrive"));
                var rightReversed = Convert.ToBoolean(getAttributeValue("reversed", "Controls", "Driver", "rightDrive"));

                double leftDz = 0;
                double rightDz = 0;

                try
                {
                    leftDz = Convert.ToDouble(getAttributeValue("deadZone", "Controls", "Driver", "leftDrive"));
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    if (VerboseOutput)
                        Report.Error(ex.Message);
                }
                try
                {
                    rightDz = Convert.ToDouble(getAttributeValue("deadZone", "Controls", "Driver", "rightDrive"));
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    if (VerboseOutput)
                        Report.Error(ex.Message);
                }

                var left = new DriverControlSchema("leftDrive", (MotorControlFitFunction) driveFit, driveFitPower,
                    toBindCommonName(getAttribute("bindTo", "Controls", "Driver", "leftDrive")), leftAxis, leftDz,
                    multiplier, leftReversed);

                var right = new DriverControlSchema("rightDrive", (MotorControlFitFunction) driveFit, driveFitPower,
                    toBindCommonName(getAttribute("bindTo", "Controls", "Driver", "rightDrive")), rightAxis, rightDz,
                    multiplier, rightReversed);

                var temp = new List<ControlSchema>();
                foreach (var element in getElements("Controls", "DriverAux"))
                {
                    bool reversed;
                    double powerMultiplier;
                    try
                    {
                        powerMultiplier = Convert.ToDouble(element.Attribute("powerMultiplier").Value);
                    }
                    catch
                    {
                        powerMultiplier = 1;
                    }

                    try
                    {
                        reversed = Convert.ToBoolean(element.Attribute("reversed").Value);
                    }
                    catch
                    {
                        reversed = false;
                    }

                    switch (ControlSchema.GetControlTypeFromAttribute(element.Attribute("type")))
                    {
                        case ControlType.Axis:
                            double deadZone = 0;
                            try
                            {
                                deadZone = Convert.ToDouble(element.Attribute("deadZone").Value);
                            }
                            catch (Exception ex)
                            {
                                Log.Write(ex);
                                if (VerboseOutput)
                                    Report.Error(ex.Message);
                            }
                            temp.Add(new ControlSchema(element.Name.ToString(), ControlType.Axis,
                                toBindCommonName(element.Attribute("bindTo")),
                                Convert.ToInt32(element.Attribute("axis").Value), deadZone,
                                powerMultiplier, reversed));
                            break;

                        case ControlType.Button:
                            temp.Add(new ControlSchema(element.Name.ToString(), ControlType.Button,
                                toBindCommonName(element.Attribute("bindTo")),
                                Convert.ToInt32(element.Attribute("button").Value), powerMultiplier, reversed));
                            break;

                        case ControlType.DualButton:
                            temp.Add(new ControlSchema(element.Name.ToString(), ControlType.DualButton,
                                toBindCommonName(element.Attribute("bindTo")),
                                Convert.ToInt32(element.Attribute("buttonA").Value),
                                Convert.ToInt32(element.Attribute("buttonB").Value),
                                powerMultiplier, reversed));
                            break;

                        case ControlType.ToggleButton:
                            temp.Add(new ControlSchema(element.Name.ToString(), ControlType.ToggleButton,
                                toBindCommonName(element.Attribute("bindTo")),
                                Convert.ToInt32(element.Attribute("button").Value), powerMultiplier, reversed));
                            break;
                    }
                }

                DriverConfig = new DriverConfig(new Joystick(controllerSlot), left, right, temp); //add driver stuff
            }
            catch (Exception ex)
            {
                Report.Error("There was an error loading the driver config. This may cause fatal runtime error!");
                Log.Write(ex);
                if (VerboseOutput)
                    Report.Error(ex.Message);
            }

            #endregion driver control schema
        }

        private void retrieveOperatorSchema()
        {
            #region operator control schema

            try
            {
                var temp = new List<ControlSchema>();
                foreach (var element in getElements("Controls", "Operator").Where(element => element.Name != "slot"))
                {
                    bool reversed;
                    double powerMultiplier;
                    try
                    {
                        powerMultiplier = Convert.ToDouble(element.Attribute("powerMultiplier").Value);
                    }
                    catch
                    {
                        powerMultiplier = 1;
                    }

                    try
                    {
                        reversed = Convert.ToBoolean(element.Attribute("reversed").Value);
                    }
                    catch
                    {
                        reversed = false;
                    }

                    switch (ControlSchema.GetControlTypeFromAttribute(element.Attribute("type")))
                    {
                        case ControlType.Axis:
                            double deadZone = 0;
                            try
                            {
                                deadZone = Convert.ToDouble(element.Attribute("deadZone").Value);
                            }
                            catch (Exception ex)
                            {
                                Log.Write(ex);
                                if (VerboseOutput)
                                    Report.Error(ex.Message);
                            }
                            temp.Add(new ControlSchema(element.Name.ToString(), ControlType.Axis,
                                toBindCommonName(element.Attribute("bindTo")),
                                Convert.ToInt32(element.Attribute("axis").Value), deadZone,
                                powerMultiplier, reversed));
                            break;

                        case ControlType.Button:
                            temp.Add(new ControlSchema(element.Name.ToString(), ControlType.Button,
                                toBindCommonName(element.Attribute("bindTo")),
                                Convert.ToInt32(element.Attribute("button").Value), powerMultiplier, reversed));
                            break;

                        case ControlType.DualButton:
                            temp.Add(new ControlSchema(element.Name.ToString(), ControlType.DualButton,
                                toBindCommonName(element.Attribute("bindTo")),
                                Convert.ToInt32(element.Attribute("buttonA").Value),
                                Convert.ToInt32(element.Attribute("buttonB").Value),
                                powerMultiplier, reversed));
                            break;

                        case ControlType.ToggleButton:
                            temp.Add(new ControlSchema(element.Name.ToString(), ControlType.ToggleButton,
                                toBindCommonName(element.Attribute("bindTo")),
                                Convert.ToInt32(element.Attribute("button").Value), powerMultiplier, reversed));
                            break;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                #region Warning checkes

                try
                {
                    var check = new List<CommonName>();
                    foreach (var c in temp)
                        check.AddRange(c.Bindings);

                    if (DriverConfig.ControlsData != null)
                        foreach (var c in DriverConfig.ControlsData)
                            check.AddRange(c.Bindings);

                    check.AddRange(DriverConfig.LeftDriveControlSchema.Bindings);
                    check.AddRange(DriverConfig.RightDriveControlSchema.Bindings);

                    var duplicateKeys =
                        check.GroupBy(x => x).Where(group => group.Count() > 1).Select(group => group.Key);

                    var errorCheck = false;
                    foreach (var component in duplicateKeys)
                    {
                        Report.Warning(
                            $"\"{component}\" was bound to more than one control, was this intentional?");
                        Log.Str($"WARNING: \"{component}\" was bound to more than once to the same control!");
                        errorCheck = true;
                    }

                    if (errorCheck)
                    {
                        var sameControlDuplicates = new List<CommonName>();
                        var sameControlDuplicatesCorrected = new List<CommonName>();
                        foreach (var c in temp)
                        {
                            var bindings =
                                c.Bindings.GroupBy(x => x).Where(group => group.Count() > 1).Select(group => group.Key);
                            var commonNames = bindings as IList<CommonName> ?? bindings.ToList();
                            sameControlDuplicates.AddRange(commonNames);

                            foreach (var cname in commonNames)
                            {
                                var count = c.Bindings.Count(item => item == cname);
                                for (var i = 1; i < count; i++)
                                    c.Bindings.Remove(cname);

                                sameControlDuplicatesCorrected.Add(cname);
                            }
                        }

                        if (DriverConfig.ControlsData != null)
                            foreach (var c in DriverConfig.ControlsData)
                            {
                                var bindings =
                                    c.Bindings.GroupBy(x => x)
                                        .Where(group => group.Count() > 1)
                                        .Select(group => group.Key);
                                var commonNames = bindings as IList<CommonName> ?? bindings.ToList();
                                sameControlDuplicates.AddRange(commonNames);

                                foreach (var cname in commonNames)
                                {
                                    var count = c.Bindings.Count(item => item == cname);
                                    for (var i = 1; i < count; i++)
                                        c.Bindings.Remove(cname);

                                    sameControlDuplicatesCorrected.Add(cname);
                                }
                            }

                        foreach (var component in sameControlDuplicates)
                        {
                            Report.Error(
                                $"\"{component}\" was bound more than once to the same control!\nCHECK BELOW to see if it was resolved.");
                            Log.Str($"ERROR: \"{component}\" was bound to more than once to the same control!");
                        }

                        foreach (var component in sameControlDuplicatesCorrected)
                        {
                            Report.General(
                                $"The \"{component}\" binding error was self corrected (resolved), but you should still fix the config to avoid confusion!");
                            Log.Str(
                                $"The \"{component}\" binding error was self corrected (resolved), but you should still fix the config to avoid confusion!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                    Report.Warning(
                        "There was an error running the validity check for all controls. See log for details.");
                    if (VerboseOutput)
                        Report.Error(ex.Message);
                }

                #endregion Warning checkes

                OperatorConfig =
                    new OperatorConfig(
                        new Joystick(Convert.ToInt32(getAttributeValue("controllerSlot", "Controls", "Operator", "slot"))),
                        temp);
            }
            catch (Exception ex)
            {
                Report.Error("There was an error loading the operator config. This may cause fatal runtime error!");
                Log.Write(ex);
                if (VerboseOutput)
                    Report.Error(ex.Message);
            }

            #endregion operator control schema
        }

        private List<CommonName> toBindCommonName(XAttribute attribute)
        {
            var bindings = new List<CommonName>();

            var values = attribute.Value.Contains(",")
                ? attribute.Value.Split(',').ToList()
                : new List<string> {attribute.Value};

            values = values.Distinct().ToList();

            for (var i = 0; i < values.Count; i++)
                values[i] = values[i].Replace(" ", string.Empty);

            var iterations = values.Count;

            for (var i = 0; i < iterations; i++)
                foreach (var t in componentNames)
                {
                    if (t.ToString() == values[0])
                    {
                        bindings.Add(t);
                        values.Remove(values[0]);
                        break;
                    }

                    if (!values.Any())
                        return bindings;
                }

            foreach (var str in values)
                Report.Error(
                    $"Control attempting to bind to the resource \"{str}\" that has no CommonName, or doesn't exist!");

            return bindings;
        }

        #endregion Private Methods
    }
}