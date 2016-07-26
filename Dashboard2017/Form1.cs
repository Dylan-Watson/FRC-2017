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
    using NetworkTables;
    using NetworkTables.Tables;
    using System;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        private readonly FeedHandler feedHandler;

        public Form1()
        {
            InitializeComponent();
            NetworkTable.SetClientMode();
            NetworkTable.SetIPAddress("127.0.0.1");
            NetworkTable.GetTable("DADHBOARD_2017").AddTableListener(new TableActivityListener(), true);

            #region CV_ImageBox_Setup

            mainVideoBox.SizeMode = PictureBoxSizeMode.StretchImage;
            compositVideoBox.SizeMode = PictureBoxSizeMode.StretchImage;
            mainVideoBox.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
            compositVideoBox.FunctionalMode = ImageBox.FunctionalModeOption.Minimum;
            mainVideoBox.Image = CvInvoke.Imread(@"defaultFeed.jpg", LoadImageType.Color);
            compositVideoBox.Image = CvInvoke.Imread(@"defaultFeed.jpg", LoadImageType.Color);

            #endregion CV_ImageBox_Setup

            KeyPreview = true;
            KeyDown += Form1_KeyPress;
            feedHandler = new FeedHandler(0, mainVideoBox, compositVideoBox);
        }

        private void Form1_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F5)
                feedHandler.Refresh();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e) => feedHandler.Refresh();

        public class TableActivityListener : ITableListener
        {
            public void ValueChanged(ITable source, string key, object value, NotifyFlags flags)
            {
            }
        }
    }
}