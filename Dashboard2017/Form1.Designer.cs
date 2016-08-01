using System.ComponentModel;
using System.Windows.Forms;
using Emgu.CV.UI;

namespace Dashboard2017
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                if(feedHandler != null)
                feedHandler.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.targetingF8ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.infoPanel = new System.Windows.Forms.TabControl();
            this.infoTab = new System.Windows.Forms.TabPage();
            this.targetingLabel = new System.Windows.Forms.Label();
            this.autonCombo = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.videoSettingsTab = new System.Windows.Forms.TabPage();
            this.targetSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.setBounds = new System.Windows.Forms.Button();
            this.upperRadiusBound = new System.Windows.Forms.TextBox();
            this.upperXBound = new System.Windows.Forms.TextBox();
            this.lowerRadiusBound = new System.Windows.Forms.TextBox();
            this.lowerXBound = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lockTargetSettings = new System.Windows.Forms.CheckBox();
            this.radiusValueLabel = new System.Windows.Forms.Label();
            this.xValueLabel = new System.Windows.Forms.Label();
            this.hsvSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.upperValueLabel = new System.Windows.Forms.Label();
            this.upperSaturationLabel = new System.Windows.Forms.Label();
            this.upperHueLabel = new System.Windows.Forms.Label();
            this.lockHSVSettings = new System.Windows.Forms.CheckBox();
            this.lowerValueLabel = new System.Windows.Forms.Label();
            this.lowerSaturationLabel = new System.Windows.Forms.Label();
            this.lowerHueLabel = new System.Windows.Forms.Label();
            this.lowerSaturationTrackbar = new System.Windows.Forms.TrackBar();
            this.upperValueTrackbar = new System.Windows.Forms.TrackBar();
            this.upperSaturationTrackbar = new System.Windows.Forms.TrackBar();
            this.upperHueTrackbar = new System.Windows.Forms.TrackBar();
            this.lowerValueTrackbar = new System.Windows.Forms.TrackBar();
            this.lowerHueTrackbar = new System.Windows.Forms.TrackBar();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.videoSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.setVideoSource = new System.Windows.Forms.Button();
            this.videoSourceTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lockVideoSource = new System.Windows.Forms.CheckBox();
            this.tableControl = new System.Windows.Forms.TabControl();
            this.videoMain = new System.Windows.Forms.TabPage();
            this.mainVideoBox = new Emgu.CV.UI.ImageBox();
            this.videoComposit = new System.Windows.Forms.TabPage();
            this.compositVideoBox = new Emgu.CV.UI.ImageBox();
            this.consoleTab = new System.Windows.Forms.TabPage();
            this.consoleTextBox = new System.Windows.Forms.RichTextBox();
            this.debugTab = new System.Windows.Forms.TabPage();
            this.debugControlLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.infoPanel.SuspendLayout();
            this.infoTab.SuspendLayout();
            this.videoSettingsTab.SuspendLayout();
            this.targetSettingsGroupBox.SuspendLayout();
            this.hsvSettingsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lowerSaturationTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upperValueTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upperSaturationTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upperHueTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowerValueTrackbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowerHueTrackbar)).BeginInit();
            this.videoSettingsGroupBox.SuspendLayout();
            this.tableControl.SuspendLayout();
            this.videoMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainVideoBox)).BeginInit();
            this.videoComposit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.compositVideoBox)).BeginInit();
            this.consoleTab.SuspendLayout();
            this.debugTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.videoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // videoToolStripMenuItem
            // 
            this.videoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.recordToolStripMenuItem,
            this.refreshToolStripMenuItem,
            this.targetingF8ToolStripMenuItem});
            this.videoToolStripMenuItem.Name = "videoToolStripMenuItem";
            this.videoToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.videoToolStripMenuItem.Text = "Video";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // recordToolStripMenuItem
            // 
            this.recordToolStripMenuItem.Name = "recordToolStripMenuItem";
            this.recordToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.recordToolStripMenuItem.Text = "Record [F7]";
            this.recordToolStripMenuItem.Click += new System.EventHandler(this.recordToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.refreshToolStripMenuItem.Text = "Refresh [F5]";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // targetingF8ToolStripMenuItem
            // 
            this.targetingF8ToolStripMenuItem.Name = "targetingF8ToolStripMenuItem";
            this.targetingF8ToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.targetingF8ToolStripMenuItem.Text = "Targeting [F8]";
            this.targetingF8ToolStripMenuItem.Click += new System.EventHandler(this.targetingF8ToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.infoPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableControl);
            this.splitContainer1.Size = new System.Drawing.Size(1008, 451);
            this.splitContainer1.SplitterDistance = 490;
            this.splitContainer1.TabIndex = 1;
            // 
            // infoPanel
            // 
            this.infoPanel.Controls.Add(this.infoTab);
            this.infoPanel.Controls.Add(this.videoSettingsTab);
            this.infoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoPanel.Location = new System.Drawing.Point(0, 0);
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.SelectedIndex = 0;
            this.infoPanel.Size = new System.Drawing.Size(490, 451);
            this.infoPanel.TabIndex = 0;
            // 
            // infoTab
            // 
            this.infoTab.Controls.Add(this.targetingLabel);
            this.infoTab.Controls.Add(this.autonCombo);
            this.infoTab.Controls.Add(this.label12);
            this.infoTab.Location = new System.Drawing.Point(4, 22);
            this.infoTab.Name = "infoTab";
            this.infoTab.Padding = new System.Windows.Forms.Padding(3);
            this.infoTab.Size = new System.Drawing.Size(482, 425);
            this.infoTab.TabIndex = 0;
            this.infoTab.Text = "Info";
            this.infoTab.UseVisualStyleBackColor = true;
            // 
            // targetingLabel
            // 
            this.targetingLabel.AutoSize = true;
            this.targetingLabel.Font = new System.Drawing.Font("Consolas", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.targetingLabel.Location = new System.Drawing.Point(240, 14);
            this.targetingLabel.Name = "targetingLabel";
            this.targetingLabel.Size = new System.Drawing.Size(215, 37);
            this.targetingLabel.TabIndex = 2;
            this.targetingLabel.Text = "TARGET: N/A";
            // 
            // autonCombo
            // 
            this.autonCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.autonCombo.FormattingEnabled = true;
            this.autonCombo.Location = new System.Drawing.Point(52, 17);
            this.autonCombo.Name = "autonCombo";
            this.autonCombo.Size = new System.Drawing.Size(139, 21);
            this.autonCombo.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(38, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Auton:";
            // 
            // videoSettingsTab
            // 
            this.videoSettingsTab.AutoScroll = true;
            this.videoSettingsTab.Controls.Add(this.targetSettingsGroupBox);
            this.videoSettingsTab.Controls.Add(this.hsvSettingsGroupBox);
            this.videoSettingsTab.Controls.Add(this.videoSettingsGroupBox);
            this.videoSettingsTab.Location = new System.Drawing.Point(4, 22);
            this.videoSettingsTab.Name = "videoSettingsTab";
            this.videoSettingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.videoSettingsTab.Size = new System.Drawing.Size(482, 425);
            this.videoSettingsTab.TabIndex = 1;
            this.videoSettingsTab.Text = "Video Settings";
            this.videoSettingsTab.UseVisualStyleBackColor = true;
            // 
            // targetSettingsGroupBox
            // 
            this.targetSettingsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.targetSettingsGroupBox.Controls.Add(this.setBounds);
            this.targetSettingsGroupBox.Controls.Add(this.upperRadiusBound);
            this.targetSettingsGroupBox.Controls.Add(this.upperXBound);
            this.targetSettingsGroupBox.Controls.Add(this.lowerRadiusBound);
            this.targetSettingsGroupBox.Controls.Add(this.lowerXBound);
            this.targetSettingsGroupBox.Controls.Add(this.label11);
            this.targetSettingsGroupBox.Controls.Add(this.label10);
            this.targetSettingsGroupBox.Controls.Add(this.label9);
            this.targetSettingsGroupBox.Controls.Add(this.label7);
            this.targetSettingsGroupBox.Controls.Add(this.lockTargetSettings);
            this.targetSettingsGroupBox.Controls.Add(this.radiusValueLabel);
            this.targetSettingsGroupBox.Controls.Add(this.xValueLabel);
            this.targetSettingsGroupBox.Location = new System.Drawing.Point(8, 488);
            this.targetSettingsGroupBox.Name = "targetSettingsGroupBox";
            this.targetSettingsGroupBox.Size = new System.Drawing.Size(434, 223);
            this.targetSettingsGroupBox.TabIndex = 2;
            this.targetSettingsGroupBox.TabStop = false;
            this.targetSettingsGroupBox.Text = "Target Settings";
            // 
            // setBounds
            // 
            this.setBounds.Location = new System.Drawing.Point(9, 149);
            this.setBounds.Name = "setBounds";
            this.setBounds.Size = new System.Drawing.Size(75, 23);
            this.setBounds.TabIndex = 30;
            this.setBounds.Text = "Set Bounds";
            this.setBounds.UseVisualStyleBackColor = true;
            this.setBounds.Click += new System.EventHandler(this.setBounds_Click);
            // 
            // upperRadiusBound
            // 
            this.upperRadiusBound.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.upperRadiusBound.Location = new System.Drawing.Point(345, 109);
            this.upperRadiusBound.Name = "upperRadiusBound";
            this.upperRadiusBound.Size = new System.Drawing.Size(83, 20);
            this.upperRadiusBound.TabIndex = 29;
            // 
            // upperXBound
            // 
            this.upperXBound.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.upperXBound.Location = new System.Drawing.Point(345, 68);
            this.upperXBound.Name = "upperXBound";
            this.upperXBound.Size = new System.Drawing.Size(83, 20);
            this.upperXBound.TabIndex = 28;
            // 
            // lowerRadiusBound
            // 
            this.lowerRadiusBound.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lowerRadiusBound.Location = new System.Drawing.Point(124, 109);
            this.lowerRadiusBound.Name = "lowerRadiusBound";
            this.lowerRadiusBound.Size = new System.Drawing.Size(83, 20);
            this.lowerRadiusBound.TabIndex = 27;
            // 
            // lowerXBound
            // 
            this.lowerXBound.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lowerXBound.Location = new System.Drawing.Point(124, 68);
            this.lowerXBound.Name = "lowerXBound";
            this.lowerXBound.Size = new System.Drawing.Size(83, 20);
            this.lowerXBound.TabIndex = 26;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 112);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(109, 13);
            this.label11.TabIndex = 25;
            this.label11.Text = "Lower Radius Bound:";
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(230, 112);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(109, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Upper Radius Bound:";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(230, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "Upper X Bound:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 71);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Lower X Bound:";
            // 
            // lockTargetSettings
            // 
            this.lockTargetSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lockTargetSettings.AutoSize = true;
            this.lockTargetSettings.Location = new System.Drawing.Point(0, 198);
            this.lockTargetSettings.Name = "lockTargetSettings";
            this.lockTargetSettings.Size = new System.Drawing.Size(50, 17);
            this.lockTargetSettings.TabIndex = 19;
            this.lockTargetSettings.Text = "Lock";
            this.lockTargetSettings.UseVisualStyleBackColor = true;
            this.lockTargetSettings.CheckedChanged += new System.EventHandler(this.lockTargetSettings_CheckedChanged);
            // 
            // radiusValueLabel
            // 
            this.radiusValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radiusValueLabel.AutoSize = true;
            this.radiusValueLabel.Location = new System.Drawing.Point(121, 25);
            this.radiusValueLabel.Name = "radiusValueLabel";
            this.radiusValueLabel.Size = new System.Drawing.Size(103, 13);
            this.radiusValueLabel.TabIndex = 21;
            this.radiusValueLabel.Text = "Current Radius: N/A";
            // 
            // xValueLabel
            // 
            this.xValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xValueLabel.AutoSize = true;
            this.xValueLabel.Location = new System.Drawing.Point(6, 25);
            this.xValueLabel.Name = "xValueLabel";
            this.xValueLabel.Size = new System.Drawing.Size(77, 13);
            this.xValueLabel.TabIndex = 19;
            this.xValueLabel.Text = "Current X: N/A";
            // 
            // hsvSettingsGroupBox
            // 
            this.hsvSettingsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hsvSettingsGroupBox.Controls.Add(this.upperValueLabel);
            this.hsvSettingsGroupBox.Controls.Add(this.upperSaturationLabel);
            this.hsvSettingsGroupBox.Controls.Add(this.upperHueLabel);
            this.hsvSettingsGroupBox.Controls.Add(this.lockHSVSettings);
            this.hsvSettingsGroupBox.Controls.Add(this.lowerValueLabel);
            this.hsvSettingsGroupBox.Controls.Add(this.lowerSaturationLabel);
            this.hsvSettingsGroupBox.Controls.Add(this.lowerHueLabel);
            this.hsvSettingsGroupBox.Controls.Add(this.lowerSaturationTrackbar);
            this.hsvSettingsGroupBox.Controls.Add(this.upperValueTrackbar);
            this.hsvSettingsGroupBox.Controls.Add(this.upperSaturationTrackbar);
            this.hsvSettingsGroupBox.Controls.Add(this.upperHueTrackbar);
            this.hsvSettingsGroupBox.Controls.Add(this.lowerValueTrackbar);
            this.hsvSettingsGroupBox.Controls.Add(this.lowerHueTrackbar);
            this.hsvSettingsGroupBox.Controls.Add(this.label8);
            this.hsvSettingsGroupBox.Controls.Add(this.label6);
            this.hsvSettingsGroupBox.Controls.Add(this.label5);
            this.hsvSettingsGroupBox.Controls.Add(this.label4);
            this.hsvSettingsGroupBox.Controls.Add(this.label3);
            this.hsvSettingsGroupBox.Controls.Add(this.label2);
            this.hsvSettingsGroupBox.Location = new System.Drawing.Point(8, 114);
            this.hsvSettingsGroupBox.Name = "hsvSettingsGroupBox";
            this.hsvSettingsGroupBox.Size = new System.Drawing.Size(434, 368);
            this.hsvSettingsGroupBox.TabIndex = 1;
            this.hsvSettingsGroupBox.TabStop = false;
            this.hsvSettingsGroupBox.Text = "HSV Settings";
            // 
            // upperValueLabel
            // 
            this.upperValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.upperValueLabel.AutoSize = true;
            this.upperValueLabel.Location = new System.Drawing.Point(6, 318);
            this.upperValueLabel.Name = "upperValueLabel";
            this.upperValueLabel.Size = new System.Drawing.Size(27, 13);
            this.upperValueLabel.TabIndex = 18;
            this.upperValueLabel.Text = "N/A";
            // 
            // upperSaturationLabel
            // 
            this.upperSaturationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.upperSaturationLabel.AutoSize = true;
            this.upperSaturationLabel.Location = new System.Drawing.Point(6, 267);
            this.upperSaturationLabel.Name = "upperSaturationLabel";
            this.upperSaturationLabel.Size = new System.Drawing.Size(27, 13);
            this.upperSaturationLabel.TabIndex = 17;
            this.upperSaturationLabel.Text = "N/A";
            // 
            // upperHueLabel
            // 
            this.upperHueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.upperHueLabel.AutoSize = true;
            this.upperHueLabel.Location = new System.Drawing.Point(6, 216);
            this.upperHueLabel.Name = "upperHueLabel";
            this.upperHueLabel.Size = new System.Drawing.Size(27, 13);
            this.upperHueLabel.TabIndex = 16;
            this.upperHueLabel.Text = "N/A";
            // 
            // lockHSVSettings
            // 
            this.lockHSVSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lockHSVSettings.AutoSize = true;
            this.lockHSVSettings.Location = new System.Drawing.Point(0, 345);
            this.lockHSVSettings.Name = "lockHSVSettings";
            this.lockHSVSettings.Size = new System.Drawing.Size(50, 17);
            this.lockHSVSettings.TabIndex = 0;
            this.lockHSVSettings.Text = "Lock";
            this.lockHSVSettings.UseVisualStyleBackColor = true;
            this.lockHSVSettings.CheckedChanged += new System.EventHandler(this.lockHSVSettings_CheckedChanged);
            // 
            // lowerValueLabel
            // 
            this.lowerValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lowerValueLabel.AutoSize = true;
            this.lowerValueLabel.Location = new System.Drawing.Point(6, 165);
            this.lowerValueLabel.Name = "lowerValueLabel";
            this.lowerValueLabel.Size = new System.Drawing.Size(27, 13);
            this.lowerValueLabel.TabIndex = 15;
            this.lowerValueLabel.Text = "N/A";
            // 
            // lowerSaturationLabel
            // 
            this.lowerSaturationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lowerSaturationLabel.AutoSize = true;
            this.lowerSaturationLabel.Location = new System.Drawing.Point(6, 114);
            this.lowerSaturationLabel.Name = "lowerSaturationLabel";
            this.lowerSaturationLabel.Size = new System.Drawing.Size(27, 13);
            this.lowerSaturationLabel.TabIndex = 14;
            this.lowerSaturationLabel.Text = "N/A";
            // 
            // lowerHueLabel
            // 
            this.lowerHueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lowerHueLabel.AutoSize = true;
            this.lowerHueLabel.Location = new System.Drawing.Point(6, 63);
            this.lowerHueLabel.Name = "lowerHueLabel";
            this.lowerHueLabel.Size = new System.Drawing.Size(27, 13);
            this.lowerHueLabel.TabIndex = 13;
            this.lowerHueLabel.Text = "N/A";
            // 
            // lowerSaturationTrackbar
            // 
            this.lowerSaturationTrackbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lowerSaturationTrackbar.Location = new System.Drawing.Point(108, 101);
            this.lowerSaturationTrackbar.Name = "lowerSaturationTrackbar";
            this.lowerSaturationTrackbar.Size = new System.Drawing.Size(315, 45);
            this.lowerSaturationTrackbar.TabIndex = 12;
            // 
            // upperValueTrackbar
            // 
            this.upperValueTrackbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.upperValueTrackbar.Location = new System.Drawing.Point(108, 305);
            this.upperValueTrackbar.Name = "upperValueTrackbar";
            this.upperValueTrackbar.Size = new System.Drawing.Size(315, 45);
            this.upperValueTrackbar.TabIndex = 11;
            // 
            // upperSaturationTrackbar
            // 
            this.upperSaturationTrackbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.upperSaturationTrackbar.Location = new System.Drawing.Point(108, 254);
            this.upperSaturationTrackbar.Name = "upperSaturationTrackbar";
            this.upperSaturationTrackbar.Size = new System.Drawing.Size(315, 45);
            this.upperSaturationTrackbar.TabIndex = 10;
            // 
            // upperHueTrackbar
            // 
            this.upperHueTrackbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.upperHueTrackbar.Location = new System.Drawing.Point(108, 203);
            this.upperHueTrackbar.Name = "upperHueTrackbar";
            this.upperHueTrackbar.Size = new System.Drawing.Size(315, 45);
            this.upperHueTrackbar.TabIndex = 9;
            // 
            // lowerValueTrackbar
            // 
            this.lowerValueTrackbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lowerValueTrackbar.Location = new System.Drawing.Point(108, 152);
            this.lowerValueTrackbar.Name = "lowerValueTrackbar";
            this.lowerValueTrackbar.Size = new System.Drawing.Size(315, 45);
            this.lowerValueTrackbar.TabIndex = 8;
            // 
            // lowerHueTrackbar
            // 
            this.lowerHueTrackbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lowerHueTrackbar.Location = new System.Drawing.Point(108, 50);
            this.lowerHueTrackbar.Name = "lowerHueTrackbar";
            this.lowerHueTrackbar.Size = new System.Drawing.Size(315, 45);
            this.lowerHueTrackbar.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 305);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Upper Value:";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 254);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Upper Saturation:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 203);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Upper Hue:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Lower Value:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Lower Saturation:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Lower Hue:";
            // 
            // videoSettingsGroupBox
            // 
            this.videoSettingsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.videoSettingsGroupBox.Controls.Add(this.setVideoSource);
            this.videoSettingsGroupBox.Controls.Add(this.videoSourceTextBox);
            this.videoSettingsGroupBox.Controls.Add(this.label1);
            this.videoSettingsGroupBox.Controls.Add(this.lockVideoSource);
            this.videoSettingsGroupBox.Location = new System.Drawing.Point(8, 6);
            this.videoSettingsGroupBox.Name = "videoSettingsGroupBox";
            this.videoSettingsGroupBox.Size = new System.Drawing.Size(434, 102);
            this.videoSettingsGroupBox.TabIndex = 0;
            this.videoSettingsGroupBox.TabStop = false;
            this.videoSettingsGroupBox.Text = "Video Source";
            // 
            // setVideoSource
            // 
            this.setVideoSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.setVideoSource.Location = new System.Drawing.Point(179, 73);
            this.setVideoSource.Name = "setVideoSource";
            this.setVideoSource.Size = new System.Drawing.Size(72, 23);
            this.setVideoSource.TabIndex = 3;
            this.setVideoSource.Text = "Set Source";
            this.setVideoSource.UseVisualStyleBackColor = true;
            this.setVideoSource.Click += new System.EventHandler(this.setVideoSource_Click);
            // 
            // videoSourceTextBox
            // 
            this.videoSourceTextBox.Location = new System.Drawing.Point(50, 33);
            this.videoSourceTextBox.Name = "videoSourceTextBox";
            this.videoSourceTextBox.Size = new System.Drawing.Size(319, 20);
            this.videoSourceTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Source:";
            // 
            // lockVideoSource
            // 
            this.lockVideoSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lockVideoSource.AutoSize = true;
            this.lockVideoSource.Location = new System.Drawing.Point(0, 77);
            this.lockVideoSource.Name = "lockVideoSource";
            this.lockVideoSource.Size = new System.Drawing.Size(50, 17);
            this.lockVideoSource.TabIndex = 0;
            this.lockVideoSource.Text = "Lock";
            this.lockVideoSource.UseVisualStyleBackColor = true;
            this.lockVideoSource.CheckedChanged += new System.EventHandler(this.lockVideoSource_CheckedChanged);
            // 
            // tableControl
            // 
            this.tableControl.Controls.Add(this.videoMain);
            this.tableControl.Controls.Add(this.videoComposit);
            this.tableControl.Controls.Add(this.consoleTab);
            this.tableControl.Controls.Add(this.debugTab);
            this.tableControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableControl.Location = new System.Drawing.Point(0, 0);
            this.tableControl.Name = "tableControl";
            this.tableControl.SelectedIndex = 0;
            this.tableControl.Size = new System.Drawing.Size(514, 451);
            this.tableControl.TabIndex = 0;
            // 
            // videoMain
            // 
            this.videoMain.Controls.Add(this.mainVideoBox);
            this.videoMain.Location = new System.Drawing.Point(4, 22);
            this.videoMain.Name = "videoMain";
            this.videoMain.Padding = new System.Windows.Forms.Padding(3);
            this.videoMain.Size = new System.Drawing.Size(506, 425);
            this.videoMain.TabIndex = 0;
            this.videoMain.Text = "Main";
            this.videoMain.UseVisualStyleBackColor = true;
            // 
            // mainVideoBox
            // 
            this.mainVideoBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainVideoBox.Location = new System.Drawing.Point(3, 3);
            this.mainVideoBox.Name = "mainVideoBox";
            this.mainVideoBox.Size = new System.Drawing.Size(500, 419);
            this.mainVideoBox.TabIndex = 2;
            this.mainVideoBox.TabStop = false;
            // 
            // videoComposit
            // 
            this.videoComposit.Controls.Add(this.compositVideoBox);
            this.videoComposit.Location = new System.Drawing.Point(4, 22);
            this.videoComposit.Name = "videoComposit";
            this.videoComposit.Padding = new System.Windows.Forms.Padding(3);
            this.videoComposit.Size = new System.Drawing.Size(506, 425);
            this.videoComposit.TabIndex = 1;
            this.videoComposit.Text = "Composit";
            this.videoComposit.UseVisualStyleBackColor = true;
            // 
            // compositVideoBox
            // 
            this.compositVideoBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compositVideoBox.Location = new System.Drawing.Point(3, 3);
            this.compositVideoBox.Name = "compositVideoBox";
            this.compositVideoBox.Size = new System.Drawing.Size(500, 419);
            this.compositVideoBox.TabIndex = 2;
            this.compositVideoBox.TabStop = false;
            // 
            // consoleTab
            // 
            this.consoleTab.Controls.Add(this.consoleTextBox);
            this.consoleTab.Location = new System.Drawing.Point(4, 22);
            this.consoleTab.Name = "consoleTab";
            this.consoleTab.Size = new System.Drawing.Size(506, 425);
            this.consoleTab.TabIndex = 2;
            this.consoleTab.Text = "Console";
            this.consoleTab.UseVisualStyleBackColor = true;
            // 
            // consoleTextBox
            // 
            this.consoleTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.consoleTextBox.Location = new System.Drawing.Point(0, 0);
            this.consoleTextBox.Name = "consoleTextBox";
            this.consoleTextBox.Size = new System.Drawing.Size(506, 425);
            this.consoleTextBox.TabIndex = 0;
            this.consoleTextBox.Text = "";
            // 
            // debugTab
            // 
            this.debugTab.Controls.Add(this.debugControlLayoutPanel);
            this.debugTab.Location = new System.Drawing.Point(4, 22);
            this.debugTab.Name = "debugTab";
            this.debugTab.Size = new System.Drawing.Size(506, 425);
            this.debugTab.TabIndex = 3;
            this.debugTab.Text = "Debug";
            this.debugTab.UseVisualStyleBackColor = true;
            // 
            // debugControlLayoutPanel
            // 
            this.debugControlLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.debugControlLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.debugControlLayoutPanel.Name = "debugControlLayoutPanel";
            this.debugControlLayoutPanel.Size = new System.Drawing.Size(506, 425);
            this.debugControlLayoutPanel.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 475);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Dahsboard 2017";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.infoPanel.ResumeLayout(false);
            this.infoTab.ResumeLayout(false);
            this.infoTab.PerformLayout();
            this.videoSettingsTab.ResumeLayout(false);
            this.targetSettingsGroupBox.ResumeLayout(false);
            this.targetSettingsGroupBox.PerformLayout();
            this.hsvSettingsGroupBox.ResumeLayout(false);
            this.hsvSettingsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lowerSaturationTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upperValueTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upperSaturationTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upperHueTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowerValueTrackbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowerHueTrackbar)).EndInit();
            this.videoSettingsGroupBox.ResumeLayout(false);
            this.videoSettingsGroupBox.PerformLayout();
            this.tableControl.ResumeLayout(false);
            this.videoMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainVideoBox)).EndInit();
            this.videoComposit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.compositVideoBox)).EndInit();
            this.consoleTab.ResumeLayout(false);
            this.debugTab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private SplitContainer splitContainer1;
        private TabControl tableControl;
        private TabPage videoMain;
        private TabPage videoComposit;
        private ImageBox mainVideoBox;
        private ImageBox compositVideoBox;
        private ToolStripMenuItem videoToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem recordToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem refreshToolStripMenuItem;
        private ToolStripMenuItem targetingF8ToolStripMenuItem;
        private TabPage consoleTab;
        private RichTextBox consoleTextBox;
        private TabControl infoPanel;
        private TabPage infoTab;
        private TabPage videoSettingsTab;
        private TabPage debugTab;
        private GroupBox hsvSettingsGroupBox;
        private CheckBox lockHSVSettings;
        private GroupBox videoSettingsGroupBox;
        private Button setVideoSource;
        private TextBox videoSourceTextBox;
        private Label label1;
        private CheckBox lockVideoSource;
        private TrackBar lowerSaturationTrackbar;
        private TrackBar upperValueTrackbar;
        private TrackBar upperSaturationTrackbar;
        private TrackBar upperHueTrackbar;
        private TrackBar lowerValueTrackbar;
        private TrackBar lowerHueTrackbar;
        private Label label8;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label upperValueLabel;
        private Label upperSaturationLabel;
        private Label upperHueLabel;
        private Label lowerValueLabel;
        private Label lowerSaturationLabel;
        private Label lowerHueLabel;
        private GroupBox targetSettingsGroupBox;
        private Label xValueLabel;
        private Label radiusValueLabel;
        private CheckBox lockTargetSettings;
        private TextBox upperRadiusBound;
        private TextBox upperXBound;
        private TextBox lowerRadiusBound;
        private TextBox lowerXBound;
        private Label label11;
        private Label label10;
        private Label label9;
        private Label label7;
        private Button setBounds;
        private ComboBox autonCombo;
        private Label label12;
        private Label targetingLabel;
        private FlowLayoutPanel debugControlLayoutPanel;
    }
}

