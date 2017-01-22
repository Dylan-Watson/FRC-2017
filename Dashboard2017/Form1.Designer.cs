using System.ComponentModel;
using System.Windows.Forms;

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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleFeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.infoPanel = new System.Windows.Forms.TabControl();
            this.infoTab = new System.Windows.Forms.TabPage();
            this.messageGroup = new System.Windows.Forms.GroupBox();
            this.targetingLabel = new System.Windows.Forms.Label();
            this.autonCombo = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tableControl = new System.Windows.Forms.TabControl();
            this.consoleTab = new System.Windows.Forms.TabPage();
            this.consoleTextBox = new System.Windows.Forms.RichTextBox();
            this.debugTab = new System.Windows.Forms.TabPage();
            this.debugControlLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ntRelayStatusLight = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.rioStatusLight = new System.Windows.Forms.Panel();
            this.toggleFeed = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.infoPanel.SuspendLayout();
            this.infoTab.SuspendLayout();
            this.tableControl.SuspendLayout();
            this.consoleTab.SuspendLayout();
            this.debugTab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.videoToolStripMenuItem,
            this.toggleFeedToolStripMenuItem,
            this.toggleFeed});
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
            this.optionsToolStripMenuItem});
            this.videoToolStripMenuItem.Name = "videoToolStripMenuItem";
            this.videoToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.videoToolStripMenuItem.Text = "Settings";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.optionsToolStripMenuItem.Text = "RIO Address";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // toggleFeedToolStripMenuItem
            // 
            this.toggleFeedToolStripMenuItem.Name = "toggleFeedToolStripMenuItem";
            this.toggleFeedToolStripMenuItem.Size = new System.Drawing.Size(12, 20);
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
            this.infoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoPanel.Location = new System.Drawing.Point(0, 0);
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.SelectedIndex = 0;
            this.infoPanel.Size = new System.Drawing.Size(490, 451);
            this.infoPanel.TabIndex = 0;
            // 
            // infoTab
            // 
            this.infoTab.Controls.Add(this.messageGroup);
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
            // messageGroup
            // 
            this.messageGroup.Location = new System.Drawing.Point(11, 101);
            this.messageGroup.Name = "messageGroup";
            this.messageGroup.Size = new System.Drawing.Size(465, 318);
            this.messageGroup.TabIndex = 3;
            this.messageGroup.TabStop = false;
            this.messageGroup.Text = "No Connection";
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
            // tableControl
            // 
            this.tableControl.Controls.Add(this.consoleTab);
            this.tableControl.Controls.Add(this.debugTab);
            this.tableControl.Controls.Add(this.tabPage1);
            this.tableControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableControl.Location = new System.Drawing.Point(0, 0);
            this.tableControl.Name = "tableControl";
            this.tableControl.SelectedIndex = 0;
            this.tableControl.Size = new System.Drawing.Size(514, 451);
            this.tableControl.TabIndex = 0;
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
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(506, 425);
            this.tabPage1.TabIndex = 4;
            this.tabPage1.Text = "Feed";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(500, 419);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(909, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "NT-RELAY:";
            // 
            // ntRelayStatusLight
            // 
            this.ntRelayStatusLight.Location = new System.Drawing.Point(978, 8);
            this.ntRelayStatusLight.Name = "ntRelayStatusLight";
            this.ntRelayStatusLight.Size = new System.Drawing.Size(26, 13);
            this.ntRelayStatusLight.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(805, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "RoboRIO:";
            // 
            // rioStatusLight
            // 
            this.rioStatusLight.Location = new System.Drawing.Point(866, 8);
            this.rioStatusLight.Name = "rioStatusLight";
            this.rioStatusLight.Size = new System.Drawing.Size(26, 13);
            this.rioStatusLight.TabIndex = 6;
            // 
            // toggleFeed
            // 
            this.toggleFeed.Name = "toggleFeed";
            this.toggleFeed.Size = new System.Drawing.Size(83, 20);
            this.toggleFeed.Text = "Toggle Feed";
            this.toggleFeed.Click += new System.EventHandler(this.toggleFeedToolStripMenuItem1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 475);
            this.Controls.Add(this.rioStatusLight);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ntRelayStatusLight);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.label1);
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
            this.tableControl.ResumeLayout(false);
            this.consoleTab.ResumeLayout(false);
            this.debugTab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private SplitContainer splitContainer1;
        private ToolStripMenuItem videoToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private TabControl infoPanel;
        private TabPage infoTab;
        private ComboBox autonCombo;
        private Label label12;
        private Label targetingLabel;
        private GroupBox messageGroup;
        private TabControl tableControl;
        private TabPage consoleTab;
        private RichTextBox consoleTextBox;
        private TabPage debugTab;
        private FlowLayoutPanel debugControlLayoutPanel;
        private Label label1;
        private Panel ntRelayStatusLight;
        private Label label2;
        private Panel rioStatusLight;
        private TabPage tabPage1;
        private PictureBox pictureBox1;
        private ToolStripMenuItem toggleFeedToolStripMenuItem;
        private ToolStripMenuItem toggleFeed;
    }
}

