﻿using System.ComponentModel;

namespace TweakUtility.Tweaks.Views
{
    partial class RestorePointsView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.addButton = new System.Windows.Forms.ToolStripButton();
            this.removeButton = new System.Windows.Forms.ToolStripButton();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusLabel = new System.Windows.Forms.ToolStripLabel();
            this.description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.creationTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sequenceNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.eventType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.restorePointType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView = new System.Windows.Forms.ListView();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.SystemColors.Window;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addButton,
            this.removeButton,
            this.progressBar,
            this.statusLabel});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 1, 6);
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(340, 29);
            this.toolStrip.TabIndex = 1;
            // 
            // addButton
            // 
            this.addButton.Image = global::TweakUtility.Properties.Resources.AddRow_16x;
            this.addButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(49, 20);
            this.addButton.Text = "&Add";
            this.addButton.Click += new System.EventHandler(this.AddButtonClick);
            // 
            // removeButton
            // 
            this.removeButton.Enabled = false;
            this.removeButton.Image = global::TweakUtility.Properties.Resources.RemoveRow_16x;
            this.removeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(70, 20);
            this.removeButton.Text = "&Remove";
            this.removeButton.Click += new System.EventHandler(this.RemoveButtonClick);
            // 
            // progressBar
            // 
            this.progressBar.MarqueeAnimationSpeed = 50;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(86, 20);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 20);
            // 
            // description
            // 
            this.description.DisplayIndex = 0;
            this.description.Text = "Description";
            this.description.Width = 89;
            // 
            // creationTime
            // 
            this.creationTime.DisplayIndex = 1;
            this.creationTime.Text = "Creation Time";
            this.creationTime.Width = 88;
            // 
            // sequenceNumber
            // 
            this.sequenceNumber.DisplayIndex = 2;
            this.sequenceNumber.Text = "Sequence Number";
            this.sequenceNumber.Width = 40;
            // 
            // eventType
            // 
            this.eventType.Text = "Event Type";
            this.eventType.Width = 76;
            // 
            // restorePointType
            // 
            this.restorePointType.Text = "Restore Point Type";
            this.restorePointType.Width = 99;
            // 
            // listView
            // 
            this.listView.AutoArrange = false;
            this.listView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.sequenceNumber,
            this.description,
            this.creationTime,
            this.eventType,
            this.restorePointType});
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.listView.FullRowSelect = true;
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(0, 29);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(340, 270);
            this.listView.TabIndex = 2;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ListViewItemSelectionChanged);
            this.listView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ListViewKeyUp);
            // 
            // RestorePointsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView);
            this.Controls.Add(this.toolStrip);
            this.Name = "RestorePointsView";
            this.Size = new System.Drawing.Size(340, 299);
            this.Load += new System.EventHandler(this.RestorePointsViewLoad);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColumnHeader restorePointType;
        private System.Windows.Forms.ColumnHeader eventType;
        private System.Windows.Forms.ColumnHeader sequenceNumber;
        private System.Windows.Forms.ColumnHeader creationTime;
        private System.Windows.Forms.ColumnHeader description;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton addButton;
        private System.Windows.Forms.ToolStripButton removeButton;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripLabel statusLabel;
    }
}