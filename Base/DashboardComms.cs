using System;

namespace Base
{
    using NetworkTables;

    public sealed class DashboardComms
    {
        private static readonly Lazy<DashboardComms> _lazy =
        new Lazy<DashboardComms>(() => new DashboardComms());

        public static DashboardComms Instance => _lazy.Value;
        private readonly NetworkTable dashboard = NetworkTable.GetTable(Constants.PRIMARY_NETWORK_TABLE);

        private DashboardComms()
        {
        }

        public void NotifyRobotState(object value)
        {
            dashboard.PutValue(@"ROBOT_STATE", value);
        }

        public void SendData(string key, object value)
        {
            if (LoopCheck._IsTeleoporated())
                dashboard.PutValue($"TELEOP_{key}", value);
            else if (LoopCheck._IsAutonomous())
                dashboard.PutValue($"AUTON_{key}", value);
            else
                dashboard.PutValue($"{key}", value);
        }

    }
}
