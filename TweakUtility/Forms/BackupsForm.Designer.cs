namespace TweakUtility.Forms
{
    partial class BackupsForm
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
            this.applyButton = new System.Windows.Forms.Button();
            this.listView = new System.Windows.Forms.ListView();
            this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dateColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sizeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.openFolderButton = new System.Windows.Forms.Button();
            this.createButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.applyButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.applyButton.Location = new System.Drawing.Point(442, 276);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(80, 23);
            this.applyButton.TabIndex = 0;
            this.applyButton.Text = global::TweakUtility.Properties.Strings.Button_Apply;
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumnHeader,
            this.dateColumnHeader,
            this.sizeColumnHeader});
            this.listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(12, 41);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.ShowGroups = false;
            this.listView.Size = new System.Drawing.Size(510, 229);
            this.listView.TabIndex = 1;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // nameColumnHeader
            // 
            this.nameColumnHeader.Text = "Name";
            this.nameColumnHeader.Width = 303;
            // 
            // dateColumnHeader
            // 
            this.dateColumnHeader.Text = "Date";
            this.dateColumnHeader.Width = 89;
            // 
            // sizeColumnHeader
            // 
            this.sizeColumnHeader.Text = "Size";
            this.sizeColumnHeader.Width = 112;
            // 
            // openFolderButton
            // 
            this.openFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.openFolderButton.Location = new System.Drawing.Point(12, 276);
            this.openFolderButton.Name = "openFolderButton";
            this.openFolderButton.Size = new System.Drawing.Size(175, 23);
            this.openFolderButton.TabIndex = 2;
            this.openFolderButton.Text = global::TweakUtility.Properties.Strings.Backups_OpenFolder;
            this.openFolderButton.UseVisualStyleBackColor = true;
            this.openFolderButton.Click += new System.EventHandler(this.OpenFolderButton_Click);
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(12, 12);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(70, 23);
            this.createButton.TabIndex = 3;
            this.createButton.Text = global::TweakUtility.Properties.Strings.Button_Create;
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.Button1_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(88, 12);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(70, 23);
            this.deleteButton.TabIndex = 4;
            this.deleteButton.Text = global::TweakUtility.Properties.Strings.Button_Delete;
            this.deleteButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(356, 276);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(80, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = global::TweakUtility.Properties.Strings.Button_Cancel;
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // BackupsForm
            // 
            this.AcceptButton = this.applyButton;
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(534, 311);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.createButton);
            this.Controls.Add(this.openFolderButton);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.applyButton);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BackupsForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Backups";
            this.Load += new System.EventHandler(this.BackupsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.Button openFolderButton;
        private System.Windows.Forms.ColumnHeader nameColumnHeader;
        private System.Windows.Forms.ColumnHeader sizeColumnHeader;
        private System.Windows.Forms.ColumnHeader dateColumnHeader;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button cancelButton;
    }
}