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
using TweakUtility.Controls;

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
                //Fallback message for empty page
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
                    //Skip property if explicitly set to hide.
                    BrowsableAttribute browsableAttribute = property.GetAttribute<BrowsableAttribute>();
                    if (browsableAttribute != null && !browsableAttribute.Browsable)
                    {
                        continue;
                    }

                    //Use display name as label, if not available use property name as fallback.
                    string displayName = property.GetAttribute<DisplayNameAttribute>()?.DisplayName ?? property.Name;

                    if (property.PropertyType == typeof(bool))
                    {
                        var checkBox = new CheckBox()
                        {
                            Text = displayName,
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
                    else if (property.PropertyType == typeof(string))
                    {
                        var parent = new LabeledControl()
                        {
                            Text = displayName,
                            AutoSize = true
                        };

                        var textBox = new TextBox();

                        try
                        {
                            textBox.Text = (string)property.GetValue(TweakPage, null);
                            textBox.Enabled = property.CanWrite;
                        }
                        catch
                        {
                            textBox.Enabled = false;
                        }

                        textBox.TextChanged += (s, e2) =>
                        {
                            property.SetValue(TweakPage, textBox.Text, null);
                        };

                        parent.Child = textBox;

                        panel.Controls.Add(parent);
                    }
                    else if (property.PropertyType.BaseType == typeof(Enum))
                    {
                        var parent = new LabeledControl()
                        {
                            Text = displayName,
                            AutoSize = true
                        };

                        var comboBox = new ComboBox()
                        {
                            DropDownStyle = ComboBoxStyle.DropDownList,
                            DrawMode = DrawMode.OwnerDrawVariable
                        };

                        comboBox.DrawItem += (s, e2) =>
                        {
                            //Can't render empty items.
                            if (e2.Index < 0)
                            {
                                return;
                            }

                            e2.DrawBackground();

                            //Use display name as label, if not available use property name as fallback.
                            var item = (Enum)comboBox.Items[e2.Index];
                            string valueDisplayName = item.GetAttribute<DescriptionAttribute>()?.Description ?? item.ToString();

                            e2.Graphics.DrawString(valueDisplayName, comboBox.Font, new SolidBrush(e2.ForeColor), e2.Bounds.X, e2.Bounds.Y);
                        };

                        try
                        {
                            foreach (Enum value in Enum.GetValues(property.PropertyType))
                            {
                                comboBox.Items.Add(value);
                            }

                            comboBox.SelectedValue = property.GetValue(TweakPage, null);

                            comboBox.Enabled = property.CanWrite;
                        }
                        catch
                        {
                            comboBox.Enabled = false;
                        }

                        comboBox.SelectedValueChanged += (s, e2) =>
                        {
                            property.SetValue(TweakPage, comboBox.SelectedValue, null);
                        };

                        parent.Child = comboBox;

                        panel.Controls.Add(parent);
                    }
                    else
                    {
                        //Display a fallback message to let the user know,
                        //so they can bully the developers of t.... fuck
                        panel.Controls.Add(new Label()
                        {
                            AutoSize = true,
                            Text = $"Unsupported property type {property.PropertyType.ToString()} on property {property.Name}",
                            ForeColor = Color.Red
                        });
                    }
                }
            }

            if (TweakPage.SubPages?.Count != 0)
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

                    label.LinkClicked += (_, __) =>
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