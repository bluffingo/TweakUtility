namespace TweakUtility.Tweaks.Views
{
    partial class HostsPageView
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HostsPageView));
			this.listView = new System.Windows.Forms.ListView();
			this.ipColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.hostColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.addButton = new System.Windows.Forms.ToolStripButton();
			this.editButton = new System.Windows.Forms.ToolStripButton();
			this.deleteButton = new System.Windows.Forms.ToolStripButton();
			this.openButton = new System.Windows.Forms.ToolStripButton();
			this.toolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// listView
			// 
			this.listView.AutoArrange = false;
			this.listView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ipColumnHeader,
            this.hostColumnHeader});
			this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView.FullRowSelect = true;
			this.listView.HideSelection = false;
			this.listView.Location = new System.Drawing.Point(0, 30);
			this.listView.MultiSelect = false;
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(500, 315);
			this.listView.SmallImageList = this.imageList;
			this.listView.TabIndex = 0;
			this.listView.UseCompatibleStateImageBehavior = false;
			this.listView.View = System.Windows.Forms.View.Details;
			this.listView.ItemActivate += new System.EventHandler(this.ListView_ItemActivate);
			this.listView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ListView_ItemSelectionChanged);
			this.listView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ListView_KeyUp);
			this.listView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListView_MouseClick);
			// 
			// ipColumnHeader
			// 
			this.ipColumnHeader.Text = "IP Address";
			this.ipColumnHeader.Width = 245;
			// 
			// hostColumnHeader
			// 
			this.hostColumnHeader.Text = "Host";
			this.hostColumnHeader.Width = 249;
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList.Images.SetKeyName(0, "direct");
			this.imageList.Images.SetKeyName(1, "loopback");
			// 
			// toolStrip
			// 
			this.toolStrip.BackColor = System.Drawing.SystemColors.Window;
			this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addButton,
            this.editButton,
            this.deleteButton,
            this.openButton});
			this.toolStrip.Location = new System.Drawing.Point(0, 0);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 1, 7);
			this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip.Size = new System.Drawing.Size(500, 30);
			this.toolStrip.TabIndex = 1;
			// 
			// addButton
			// 
			this.addButton.Image = global::TweakUtility.Properties.Resources.AddRow_16x;
			this.addButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(46, 20);
			this.addButton.Text = "&Add";
			this.addButton.Click += new System.EventHandler(this.AddButton_Click);
			// 
			// editButton
			// 
			this.editButton.Enabled = false;
			this.editButton.Image = global::TweakUtility.Properties.Resources.EditRow_16x;
			this.editButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.editButton.Name = "editButton";
			this.editButton.Size = new System.Drawing.Size(45, 20);
			this.editButton.Text = "&Edit";
			this.editButton.Click += new System.EventHandler(this.EditButton_Click);
			// 
			// deleteButton
			// 
			this.deleteButton.Enabled = false;
			this.deleteButton.Image = global::TweakUtility.Properties.Resources.RemoveRow_16x;
			this.deleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(66, 20);
			this.deleteButton.Text = "&Remove";
			this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
			// 
			// openButton
			// 
			this.openButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.openButton.Image = ((System.Drawing.Image)(resources.GetObject("openButton.Image")));
			this.openButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.openButton.Name = "openButton";
			this.openButton.Size = new System.Drawing.Size(108, 20);
			this.openButton.Text = "Open in &Notepad";
			this.openButton.Click += new System.EventHandler(this.OpenButton_Click);
			// 
			// HostsPageView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.listView);
			this.Controls.Add(this.toolStrip);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "HostsPageView";
			this.Size = new System.Drawing.Size(500, 345);
			this.Load += new System.EventHandler(this.HostsPageView_Load);
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton addButton;
        private System.Windows.Forms.ToolStripButton deleteButton;
        private System.Windows.Forms.ColumnHeader ipColumnHeader;
        private System.Windows.Forms.ColumnHeader hostColumnHeader;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStripButton editButton;
        private System.Windows.Forms.ToolStripButton openButton;
    }
}
