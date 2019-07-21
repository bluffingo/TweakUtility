namespace TweakUtility.Forms
{
    partial class AboutForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.components = new System.ComponentModel.Container();
            this.okButton = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.iconPictureBox = new System.Windows.Forms.PictureBox();
            this.versionLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.feedbackButton = new System.Windows.Forms.Button();
            this.githubLabel = new System.Windows.Forms.LinkLabel();
            this.debugLabel = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(348, 141);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(66, 13);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(118, 25);
            this.titleLabel.TabIndex = 1;
            this.titleLabel.Text = "Tweak Utility";
            // 
            // iconPictureBox
            // 
            this.iconPictureBox.Image = global::TweakUtility.Properties.Resources.TweakUtility;
            this.iconPictureBox.Location = new System.Drawing.Point(12, 12);
            this.iconPictureBox.Name = "iconPictureBox";
            this.iconPictureBox.Size = new System.Drawing.Size(48, 48);
            this.iconPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.iconPictureBox.TabIndex = 2;
            this.iconPictureBox.TabStop = false;
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionLabel.Location = new System.Drawing.Point(67, 38);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(126, 19);
            this.versionLabel.TabIndex = 3;
            this.versionLabel.Text = "Application Version";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionLabel.Location = new System.Drawing.Point(12, 74);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(411, 30);
            this.descriptionLabel.TabIndex = 4;
            this.descriptionLabel.Text = "Tweak Utility is an application for tweaking Windows similiar to Tweak UI and Win" +
    "aero Tweaker.";
            this.descriptionLabel.UseCompatibleTextRendering = true;
            // 
            // feedbackButton
            // 
            this.feedbackButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.feedbackButton.Location = new System.Drawing.Point(12, 141);
            this.feedbackButton.Name = "feedbackButton";
            this.feedbackButton.Size = new System.Drawing.Size(75, 23);
            this.feedbackButton.TabIndex = 5;
            this.feedbackButton.Text = "&Feedback";
            this.feedbackButton.UseVisualStyleBackColor = true;
            this.feedbackButton.Click += new System.EventHandler(this.FeedbackButton_Click);
            // 
            // githubLabel
            // 
            this.githubLabel.AutoSize = true;
            this.githubLabel.LinkArea = new System.Windows.Forms.LinkArea(30, 6);
            this.githubLabel.Location = new System.Drawing.Point(12, 111);
            this.githubLabel.Name = "githubLabel";
            this.githubLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.githubLabel.Size = new System.Drawing.Size(324, 20);
            this.githubLabel.TabIndex = 7;
            this.githubLabel.TabStop = true;
            this.githubLabel.Text = "Tweak Utility is available on GitHub, and licensed under GPL-3.0.";
            this.githubLabel.UseCompatibleTextRendering = true;
            this.githubLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GithubLabel_LinkClicked);
            // 
            // debugLabel
            // 
            this.debugLabel.AutoSize = true;
            this.debugLabel.Cursor = System.Windows.Forms.Cursors.Help;
            this.debugLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.debugLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.debugLabel.Location = new System.Drawing.Point(381, 12);
            this.debugLabel.Name = "debugLabel";
            this.debugLabel.Size = new System.Drawing.Size(42, 15);
            this.debugLabel.TabIndex = 8;
            this.debugLabel.Text = "Debug";
            this.toolTip.SetToolTip(this.debugLabel, "Debug builds are experimental and do not gurantee to be working or offer any supp" +
        "ort.\r\nAutomated crash reports are also disabled.");
            this.debugLabel.Visible = false;
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 15000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.ReshowDelay = 0;
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // AboutForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 176);
            this.Controls.Add(this.debugLabel);
            this.Controls.Add(this.githubLabel);
            this.Controls.Add(this.feedbackButton);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.iconPictureBox);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.okButton);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About Tweak Utility";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.PictureBox iconPictureBox;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Button feedbackButton;
        private System.Windows.Forms.LinkLabel githubLabel;
        private System.Windows.Forms.Label debugLabel;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
