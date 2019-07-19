using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TweakUtility.TweakPages;
using System.Reflection;
using TweakUtility.Forms;

namespace TweakUtility
{
    public partial class TweakPageView : UserControl
    {
        public TweakPage TweakPage { get; }

        public TweakPageView(TweakPage tweakPage)
        {
            this.TweakPage = tweakPage;

            InitializeComponent();
        }

        private void TweakPageView_Load(object sender, EventArgs e)
        {
            var panel = new FlowLayoutPanel()
            {
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = true
            };

            panel.Controls.Add(new Label()
            {
                Text = TweakPage.Name,
                Font = new Font(this.Font.FontFamily, 14f, GraphicsUnit.Point),
                AutoSize = true,
                Padding = new Padding(0, 0, 0, 8),
                Margin = new Padding(0),
            });

            var properties = TweakPage.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);

            if (properties.Length == 0)
            {
                panel.Controls.Add(new Label()
                {
                    Text = "This page contains no tweaks.",
                    ForeColor = SystemColors.GrayText,
                    Dock = DockStyle.Fill
                });
            }
            else
            {
                foreach (PropertyInfo property in properties)
                {
                    if (property.PropertyType == typeof(bool))
                    {
                        var checkBox = new CheckBox()
                        {
                            //Use display name as label, if not available use property name as fallback.
                            Text = property.GetAttribute<DisplayNameAttribute>()?.DisplayName ?? property.Name,
                            AutoSize = true
                        };

                        try
                        {
                            checkBox.Checked = (bool)property.GetValue(TweakPage, null);
                            checkBox.Enabled = property.CanWrite;
                        }
                        catch
                        {
                            checkBox.Enabled = false;
                            checkBox.CheckState = CheckState.Indeterminate;
                        }

                        checkBox.CheckedChanged += (s, e2) =>
                        {
                            property.SetValue(TweakPage, checkBox.Checked, null);
                        };

                        panel.Controls.Add(checkBox);
                    }
                }
            }

            if (TweakPage.SubPages != null && TweakPage.SubPages.Count != 0)
            {
                panel.Controls.Add(new Label()
                {
                    Text = "Related Tweak Pages",
                    Font = new Font(this.Font, FontStyle.Bold),
                    Padding = new Padding(0, 0, 0, 4),
                    AutoSize = true
                });

                foreach (TweakPage subPage in TweakPage.SubPages)
                {
                    var label = new LinkLabel()
                    {
                        Text = subPage.Name,
                        AutoSize = true
                    };

                    label.LinkClicked += (s, e2) =>
                    {
                        if (ParentForm is MainForm form)
                        {
                            form.Select(subPage);
                        }
                    };

                    panel.Controls.Add(label);
                }
            }

            this.Controls.Add(panel);
            panel.Dock = DockStyle.Fill;
        }
    }
}