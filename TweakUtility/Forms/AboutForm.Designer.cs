namespace TweakUtility.Forms
{
    internal partial class AboutForm
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
            this.versionLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.feedbackButton = new System.Windows.Forms.Button();
            this.githubLabel = new System.Windows.Forms.LinkLabel();
            this.debugLabel = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.copyrightLabel = new System.Windows.Forms.Label();
            this.creditsButton = new System.Windows.Forms.Button();
            this.iconPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(348, 169);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = global::TweakUtility.Properties.Strings.Button_OK;
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 14F);
            this.titleLabel.Location = new System.Drawing.Point(66, 15);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(122, 25);
            this.titleLabel.TabIndex = 1;
            this.titleLabel.Text = "Tweak Utility";
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.versionLabel.Location = new System.Drawing.Point(67, 38);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(132, 19);
            this.versionLabel.TabIndex = 3;
            this.versionLabel.Text = "Application Version";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionLabel.Location = new System.Drawing.Point(12, 72);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(411, 30);
            this.descriptionLabel.TabIndex = 4;
            this.descriptionLabel.Text = "Tweak Utility is an application for tweaking Windows, as well as other applicatio" +
    "ns, similar to Tweak UI and Winaero Tweaker.";
            this.descriptionLabel.UseCompatibleTextRendering = true;
            // 
            // feedbackButton
            // 
            this.feedbackButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.feedbackButton.Location = new System.Drawing.Point(12, 169);
            this.feedbackButton.Name = "feedbackButton";
            this.feedbackButton.Size = new System.Drawing.Size(75, 23);
            this.feedbackButton.TabIndex = 5;
            this.feedbackButton.Text = global::TweakUtility.Properties.Strings.Button_Feedback;
            this.feedbackButton.UseVisualStyleBackColor = true;
            this.feedbackButton.Click += new System.EventHandler(this.FeedbackButton_Click);
            // 
            // githubLabel
            // 
            this.githubLabel.AutoSize = true;
            this.githubLabel.Location = new System.Drawing.Point(12, 109);
            this.githubLabel.Name = "githubLabel";
            this.githubLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.githubLabel.Size = new System.Drawing.Size(0, 18);
            this.githubLabel.TabIndex = 7;
            this.githubLabel.UseCompatibleTextRendering = true;
            this.githubLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GithubLabel_LinkClicked);
            // 
            // debugLabel
            // 
            this.debugLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.debugLabel.AutoSize = true;
            this.debugLabel.Cursor = System.Windows.Forms.Cursors.Help;
            this.debugLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.debugLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.debugLabel.Location = new System.Drawing.Point(378, 12);
            this.debugLabel.Margin = new System.Windows.Forms.Padding(0);
            this.debugLabel.Name = "debugLabel";
            this.debugLabel.Size = new System.Drawing.Size(43, 15);
            this.debugLabel.TabIndex = 8;
            this.debugLabel.Text = "Debug";
            this.toolTip.SetToolTip(this.debugLabel, global::TweakUtility.Properties.Strings.Debug_Disclaimer);
            this.debugLabel.Visible = false;
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 15000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.ReshowDelay = 0;
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // copyrightLabel
            // 
            this.copyrightLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.copyrightLabel.Location = new System.Drawing.Point(12, 131);
            this.copyrightLabel.Name = "copyrightLabel";
            this.copyrightLabel.Size = new System.Drawing.Size(409, 32);
            this.copyrightLabel.TabIndex = 9;
            this.copyrightLabel.Text = "All icons used (except the application icon), belong to their rightful owners.";
            this.copyrightLabel.UseCompatibleTextRendering = true;
            // 
            // creditsButton
            // 
            this.creditsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.creditsButton.Location = new System.Drawing.Point(93, 169);
            this.creditsButton.Name = "creditsButton";
            this.creditsButton.Size = new System.Drawing.Size(75, 23);
            this.creditsButton.TabIndex = 10;
            this.creditsButton.Text = "&Credits";
            this.creditsButton.UseVisualStyleBackColor = true;
            this.creditsButton.Click += new System.EventHandler(this.CreditsButton_Click);
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
            // AboutForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 204);
            this.Controls.Add(this.creditsButton);
            this.Controls.Add(this.copyrightLabel);
            this.Controls.Add(this.debugLabel);
            this.Controls.Add(this.githubLabel);
            this.Controls.Add(this.feedbackButton);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.iconPictureBox);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.okButton);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About Tweak Utility";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.AboutForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //https://www.youtube.com/watch?v=9ULDCUvgkSQ is not a good concept, Do not try to add new styles for remaking a old Operating System.

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.PictureBox iconPictureBox;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Button feedbackButton;
        private System.Windows.Forms.LinkLabel githubLabel;
        private System.Windows.Forms.Label debugLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label copyrightLabel;
        private System.Windows.Forms.Button creditsButton;
    }
}
