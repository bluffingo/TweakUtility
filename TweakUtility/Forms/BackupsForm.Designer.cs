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
			this.cancelButton = new System.Windows.Forms.Button();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.createButton = new System.Windows.Forms.ToolStripButton();
			this.deleteButton = new System.Windows.Forms.ToolStripButton();
			this.openFolderButton = new System.Windows.Forms.ToolStripButton();
			this.toolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// applyButton
			// 
			this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.applyButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.applyButton.Enabled = false;
			this.applyButton.Location = new System.Drawing.Point(427, 276);
			this.applyButton.Name = "applyButton";
			this.applyButton.Size = new System.Drawing.Size(95, 23);
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
			this.listView.FullRowSelect = true;
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
			this.listView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ListView_ItemSelectionChanged);
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
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(326, 276);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(95, 23);
			this.cancelButton.TabIndex = 5;
			this.cancelButton.Text = global::TweakUtility.Properties.Strings.Button_Cancel;
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// toolStrip
			// 
			this.toolStrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.toolStrip.AutoSize = false;
			this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip.GripMargin = new System.Windows.Forms.Padding(0);
			this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createButton,
            this.deleteButton,
            this.openFolderButton});
			this.toolStrip.Location = new System.Drawing.Point(12, 9);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip.Size = new System.Drawing.Size(510, 25);
			this.toolStrip.TabIndex = 6;
			this.toolStrip.Text = "toolStrip1";
			// 
			// createButton
			// 
			this.createButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.createButton.Name = "createButton";
			this.createButton.Size = new System.Drawing.Size(61, 22);
			this.createButton.Text = "&Create";
			this.createButton.Click += new System.EventHandler(this.CreateButton_Click);
            this.createButton.Image = Properties.Resources.AddDatabase_16x;
			// 
			// deleteButton
			// 
			this.deleteButton.Enabled = false;
			this.deleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(60, 22);
			this.deleteButton.Text = "&Delete";
			this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            this.deleteButton.Image = Properties.Resources.DeleteDatabase_16x;
            // 
            // openFolderButton
            // 
            this.openFolderButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.openFolderButton.Image = global::TweakUtility.Properties.Resources.OpenFolder_16x;
			this.openFolderButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.openFolderButton.Name = "openFolderButton";
			this.openFolderButton.Size = new System.Drawing.Size(139, 22);
			this.openFolderButton.Text = "&Open Backups Folder";
			// 
			// BackupsForm
			// 
			this.AcceptButton = this.applyButton;
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(534, 311);
			this.Controls.Add(this.toolStrip);
			this.Controls.Add(this.cancelButton);
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
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader nameColumnHeader;
        private System.Windows.Forms.ColumnHeader sizeColumnHeader;
        private System.Windows.Forms.ColumnHeader dateColumnHeader;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton createButton;
        private System.Windows.Forms.ToolStripButton deleteButton;
        private System.Windows.Forms.ToolStripButton openFolderButton;
    }
}