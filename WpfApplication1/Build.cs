using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace WpfApplication1
{
    public static class Build
    {
        /// <summary>
        ///     THe XDocument used to store the current config
        /// </summary>
        public static XDocument doc;

        /// <summary>
        ///     Utility adaptation of the config load file in Base
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>custom text exception, thrown exception/returns>
        public static Tuple<string, Exception> BuildFile(string filePath)
        {
            #region LoadFile

            try
            {
                Load(filePath);
            }
            catch (Exception e)
            {
                return new Tuple<string,Exception>(@"Faied to load the config!", e);
            }

            #endregion

            #region AllocateMetaData
            try
            {
                Convert.ToDouble(getAttributeValue("version", "Version"));
                getAttributeValue("comment", "Comment");
                Convert.ToBoolean(getAttributeValue("value", "VerboseOutput"));
                Convert.ToBoolean(getAttributeValue("value", "EnableAuton"));
            }
            catch (Exception e)
            {
                return new Tuple<string, Exception>(@"Failed to locate one or more metadata values!", e);
            }
            #endregion

            #region AllocateComponents

            #region QuickLoad

            try
            {
                Convert.ToBoolean(getAttributeValue("value", "QuickLoad"));
            }
            catch (Exception e)
            {
                return new Tuple<string, Exception>(@"Failed to load the Quickoad value!", e);
            }

            #endregion

            #region NavX

            try
            {
                Convert.ToBoolean(getAttributeValue("value", "UseNavX"));
            }
            catch (Exception e)
            {
                return new Tuple<string, Exception>(@"Faied to load the UseNavX Value!", e);
            }

            #endregion

            #region Vision

            try
            {
                Convert.ToBoolean(getAttributeValue("value", "EnableSecondaryCameraServer"));

                try
                {
                    Convert.ToBoolean(getAttributeValue("autoExposure", "EnableSecondaryCameraServer"));
                }
                catch (Exception e)
                {
                    return new Tuple<string, Exception>("Faied to load the AutoExposure Value!", e);
                }

                try {
                    Convert.ToInt32(getAttributeValue("width", "EnableSecondaryCameraServer"));
                    Convert.ToInt32(getAttributeValue("height", "EnableSecondaryCameraServer"));
                    Convert.ToInt32(getAttributeValue("fps", "EnableSecondaryCameraServer"));
                }
                catch (Exception e)
                {
                    return new Tuple<string, Exception>(@"Faied to load one or more of the EnableSecondaryCamera attribute Values!", e);
                }
            }
            catch (Exception e)
            {
                return new Tuple<string, Exception>(@"Failed to load the EnableSecondaryServer!", e);
            }

            try
            {
                foreach (var element in getElements("TargetSettings"))
                {
                    try
                    {
                        Convert.ToInt32(element.Attribute("id").Value);
                        Convert.ToBoolean(element.Attribute("enabled").Value);
                        Convert.ToInt32(element.Attribute("minRadius").Value);
                        Convert.ToInt16(element.Attribute("maxRadius").Value);
                        Convert.ToByte(element.Attribute("lowerHue").Value);
                        Convert.ToByte(element.Attribute("upperHue").Value);
                        Convert.ToByte(element.Attribute("lowerSaturation").Value);
                        Convert.ToByte(element.Attribute("upperSaturation").Value);
                        Convert.ToByte(element.Attribute("lowerValue").Value);
                        Convert.ToByte(element.Attribute("upperValue").Value);
                        Convert.ToByte(element.Attribute("red").Value);
                        Convert.ToByte(element.Attribute("green").Value);
                        Convert.ToByte(element.Attribute("blue").Value);
                        Convert.ToInt32(element.Attribute("maxObjects").Value);
                    }
                    catch (Exception e)
                    {
                        return new Tuple<string, Exception>(@"Failed to load one or more of the TargetSetting attributes!", e);
                    }
                }
            }
            catch (Exception e)
            {
                return new Tuple<string, Exception>(@"Failed to load the TargetSettings Element!", e);
            }

            try
            {
                if (getAttributeValue("exposure", "CameraSettings") != "null")
                {
                    Convert.ToDouble(getAttributeValue("exposure", "CameraSettings"));
                }
            }
            catch (Exception e)
            {
                return new Tuple<string, Exception>(@"Failed to load the exposure attribute of the CameraSettings element!", e);
            }

            #endregion

            #region Encoders

            try
            {
                foreach (var element in getElements("RobotConfig", "Encoders"))
                    try
                    {
                        Convert.ToBoolean(element.Attribute("reversed").Value);
                        Convert.ToBoolean(element.Attribute("debug")?.Value);
                        Convert.ToInt32(element.Attribute("aChannel").Value);
                        Convert.ToInt32(element.Attribute("bChannel").Value);

                    }
                    catch (Exception e)
                    {
                        return new Tuple<string, Exception>(@"Failed to load one or more of the Encoder attributes!", e);
                    }
            }
            catch (Exception e)
            {
                return new Tuple<string, Exception>(@"Failed to load one or more of the Encoder elements!", e);
            }

            #endregion Encoders

            #region DI

            try
            {
                foreach (var element in getElements("RobotConfig", "DI"))
                    try
                    {
                        Convert.ToInt32(element.Attribute("channel").Value);
                        Convert.ToBoolean(element.Attribute("debug")?.Value);
                    }
                    catch (Exception e)
                    {
                        return new Tuple<string, Exception>(@"Failed to load one or more of the attributes for the DI Element!", e);
                    }
            }
            catch (Exception e)
            {
                return new Tuple<string, Exception>(@"Failed to load one or more of the DI Elements!!", e);
            }

            #endregion DI

            #region DO

            try
            {
                foreach (var element in getElements("RobotConfig", "DO"))
                    try
                    {
                        Convert.ToInt32(element.Attribute("channel").Value);
                        Convert.ToBoolean(element.Attribute("debug")?.Value);
                    }
                    catch (Exception e)
                    {
                        return new Tuple<string, Exception>(@"Failed to load one or more of the attributes for the DO Element!", e);
                    }
            }
            catch (Exception e)
            {
                return new Tuple<string, Exception>(@"Failed to load one or more of the DO elements!", e);
            }

            #endregion DO

            #region AI

            try
            {
                foreach (var element in getElements("RobotConfig", "AI"))
                    try
                    {
                        Convert.ToInt32(element.Attribute("channel").Value);
                        Convert.ToBoolean(element.Attribute("debug")?.Value);
                    }
                    catch (Exception e)
                    {
                        return new Tuple<string, Exception>(@"Failed to load one or more of the attributes for the AI Element!", e);
                    }
            }
            catch (Exception e)
            {
                return new Tuple<string, Exception>(@"Failed to load one or more of the AI Elements!", e);
            }

            #endregion AI

            #region AO

            try
            {
                foreach (var element in getElements("RobotConfig", "AO"))
                    try
                    {
                        Convert.ToInt32(element.Attribute("channel").Value);
                        Convert.ToBoolean(element.Attribute("debug")?.Value);
                    }
                    catch (Exception e)
                    {
                        return new Tuple<string, Exception>(@"Failed to load one or more of the attributes for the AO Element!", e);
                    }
            }
            catch (Exception e)
            {
                return new Tuple<string, Exception>(@"Failed to load one or more of the AO Elements!", e);
            }

            #endregion AO

            #region Victors

            try
            {
                foreach (var element in getElements("RobotConfig", "Victors")) {
                    try
                    {
                        var type = element.Attribute("type").Value;
                        Convert.ToInt32(element.Attribute("channel").Value);
                        Convert.ToBoolean(element.Attribute("reversed").Value);
                        Convert.ToBoolean(element.Attribute("drive").Value);
                    }
                    catch (Exception e)
                    {
                        return new Tuple<string, Exception>(@"Failed to load one or more of the attributes for the Victor Element!", e);
                    }
                }
            }
            catch (Exception e)
            {
                return new Tuple<string, Exception>(@"Failed to load one or more of the Victor Elements!", e);
            }

            #endregion Victors

            #region Talons
            try
            {
                foreach (var element in getElements("RobotConfig", "Talons"))
                    try
                    {
                        if (element.Attribute("type").Value == "pwm")
                        {
                            Convert.ToInt32(element.Attribute("channel").Value);
                            Convert.ToBoolean(element.Attribute("reversed").Value);
                        }
                        else
                            switch (element.Attribute("type").Value)
                            {
                                case "master":
                                    double p = Convert.ToDouble(element.Attribute("p").Value);
                                    double i = Convert.ToDouble(element.Attribute("i").Value);
                                    double d = Convert.ToDouble(element.Attribute("d").Value);
                                    Convert.ToInt32(element.Attribute("channel").Value);
                                    Convert.ToBoolean(element.Attribute("reversed").Value);
                                    break;
                                case "slave":
                                    var master = element.Attribute("master").Value;
                                    Convert.ToInt32(element.Attribute("channel").Value);
                                    Convert.ToBoolean(element.Attribute("reversed").Value);
                                    break;
                            }
                    }
                    catch (Exception e)
                    {
                        return new Tuple<string, Exception>(@"Failed to load one or more of the attributes for the Talon Element!", e);
                    }
            }
            catch (Exception e)
            {
                return new Tuple<string, Exception>(@"Failed to load one or more of the Talon Elements!", e);
            }

            #endregion Talon

            #region DoubleSolenoids

            try
            {
                foreach (var element in getElements("RobotConfig", "Solenoids")) {
                    try
                    {
                        Convert.ToInt32(element.Attribute("forward").Value);
                        Convert.ToInt32(element.Attribute("reverse").Value);
                        Convert.ToBoolean(element.Attribute("reversed").Value);
                        Convert.ToBoolean(element.Attribute("debug")?.Value);
                    }
                    catch (Exception e)
                    {
                        return new Tuple<string, Exception>(@"Failed to load one or more of the attributes for the Solenoid Element!", e);
                    }
                }
            }
            catch (Exception e)
            {
                return new Tuple<string, Exception>(@"Failed to load one or more of the Solenoid Elements!", e);
            }

            #endregion DoubleSolenoids

            #region Relays

            try
            {
                foreach (var element in getElements("RobotConfig", "Relays"))
                    try
                    {
                        Convert.ToInt32(element.Attribute("channel").Value);
                        Convert.ToBoolean(element.Attribute("debug")?.Value);
                    }
                    catch (Exception e)
                    {
                        return new Tuple<string, Exception>(@"Failed to load one or more of the attributes for the Relay Element!", e);
                    }
            }
            catch (Exception e)
            {
                return new Tuple<string, Exception>(@"Failed to load one or more of the Relay Elements!", e);
            }

            #endregion Relays

            #region Potentiometers

            try
            {
                foreach (var element in getElements("RobotConfig", "Potentiometers"))
                    try
                    {
                        Convert.ToInt32(element.Attribute("channel").Value);
                        Convert.ToBoolean(element.Attribute("debug")?.Value);
                    }
                    catch (Exception e)
                    {
                        return new Tuple<string, Exception>(@"Failed to load one or more of the attributes for the Potentiometer Element!", e);
                    }
            }
            catch (Exception e)
            {
                return new Tuple<string, Exception>(@"Failed to load one or more of the Potentiometer Elements!", e);
            }

            #endregion

            #endregion

            #region AllocateDriverSchema

            try
            {
                Convert.ToInt32(getAttributeValue("controllerSlot", "Controls", "Driver", "slot"));
                Convert.ToInt32(getAttributeValue("driveFit", "Controls", "Driver", "drive"));
                Convert.ToDouble(getAttributeValue("power", "Controls", "Driver", "drive"));
                Convert.ToDouble(getAttributeValue("powerMultiplier", "Controls", "Driver", "powerMultiplier"));

                Convert.ToInt32(getAttributeValue("axis", "Controls", "Driver", "leftDrive"));
                Convert.ToInt32(getAttributeValue("axis", "Controls", "Driver", "rightDrive"));
                Convert.ToBoolean(getAttributeValue("reversed", "Controls", "Driver", "leftDrive"));
                Convert.ToBoolean(getAttributeValue("reversed", "Controls", "Driver", "rightDrive"));
                Convert.ToDouble(getAttributeValue("deadZone", "Controls", "Driver", "leftDrive"));
                Convert.ToDouble(getAttributeValue("deadZone", "Controls", "Driver", "rightDrive"));
                XAttribute _temp = getAttribute("bindTo", "Controls", "Driver", "leftDrive");

                Convert.ToBoolean(getAttributeValue("debug", "Controls", "Driver", "leftDrive"));
                
                _temp = getAttribute("bindTo", "Controls", "Driver", "rightDrive");

                Convert.ToBoolean(getAttributeValue("debug", "Controls", "Driver", "rightDrive"));

                foreach (var element in getElements("Controls", "DriverAux")) {
                    try
                    {
                        Convert.ToBoolean(element.Attribute("reversed").Value);

                        switch (GetControlTypeFromAttribute(element.Attribute("type")))
                        {
                            case ControlType.Axis:
                                Convert.ToDouble(element.Attribute("deadZone").Value);
                                _temp = element.Attribute("bindTo");
                                Convert.ToInt32(element.Attribute("axis").Value);
                                Convert.ToBoolean(element.Attribute("debug")?.Value);
                                break;

                            case ControlType.Button:
                                Convert.ToBoolean(element.Attribute("actOnRelease").Value);
                                _temp = element.Attribute("bindTo");
                                Convert.ToInt32(element.Attribute("button").Value);
                                Convert.ToBoolean(element.Attribute("debug")?.Value);
                                break;

                            case ControlType.DualButton:
                                _temp = element.Attribute("bindTo");
                                Convert.ToInt32(element.Attribute("buttonA").Value);
                                Convert.ToInt32(element.Attribute("buttonB").Value);
                                Convert.ToBoolean(element.Attribute("debug")?.Value);
                                break;

                            case ControlType.ToggleButton:
                                _temp = element.Attribute("bindTo");
                                Convert.ToInt32(element.Attribute("button").Value);
                                Convert.ToBoolean(element.Attribute("debug")?.Value);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    catch (Exception e) {
                        return new Tuple<string, Exception>(@"Failed to load one or more of the Driver AUX Elements or Attributes!", e);
                    }
                }
            }
            catch (Exception e) {
                return new Tuple<string, Exception>(@"Failed to load one or more of the Driver Schema Elements!", e);
            }

            #endregion

            #region AllocateOperatorSchema

            try
            {
                foreach (var element in getElements("Controls", "Operator").Where(element => element.Name != "slot"))
                {
                    try
                    {
                        XAttribute _temp;
                        //Convert.ToDouble(element.Attribute("powerMultiplier").Value);
                        Convert.ToBoolean(element.Attribute("reversed").Value);
                        switch (GetControlTypeFromAttribute(element.Attribute("type")))
                        {
                            case ControlType.Axis:
                                Convert.ToDouble(element.Attribute("deadZone").Value);
                                _temp = element.Attribute("bindTo");
                                Convert.ToInt32(element.Attribute("axis").Value);
                                Convert.ToBoolean(element.Attribute("debug")?.Value);
                                break;

                            case ControlType.Button:
                                Convert.ToBoolean(element.Attribute("actOnRelease").Value);
                                _temp = element.Attribute("bindTo");
                                Convert.ToInt32(element.Attribute("button").Value);
                                Convert.ToBoolean(element.Attribute("debug")?.Value);
                                break;

                            case ControlType.DualButton:
                                _temp = element.Attribute("bindTo");
                                Convert.ToInt32(element.Attribute("buttonA").Value);
                                Convert.ToInt32(element.Attribute("buttonB").Value);
                                Convert.ToBoolean(element.Attribute("debug")?.Value);
                                break;

                            case ControlType.ToggleButton:
                                _temp = element.Attribute("bindTo");
                                Convert.ToInt32(element.Attribute("button").Value);
                                Convert.ToBoolean(element.Attribute("debug")?.Value);
                                break;

                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    catch (Exception e)
                    {
                        return new Tuple<string, Exception>(@"Failed to load one or more of the Operator Element Attributes!", e);
                    }

                }
            }
            catch (Exception e)
            {
                return new Tuple<string, Exception>(@"Failed to load one or more of the Operator Elements!", e);
            }

            #endregion

            #region Construct VirtualControlEvents

            try
            {
                foreach (var element in getElements("VirtualControlEvents"))
                    try
                    {
                        string _temp = element.Attribute("type")?.Value;
                        _temp = element.Attribute("setMethod")?.Value;
                        Convert.ToBoolean(element.Attribute("auton")?.Value);
                        Convert.ToBoolean(element.Attribute("teleop")?.Value);
                        XAttribute _ttemp = element.Attribute("drivers");
                        _ttemp = element.Attribute("actions");
                    }
                    catch (Exception e)
                    {
                        return new Tuple<string, Exception>(@"Failed to load one or more of the VirtualControlEvents Elements!", e);
                    }
            }
            catch (Exception e)
            {
                return new Tuple<string, Exception>(@"Failed to load one or more of the VirtualControlEvents Elements!", e);
            }

            #endregion

            return new Tuple<string, Exception>(null,null);
        }

        #region Util

        /// <summary>
        ///     Utility method to get the value of an attribute
        /// </summary>
        /// <param name="attribute">The name of the attribute to find</param>
        /// <param name="elements">The element tree to follow</param>
        /// <returns>The value of the attributes</returns>
        private static string getAttributeValue(string attribute, params string[] elements)
        {
            return getNode(elements).Attribute(attribute).Value;
        }

        /// <summary>
        ///     Utility method to get the XElement of an XML Tree
        /// </summary>
        /// <param name="elements"></param>
        /// <returns>XElement element</returns>
        private static XElement getNode(params string[] elements)
        {
            var node = doc.Root;

            node = elements.Aggregate(node, (current, value) => current.Element(value));
            return node;
        }

        /// <summary>
        ///     Load the document from the filePath
        /// </summary>
        /// <param name="fileName">The full system file path</param>
        public static void Load(string fileName)
        {
            doc = XDocument.Load(fileName);
        }

        /// <summary>
        ///     Utility method to get an IEnumerable of XElements
        /// </summary>
        /// <param name="elements">Names of elements</param>
        /// <returns>IEnumerable of XElements</returns>
        private static IEnumerable<XElement> getElements(params string[] elements) => getNode(elements).Elements();

        /// <summary>
        ///     Utility method to get an Attribute
        /// </summary>
        /// <param name="attribute">Name of the attribute</param>
        /// <param name="elements">List of element names</param>
        /// <returns>XAttribute attribute</returns>
        private static XAttribute getAttribute(string attribute, params string[] elements)
        {
            return getNode(elements).Attribute(attribute);
        }

        /// <summary>
        ///     Utility method to get the control method from an attribute
        /// </summary>
        /// <param name="attribute">The XAttribute</param>
        /// <returns>ControlType controltype</returns>
        private static ControlType GetControlTypeFromAttribute(XAttribute attribute)
        {
            if (attribute == null)
            {
                throw new ArgumentNullException(nameof(attribute),
                    "Parameter cannot be null in GetControlTypeFromAttribute.");
            }

            switch (attribute.Value)
            {
                case "button":
                    return ControlType.Button;

                case "dualButton":
                    return ControlType.DualButton;

                case "toggle":
                    return ControlType.ToggleButton;

                default:
                    return ControlType.Axis;
            }
        }

        private enum ControlType {
            /// <summary>
            ///     Axis control
            /// </summary>
            Axis,

            /// <summary>
            ///     Button control
            /// </summary>
            Button,

            /// <summary>
            ///     Two button control
            /// </summary>
            DualButton,

            /// <summary>
            ///     Toggle button control
            /// </summary>
            ToggleButton
        }

        #endregion
    }
}
