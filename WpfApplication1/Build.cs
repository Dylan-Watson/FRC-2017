using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WpfApplication1
{
    public static class Build
    {
        public static XDocument doc;

        public static Exception BuildFile(string filePath)
        {
            #region LoadFile

            try
            {
                Load(filePath);
            }
            catch (Exception e)
            {
                return e;
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
                return e;
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
                return e;
            }

            #endregion

            #region NavX

            //TODO: allow different ports to be used when initializing NavX
            try
            {
                Convert.ToBoolean(getAttributeValue("value", "UseNavX"));
            }
            catch (Exception e)
            {
                return e;
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
                    return e;
                }

                try{
                    Convert.ToInt32(getAttributeValue("width", "EnableSecondaryCameraServer"));
                    Convert.ToInt32(getAttributeValue("height", "EnableSecondaryCameraServer"));
                    Convert.ToInt32(getAttributeValue("fps", "EnableSecondaryCameraServer"));
                }
                catch(Exception e)
                {
                    return e;
                }
            }
            catch (Exception e)
            {
                return e;
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
                        return e;
                    }
                }
            }
            catch (Exception e)
            {
                return e;
            }

            try
            {
                Convert.ToDouble(getAttributeValue("exposure", "CameraSettings"));
            }
            catch (Exception e)
            {
                return e;
            }

            #endregion

            #region Encoders

            try
            {
                foreach (var element in getElements("RobotConfig", "Encoders"))
                    try
                    {
                        Convert.ToBoolean(element.Attribute("reversed").Value);

                        Convert.ToInt32(element.Attribute("aChannel").Value);
                        Convert.ToInt32(element.Attribute("bChannel").Value);

                        Convert.ToBoolean(element.Attribute("debug")?.Value));
                    }
                    catch (Exception e)
                    {
                        return e;
                    }
            }
            catch (Exception e)
            {
                return e;
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
                        return e;
                    }
            }
            catch (Exception e)
            {
                return e;
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
                        return e;
                    }
            }
            catch (Exception e)
            {
                return e;
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
                    catch (Exception ex)
                    {
                        if (ex is AllocationException)
                        {
                            AllocationExceptionReport(ex, element);
                        }
                        else
                        {
                            Report.Error(
                                $"Failed to load AnalogInput {element?.Name}. This may cause a fatal runtime error! See log for details.");
                            Log.Write(ex);
                            if (VerboseOutput)
                                Report.Error(ex.Message);
                        }
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
                        var ao = new AnalogOutputItem(Convert.ToInt32(element.Attribute("channel").Value),
                            element.Name.ToString());

                        ActiveCollection.AddComponent(ao);

                        if (Convert.ToBoolean(element.Attribute("debug")?.Value))
                            ao.Debug = true;
                    }
                    catch (Exception ex)
                    {
                        if (ex is AllocationException)
                        {
                            AllocationExceptionReport(ex, element);
                        }
                        else
                        {
                            Report.Error(
                                $"Failed to load AnalogOutput {element?.Name}. This may cause a fatal runtime error! See log for details.");
                            Log.Write(ex);
                            if (VerboseOutput)
                                Report.Error(ex.Message);
                        }
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
                            motorEncoder =
                                (EncoderItem)
                                ActiveCollection.Get(toBindCommonName(element.Attribute("encoder"))[0]);

                        componentNames.Add(new CommonName(element.Name.ToString()));
                        Report.General(
                            $"Added Victor{type} {element.Name}, channel {Convert.ToInt32(element.Attribute("channel").Value)}, is reversed = {Convert.ToBoolean(element.Attribute("reversed").Value)}");
                        if (!Convert.ToBoolean(element.Attribute("drive").Value))
                        {
                            var temp = new VictorItem(t, Convert.ToInt32(element.Attribute("channel").Value),
                                element.Name.ToString(), Convert.ToBoolean(element.Attribute("reversed").Value));

                            ActiveCollection.AddComponent(temp);
                            temp.SetUpperLimit(upperLimit);
                            temp.SetLowerLimit(lowerLimit);
                            temp.SetEncoder(motorEncoder);

                            if (Convert.ToBoolean(element.Attribute("debug")?.Value))
                                temp.Debug = true;
                        }
                        else
                        {
                            switch (element.Attribute("side").Value)
                            {
                                case "right":
                                    var temp =
                                        new VictorItem(t, Convert.ToInt32(element.Attribute("channel").Value),
                                            element.Name.ToString(), Motor.Side.Right,
                                            Convert.ToBoolean(element.Attribute("reversed").Value));

                                    ActiveCollection.AddComponent(temp);
                                    temp.SetUpperLimit(upperLimit);
                                    temp.SetLowerLimit(lowerLimit);
                                    temp.SetEncoder(motorEncoder);

                                    if (Convert.ToBoolean(element.Attribute("debug")?.Value))
                                        temp.Debug = true;

                                    break;

                                case "left":
                                    temp =
                                        new VictorItem(t, Convert.ToInt32(element.Attribute("channel").Value),
                                            element.Name.ToString(), Motor.Side.Left,
                                            Convert.ToBoolean(element.Attribute("reversed").Value));

                                    ActiveCollection.AddComponent(temp);
                                    temp.SetUpperLimit(upperLimit);
                                    temp.SetLowerLimit(lowerLimit);
                                    temp.SetEncoder(motorEncoder);

                                    if (Convert.ToBoolean(element.Attribute("debug")?.Value))
                                        temp.Debug = true;

                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex is AllocationException)
                        {
                            AllocationExceptionReport(ex, element);
                        }
                        else
                        {
                            Report.Error(
                                $"Failed to load Victor {element?.Name}. This may cause a fatal runtime error! See log for details.");
                            Log.Write(ex);
                            if (VerboseOutput)
                                Report.Error(ex.Message);
                        }
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
                            $"Added Double Solenoid {element.Name}, forward channel {Convert.ToInt32(element.Attribute("forward").Value)}, reverse channel {Convert.ToInt32(element.Attribute("reverse").Value)}, default position = {d}, is reversed = {Convert.ToBoolean(element.Attribute("reversed").Value)}");
                        var ds = new DoubleSolenoidItem(element.Name.ToString(),
                            Convert.ToInt32(element.Attribute("forward").Value),
                            Convert.ToInt32(element.Attribute("reverse").Value), d,
                            Convert.ToBoolean(element.Attribute("reversed").Value));

                        ActiveCollection.AddComponent(ds);

                        if (Convert.ToBoolean(element.Attribute("debug")?.Value))
                            ds.Debug = true;
                    }
                    catch (Exception ex)
                    {
                        if (ex is AllocationException)
                        {
                            AllocationExceptionReport(ex, element);
                        }
                        else
                        {
                            Report.Error(
                                $"Failed to load DoubleSolenoid {element?.Name}. This may cause a fatal runtime error! See log for details.");
                            Log.Write(ex);
                            if (VerboseOutput)
                                Report.Error(ex.Message);
                        }
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
                            $"Added Relay {element.Name}, channel {Convert.ToInt32(element.Attribute("channel").Value)}, default position {d}");
                        var relay = new RelayItem(Convert.ToInt32(element.Attribute("channel").Value),
                            element.Name.ToString(), d);
                        ActiveCollection.AddComponent(relay);

                        if (Convert.ToBoolean(element.Attribute("debug")?.Value))
                            relay.Debug = true;
                    }
                    catch (Exception ex)
                    {
                        if (ex is AllocationException)
                        {
                            AllocationExceptionReport(ex, element);
                        }
                        else
                        {
                            Report.Error(
                                $"Failed to load Relay {element?.Name}. This may cause a fatal runtime error! See log for details.");
                            Log.Write(ex);
                            if (VerboseOutput)
                                Report.Error(ex.Message);
                        }
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
                foreach (var element in getElements("RobotConfig", "Potentiometers"))
                    try
                    {
                        componentNames.Add(new CommonName(element.Name.ToString()));

                        Report.General(
                            $"Added Potentiometer {element.Name}, channel {Convert.ToInt32(element.Attribute("channel").Value)}");
                        var tmp = new PotentiometerItem(Convert.ToInt32(element.Attribute("channel").Value),
                            element.Name.ToString());
                        ActiveCollection.AddComponent(tmp);

                        if (Convert.ToBoolean(element.Attribute("debug")?.Value))
                            tmp.Debug = true;
                    }
                    catch (Exception ex)
                    {
                        if (ex is AllocationException)
                        {
                            AllocationExceptionReport(ex, element);
                        }
                        else
                        {
                            Report.Error(
                                $"Failed to load Potentiometer {element?.Name}. This may cause a fatal runtime error! See log for details.");
                            Log.Write(ex);
                            if (VerboseOutput)
                                Report.Error(ex.Message);
                        }
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

            #endregion

            retrieveDriverSchema();
            retrieveOperatorSchema();
            constructVirtualControlEvents();
        }
        #endregion Potentiometers

        #region Util

        private static string getAttributeValue(string attribute, params string[] elements)
            {
                try
                {
                    return getNode(elements).Attribute(attribute).Value;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return null;
            }

            private static XElement getNode(params string[] elements)
            {
                var node = doc.Root;
            
                node = elements.Aggregate(node, (current, value) => current.Element(value));
                return node;
            }

            public static void Load(string fileName)
            {
                doc = XDocument.Load(fileName);
            }

            private static IEnumerable<XElement> getElements(params string[] elements) => getNode(elements).Elements();


        #endregion

    }
    }
}
