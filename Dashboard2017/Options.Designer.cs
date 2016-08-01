namespace Dashboard2017
{
    partial class Options
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.overrideAddress = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.addressBox = new System.Windows.Forms.TextBox();
            this.setTeamNumber = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.teamNumber = new System.Windows.Forms.TextBox();
            this.cancel = new System.Windows.Forms.Button();
            this.ok = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.overrideAddress);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.addressBox);
            this.groupBox2.Controls.Add(this.setTeamNumber);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.teamNumber);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(447, 186);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Team Info";
            // 
            // overrideAddress
            // 
            this.overrideAddress.AutoSize = true;
            this.overrideAddress.Location = new System.Drawing.Point(334, 19);
            this.overrideAddress.Name = "overrideAddress";
            this.overrideAddress.Size = new System.Drawing.Size(107, 17);
            this.overrideAddress.TabIndex = 5;
            this.overrideAddress.Text = "Override Address";
            this.overrideAddress.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "RIO Address:";
            // 
            // addressBox
            // 
            this.addressBox.Location = new System.Drawing.Point(89, 63);
            this.addressBox.Name = "addressBox";
            this.addressBox.Size = new System.Drawing.Size(181, 20);
            this.addressBox.TabIndex = 3;
            // 
            // setTeamNumber
            // 
            this.setTeamNumber.Location = new System.Drawing.Point(195, 21);
            this.setTeamNumber.Name = "setTeamNumber";
            this.setTeamNumber.Size = new System.Drawing.Size(75, 23);
            this.setTeamNumber.TabIndex = 2;
            this.setTeamNumber.Text = "Set Team";
            this.setTeamNumber.UseVisualStyleBackColor = true;
            this.setTeamNumber.Click += new System.EventHandler(this.setTeamNumber_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Team Number:";
            // 
            // teamNumber
            // 
            this.teamNumber.Location = new System.Drawing.Point(89, 23);
            this.teamNumber.Name = "teamNumber";
            this.teamNumber.Size = new System.Drawing.Size(100, 20);
            this.teamNumber.TabIndex = 0;
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Location = new System.Drawing.Point(126, 213);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 2;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(253, 213);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(75, 23);
            this.ok.TabIndex = 3;
            this.ok.Text = "OK";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 248);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "Options";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button setTeamNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox teamNumber;
        private System.Windows.Forms.CheckBox overrideAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox addressBox;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button ok;
    }
}