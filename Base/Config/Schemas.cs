using System;
using System.Collections.Generic;
using System.Xml.Linq;
using WPILib;

namespace Base.Config
{
    /// <summary>
    /// Class to define a schema for a partucular control or set of controls.
    /// </summary>
    public static class Schemas
    {
        #region Public Enums

        /// <summary>
        ///     Defines the type of control that a control schema represents
        /// </summary>
        public enum ControlType
        {
            Axis,
            Button,
            DualButton,
            ToggleButton
        }

        #endregion Public Enums

        #region Public Structs

        public struct DriverConfig
        {
            #region Public Constructors

            public DriverConfig(Joystick driver, DriverControlSchema leftSchema, DriverControlSchema rightSchema,
                List<ControlSchema> controlsData = null)
            {
                Driver = driver;
                LeftDriveControlSchema = leftSchema;
                RightDriveControlSchema = rightSchema;
                ControlsData = controlsData;
            }

            #endregion Public Constructors

            #region Public Properties

            public List<ControlSchema> ControlsData { get; private set; }
            public Joystick Driver { get; private set; }
            public DriverControlSchema LeftDriveControlSchema { get; private set; }
            public DriverControlSchema RightDriveControlSchema { get; private set; }

            #endregion Public Properties
        }

        public struct OperatorConfig
        {
            #region Public Constructors

            public OperatorConfig(Joystick operator_, List<ControlSchema> controlsData = null)
            {
                Operator = operator_;
                ControlsData = controlsData;
            }

            #endregion Public Constructors

            #region Public Properties

            public List<ControlSchema> ControlsData { get; private set; }
            public Joystick Operator { get; private set; }

            #endregion Public Properties
        }

        #endregion Public Structs

        #region Public Classes

        public class ControlSchema
        {
            #region Public Methods

            public static ControlType GetControlTypeFromAttribute(XAttribute attribute)
            {
                if (attribute == null)
                    throw new ArgumentNullException(nameof(attribute),
                        "Parameter cannot be null in GetControlTypeFromAttribute.");

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

            #endregion Public Methods

            #region Public Constructors

            public ControlSchema() {}

            public ControlSchema(string name, ControlType controlType, List<CommonName> bindTo, int button,
                double powerMultiplier = 1, bool reversed = false)
            {
                Name = name;
                ControlType = controlType;
                Bindings = bindTo;
                ButtonA = button;
                PowerMultiplier = powerMultiplier;
                Reversed = reversed;
            }

            public ControlSchema(string name, ControlType controlType, List<CommonName> bindTo, int buttona, int buttonb,
                double powerMultiplier = 1, bool reversed = false)
            {
                Name = name;
                ControlType = controlType;
                Bindings = bindTo;
                ButtonA = buttona;
                ButtonB = buttonb;
                PowerMultiplier = powerMultiplier;
                Reversed = reversed;
            }

            public ControlSchema(string name, ControlType controlType, List<CommonName> bindTo, int axis,
                double deadZone, double powerMultiplier = 1, bool reversed = false)
            {
                Name = name;
                ControlType = controlType;
                Bindings = bindTo;
                Axis = axis;
                DeadZone = deadZone;
                PowerMultiplier = powerMultiplier;
                Reversed = reversed;
            }

            #endregion Public Constructors

            #region Public Properties

            public int Axis { get; protected set; }
            public List<CommonName> Bindings { get; protected set; }
            public int ButtonA { get; private set; }
            public int ButtonB { get; private set; }
            public ControlType ControlType { get; protected set; }

            public double DeadZone { get; protected set; }

            public string Name { get; protected set; }

            public double PowerMultiplier { get; protected set; }

            public bool Reversed { get; protected set; }

            #endregion Public Properties
        }

        public sealed class DriverControlSchema : ControlSchema
        {
            #region Public Constructors

            public DriverControlSchema(string name, MotorControlFitFunction fitFunction, double fitPower,
                List<CommonName> bindTo, int axis, double deadZone, double powerMultiplier = 1, bool reversed = false)
            {
                Name = name;
                ControlType = ControlType.Axis;
                Bindings = bindTo;
                Axis = axis;
                DeadZone = deadZone;
                PowerMultiplier = powerMultiplier;
                Reversed = reversed;
                FitFunction = fitFunction;
                FitPower = fitPower;
            }

            #endregion Public Constructors

            #region Public Properties

            public MotorControlFitFunction FitFunction { get; private set; }

            public double FitPower { get; private set; }

            #endregion Public Properties
        }

        #endregion Public Classes
    }
}