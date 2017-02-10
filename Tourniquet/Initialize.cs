/****************************** Header ******************************\
Class Name: Initialize [static]
Summary: Contains methods used to setup operator and driver control
schemas, and map the controls to the physical components.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using Base;
using Base.Config;
using Tourniquet.ControlItems;
using static Base.Config.Schemas;

namespace Tourniquet
{
    /// <summary>
    ///     Sets up everything required for teleop to run
    /// </summary>
    public static class Initialize
    {
        #region Public Methods

        /// <summary>
        ///     Builds the driver and operators controls based of off their ControlSchemas
        /// </summary>
        /// <param name="config">The main program's instance of the config</param>
        public static void BuildControlSchema(Config config)
        {
            if (config == null)
                return;

            ControlCollection.Instance.CleanCollection();

            #region Driver_Setup

            var leftDriverConfig = config.DriverConfig.LeftDriveControlSchema;
            var leftDriveControl = new AxisControl(leftDriverConfig.Name, config.DriverConfig.Driver,
                leftDriverConfig.Axis, leftDriverConfig.FitFunction, leftDriverConfig.Reversed,
                leftDriverConfig.DeadZone, config.DriverConfig.LeftDriveControlSchema.IsEnabled,
                leftDriverConfig.PowerMultiplier, leftDriverConfig.FitPower);

            if (leftDriverConfig.Debug)
                leftDriveControl.Debug = true;

            foreach (var binding in leftDriverConfig.Bindings)
                leftDriveControl.AddComponent(config.ActiveCollection.Get(binding));

            ControlCollection.Instance.AddDriverControl(leftDriveControl);

            var rightDriverConfig = config.DriverConfig.RightDriveControlSchema;
            var rightDriveControl = new AxisControl(rightDriverConfig.Name, config.DriverConfig.Driver,
                rightDriverConfig.Axis, rightDriverConfig.FitFunction, rightDriverConfig.Reversed,
                rightDriverConfig.DeadZone, config.DriverConfig.RightDriveControlSchema.IsEnabled,
                rightDriverConfig.PowerMultiplier, rightDriverConfig.FitPower);

            if (rightDriverConfig.Debug)
                rightDriveControl.Debug = true;

            foreach (var binding in rightDriverConfig.Bindings)
                rightDriveControl.AddComponent(config.ActiveCollection.Get(binding));

            ControlCollection.Instance.AddDriverControl(rightDriveControl);

            //load auxillary controls that the driver may have
            foreach (var s in config.DriverConfig.ControlsData)
                switch (s.ControlType)
                {
                    case ControlType.Axis:
                        var axisControl = new AxisControl(s.Name, config.DriverConfig.Driver,
                            s.Axis, MotorControlFitFunction.Linear, s.Reversed, s.DeadZone, s.IsEnabled,
                            s.PowerMultiplier);

                        foreach (var binding in s.Bindings)
                            axisControl.AddComponent(config.ActiveCollection.Get(binding));

                        ControlCollection.Instance.AddDriverControl(axisControl);

                        if (s.Debug)
                            axisControl.Debug = true;

                        break;

                    case ControlType.Button:
                        var btnControl = new ButtonControl(s.Name, config.DriverConfig.Driver,
                            s.ButtonA, s.Reversed, s.IsEnabled, s.ActOnRelease, s.PowerMultiplier);

                        foreach (var binding in s.Bindings)
                            btnControl.AddComponent(config.ActiveCollection.Get(binding));

                        ControlCollection.Instance.AddDriverControl(btnControl);

                        if (s.Debug)
                            btnControl.Debug = true;

                        break;

                    case ControlType.DualButton:
                        var dualBtnControl = new DualButtonControl(s.Name, config.DriverConfig.Driver,
                            s.ButtonA, s.ButtonB, s.Reversed, s.IsEnabled, s.PowerMultiplier);

                        foreach (var binding in s.Bindings)
                            dualBtnControl.AddComponent(config.ActiveCollection.Get(binding));

                        ControlCollection.Instance.AddDriverControl(dualBtnControl);

                        if (s.Debug)
                            dualBtnControl.Debug = true;

                        break;

                    case ControlType.ToggleButton:
                        var toggleBtnControl = new ToggleButtonControl(s.Name, config.DriverConfig.Driver,
                            s.ButtonA, s.Reversed, s.IsEnabled, s.PowerMultiplier);

                        foreach (var binding in s.Bindings)
                            toggleBtnControl.AddComponent(config.ActiveCollection.Get(binding));

                        ControlCollection.Instance.AddDriverControl(toggleBtnControl);

                        if (s.Debug)
                            toggleBtnControl.Debug = true;

                        break;
                }

            #endregion Driver_Setup

            #region Operator_Setup

            foreach (var s in config.OperatorConfig.ControlsData)
                switch (s.ControlType)
                {
                    case ControlType.Axis:
                        var axisControl = new AxisControl(s.Name,
                            config.OperatorConfig.Operator,
                            s.Axis, MotorControlFitFunction.Linear, s.Reversed, s.DeadZone, s.IsEnabled,
                            s.PowerMultiplier);

                        foreach (var binding in s.Bindings)
                            axisControl.AddComponent(config.ActiveCollection.Get(binding));

                        ControlCollection.Instance.AddOperatorControl(axisControl);

                        if (s.Debug)
                            axisControl.Debug = true;

                        break;

                    case ControlType.Button:
                        var btnControl = new ButtonControl(s.Name, config.OperatorConfig.Operator,
                            s.ButtonA, s.Reversed, s.IsEnabled, s.ActOnRelease, s.PowerMultiplier);

                        foreach (var binding in s.Bindings)
                            btnControl.AddComponent(config.ActiveCollection.Get(binding));

                        ControlCollection.Instance.AddOperatorControl(btnControl);

                        if (s.Debug)
                            btnControl.Debug = true;

                        break;

                    case ControlType.DualButton:
                        var dualBtnControl = new DualButtonControl(s.Name, config.OperatorConfig.Operator,
                            s.ButtonA, s.ButtonB, s.Reversed, s.IsEnabled, s.PowerMultiplier);

                        foreach (var binding in s.Bindings)
                            dualBtnControl.AddComponent(config.ActiveCollection.Get(binding));

                        ControlCollection.Instance.AddOperatorControl(dualBtnControl);

                        if (s.Debug)
                            dualBtnControl.Debug = true;

                        break;

                    case ControlType.ToggleButton:
                        var toggleBtnControl = new ToggleButtonControl(s.Name, config.OperatorConfig.Operator,
                            s.ButtonA, s.Reversed, s.IsEnabled, s.PowerMultiplier);

                        foreach (var binding in s.Bindings)
                            toggleBtnControl.AddComponent(config.ActiveCollection.Get(binding));

                        ControlCollection.Instance.AddOperatorControl(toggleBtnControl);

                        if (s.Debug)
                            toggleBtnControl.Debug = true;

                        break;
                }

            #endregion Operator_Setup
        }

        #endregion Public Methods
    }
}