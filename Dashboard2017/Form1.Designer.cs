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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableControl = new System.Windows.Forms.TabControl();
            this.videoMain = new System.Windows.Forms.TabPage();
            this.videoComposit = new System.Windows.Forms.TabPage();
            this.mainVideoBox = new Emgu.CV.UI.ImageBox();
            this.compositVideoBox = new Emgu.CV.UI.ImageBox();
            this.videoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableControl.SuspendLayout();
            this.videoMain.SuspendLayout();
            this.videoComposit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainVideoBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.compositVideoBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.videoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(753, 24);
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
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableControl);
            this.splitContainer1.Size = new System.Drawing.Size(753, 389);
            this.splitContainer1.SplitterDistance = 251;
            this.splitContainer1.TabIndex = 1;
            // 
            // tableControl
            // 
            this.tableControl.Controls.Add(this.videoMain);
            this.tableControl.Controls.Add(this.videoComposit);
            this.tableControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableControl.Location = new System.Drawing.Point(0, 0);
            this.tableControl.Name = "tableControl";
            this.tableControl.SelectedIndex = 0;
            this.tableControl.Size = new System.Drawing.Size(498, 389);
            this.tableControl.TabIndex = 0;
            // 
            // videoMain
            // 
            this.videoMain.Controls.Add(this.mainVideoBox);
            this.videoMain.Location = new System.Drawing.Point(4, 22);
            this.videoMain.Name = "videoMain";
            this.videoMain.Padding = new System.Windows.Forms.Padding(3);
            this.videoMain.Size = new System.Drawing.Size(490, 363);
            this.videoMain.TabIndex = 0;
            this.videoMain.Text = "Main";
            this.videoMain.UseVisualStyleBackColor = true;
            // 
            // videoComposit
            // 
            this.videoComposit.Controls.Add(this.compositVideoBox);
            this.videoComposit.Location = new System.Drawing.Point(4, 22);
            this.videoComposit.Name = "videoComposit";
            this.videoComposit.Padding = new System.Windows.Forms.Padding(3);
            this.videoComposit.Size = new System.Drawing.Size(490, 363);
            this.videoComposit.TabIndex = 1;
            this.videoComposit.Text = "Composit";
            this.videoComposit.UseVisualStyleBackColor = true;
            // 
            // mainVideoBox
            // 
            this.mainVideoBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainVideoBox.Location = new System.Drawing.Point(3, 3);
            this.mainVideoBox.Name = "mainVideoBox";
            this.mainVideoBox.Size = new System.Drawing.Size(484, 357);
            this.mainVideoBox.TabIndex = 2;
            this.mainVideoBox.TabStop = false;
            // 
            // compositVideoBox
            // 
            this.compositVideoBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compositVideoBox.Location = new System.Drawing.Point(3, 3);
            this.compositVideoBox.Name = "compositVideoBox";
            this.compositVideoBox.Size = new System.Drawing.Size(484, 357);
            this.compositVideoBox.TabIndex = 2;
            this.compositVideoBox.TabStop = false;
            // 
            // videoToolStripMenuItem
            // 
            this.videoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.recordToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.videoToolStripMenuItem.Name = "videoToolStripMenuItem";
            this.videoToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.videoToolStripMenuItem.Text = "Video";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // recordToolStripMenuItem
            // 
            this.recordToolStripMenuItem.Name = "recordToolStripMenuItem";
            this.recordToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.recordToolStripMenuItem.Text = "Record";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.refreshToolStripMenuItem.Text = "Refresh [F5]";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 413);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableControl.ResumeLayout(false);
            this.videoMain.ResumeLayout(false);
            this.videoComposit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainVideoBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.compositVideoBox)).EndInit();
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
    }
}

