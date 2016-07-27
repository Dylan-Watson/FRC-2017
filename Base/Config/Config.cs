﻿/****************************** Header ******************************\
Class Name: Config [singleton]
Summary: Manages and loads the configuration file from XML.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoftware.co
\********************************************************************/

using static Base.Config.Schemas;
using static Base.Motor;

namespace Base.Config
{
    using Base.Components;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using WPILib;

    /// <summary>
    ///     Manages and loads the configuration file from XML.
    /// </summary>
    public class Config
    {
        #region Public Constructors

        /// <summary>
        ///     Default constructor
        /// </summary>
        public Config()
        {
            ActiveCollection = new ActiveCollection();
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///     Loads the config.
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
        ///     Instance of the ActiveCollection to be used thoughout the program.
        /// </summary>
        public ActiveCollection ActiveCollection { get; }

        /// <summary>
        ///     Boolean flag to determin if autonomous should be enabled in any sence.
        /// </summary>
        public bool AutonEnabled { get; private set; }

        /// <summary>
        ///     Instance of the driver's control schema to be used thoughout the program.
        /// </summary>
        public DriverConfig DriverConfig { get; private set; }

        /// <summary>
        ///     Instance of the operators's control schema to be used thoughout the program.
        /// </summary>
        public OperatorConfig OperatorConfig { get; private set; }

        /// <summary>
        ///     Boolean flag to set QuickLoad mode, see reference manule for details.
        /// </summary>
        public bool QuickLoad { get; private set; }

        #endregion Public Properties

        #region Private Methods

        private void AllocateComponents()
        {
            #region channel asignments

            if (QuickLoad)
            {
                Report.Warning("I see QuickLoad is turned... This should only be used during practice!");
                return;
            }

            QuickLoad = Convert.ToBoolean(GetAttributeValue("value", "QuickLoad"));
            if (QuickLoad) Report.Warning("I see QuickLoad is turned... This should only be used during practice!");

            try
            {
                foreach (var element in GetElements("RobotConfig", "Victors"))
                {
                    var type = element.Attribute("type").Value;

                    var t = VictorType.Sp;
                    if (type == "888")
                        t = VictorType.EightEightEight;

                    componentNames.Add(new CommonName(element.Name.ToString()));
                    Report.General(
                        $"Added Victor{type} {element.Name}, channel {Convert.ToInt32(element.Attribute("channel").Value)}, is reversed = {Convert.ToBoolean(element.Attribute("reversed").Value)}");
                    if (!Convert.ToBoolean(element.Attribute("drive").Value))
                        ActiveCollection.AddComponent(
                            new VictorItem(t, Convert.ToInt32(element.Attribute("channel").Value),
                                element.Name.ToString(), Convert.ToBoolean(element.Attribute("reversed").Value)));
                    else
                        switch (element.Attribute("side").Value)
                        {
                            case "right":
                                ActiveCollection.AddComponent(
                                    new VictorItem(t, Convert.ToInt32(element.Attribute("channel").Value),
                                        element.Name.ToString(), Side.Right,
                                        Convert.ToBoolean(element.Attribute("reversed").Value)));
                                break;

                            case "left":
                                ActiveCollection.AddComponent(
                                    new VictorItem(t, Convert.ToInt32(element.Attribute("channel").Value),
                                        element.Name.ToString(), Side.Left,
                                        Convert.ToBoolean(element.Attribute("reversed").Value)));
                                break;
                        }
                }
            }
            catch (Exception ex)
            {
                Report.Error("There was an error loading one or more victors. This may cause a fatal runtime error! CHECK CONFIG AND LOGS!");
                Log.Write(ex);
            }

            try
            {
                foreach (var element in GetElements("RobotConfig", "Talons"))
                {
                    componentNames.Add(new CommonName(element.Name.ToString()));
                    Report.General(
                        $"Added Talon {element.Name}, channel {Convert.ToInt32(element.Attribute("channel").Value)}, is reversed = {Convert.ToBoolean(element.Attribute("reversed").Value)}");

                    if (element.Attribute("type").Value == "pwm")
                        ActiveCollection.AddComponent(
                            new CanTalonItem(Convert.ToInt32(element.Attribute("channel").Value),
                                element.Name.ToString(), Convert.ToBoolean(element.Attribute("reversed").Value)));
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

                                ActiveCollection.AddComponent(
                                    new CanTalonItem(Convert.ToInt32(element.Attribute("channel").Value),
                                        element.Name.ToString(), p, i, d,
                                        Convert.ToBoolean(element.Attribute("reversed").Value)));
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
            }
            catch (Exception ex)
            {
                Report.Error("There was an error loading one or more talons. This may cause a fatal runtime error! CHECK CONFIG AND LOGS!");
                Log.Write(ex);
            }

            #endregion channel asignments
        }

        /// <summary>
        ///     Returns the attribute of an XElement.
        /// </summary>
        /// <param name="attribute">The attribute from which to obtain the value.</param>
        /// <param name="elements">Name of elements to navigate.</param>
        /// <returns>The value from the attribute as a string.</returns>
        private XAttribute GetAttribute(string attribute, params string[] elements)
        {
            try
            {
                return GetNode(elements).Attribute(attribute);
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                Report.Error($"There was an error obtaining the attribute '{attribute}'. See log for details.");
            }

            return null;
        }

        /// <summary>
        ///     Returns the attribute value of an XElement.
        /// </summary>
        /// <param name="attribute">The attribute from which to obtain the value.</param>
        /// <param name="elements">Name of elements to navigate.</param>
        /// <returns>The value from the attribute as a string.</returns>
        private string GetAttributeValue(string attribute, params string[] elements)
        {
            try
            {
                return GetNode(elements).Attribute(attribute).Value;
            }
            catch (Exception ex)
            {
                Log.Write(ex);
                Report.Error(
                    $"There was an error obtaining the value from the attribute '{attribute}'. See log for details.");
            }

            return null;
        }

        /// <summary>
        ///     Returns the last nodes Elements from a path of XElements.
        /// </summary>
        /// <param name="elements">Name of elements to navigate.</param>
        /// <returns></returns>
        private IEnumerable<XElement> GetElements(params string[] elements) => GetNode(elements).Elements();

        /// <summary>
        ///     Returns the last nodes from a path of XElements.
        /// </summary>
        /// <param name="elements">Name of elements to navigate.</param>
        /// <returns></returns>
        private XElement GetNode(params string[] elements)
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
            }
            return node;
        }

        /// <summary>
        ///     Loads all relevant attributes and values from the config file.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        private void load()
        {
            try
            {
                Report.General($"Version: {Convert.ToDouble(GetAttributeValue("version", "Version"))}");
                Report.General($"Comment: {GetAttributeValue("comment", "Comment")}");
                AutonEnabled = Convert.ToBoolean(GetAttributeValue("value", "EnableAuton"));
                Report.General($"Auton Enabled: {AutonEnabled}");
                AllocateComponents();
                RetrieveDriverSchema();
                RetrieveOperatorSchema();
            }
            catch (Exception ex)
            {
                Report.Error("The config file could not be found or there was an error reading it. See log for details.");
                Log.Write(ex);
            }
        }

        private void RetrieveDriverSchema()
        {
            #region driver control schema

            //drive controls are not dynamic, they will always be axis based controls.
            try
            {
                var controllerSlot = Convert.ToInt32(GetAttributeValue("controllerSlot", "Controls", "Driver", "slot"));
                var driveFit = Convert.ToInt32(GetAttributeValue("driveFit", "Controls", "Driver", "drive"));
                var driveFitPower = Convert.ToDouble(GetAttributeValue("power", "Controls", "Driver", "drive"));
                var multiplier =
                    Convert.ToDouble(GetAttributeValue("powerMultiplier", "Controls", "Driver", "powerMultiplier"));

                var leftAxis = Convert.ToInt32(GetAttributeValue("axis", "Controls", "Driver", "leftDrive"));
                var rightAxis = Convert.ToInt32(GetAttributeValue("axis", "Controls", "Driver", "rightDrive"));
                var leftReversed = Convert.ToBoolean(GetAttributeValue("reversed", "Controls", "Driver", "leftDrive"));
                var rightReversed = Convert.ToBoolean(GetAttributeValue("reversed", "Controls", "Driver", "rightDrive"));

                double leftDz = 0;
                double rightDz = 0;

                try
                {
                    leftDz = Convert.ToDouble(GetAttributeValue("deadZone", "Controls", "Driver", "leftDrive"));
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                }
                try
                {
                    rightDz = Convert.ToDouble(GetAttributeValue("deadZone", "Controls", "Driver", "rightDrive"));
                }
                catch (Exception ex)
                {
                    Log.Write(ex);
                }

                var left = new DriverControlSchema("leftDrive", (MotorControlFitFunction)driveFit, driveFitPower,
                    ToBindCommonName(GetAttribute("bindTo", "Controls", "Driver", "leftDrive")), leftAxis, leftDz,
                    multiplier, leftReversed);

                var right = new DriverControlSchema("rightDrive", (MotorControlFitFunction)driveFit, driveFitPower,
                    ToBindCommonName(GetAttribute("bindTo", "Controls", "Driver", "rightDrive")), rightAxis, rightDz,
                    multiplier, rightReversed);

                var temp = new List<ControlSchema>();
                foreach (var element in GetElements("Controls", "DriverAux"))
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
                            }
                            temp.Add(new ControlSchema(element.Name.ToString(), ControlType.Axis,
                                ToBindCommonName(element.Attribute("bindTo")),
                                Convert.ToInt32(element.Attribute("axis").Value), deadZone,
                                powerMultiplier, reversed));
                            break;

                        case ControlType.Button:
                            temp.Add(new ControlSchema(element.Name.ToString(), ControlType.Button,
                                ToBindCommonName(element.Attribute("bindTo")),
                                Convert.ToInt32(element.Attribute("button").Value), powerMultiplier, reversed));
                            break;

                        case ControlType.DualButton:
                            temp.Add(new ControlSchema(element.Name.ToString(), ControlType.DualButton,
                                ToBindCommonName(element.Attribute("bindTo")),
                                Convert.ToInt32(element.Attribute("buttonA").Value),
                                Convert.ToInt32(element.Attribute("buttonB").Value),
                                powerMultiplier, reversed));
                            break;

                        case ControlType.ToggleButton:
                            temp.Add(new ControlSchema(element.Name.ToString(), ControlType.ToggleButton,
                                ToBindCommonName(element.Attribute("bindTo")),
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
            }

            #endregion driver control schema
        }

        private void RetrieveOperatorSchema()
        {
            #region operator control schema

            try
            {
                var temp = new List<ControlSchema>();
                foreach (var element in GetElements("Controls", "Operator").Where(element => element.Name != "slot"))
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
                            }
                            temp.Add(new ControlSchema(element.Name.ToString(), ControlType.Axis,
                                ToBindCommonName(element.Attribute("bindTo")),
                                Convert.ToInt32(element.Attribute("axis").Value), deadZone,
                                powerMultiplier, reversed));
                            break;

                        case ControlType.Button:
                            temp.Add(new ControlSchema(element.Name.ToString(), ControlType.Button,
                                ToBindCommonName(element.Attribute("bindTo")),
                                Convert.ToInt32(element.Attribute("button").Value), powerMultiplier, reversed));
                            break;

                        case ControlType.DualButton:
                            temp.Add(new ControlSchema(element.Name.ToString(), ControlType.DualButton,
                                ToBindCommonName(element.Attribute("bindTo")),
                                Convert.ToInt32(element.Attribute("buttonA").Value),
                                Convert.ToInt32(element.Attribute("buttonB").Value),
                                powerMultiplier, reversed));
                            break;

                        case ControlType.ToggleButton:
                            temp.Add(new ControlSchema(element.Name.ToString(), ControlType.ToggleButton,
                                ToBindCommonName(element.Attribute("bindTo")),
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
                }

                #endregion Warning checkes

                OperatorConfig =
                    new OperatorConfig(
                        new Joystick(Convert.ToInt32(GetAttributeValue("controllerSlot", "Controls", "Operator", "slot"))),
                        temp);
            }
            catch (Exception ex)
            {
                Report.Error("There was an error loading the operator config. This may cause fatal runtime error!");
                Log.Write(ex);
            }

            #endregion operator control schema
        }

        private List<CommonName> ToBindCommonName(XAttribute attribute)
        {
            var bindings = new List<CommonName>();

            var values = attribute.Value.Contains(",")
                ? attribute.Value.Split(',').ToList()
                : new List<string> { attribute.Value };

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