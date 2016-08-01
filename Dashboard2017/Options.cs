using System;
using System.Windows.Forms;

namespace Dashboard2017
{
    using System.Net;
    using System.Text.RegularExpressions;

    public partial class Options : Form
    {
        private string rioIP;

        public Options()
        {
            InitializeComponent();
            overrideAddress.CheckedChanged += OverrideAddress_CheckedChanged;
            overrideAddress.Checked = false;
            OverrideAddress_CheckedChanged(this, new EventArgs());

            IPAddress ipAddress;
            if (IPAddress.TryParse(Properties.Settings.Default.rioAddress, out ipAddress))
                addressBox.Text = Properties.Settings.Default.rioAddress;
        }

        private void OverrideAddress_CheckedChanged(object sender, EventArgs e)
        {
            addressBox.ReadOnly = !overrideAddress.Checked;
            teamNumber.Enabled = !overrideAddress.Checked;
            setTeamNumber.Enabled = !overrideAddress.Checked;
        }

        private void setTeamNumber_Click(object sender, EventArgs e)
        {
            if (Regex.IsMatch(teamNumber.Text, "[0-9]") && (teamNumber.Text.Length < 5) &&
                (teamNumber.Text.Length >= 2))
            {
                switch (teamNumber.Text.Length)
                {
                    case 2:
                        rioIP = $"10.{teamNumber.Text[0]}.{teamNumber.Text[1]}.2";
                        break;
                    case 3:
                        rioIP = $"10.{teamNumber.Text[0]}{teamNumber.Text[1]}.{teamNumber.Text[2]}.2";
                        break;
                    case 4:
                        rioIP = $"10.{teamNumber.Text[0]}{teamNumber.Text[1]}.{teamNumber.Text[2]}{teamNumber.Text[3]}.2";
                        break;
                }

                addressBox.Text = rioIP;
            }
            else
            {
                MessageBox.Show($"The value {teamNumber.Text} is not a valid team number.", @"Invalid Input");
            }
        }

        private void ok_Click(object sender, EventArgs e)
        {
            IPAddress ipAddress;
            if (IPAddress.TryParse(addressBox.Text, out ipAddress))
            {
                Properties.Settings.Default.rioAddress = addressBox.Text;
                Properties.Settings.Default.Save();
                DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show(@"The RIO address is not a valid IPv4 or IPv6 address.", @"Invalid Input");
        }
    }
}
