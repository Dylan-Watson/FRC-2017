using System;
using System.Windows.Forms;

namespace Dashboard2017
{
    public class DebugControl : Label
    {
        #region Public Constructors

        public DebugControl(string name)
        {
            Name = name;
        }

        #endregion Public Constructors

        #region Public Properties

        public new string Name { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public void UpdateLabel(string value)
        {
            Invoke(new Action(() => { Text = value; }));
        }

        #endregion Public Methods
    }
}