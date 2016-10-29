using WPILib;

namespace Base
{
    internal class HealthMonitor : ControlLoop
    {
        #region Protected Methods

        protected override void main()
        {
            /*comms.SendHealthData("Match Time", DriverStation.Instance.GetMatchTime());
            comms.SendHealthData("Main Voltage", DriverStation.Instance.GetBatteryVoltage());

            for(int i=0;i<=15;i++)
                comms.SendHealthData($"Channel {i} Current", pdb.GetCurrent(i));

            comms.SendHealthData("PDB Voltage", pdb.GetVoltage());
            comms.SendHealthData("PDB Temperature", pdb.GetTemperature());
            comms.SendHealthData("PDB Total Current", pdb.GetTotalCurrent());
            comms.SendHealthData("PDB Total Energy", pdb.GetTotalEnergy());
            comms.SendHealthData("PDB Total Power", pdb.GetTotalPower());
            comms.SendHealthData("RIO 3V3 Current", ControllerPower.GetCurrent3V3());
            comms.SendHealthData("RIO 5V Current", ControllerPower.GetCurrent5V());
            comms.SendHealthData("RIO 6V Current", ControllerPower.GetCurrent6V());
            comms.SendHealthData("RIO Input Current", ControllerPower.GetInputCurrrent());
            comms.SendHealthData("RIO Intput Voltage", ControllerPower.GetInputVoltage());
            comms.SendHealthData("RIO 3V3 Voltage", ControllerPower.GetVoltage3V3());
            comms.SendHealthData("RIO 5V Voltage", ControllerPower.GetVoltage5V());
            comms.SendHealthData("RIO 5V Voltage", ControllerPower.GetVoltage6V());*/
        }

        #endregion Protected Methods

        #region Private Fields

        private FrameworkCommunication comms = FrameworkCommunication.Instance;

        private PowerDistributionPanel pdb = new PowerDistributionPanel();

        #endregion Private Fields
    }
}