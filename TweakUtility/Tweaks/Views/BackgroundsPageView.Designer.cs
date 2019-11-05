namespace TweakUtility.Tweaks.Views
{
    partial class BackgroundsPageView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.desktopPictureBox = new System.Windows.Forms.PictureBox();
            this.desktopTitleLabel = new System.Windows.Forms.Label();
            this.desktopStyleLabel = new System.Windows.Forms.Label();
            this.desktopStyleComboBox = new System.Windows.Forms.ComboBox();
            this.loginPictureBox = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.loginRestoreLinkLabel = new System.Windows.Forms.LinkLabel();
            this.loginPreviewLinkLabel = new System.Windows.Forms.LinkLabel();
            this.qualityNoticeLinkLabel = new System.Windows.Forms.LinkLabel();
            this.logonStyleLabel = new System.Windows.Forms.Label();
            this.logonStyleComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.desktopPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // desktopPictureBox
            // 
            this.desktopPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.desktopPictureBox.Location = new System.Drawing.Point(22, 39);
            this.desktopPictureBox.Name = "desktopPictureBox";
            this.desktopPictureBox.Size = new System.Drawing.Size(192, 108);
            this.desktopPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.desktopPictureBox.TabIndex = 0;
            this.desktopPictureBox.TabStop = false;
            this.desktopPictureBox.Click += new System.EventHandler(this.desktopPictureBox_Click);
            // 
            // desktopTitleLabel
            // 
            this.desktopTitleLabel.AutoSize = true;
            this.desktopTitleLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.desktopTitleLabel.Location = new System.Drawing.Point(18, 15);
            this.desktopTitleLabel.Name = "desktopTitleLabel";
            this.desktopTitleLabel.Size = new System.Drawing.Size(169, 21);
            this.desktopTitleLabel.TabIndex = 1;
            this.desktopTitleLabel.Text = "Desktop Background";
            // 
            // desktopStyleLabel
            // 
            this.desktopStyleLabel.AutoSize = true;
            this.desktopStyleLabel.Location = new System.Drawing.Point(220, 45);
            this.desktopStyleLabel.Name = "desktopStyleLabel";
            this.desktopStyleLabel.Size = new System.Drawing.Size(35, 15);
            this.desktopStyleLabel.TabIndex = 2;
            this.desktopStyleLabel.Text = "Style:";
            // 
            // desktopStyleComboBox
            // 
            this.desktopStyleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.desktopStyleComboBox.FormattingEnabled = true;
            this.desktopStyleComboBox.Location = new System.Drawing.Point(301, 42);
            this.desktopStyleComboBox.Name = "desktopStyleComboBox";
            this.desktopStyleComboBox.Size = new System.Drawing.Size(130, 23);
            this.desktopStyleComboBox.TabIndex = 3;
            this.desktopStyleComboBox.SelectedIndexChanged += new System.EventHandler(this.DesktopStyleComboBox_SelectedIndexChanged);
            this.desktopStyleComboBox.SelectedValueChanged += new System.EventHandler(this.DesktopStyleComboBox_SelectedValueChanged);
            // 
            // loginPictureBox
            // 
            this.loginPictureBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loginPictureBox.Location = new System.Drawing.Point(22, 190);
            this.loginPictureBox.Name = "loginPictureBox";
            this.loginPictureBox.Size = new System.Drawing.Size(192, 108);
            this.loginPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.loginPictureBox.TabIndex = 4;
            this.loginPictureBox.TabStop = false;
            this.loginPictureBox.Click += new System.EventHandler(this.loginPictureBox_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 166);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "Logon Background";
            // 
            // loginRestoreLinkLabel
            // 
            this.loginRestoreLinkLabel.AutoSize = true;
            this.loginRestoreLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.loginRestoreLinkLabel.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.loginRestoreLinkLabel.Location = new System.Drawing.Point(220, 268);
            this.loginRestoreLinkLabel.Name = "loginRestoreLinkLabel";
            this.loginRestoreLinkLabel.Size = new System.Drawing.Size(100, 15);
            this.loginRestoreLinkLabel.TabIndex = 6;
            this.loginRestoreLinkLabel.TabStop = true;
            this.loginRestoreLinkLabel.Text = "Restore to default";
            this.loginRestoreLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.loginRestoreLinkLabel_LinkClicked);
            // 
            // loginPreviewLinkLabel
            // 
            this.loginPreviewLinkLabel.AutoSize = true;
            this.loginPreviewLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.loginPreviewLinkLabel.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.loginPreviewLinkLabel.Location = new System.Drawing.Point(220, 283);
            this.loginPreviewLinkLabel.Name = "loginPreviewLinkLabel";
            this.loginPreviewLinkLabel.Size = new System.Drawing.Size(48, 15);
            this.loginPreviewLinkLabel.TabIndex = 7;
            this.loginPreviewLinkLabel.TabStop = true;
            this.loginPreviewLinkLabel.Text = "Preview";
            this.loginPreviewLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.loginPreviewLinkLabel_LinkClicked);
            // 
            // qualityNoticeLinkLabel
            // 
            this.qualityNoticeLinkLabel.AutoSize = true;
            this.qualityNoticeLinkLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.qualityNoticeLinkLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.qualityNoticeLinkLabel.LinkArea = new System.Windows.Forms.LinkArea(45, 11);
            this.qualityNoticeLinkLabel.LinkColor = System.Drawing.SystemColors.HotTrack;
            this.qualityNoticeLinkLabel.Location = new System.Drawing.Point(15, 313);
            this.qualityNoticeLinkLabel.Margin = new System.Windows.Forms.Padding(0);
            this.qualityNoticeLinkLabel.Name = "qualityNoticeLinkLabel";
            this.qualityNoticeLinkLabel.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.qualityNoticeLinkLabel.Size = new System.Drawing.Size(327, 21);
            this.qualityNoticeLinkLabel.TabIndex = 9;
            this.qualityNoticeLinkLabel.TabStop = true;
            this.qualityNoticeLinkLabel.Text = "The background was applied with 90% quality. Learn More";
            this.qualityNoticeLinkLabel.UseCompatibleTextRendering = true;
            this.qualityNoticeLinkLabel.Visible = false;
            this.qualityNoticeLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.qualityNoticeLinkLabel_LinkClicked);
            // 
            // logonStyleLabel
            // 
            this.logonStyleLabel.AutoSize = true;
            this.logonStyleLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logonStyleLabel.Location = new System.Drawing.Point(220, 192);
            this.logonStyleLabel.Name = "logonStyleLabel";
            this.logonStyleLabel.Size = new System.Drawing.Size(75, 15);
            this.logonStyleLabel.TabIndex = 11;
            this.logonStyleLabel.Text = "Text shadow:";
            // 
            // logonStyleComboBox
            // 
            this.logonStyleComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.logonStyleComboBox.FormattingEnabled = true;
            this.logonStyleComboBox.Location = new System.Drawing.Point(301, 189);
            this.logonStyleComboBox.Name = "logonStyleComboBox";
            this.logonStyleComboBox.Size = new System.Drawing.Size(130, 23);
            this.logonStyleComboBox.TabIndex = 12;
            this.logonStyleComboBox.SelectedValueChanged += new System.EventHandler(this.LogonStyleComboBox_SelectedValueChanged);
            // 
            // BackgroundsPageView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.logonStyleComboBox);
            this.Controls.Add(this.logonStyleLabel);
            this.Controls.Add(this.qualityNoticeLinkLabel);
            this.Controls.Add(this.loginPreviewLinkLabel);
            this.Controls.Add(this.loginRestoreLinkLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.desktopStyleComboBox);
            this.Controls.Add(this.desktopStyleLabel);
            this.Controls.Add(this.desktopTitleLabel);
            this.Controls.Add(this.desktopPictureBox);
            this.Controls.Add(this.loginPictureBox);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "BackgroundsPageView";
            this.Padding = new System.Windows.Forms.Padding(15);
            this.Size = new System.Drawing.Size(449, 349);
            this.Load += new System.EventHandler(this.BackgroundsPageView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.desktopPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loginPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox desktopPictureBox;
        private System.Windows.Forms.Label desktopTitleLabel;
        private System.Windows.Forms.Label desktopStyleLabel;
        private System.Windows.Forms.ComboBox desktopStyleComboBox;
        private System.Windows.Forms.PictureBox loginPictureBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel loginRestoreLinkLabel;
        private System.Windows.Forms.LinkLabel loginPreviewLinkLabel;
        private System.Windows.Forms.LinkLabel qualityNoticeLinkLabel;
        private System.Windows.Forms.Label logonStyleLabel;
        private System.Windows.Forms.ComboBox logonStyleComboBox;
    }
}
