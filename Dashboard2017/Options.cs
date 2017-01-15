using Dashboard2017.Properties;
using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Dashboard2017
{
    /// <summary>
    ///     Class to manage the Options
    /// </summary>
    public partial class Options : Form
    {
        #region Private Fields

        private string rioIp;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///     Constructor
        /// </summary>
        public Options()
        {
            InitializeComponent();
            overrideAddress.CheckedChanged += OverrideAddress_CheckedChanged;
            overrideAddress.Checked = false;
            OverrideAddress_CheckedChanged(this, new EventArgs());

            IPAddress ipAddress;
            if (IPAddress.TryParse(Settings.Default.rioAddress, out ipAddress))
                addressBox.Text = Settings.Default.rioAddress;
        }

        #endregion Public Constructors

        #region Private Methods

        private void ok_Click(object sender, EventArgs e)
        {
            IPAddress ipAddress;
            if (IPAddress.TryParse(addressBox.Text, out ipAddress))
            {
                Settings.Default.rioAddress = addressBox.Text;
                Settings.Default.Save();
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(@"The RIO address is not a valid IPv4 or IPv6 address.", @"Invalid Input");
            }
        }

        private void OverrideAddress_CheckedChanged(object sender, EventArgs e)
        {
            addressBox.ReadOnly = !overrideAddress.Checked;
            teamNumber.Enabled = !overrideAddress.Checked;
            setTeamNumber.Enabled = !overrideAddress.Checked;
        }

        private void setTeamNumber_Click(object sender, EventArgs e)
        {
            if (Regex.IsMatch(teamNumber.Text, "[0-9]") && teamNumber.Text.Length < 5 &&
                teamNumber.Text.Length >= 2)
            {
                switch (teamNumber.Text.Length)
                {
                    case 2:
                        rioIp = $"10.{teamNumber.Text[0]}.{teamNumber.Text[1]}.2";
                        break;

                    case 3:
                        rioIp = $"10.{teamNumber.Text[0]}{teamNumber.Text[1]}.{teamNumber.Text[2]}.2";
                        break;

                    case 4:
                        rioIp =
                            $"10.{teamNumber.Text[0]}{teamNumber.Text[1]}.{teamNumber.Text[2]}{teamNumber.Text[3]}.2";
                        break;
                }

                addressBox.Text = rioIp;
            }
            else
            {
                MessageBox.Show($@"The value {teamNumber.Text} is not a valid team number.", @"Invalid Input");
            }
        }

        #endregion Private Methods
    }
}