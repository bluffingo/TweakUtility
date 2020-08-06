namespace TweakUtility.Forms
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.treeView = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.startupLabel = new System.Windows.Forms.Label();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.preferencesButton = new System.Windows.Forms.Button();
            this.extensionsButton = new System.Windows.Forms.Button();
            this.backupsButton = new System.Windows.Forms.Button();
            this.aboutLabel = new System.Windows.Forms.LinkLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(11, 11);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.searchTextBox);
            this.splitContainer.Panel1.Controls.Add(this.treeView);
            this.splitContainer.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.SplitContainer_BorderPaint);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainer.Panel2.Controls.Add(this.startupLabel);
            this.splitContainer.Panel2.Padding = new System.Windows.Forms.Padding(1);
            this.splitContainer.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.SplitContainer_BorderPaint);
            this.splitContainer.Size = new System.Drawing.Size(599, 350);
            this.splitContainer.SplitterDistance = 198;
            this.splitContainer.TabIndex = 1;
            this.splitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.SplitContainer_SplitterMoved);
            // 
            // searchTextBox
            // 
            this.searchTextBox.AcceptsReturn = true;
            this.searchTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.searchTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.searchTextBox.Location = new System.Drawing.Point(0, 0);
            this.searchTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(198, 23);
            this.searchTextBox.TabIndex = 4;
            this.searchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchTextBox_KeyDown);
            // 
            // treeView
            // 
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView.FullRowSelect = true;
            this.treeView.HideSelection = false;
            this.treeView.ImageIndex = 0;
            this.treeView.ImageList = this.imageList;
            this.treeView.Indent = 12;
            this.treeView.ItemHeight = 24;
            this.treeView.Location = new System.Drawing.Point(1, 33);
            this.treeView.Margin = new System.Windows.Forms.Padding(3, 50, 3, 3);
            this.treeView.Name = "treeView";
            this.treeView.SelectedImageIndex = 0;
            this.treeView.ShowLines = false;
            this.treeView.Size = new System.Drawing.Size(196, 316);
            this.treeView.TabIndex = 2;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterSelect);
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // startupLabel
            // 
            this.startupLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startupLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.startupLabel.Location = new System.Drawing.Point(1, 1);
            this.startupLabel.Name = "startupLabel";
            this.startupLabel.Size = new System.Drawing.Size(395, 348);
            this.startupLabel.TabIndex = 3;
            this.startupLabel.Text = "Navigate to a page to get started!";
            this.startupLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bottomPanel
            // 
            this.bottomPanel.BackColor = System.Drawing.SystemColors.Control;
            this.bottomPanel.Controls.Add(this.preferencesButton);
            this.bottomPanel.Controls.Add(this.extensionsButton);
            this.bottomPanel.Controls.Add(this.backupsButton);
            this.bottomPanel.Controls.Add(this.aboutLabel);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 374);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(622, 45);
            this.bottomPanel.TabIndex = 2;
            // 
            // preferencesButton
            // 
            this.preferencesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.preferencesButton.Location = new System.Drawing.Point(530, 9);
            this.preferencesButton.Name = "preferencesButton";
            this.preferencesButton.Size = new System.Drawing.Size(25, 25);
            this.preferencesButton.TabIndex = 5;
            this.preferencesButton.UseVisualStyleBackColor = true;
            this.preferencesButton.Click += new System.EventHandler(this.PreferencesButton_Click);
            // 
            // extensionsButton
            // 
            this.extensionsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.extensionsButton.Location = new System.Drawing.Point(584, 9);
            this.extensionsButton.Name = "extensionsButton";
            this.extensionsButton.Size = new System.Drawing.Size(25, 25);
            this.extensionsButton.TabIndex = 4;
            this.extensionsButton.UseVisualStyleBackColor = true;
            this.extensionsButton.Click += new System.EventHandler(this.ExtensionsButton_Click);
            // 
            // backupsButton
            // 
            this.backupsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.backupsButton.Location = new System.Drawing.Point(557, 9);
            this.backupsButton.Name = "backupsButton";
            this.backupsButton.Size = new System.Drawing.Size(25, 25);
            this.backupsButton.TabIndex = 3;
            this.backupsButton.UseVisualStyleBackColor = true;
            this.backupsButton.Click += new System.EventHandler(this.BackupsButton_Click);
            // 
            // aboutLabel
            // 
            this.aboutLabel.AutoSize = true;
            this.aboutLabel.LinkArea = new System.Windows.Forms.LinkArea(0, 28);
            this.aboutLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.aboutLabel.LinkColor = System.Drawing.SystemColors.GrayText;
            this.aboutLabel.Location = new System.Drawing.Point(10, 13);
            this.aboutLabel.Name = "aboutLabel";
            this.aboutLabel.Size = new System.Drawing.Size(108, 21);
            this.aboutLabel.TabIndex = 2;
            this.aboutLabel.TabStop = true;
            this.aboutLabel.Text = "PF94\'s TweakUtility";
            this.aboutLabel.UseCompatibleTextRendering = true;
            this.aboutLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AboutLabel_LinkClicked);
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 0;
            this.toolTip.InitialDelay = 500;
            this.toolTip.ReshowDelay = 100;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(622, 419);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.splitContainer);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "PF94\'s TweakUtility";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.bottomPanel.ResumeLayout(false);
            this.bottomPanel.PerformLayout();
            this.ResumeLayout(false);

        }

#endregion
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.LinkLabel aboutLabel;
        public System.Windows.Forms.Panel bottomPanel;
        public System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.Label startupLabel;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Button backupsButton;
        private System.Windows.Forms.Button extensionsButton;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button preferencesButton;
    }
}

