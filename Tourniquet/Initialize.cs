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

using static Base.Config.Schemas;

namespace Tourniquet
{
    using Base;
    using Base.Config;
    using Tourniquet.ControlItems;

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
                leftDriverConfig.DeadZone,
                leftDriverConfig.PowerMultiplier, leftDriverConfig.FitPower);

            foreach (var binding in leftDriverConfig.Bindings)
                leftDriveControl.AddComponent(config.ActiveCollection.Get(binding));

            ControlCollection.Instance.AddDriverControl(leftDriveControl);

            var rightDriverConfig = config.DriverConfig.RightDriveControlSchema;
            var rightDriveControl = new AxisControl(rightDriverConfig.Name, config.DriverConfig.Driver,
                rightDriverConfig.Axis, rightDriverConfig.FitFunction, rightDriverConfig.Reversed,
                rightDriverConfig.DeadZone,
                rightDriverConfig.PowerMultiplier, rightDriverConfig.FitPower);

            foreach (var binding in rightDriverConfig.Bindings)
                rightDriveControl.AddComponent(config.ActiveCollection.Get(binding));

            ControlCollection.Instance.AddDriverControl(rightDriveControl);

            //load auxillary controls that the driver may have
            foreach (var s in config.DriverConfig.ControlsData)
                switch (s.ControlType)
                {
                    case ControlType.Axis:
                        var control = new AxisControl(s.Name, config.DriverConfig.Driver,
                            s.Axis, MotorControlFitFunction.Linear, s.Reversed, s.DeadZone, s.PowerMultiplier);

                        foreach (var binding in s.Bindings)
                            control.AddComponent(config.ActiveCollection.Get(binding));

                        ControlCollection.Instance.AddDriverControl(control);
                        break;
                }

            #endregion Driver_Setup

            #region Operator_Setup

            foreach (var s in config.OperatorConfig.ControlsData)
                switch (s.ControlType)
                {
                    case ControlType.Axis:
                        var control = new AxisControl(s.Name,
                            config.OperatorConfig.Operator,
                            s.Axis, MotorControlFitFunction.Linear, s.Reversed, s.DeadZone, s.PowerMultiplier);

                        foreach (var binding in s.Bindings)
                            control.AddComponent(config.ActiveCollection.Get(binding));

                        ControlCollection.Instance.AddOperatorControl(control);
                        break;
                }

            #endregion Operator_Setup
        }

        #endregion Public Methods
    }
}