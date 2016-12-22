using System;
using System.Windows.Forms;

namespace Dashboard2017
{
    /// <summary>
    ///     Class to control debugging
    /// </summary>
    public class DebugControl : Label
    {
        #region Public Constructors

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="name">Name that the instantiation of the debugger controller will be set to</param>
        public DebugControl(string name)
        {
            Name = name;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        ///     Name of this debugging controller
        /// </summary>
        public new string Name { get; private set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        ///     Method to update the label of the debugger controller
        /// </summary>
        /// <param name="value">The new label</param>
        public void UpdateLabel(string value)
        {
            Invoke(new Action(() => { Text = value; }));
        }

        #endregion Public Methods
    }
}