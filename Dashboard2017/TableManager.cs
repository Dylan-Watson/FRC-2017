/****************************** Header ******************************\
Class Name: TableManager [singleton]
Summary: Manages the NetworkTable for the Dashboard
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoftware.co
\********************************************************************/

using NetworkTables;

namespace Dashboard2017
{
    /// <summary>
    ///     Class to manage the NetworkTables
    /// </summary>
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

        /// <summary>
        ///     Method to return the instance of this class, or a new instance if this instance is null
        /// </summary>
        public static TableManager Instance => instance ?? (instance = new TableManager());

        /// <summary>
        ///     The NetworkTable to manage
        /// </summary>
        public NetworkTable Table { get; set; }

        #endregion Public Properties
    }
}