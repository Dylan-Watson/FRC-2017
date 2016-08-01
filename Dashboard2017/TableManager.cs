namespace Dashboard2017
{
    using NetworkTables;

    public class TableManager
    {
        public NetworkTable Table { get; set; }

        private TableManager()
        {
        }

        private static TableManager instance;

        public static TableManager Instance => instance ?? (instance = new TableManager());
    }
}
