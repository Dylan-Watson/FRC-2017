using System;
using System.Collections.Generic;
using System.Xml.Linq;
using WPILib;

namespace Base.Config
{
    /// <summary>
    /// Class to store all relevant ControlSchemas
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

        /// <summary>
        /// Structure to define all of the drivers control schemas.
        /// </summary>
        public struct DriverConfig
        {
            #region Public Constructors

            /// <summary>
            /// Default contructor.
            /// </summary>
            /// <param name="driver">WPIlib Joystick that the driver will use</param>
            /// <param name="leftSchema">DriverControlSchema for the left side of the drive train</param>
            /// <param name="rightSchema">DriverControlSchema for the right side of the drive train</param>
            /// <param name="controlsData">List of control schemas for auxillary driver controls</param>
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

            /// <summary>
            /// List to hold auxillary driver control schemas (aka not drive train controls)
            /// </summary>
            public List<ControlSchema> ControlsData { get; private set; }

            /// <summary>
            /// The object to hold the WPIlib Joystick that the driver will use.
            /// </summary>
            public Joystick Driver { get; private set; }

            /// <summary>
            /// Defines the left drive controls.
            /// </summary>
            public DriverControlSchema LeftDriveControlSchema { get; private set; }

            /// <summary>
            /// Defines the right drive controls.
            /// </summary>
            public DriverControlSchema RightDriveControlSchema { get; private set; }

            #endregion Public Properties
        }

        /// <summary>
        /// Structure to define all of the operators control schemas.
        /// </summary>
        public struct OperatorConfig
        {
            #region Public Constructors

            /// <summary>
            /// Default constructor.
            /// </summary>
            /// <param name="operator_">WPIlib Joystick that the operator will use</param>
            /// <param name="controlsData">List of control schemas for the operator's controls</param>
            public OperatorConfig(Joystick operator_, List<ControlSchema> controlsData = null)
            {
                Operator = operator_;
                ControlsData = controlsData;
            }

            #endregion Public Constructors

            #region Public Properties

            /// <summary>
            /// List to hold the operator's control schemas.
            /// </summary>
            public List<ControlSchema> ControlsData { get; private set; }

            /// <summary>
            /// The object to hold the WPIlib Joystick that the operator will use.
            /// </summary>
            public Joystick Operator { get; private set; }

            #endregion Public Properties
        }

        #endregion Public Structs

        #region Public Classes

        /// <summary>
        /// Class to define the schema of a control.
        /// </summary>
        public class ControlSchema
        {
            #region Public Methods

            /// <summary>
            /// Gets the type of control that a schema would represent from an XAttribute object.
            /// </summary>
            /// <param name="attribute">XAttribute object loaded from XML</param>
            /// <returns></returns>
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

            /// <summary>
            /// Default constructor.
            /// </summary>
            public ControlSchema() {}

            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="name">Name of the control schema</param>
            /// <param name="controlType">Type of control that the schema represents</param>
            /// <param name="bindTo">List of CommonNames to represent what components the schema is bound to</param>
            /// <param name="button">The address of the button on the controller that will be used by this control</param>
            /// <param name="powerMultiplier">The multiplier to be applied before output to the IComponents</param>
            /// <param name="reversed">Defines if the control will be reversed</param>
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

            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="name">Name of the control schema</param>
            /// <param name="controlType">Type of control that the schema represents</param>
            /// <param name="bindTo">List of CommonNames to represent what components the schema is bound to</param>
            /// <param name="buttona">The address of one of the buttons on the controller that will be used by this control</param>
            /// <param name="buttonb">The address of one of the buttons on the controller that will be used by this control</param>
            /// <param name="powerMultiplier">The multiplier to be applied before output to the IComponents</param>
            /// <param name="reversed">Defines if the control will be reversed</param>
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

            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="name">Name of the control schema</param>
            /// <param name="controlType">Type of control that the schema represents</param>
            /// <param name="bindTo">List of CommonNames to represent what components the schema is bound to</param>
            /// <param name="axis">The address of the axis on the controller that will be used by this control</param>
            /// <param name="deadZone">The deadzones (lower limits) on the axis that this shema represents</param>
            /// <param name="powerMultiplier">The multiplier to be applied before output to the IComponents</param>
            /// <param name="reversed">Defines if the control will be reversed</param>
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

            /// <summary>
            /// Axis for this control
            /// </summary>
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