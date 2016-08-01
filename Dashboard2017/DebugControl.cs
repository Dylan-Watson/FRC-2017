using System;
using System.Windows.Forms;

namespace Dashboard2017
{

    public class DebugControl : Label
    {
        public string Name { get; private set; }


        public DebugControl(string name)
        {
            Name = name;
        }

        public void UpdateLabel(string value)
        {
            Invoke(new Action(() =>
            {
                Text = value;
            }));
        }
    }
}
