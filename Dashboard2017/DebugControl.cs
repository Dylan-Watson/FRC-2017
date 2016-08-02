using System;
using System.Windows.Forms;

namespace Dashboard2017
{
    public class DebugControl : Label
    {
        public DebugControl(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public void UpdateLabel(string value)
        {
            Invoke(new Action(() => { Text = value; }));
        }
    }
}