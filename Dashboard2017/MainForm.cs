﻿/****************************** Header ******************************\
Class Name:
Summary:
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using Dashboard2017.Properties;
using MjpegProcessor;
using NetworkTables;
using NetworkTables.Tables;
using System;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Dashboard2017
{
    /// <summary>
    ///     Main window of the dashboard
    /// </summary>
    public partial class MainForm : Form
    {
        #region Private Fields

        private MjpegDecoder m;

        private string healthFile = null;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///     Constructor
        /// </summary>
        public MainForm()
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
            autonCombo.GotFocus += AutonCombo_GotFocus;
            autonCombo.LostFocus += AutonCombo_LostFocus;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            toggleFeed.BackColor = Color.Red;
            pictureBox1.ImageLocation = @"default.jpg";
        }

        #endregion Public Constructors

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
        /// </summary>
        /// <param name="connected"></param>
        public void SetNtRelayStatusLight(bool connected)
        {
            ntRelayStatusLight.Invoke(
                new Action(() => { ntRelayStatusLight.BackColor = connected ? Color.Green : Color.Red; }));
        }

        /// <summary>
        /// </summary>
        /// <param name="connected"></param>
        public void SetRioStatusLight(bool connected)
        {
            rioStatusLight.Invoke(new Action(() => { rioStatusLight.BackColor = connected ? Color.Green : Color.Red; }));
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

        #endregion Public Methods

        #region Private Methods

        private void AutonCombo_GotFocus(object sender, EventArgs e)
        {
            autonCombo.SelectedIndexChanged += AutonCombo_SelectedIndexChanged;
        }

        private void AutonCombo_LostFocus(object sender, EventArgs e)
        {
            autonCombo.SelectedIndexChanged -= AutonCombo_SelectedIndexChanged;
        }

        private void AutonCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            TableManager.Instance.Table.PutValue(@"AUTON_SELECT", Value.MakeString(autonCombo.SelectedItem.ToString()));
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

        private void M_FrameReady(object sender, FrameReadyEventArgs e)
        {
            pictureBox1.Image = e.Bitmap;
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
            {
                ConsoleManager.Instance.AppendError(
                    "No NetworkTables address set, go to 'Options' to set roboRIO address.");
            }
        }

        private void ToggleFeed_Click(object sender, EventArgs e)
        {
            m.StopStream();
            m.FrameReady -= M_FrameReady;
            toggleFeed.BackColor = Color.Red;
            pictureBox1.ImageLocation = @"default.jpg";
            toggleFeed.Click += toggleFeedToolStripMenuItem1_Click;
            toggleFeed.Click -= ToggleFeed_Click;
        }

        private void toggleFeedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toggleFeed.BackColor = Color.Green;
            m = new MjpegDecoder();
            m.FrameReady += M_FrameReady;
            m.ParseStream(new Uri(@"http://10.34.81.2:1181/stream.mjpg"));
            toggleFeed.Click -= toggleFeedToolStripMenuItem1_Click;
            toggleFeed.Click += ToggleFeed_Click;
        }

        #endregion Private Methods

        #region Private Classes

        private class NetworkTableLister : IRemoteConnectionListener
        {
            #region Private Fields

            private readonly MainForm parent;

            #endregion Private Fields

            #region Public Constructors

            public NetworkTableLister(MainForm parent)
            {
                this.parent = parent;
            }

            #endregion Public Constructors

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

            public TableActivityListener(MainForm parent)
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
                        if (newState == @"DISABLED")
                        {
                            parent.healthFile = null;
                        }

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
                else if (key.StartsWith(@"H_") && currentState != @"DISABLED" && currentState != null)
                {
                    XDocument doc;
                    if (parent.healthFile == null)
                    {
                        if (!Directory.Exists(@"Logs"))
                            Directory.CreateDirectory(@"Logs");
                        parent.healthFile = $@"Logs/{string.Format("Log_{0:dd_MM_yyyy_hh_mm_ss}.xml", DateTime.Now.ToUniversalTime())}";
                        File.Create(parent.healthFile).Close();
                        doc = new XDocument(
                            new XElement("Match", 
                                new XAttribute("date", $"{DateTime.Today.Day}/{DateTime.Today.Month}/{DateTime.Today.Year}"),
                                new XAttribute("time", $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}")));
                    }
                    else
                    {
                        StreamReader read = new StreamReader(parent.healthFile);
                        doc = XDocument.Load(read);
                        read.Close();
                    }
                    doc.Root.Add(new XElement(key, 
                        new XAttribute("value", value.GetDouble())));
                    doc.Save(parent.healthFile);
                }
                else if (key == @"TARGET")
                {
                    if (source.GetBoolean(@"TARGET"))
                        parent.TargetAquired();
                    else
                        parent.NoTarget();
                }
                else if (key == @"NTRELAY_CONNECTION")
                    parent.SetNtRelayStatusLight(source.GetBoolean(@"NTRELAY_CONNECTION"));
                else if (key == @"AUTON_LIST")
                {
                    parent.autonCombo.Invoke(
                        new Action(
                            () => { parent.autonCombo.DataSource = source.GetValue(@"AUTON_LIST").GetObjectValue(); }));
                }
                else if (key == @"MESSAGE")
                    ConsoleManager.Instance.AppendInfo(value.GetString(), Color.Magenta);

                parent.debugControlLayoutPanel.Invoke(new Action(() =>
                {
                    var controls =
                        parent.debugControlLayoutPanel.Controls.OfType<DebugControl>()
                            .Select(control => control)
                            .ToList();

                    if (key.Split('_')[0] == @"AUTON" || key.Split('_')[0] == @"TELEOP" || key.Split('_')[0] == @"DEBUG")
                    {
                        if (controls.All(c => c.Name != key))
                        {
                            var tmp = new DebugControl(key);
                            parent.debugControlLayoutPanel.Controls.Add(tmp);

                            if (source.GetValue(key).Type == NtType.Double)
                            {
                                var val = string.Format("{0:#,0.000}", source.GetNumber(key));
                                tmp.UpdateLabel($"{key.Substring(7)}: {val}");
                            }
                            else
                                tmp.UpdateLabel($"{key.Substring(7)}: {source.GetValue(key)}");
                        }
                        else
                        {
                            var control = controls.FirstOrDefault(c => c.Name == key);

                            if (source.GetValue(key).Type == NtType.Double)
                            {
                                var val = string.Format("{0:#,0.000}", source.GetNumber(key));
                                control?.UpdateLabel($"{key.Substring(7)}: {val}");
                            }
                            else
                                control?.UpdateLabel($"{key.Substring(7)}: {source.GetValue(key)}");
                        }
                    }
                }));
            }

            #endregion Public Methods

            #region Private Fields

            private readonly MainForm parent;

            private string currentState;

            #endregion Private Fields
        }

        #endregion Private Classes
    }
}