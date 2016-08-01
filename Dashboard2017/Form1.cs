/****************************** Header ******************************\
Class Name:
Summary:
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

namespace Dashboard2017
{
    using Emgu.CV;
    using Emgu.CV.CvEnum;
    using Emgu.CV.UI;
    using System;
    using System.ComponentModel;
    using NetworkTables;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Forms;
    using NetworkTables.Tables;

    /// <summary>
    /// Main window of the dashboard
    /// </summary>
    public partial class Form1 : Form
    {
        private bool pingSuccess;
        private AbortableBackgroundWorker bw;
        private FeedHandler feedHandler;

        /// <summary>
        /// Constructor
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            #region CV_ImageBox_Setup

            mainVideoBox.SizeMode = PictureBoxSizeMode.StretchImage;
            compositVideoBox.SizeMode = PictureBoxSizeMode.StretchImage;
            mainVideoBox.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
            compositVideoBox.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
            mainVideoBox.Image = CvInvoke.Imread(@"defaultFeed.jpg", LoadImageType.Color);
            compositVideoBox.Image = CvInvoke.Imread(@"defaultFeed.jpg", LoadImageType.Color);

            #endregion CV_ImageBox_Setup

            #region TRACKBAR_SETUP

            lowerHueTrackbar.Minimum = 0;
            lowerHueTrackbar.Maximum = 255;
            lowerSaturationTrackbar.Minimum = 0;
            lowerSaturationTrackbar.Maximum = 255;
            lowerValueTrackbar.Minimum = 0;
            lowerValueTrackbar.Maximum = 255;
            upperHueTrackbar.Minimum = 0;
            upperHueTrackbar.Maximum = 255;
            upperSaturationTrackbar.Minimum = 0;
            upperSaturationTrackbar.Maximum = 255;
            upperValueTrackbar.Minimum = 0;
            upperValueTrackbar.Maximum = 255;

            upperValueTrackbar.Value = (int) Properties.Settings.Default.upperValue;
            upperSaturationTrackbar.Value = (int) Properties.Settings.Default.upperSaturation;
            upperHueTrackbar.Value = (int) Properties.Settings.Default.upperHue;
            lowerValueTrackbar.Value = (int) Properties.Settings.Default.lowerValue;
            lowerSaturationTrackbar.Value = (int) Properties.Settings.Default.lowerSaturation;
            lowerHueTrackbar.Value = (int) Properties.Settings.Default.lowerHue;

            HsvTargetingSettings.Instance.UpperValue = Properties.Settings.Default.upperValue;
            HsvTargetingSettings.Instance.UpperSaturation = Properties.Settings.Default.upperSaturation;
            HsvTargetingSettings.Instance .UpperHue= Properties.Settings.Default.upperHue;
            HsvTargetingSettings.Instance.LowerValue = Properties.Settings.Default.lowerValue;
            HsvTargetingSettings.Instance.LowerSaturation = Properties.Settings.Default.lowerSaturation;
            HsvTargetingSettings.Instance.LowerHue = Properties.Settings.Default.lowerHue;

            lowerHueLabel.Text = lowerHueTrackbar.Value.ToString();
            lowerValueLabel.Text = lowerValueTrackbar.Value.ToString();
            lowerSaturationLabel.Text = lowerSaturationTrackbar.Value.ToString();
            upperHueLabel.Text = upperHueTrackbar.Value.ToString();
            upperValueLabel.Text = upperValueTrackbar.Value.ToString();
            upperSaturationLabel.Text = upperSaturationTrackbar.Value.ToString();

            bindTrackbarEvent();

            #endregion

            #region TARGETING_SETUP

            targetingLabel.BackColor = DefaultBackColor;
            targetingLabel.Text = @"NO TARGET";
            targetingLabel.Enabled = false;
            lowerXBound.Text = Properties.Settings.Default.targetLeftXBound.ToString();
            upperXBound.Text = Properties.Settings.Default.targetRightXBound.ToString();
            lowerRadiusBound.Text = Properties.Settings.Default.targetRadiusLowerBound.ToString();
            upperRadiusBound.Text = Properties.Settings.Default.targetRadiusUpperBound.ToString();
            HsvTargetingSettings.Instance.TargetLeftXBound = Properties.Settings.Default.targetLeftXBound;
            HsvTargetingSettings.Instance.TargetRightXBound = Properties.Settings.Default.targetRightXBound;
            HsvTargetingSettings.Instance.TargetRadiusLowerBound = Properties.Settings.Default.targetRadiusLowerBound;
            HsvTargetingSettings.Instance.TargetRadiusUpperBound = Properties.Settings.Default.targetRadiusUpperBound;

            #endregion

            KeyPreview = true;
            Shown += Form1_Shown;
            Closing += Form1_Closing;
            KeyDown += Form1_KeyPress;
            lockVideoSource.Checked = true;
            lockHSVSettings.Checked = true;
            lockTargetSettings.Checked = true;
            videoSourceTextBox.Text = Properties.Settings.Default.videoSource;
        }

        #region UI_EVENTS

        private void Form1_Shown(object sender, EventArgs e)
        {
            ConsoleManager.Instance.SetConsoleManager(consoleTextBox);
            setupNetworkTables();
            ConsoleManager.Instance.AppendInfo("Dashboard ready.", Color.Green);
            new Thread(createFeedHandler).Start();
        }

        private void Form1_Closing(object sender, CancelEventArgs e)
        {
            VideoWriterManager.Instance.Dispose();
            foreach (var file in new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).GetFiles(@"*.avi"))
                if (file.Length == 0)
                    File.Delete(file.Name);
        }

        private void Form1_KeyPress(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.F5:
                    feedHandler?.Refresh();
                    break;
                case Keys.F7:
                    recordToolStripMenuItem_Click(this, new EventArgs());
                    break;
                case Keys.F8:
                    targetingF8ToolStripMenuItem_Click(this, new EventArgs());
                    break;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e) => feedHandler?.Refresh();

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (new Options().ShowDialog() == DialogResult.OK)
                networkTablesInit(Properties.Settings.Default.rioAddress);
        }

        private void recordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VideoWriterManager.Instance.Record = !VideoWriterManager.Instance.Record;
            tableControl.Controls[0].BackColor = VideoWriterManager.Instance.Record ? Color.Red : Color.Empty;
            recordToolStripMenuItem.BackColor = VideoWriterManager.Instance.Record ? Color.Red : Color.Empty;
        }

        private void targetingF8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (feedHandler == null) return;
            feedHandler.Targeting = !feedHandler.Targeting;
            recordToolStripMenuItem.BackColor = VideoWriterManager.Instance.Record ? Color.Green : Color.Empty;

            if (feedHandler.Targeting) return;
            targetingLabel.BackColor = DefaultBackColor;
            targetingLabel.Text = @"NO TARGET";
            targetingLabel.Enabled = false;
        }

        private void setVideoSource_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.videoSource = videoSourceTextBox.Text;
            Properties.Settings.Default.Save();
            new Thread(createFeedHandler).Start();
        }

        private void lockVideoSource_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var control in videoSettingsGroupBox.Controls)
                if (control != lockVideoSource)
                    ((Control) control).Enabled = !lockVideoSource.Checked;
        }

        private void lockHSVSettings_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var control in hsvSettingsGroupBox.Controls)
                if (control != lockHSVSettings)
                    ((Control) control).Enabled = !lockHSVSettings.Checked;
        }

        private void lockTargetSettings_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var control in targetSettingsGroupBox.Controls)
                if (control != lockTargetSettings)
                    ((Control) control).Enabled = !lockTargetSettings.Checked;
        }

        private void setBounds_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.targetLeftXBound = Convert.ToInt32(lowerXBound.Text);
                Properties.Settings.Default.targetRightXBound = Convert.ToInt32(upperXBound.Text);
                Properties.Settings.Default.targetRadiusLowerBound = Convert.ToInt32(lowerRadiusBound.Text);
                Properties.Settings.Default.targetRadiusUpperBound = Convert.ToInt32(upperRadiusBound.Text);
                HsvTargetingSettings.Instance.TargetLeftXBound = Convert.ToInt32(lowerXBound.Text);
                HsvTargetingSettings.Instance.TargetRightXBound = Convert.ToInt32(upperXBound.Text);
                HsvTargetingSettings.Instance.TargetRadiusLowerBound = Convert.ToInt32(lowerRadiusBound.Text);
                HsvTargetingSettings.Instance.TargetRadiusUpperBound = Convert.ToInt32(upperRadiusBound.Text);
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Check to make sure your inputs are valid integers.\n {ex}", @"Invalid Input");
            }
        }

        private void Trackbar_ValueChanged(object sender, EventArgs e)
        {
            unbindTrackbarEvent();
            Properties.Settings.Default.upperValue = (uint) upperValueTrackbar.Value;
            Properties.Settings.Default.lowerValue = (uint) lowerValueTrackbar.Value;
            Properties.Settings.Default.upperHue = (uint) upperHueTrackbar.Value;
            Properties.Settings.Default.lowerHue = (uint) lowerHueTrackbar.Value;
            Properties.Settings.Default.upperSaturation = (uint) upperSaturationTrackbar.Value;
            Properties.Settings.Default.lowerSaturation = (uint) lowerSaturationTrackbar.Value;
            HsvTargetingSettings.Instance.UpperValue = (uint)upperValueTrackbar.Value;
            HsvTargetingSettings.Instance.UpperSaturation = (uint)upperSaturationTrackbar.Value;
            HsvTargetingSettings.Instance.UpperHue = (uint)upperHueTrackbar.Value;
            HsvTargetingSettings.Instance.LowerValue = (uint)lowerValueTrackbar.Value;
            HsvTargetingSettings.Instance.LowerSaturation = (uint)lowerSaturationTrackbar.Value;
            HsvTargetingSettings.Instance.LowerHue = (uint)lowerHueTrackbar.Value;
            Properties.Settings.Default.Save();

            lowerHueLabel.Text = lowerHueTrackbar.Value.ToString();
            lowerValueLabel.Text = lowerValueTrackbar.Value.ToString();
            lowerSaturationLabel.Text = lowerSaturationTrackbar.Value.ToString();
            upperHueLabel.Text = upperHueTrackbar.Value.ToString();
            upperValueLabel.Text = upperValueTrackbar.Value.ToString();
            upperSaturationLabel.Text = upperSaturationTrackbar.Value.ToString();

            bindTrackbarEvent();
        }

        #endregion

        #region Utilities

        private void bindTrackbarEvent()
        {
            lowerHueTrackbar.ValueChanged += Trackbar_ValueChanged;
            lowerSaturationTrackbar.ValueChanged += Trackbar_ValueChanged;
            lowerValueTrackbar.ValueChanged += Trackbar_ValueChanged;
            upperHueTrackbar.ValueChanged += Trackbar_ValueChanged;
            upperSaturationTrackbar.ValueChanged += Trackbar_ValueChanged;
            upperValueTrackbar.ValueChanged += Trackbar_ValueChanged;
        }

        private void unbindTrackbarEvent()
        {
            lowerHueTrackbar.ValueChanged -= Trackbar_ValueChanged;
            lowerSaturationTrackbar.ValueChanged -= Trackbar_ValueChanged;
            lowerValueTrackbar.ValueChanged -= Trackbar_ValueChanged;
            upperHueTrackbar.ValueChanged -= Trackbar_ValueChanged;
            upperSaturationTrackbar.ValueChanged -= Trackbar_ValueChanged;
            upperValueTrackbar.ValueChanged -= Trackbar_ValueChanged;
        }

        private void createFeedHandler()
        {
            justSet = false;
            feedHandler?.Dispose();
            feedHandler = null;

            mainVideoBox.Invoke(new Action(() =>
            {
                mainVideoBox.Image = CvInvoke.Imread(@"defaultFeed.jpg", LoadImageType.Color);
                mainVideoBox.Refresh();
            }));

            mainVideoBox.Invoke(new Action(() =>
            {
                compositVideoBox.Image = CvInvoke.Imread(@"defaultFeed.jpg", LoadImageType.Color);
                compositVideoBox.Refresh();
            }));

            try
            {
                var source = Convert.ToInt32(Properties.Settings.Default.videoSource);
                feedHandler = new FeedHandler(source, mainVideoBox, compositVideoBox, this);
                ConsoleManager.Instance.AppendInfo("FeedHandler created for source at : " + source);
                feedHandler.Targeting = true;
            }
            catch(Exception)
            {
                // ignored
            }

            if (Properties.Settings.Default.videoSource.Contains("://"))
            {
                if ((bw != null) && bw.IsBusy)
                {
                    bw.Abort();
                    bw.Dispose();
                    bw = null;
                }

                bw = new AbortableBackgroundWorker();
                bw.DoWork += Bw_DoWork;
                bw.RunWorkerCompleted += Bw_RunWorkerCompleted;
                bw.RunWorkerAsync();
            }
            else
            {
                ConsoleManager.Instance.AppendError(
                    $"Error creating a feed handler for {Properties.Settings.Default.videoSource}. Check to make sure the source is valid.");
            }
        }

        private bool justSet;
        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!pingSuccess || (feedHandler!=null) || justSet) return;
            justSet = true;
            feedHandler = new FeedHandler(Properties.Settings.Default.videoSource, mainVideoBox, compositVideoBox, this);
            ConsoleManager.Instance.AppendInfo("FeedHandler created for source at : " +
                                               Properties.Settings.Default.videoSource);
            feedHandler.Targeting = true;
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var timeout = 25;
            var match = Regex.Match(Properties.Settings.Default.videoSource, @"\b(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})\b");
            ConsoleManager.Instance.AppendInfo("Attempting to contact http://" + match.Captures[0]);

            var pingSender = new Ping();
            var reply = pingSender.Send(IPAddress.Parse(match.Value));

            while ((reply != null) && (reply.Status != IPStatus.Success) && (timeout > 0))
            {
                ConsoleManager.Instance.AppendError($"Failed to ping {match.Value}, trying {timeout} more times.");
                timeout--;
                reply = pingSender.Send(IPAddress.Parse(match.Value));
            }

            if ((reply != null) && (reply.Status == IPStatus.Success))
            {
                pingSuccess = true;
                return;
            }

            pingSuccess = false;
            ConsoleManager.Instance.AppendError(
                $"Failed to ping {match.Value}, canceling operation. Check video source.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius"></param>
        public void UpdateTargetLabels(int x, int radius)
        {
            xValueLabel.Invoke(new Action(() =>
            {
                xValueLabel.Text = $"Current X: {x}";
            }));

            radiusValueLabel.Invoke(new Action(() =>
            {
                radiusValueLabel.Text = $"Current Radius: {radius}";
            }));
        }

        /// <summary>
        /// Updates targetingLabel
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
        /// Updates targetingLabel
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
        /// Updates targetingLabel
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

        #endregion

        #region NetworkTables_INIT

        private void setupNetworkTables()
        {
            IPAddress ipAddress;
            if (IPAddress.TryParse(Properties.Settings.Default.rioAddress, out ipAddress))
                networkTablesInit(Properties.Settings.Default.rioAddress);
            else
                ConsoleManager.Instance.AppendError(
                    "No NetworkTables address set, go to 'Options' to set roboRIO address.");
        }

        private void networkTablesInit(string address)
        {
            NetworkTable.Shutdown();
            NetworkTable.SetClientMode();
            NetworkTable.SetIPAddress(address);
            TableManager.Instance.Table = NetworkTable.GetTable("DASHBOARD_2017");
            TableManager.Instance.Table.AddConnectionListener(new NetworkTableLister(), true);
            TableManager.Instance.Table.AddTableListener(new TableActivityListener(this), true);
        }

        #endregion

        #region NetworkTables_LISTENERS

        private class TableActivityListener : ITableListener
        {
            private Form1 parent;

            public TableActivityListener(Form1 parent)
            {
                this.parent = parent;
            }

            public void ValueChanged(ITable source, string key, object value, NotifyFlags flags)
            {
                /*if (key == "AUTONS")
                    parent.UpdateAutonList(source.GetStringArray(key));*/
            }
        }

        private class NetworkTableLister : IRemoteConnectionListener
        {
            public void Connected(IRemote remote, ConnectionInfo info)
            {
                ConsoleManager.Instance.AppendInfo(
                    $"Network tables connected at {info.RemoteName}. Protocol version: {info.ProtocolVersion}.",
                    Color.Green);
            }

            public void Disconnected(IRemote remote, ConnectionInfo info)
            {
                ConsoleManager.Instance.AppendError(
                    $"Network tables disconnected from {info.RemoteName}. Protocol version: {info.ProtocolVersion}.");
            }
        }

        #endregion

    }
}