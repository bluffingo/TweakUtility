namespace TweakUtility.Tweaks.Views
{
    partial class SoftwarePageView
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
			System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("uwpGroup", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("desktopGroup", System.Windows.Forms.HorizontalAlignment.Left);
			this.miniToolStrip = new System.Windows.Forms.ToolStrip();
			this.smallImageList = new System.Windows.Forms.ImageList(this.components);
			this.largeImageList = new System.Windows.Forms.ImageList(this.components);
			this.bottomPanel = new System.Windows.Forms.Panel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.uninstallButton = new System.Windows.Forms.ToolStripButton();
			this.versionLabel = new System.Windows.Forms.Label();
			this.iconPictureBox = new System.Windows.Forms.PictureBox();
			this.nameLabel = new System.Windows.Forms.Label();
			this.listView = new System.Windows.Forms.ListView();
			this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.bottomPanel.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// miniToolStrip
			// 
			this.miniToolStrip.AccessibleName = "New item selection";
			this.miniToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.ButtonDropDown;
			this.miniToolStrip.AutoSize = false;
			this.miniToolStrip.CanOverflow = false;
			this.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.miniToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.miniToolStrip.Location = new System.Drawing.Point(73, 0);
			this.miniToolStrip.Name = "miniToolStrip";
			this.miniToolStrip.Size = new System.Drawing.Size(74, 54);
			this.miniToolStrip.TabIndex = 3;
			// 
			// smallImageList
			// 
			this.smallImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.smallImageList.ImageSize = new System.Drawing.Size(16, 16);
			this.smallImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// largeImageList
			// 
			this.largeImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.largeImageList.ImageSize = new System.Drawing.Size(48, 48);
			this.largeImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// bottomPanel
			// 
			this.bottomPanel.BackColor = System.Drawing.SystemColors.Window;
			this.bottomPanel.Controls.Add(this.toolStrip1);
			this.bottomPanel.Controls.Add(this.versionLabel);
			this.bottomPanel.Controls.Add(this.iconPictureBox);
			this.bottomPanel.Controls.Add(this.nameLabel);
			this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.bottomPanel.Location = new System.Drawing.Point(0, 283);
			this.bottomPanel.Name = "bottomPanel";
			this.bottomPanel.Padding = new System.Windows.Forms.Padding(4);
			this.bottomPanel.Size = new System.Drawing.Size(691, 64);
			this.bottomPanel.TabIndex = 1;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Right;
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uninstallButton});
			this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.toolStrip1.Location = new System.Drawing.Point(582, 4);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(105, 56);
			this.toolStrip1.TabIndex = 7;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// uninstallButton
			// 
			this.uninstallButton.Image = global::TweakUtility.Properties.Resources.DeleteApplication_16x;
			this.uninstallButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.uninstallButton.Name = "uninstallButton";
			this.uninstallButton.Size = new System.Drawing.Size(73, 20);
			this.uninstallButton.Text = "&Uninstall";
			this.uninstallButton.Click += new System.EventHandler(this.UninstallButton_Click);
			// 
			// versionLabel
			// 
			this.versionLabel.AutoSize = true;
			this.versionLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.versionLabel.ForeColor = System.Drawing.SystemColors.GrayText;
			this.versionLabel.Location = new System.Drawing.Point(145, 13);
			this.versionLabel.Name = "versionLabel";
			this.versionLabel.Size = new System.Drawing.Size(37, 15);
			this.versionLabel.TabIndex = 6;
			this.versionLabel.Text = "v1.0.0";
			// 
			// iconPictureBox
			// 
			this.iconPictureBox.BackColor = System.Drawing.Color.Transparent;
			this.iconPictureBox.Location = new System.Drawing.Point(8, 8);
			this.iconPictureBox.Name = "iconPictureBox";
			this.iconPictureBox.Size = new System.Drawing.Size(48, 48);
			this.iconPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.iconPictureBox.TabIndex = 5;
			this.iconPictureBox.TabStop = false;
			// 
			// nameLabel
			// 
			this.nameLabel.AutoSize = true;
			this.nameLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.nameLabel.Location = new System.Drawing.Point(62, 8);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(88, 21);
			this.nameLabel.TabIndex = 4;
			this.nameLabel.Text = "Application";
			this.nameLabel.TextChanged += new System.EventHandler(this.NameLabel_TextChanged);
			// 
			// listView
			// 
			this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumnHeader});
			this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView.FullRowSelect = true;
			listViewGroup3.Header = "uwpGroup";
			listViewGroup3.Name = "UWP Applications";
			listViewGroup4.Header = "desktopGroup";
			listViewGroup4.Name = "Desktop Applications";
			this.listView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup3,
            listViewGroup4});
			this.listView.HideSelection = false;
			this.listView.LargeImageList = this.largeImageList;
			this.listView.Location = new System.Drawing.Point(0, 0);
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(691, 283);
			this.listView.SmallImageList = this.smallImageList;
			this.listView.TabIndex = 2;
			this.listView.UseCompatibleStateImageBehavior = false;
			this.listView.View = System.Windows.Forms.View.Details;
			this.listView.SelectedIndexChanged += new System.EventHandler(this.ListView_SelectedIndexChanged);
			// 
			// nameColumnHeader
			// 
			this.nameColumnHeader.Text = "Name";
			this.nameColumnHeader.Width = 447;
			// 
			// SoftwarePageView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.listView);
			this.Controls.Add(this.bottomPanel);
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "SoftwarePageView";
			this.Size = new System.Drawing.Size(691, 347);
			this.Load += new System.EventHandler(this.SoftwarePageView_Load);
			this.bottomPanel.ResumeLayout(false);
			this.bottomPanel.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).EndInit();
			this.ResumeLayout(false);

        }

		#endregion

		private System.Windows.Forms.ToolStrip miniToolStrip;
		private System.Windows.Forms.ImageList smallImageList;
		private System.Windows.Forms.ImageList largeImageList;
		private System.Windows.Forms.Panel bottomPanel;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton uninstallButton;
		private System.Windows.Forms.Label versionLabel;
		private System.Windows.Forms.PictureBox iconPictureBox;
		private System.Windows.Forms.Label nameLabel;
		private System.Windows.Forms.ListView listView;
		private System.Windows.Forms.ColumnHeader nameColumnHeader;
	}
}
