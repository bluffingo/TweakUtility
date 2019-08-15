namespace TweakUtility.Forms
{
    partial class ExtensionInstallForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.installButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.authorTitleLabel = new System.Windows.Forms.Label();
            this.nameTitleLabel = new System.Windows.Forms.Label();
            this.descriptionTitleLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.authorLabel = new System.Windows.Forms.Label();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.dontAskCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Are you sure you want to install this extension?";
            // 
            // installButton
            // 
            this.installButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.installButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.installButton.Location = new System.Drawing.Point(347, 116);
            this.installButton.Name = "installButton";
            this.installButton.Size = new System.Drawing.Size(75, 23);
            this.installButton.TabIndex = 1;
            this.installButton.Text = "&Install";
            this.installButton.UseVisualStyleBackColor = true;
            this.installButton.Click += new System.EventHandler(this.InstallButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(266, 116);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // authorTitleLabel
            // 
            this.authorTitleLabel.AutoSize = true;
            this.authorTitleLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.authorTitleLabel.Location = new System.Drawing.Point(12, 66);
            this.authorTitleLabel.Name = "authorTitleLabel";
            this.authorTitleLabel.Size = new System.Drawing.Size(44, 13);
            this.authorTitleLabel.TabIndex = 3;
            this.authorTitleLabel.Text = "Author";
            // 
            // nameTitleLabel
            // 
            this.nameTitleLabel.AutoSize = true;
            this.nameTitleLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.nameTitleLabel.Location = new System.Drawing.Point(12, 47);
            this.nameTitleLabel.Name = "nameTitleLabel";
            this.nameTitleLabel.Size = new System.Drawing.Size(38, 13);
            this.nameTitleLabel.TabIndex = 4;
            this.nameTitleLabel.Text = "Name";
            // 
            // descriptionTitleLabel
            // 
            this.descriptionTitleLabel.AutoSize = true;
            this.descriptionTitleLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.descriptionTitleLabel.Location = new System.Drawing.Point(12, 85);
            this.descriptionTitleLabel.Name = "descriptionTitleLabel";
            this.descriptionTitleLabel.Size = new System.Drawing.Size(66, 13);
            this.descriptionTitleLabel.TabIndex = 5;
            this.descriptionTitleLabel.Text = "Description";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(100, 47);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(102, 13);
            this.nameLabel.TabIndex = 6;
            this.nameLabel.Text = "No name specified";
            // 
            // authorLabel
            // 
            this.authorLabel.AutoSize = true;
            this.authorLabel.Location = new System.Drawing.Point(100, 66);
            this.authorLabel.Name = "authorLabel";
            this.authorLabel.Size = new System.Drawing.Size(109, 13);
            this.authorLabel.TabIndex = 7;
            this.authorLabel.Text = "No author specified";
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoSize = true;
            this.descriptionLabel.Location = new System.Drawing.Point(100, 85);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(132, 13);
            this.descriptionLabel.TabIndex = 8;
            this.descriptionLabel.Text = "No description specified";
            // 
            // dontAskCheckBox
            // 
            this.dontAskCheckBox.AutoSize = true;
            this.dontAskCheckBox.Location = new System.Drawing.Point(15, 120);
            this.dontAskCheckBox.Name = "dontAskCheckBox";
            this.dontAskCheckBox.Size = new System.Drawing.Size(107, 17);
            this.dontAskCheckBox.TabIndex = 10;
            this.dontAskCheckBox.Text = "Don\'t ask again";
            this.dontAskCheckBox.UseVisualStyleBackColor = true;
            this.dontAskCheckBox.CheckedChanged += new System.EventHandler(this.DontAskCheckBox_CheckedChanged);
            // 
            // ExtensionInstallForm
            // 
            this.AcceptButton = this.installButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(434, 151);
            this.Controls.Add(this.dontAskCheckBox);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.authorLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.descriptionTitleLabel);
            this.Controls.Add(this.nameTitleLabel);
            this.Controls.Add(this.authorTitleLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.installButton);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExtensionInstallForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Install Extension";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button installButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label authorTitleLabel;
        private System.Windows.Forms.Label nameTitleLabel;
        private System.Windows.Forms.Label descriptionTitleLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label authorLabel;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.CheckBox dontAskCheckBox;
    }
}