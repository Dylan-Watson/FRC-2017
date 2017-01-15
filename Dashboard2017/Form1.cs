/****************************** Header ******************************\
Class Name:
Summary:
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using Dashboard2017.Properties;
using NetworkTables;
using NetworkTables.Tables;
using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace Dashboard2017
{
    /// <summary>
    ///     Main window of the dashboard
    /// </summary>
    public partial class Form1 : Form
    {
        #region Public Constructors

        /// <summary>
        ///     Constructor
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            #region TARGETING_SETUP

            targetingLabel.BackColor = DefaultBackColor;
            targetingLabel.Text = @"NO TARGET";
            targetingLabel.Enabled = false;

            #endregion TARGETING_SETUP

            KeyPreview = true;
            Shown += Form1_Shown;
            KeyDown += Form1_KeyPress;
            debugControlLayoutPanel.FlowDirection = FlowDirection.TopDown;
        }

        #endregion Public Constructors

        #region Private Fields

        private bool justSet;

        private bool pingSuccess;

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        ///     Updates targetingLabel
        /// </summary>
        public void HasTarget()
        {
            targetingLabel.Invoke(new Action(() =>
            {
                targetingLabel.Enabled = true;
                targetingLabel.BackColor = Color.Red;
                targetingLabel.Text = "TARGET \nVISIBLE";
            }));
        }

        /// <summary>
        ///     Updates targetingLabel
        /// </summary>
        public void NoTarget()
        {
            targetingLabel.Invoke(new Action(() =>
            {
                targetingLabel.BackColor = DefaultBackColor;
                targetingLabel.Text = @"NO TARGET";
                targetingLabel.Enabled = false;
            }));
        }

        /// <summary>
        ///     Updates targetingLabel
        /// </summary>
        public void TargetAquired()
        {
            targetingLabel.Invoke(new Action(() =>
            {
                targetingLabel.Enabled = true;
                targetingLabel.BackColor = Color.LawnGreen;
                targetingLabel.Text = "TARGET \nAQUIRED";
            }));
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        private void Form1_KeyPress(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.F5:
                    break;
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            ConsoleManager.Instance.SetConsoleManager(consoleTextBox);
            setupNetworkTables();
            ConsoleManager.Instance.AppendInfo("Dashboard ready.", Color.Green);
        }

        private void networkTablesInit(string address)
        {
            NetworkTable.Shutdown();
            NetworkTable.SetClientMode();
            NetworkTable.SetIPAddress(address);
            TableManager.Instance.Table = NetworkTable.GetTable("DASHBOARD_2017");
            TableManager.Instance.Table.AddConnectionListener(new NetworkTableLister(this), true);
            TableManager.Instance.Table.AddTableListener(new TableActivityListener(this), true);
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (new Options().ShowDialog() == DialogResult.OK)
                networkTablesInit(Settings.Default.rioAddress);
        }

        private void setupNetworkTables()
        {
            IPAddress ipAddress;
            if (IPAddress.TryParse(Settings.Default.rioAddress, out ipAddress))
                networkTablesInit(Settings.Default.rioAddress);
            else
                ConsoleManager.Instance.AppendError(
                    "No NetworkTables address set, go to 'Options' to set roboRIO address.");
        }

        public void SetRioStatusLight(bool connected)
        {
            rioStatusLight.Invoke(new Action(() =>
            {
                rioStatusLight.BackColor = connected ? Color.Green : Color.Red;
            }));
        }

        public void SetNtRelayStatusLight(bool connected)
        {
            ntRelayStatusLight.Invoke(new Action(() =>
            {
                ntRelayStatusLight.BackColor = connected ? Color.Green : Color.Red;
            }));
        }

        #endregion Private Methods

        #region Private Classes

        private class NetworkTableLister : IRemoteConnectionListener
        {
            public NetworkTableLister(Form1 parent)
            {
                this.parent = parent;
            }

            private readonly Form1 parent;

            #region Public Methods

            public void Connected(IRemote remote, ConnectionInfo info)
            {
                ConsoleManager.Instance.AppendInfo(
                    $"Network tables connected at {info.RemoteIp}. Protocol version: {info.ProtocolVersion}.",
                    Color.Green);
                parent.SetRioStatusLight(true);
            }

            public void Disconnected(IRemote remote, ConnectionInfo info)
            {
                ConsoleManager.Instance.AppendError(
                    $"Network tables disconnected from {info.RemoteIp}. Protocol version: {info.ProtocolVersion}.");
                parent.SetRioStatusLight(false);
                parent.SetNtRelayStatusLight(false);
            }

            #endregion Public Methods
        }

        private class TableActivityListener : ITableListener
        {
            #region Public Constructors

            public TableActivityListener(Form1 parent)
            {
                this.parent = parent;
            }

            #endregion Public Constructors

            #region Public Methods


            public void ValueChanged(ITable source, string key, Value value, NotifyFlags flags)
            {
                /*if (key == "AUTONS")
                   parent.UpdateAutonList(source.GetStringArray(key));*/

                if (key == "ROBOT_STATE")
                {
                    var newState = source.GetValue(key).ToString();
                    if (currentState != newState)
                    {
                        parent.debugControlLayoutPanel.Invoke(
                            new Action(() => parent.debugControlLayoutPanel.Controls.Clear()));

                        parent.messageGroup.Invoke(new Action(() =>
                        {
                            parent.messageGroup.Text = newState;
                            parent.messageGroup.Controls.Clear();
                            parent.messageGroup.Refresh();
                        }));
                    }
                    currentState = newState;
                }
                else if (key.StartsWith(@"HEALTH_"))
                {
                }
                else if (key == @"TARGET")
                {
                    if (source.GetBoolean(@"TARGET"))
                        parent.TargetAquired();
                    else
                        parent.NoTarget();
                }
                else if (key == @"NTRELAY_CONNECTION")
                {
                    parent.SetNtRelayStatusLight(source.GetBoolean(@"NTRELAY_CONNECTION"));
                }

                parent.debugControlLayoutPanel.Invoke(new Action(() =>
                {
                    var controls =
                        parent.debugControlLayoutPanel.Controls.OfType<DebugControl>()
                            .Select(control => control)
                            .ToList();

                    if ((key.Split('_')[0] == "AUTON") || (key.Split('_')[0] == "TELEOP"))
                        if (controls.All(c => c.Name != key))
                        {
                            var tmp = new DebugControl(key);
                            parent.debugControlLayoutPanel.Controls.Add(tmp);
                            tmp.UpdateLabel($"{key.Substring(6)}: {source.GetValue(key)}");
                        }
                        else
                        {
                            var control = controls.FirstOrDefault(c => c.Name == key);
                            control?.UpdateLabel($"{key.Substring(6)}: {source.GetValue(key)}");
                        }
                }));
            }

            #endregion Public Methods

            #region Private Fields

            private readonly Form1 parent;

            private string currentState;

            #endregion Private Fields
        }

        #endregion Private Classes
    }
}