using NetworkTables;

namespace Dashboard2017
{
    public class TableManager
    {
        private static TableManager instance;

        private TableManager()
        {
        }

        public NetworkTable Table { get; set; }

        public static TableManager Instance => instance ?? (instance = new TableManager());
    }
}