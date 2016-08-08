using NetworkTables;

namespace Dashboard2017
{
    public class TableManager
    {
        #region Private Fields

        private static TableManager instance;

        #endregion Private Fields

        #region Private Constructors

        private TableManager()
        {
        }

        #endregion Private Constructors

        #region Public Properties

        public static TableManager Instance => instance ?? (instance = new TableManager());
        public NetworkTable Table { get; set; }

        #endregion Public Properties
    }
}