/****************************** Header ******************************\
Class Name: HealthMonitor inherits ControlLoop
Summary: Class to handle monitoring the system health of the robot.
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using WPILib;

namespace Base
{
    /// <summary>
    ///     Class to handle robot system diagnostics
    /// </summary>
    public class HealthMonitor : ControlLoop
    {
        #region Protected Methods

        /// <summary>
        ///     Main Method
        /// </summary>
        protected override void main() {

            comms.SendHealthData($"Main_Voltage", DriverStation.Instance.GetBatteryVoltage());
            comms.SendHealthData($"PDB_Voltag", pdb.GetVoltage());

            for (int i = 0; i <= 15; i++)
                comms.SendHealthData($"Channel_{i}", pdb.GetCurrent(i));

            if (DriverStation.Instance.GetBatteryVoltage() < 6.5)
                config.ActiveCollection.GetAllMotors.ForEach(s => s.setBrownOut(true));
            else
                config.ActiveCollection.GetAllMotors.ForEach(s => s.setBrownOut(false));

        }

        public void setConfig(Config.Config _config) {
            config = _config;
        }

        #endregion Protected Methods

        #region Private Fields

        private FrameworkCommunication comms = FrameworkCommunication.Instance;

        private PowerDistributionPanel pdb = new PowerDistributionPanel();

        private Config.Config config;

        #endregion Private Fields
    }
}