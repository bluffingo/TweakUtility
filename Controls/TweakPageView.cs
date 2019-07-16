using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using TweakUtility.Controls;
using TweakUtility.Forms;
using TweakUtility.TweakPages;

namespace TweakUtility
{
    public partial class TweakPageView : UserControl
    {
        public TweakPageView(TweakPage tweakPage)
        {
            this.TweakPage = tweakPage;

            InitializeComponent();
        }

        public TweakPage TweakPage { get; }

        private void AddOption(object option, TweakPage tweakPage, Control panel)
        {
            //Skip property if explicitly set to hide.
            var browsableAttribute = option.GetAttributeReflection<BrowsableAttribute>();
            if (browsableAttribute != null && !browsableAttribute.Browsable)
            {
                return;
            }

            var supportedAttribute = option.GetAttributeReflection<OperatingSystemSupportedAttribute>();
            if (supportedAttribute != null && !Program.IsSupported(supportedAttribute.Mininum, supportedAttribute.Maximum))
            {
                return;
            }

            //Use display name as label, if not available use name as fallback.
            string displayName = option.GetAttributeReflection<DisplayNameAttribute>()?.DisplayName;

            try
            {
                if (option is MethodInfo method)
                {
                    displayName = displayName ?? method.Name;

                    var button = new Button()
                    {
                        Text = displayName,
                        AutoSize = true
                    };

                    button.Click += (s, e2) => method.Invoke(TweakPage, null);

                    panel.Controls.Add(button);

                    return;
                }
                else if (option is PropertyInfo property)
                {
                    //Use display name as label, if not available use property name as fallback.
                    displayName = displayName ?? property.Name;

                    if (property.PropertyType == typeof(bool))
                    {
                        var checkBox = new CheckBox()
                        {
                            Text = displayName,
                            AutoSize = true
                        };

                        try
                        {
                            checkBox.Checked = (bool)property.GetValue(tweakPage, null);
                            checkBox.Enabled = property.CanWrite;
                        }
                        catch
                        {
                            checkBox.Enabled = false;
                            checkBox.CheckState = CheckState.Indeterminate;
                        }

                        checkBox.CheckedChanged += (s, e2) =>
                        {
                            property.SetValue(tweakPage, checkBox.Checked, null);
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
                            textBox.Text = (string)property.GetValue(tweakPage, null);
                            textBox.Enabled = property.CanWrite;
                        }
                        catch
                        {
                            textBox.Enabled = false;
                        }

                        textBox.TextChanged += (s, e2) =>
                        {
                            property.SetValue(tweakPage, textBox.Text, null);
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

                            comboBox.SelectedValue = property.GetValue(tweakPage, null);

                            comboBox.Enabled = property.CanWrite;
                        }
                        catch
                        {
                            comboBox.Enabled = false;
                        }

                        comboBox.SelectedValueChanged += (s, e2) =>
                        {
                            property.SetValue(tweakPage, comboBox.SelectedValue, null);
                        };

                        parent.Child = comboBox;

                        panel.Controls.Add(parent);
                    }
                    else if (property.PropertyType == typeof(Color))
                    {
                        var colorButton = new ColorButton()
                        {
                            Text = displayName,
                            AutoSize = true
                        };

                        try
                        {
                            colorButton.Color = (Color)property.GetValue(tweakPage, null);
                        }
                        catch
                        {
                            colorButton.Color = Color.Transparent;
                            colorButton.Enabled = false;
                        }

                        colorButton.ColorChanged += (s, e2) =>
                        {
                            property.SetValue(tweakPage, colorButton.Color, null);
                        };

                        panel.Controls.Add(colorButton);
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
            catch
            {
                panel.Controls.Add(new Label()
                {
                    AutoSize = true,
                    Text = $"Error displaying option {displayName}",
                    ForeColor = Color.Red
                });
            }
        }

        private void AddOptions(TweakPage tweakPage, Control panel)
        {
            var properties = GetProperties(tweakPage);
            var methods = GetMethods(tweakPage);

            var categories = new Dictionary<string, List<object>>();

            foreach (PropertyInfo property in properties)
            {
                string category = property.GetAttribute<CategoryAttribute>()?.Category;

                if (category == null)
                {
                    category = "";
                }

                if (!categories.ContainsKey(category))
                {
                    categories[category] = new List<object>();
                }

                categories[category].Add(property);
            }

            foreach (MethodInfo method in methods)
            {
                string category = method.GetAttribute<CategoryAttribute>()?.Category;

                if (categories.ContainsKey(category))
                {
                    categories[category] = new List<object>();
                }

                categories[category].Add(method);
            }

            if (categories.Count == 0)
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
                foreach (string category in categories.Keys)
                {
                    if (category != "")
                    {
                        panel.Controls.Add(new Label()
                        {
                            Text = category,
                            Font = new Font(this.Font.FontFamily, 11, GraphicsUnit.Point),
                            AutoSize = true,
                            Padding = new Padding(0, 0, 0, 8),
                            Margin = new Padding(0),
                            ForeColor = Color.FromArgb(0, 51, 153)
                        });
                    }

                    foreach (object option in categories[category])
                    {
                        AddOption(option, tweakPage, panel);
                    }
                }
            }
        }

        private void AddSubPages(TweakPage tweakPage, Control panel)
        {
            //Skip if there are no sub-pages
            if (tweakPage.SubPages?.Count == 0)
            {
                return;
            }

            //Header
            panel.Controls.Add(GetHeader("Related Tweak Pages", 8, 4, true));

            //Links
            foreach (TweakPage subPage in TweakPage.SubPages)
            {
                var label = new LinkLabel()
                {
                    Text = subPage.Name,
                    AutoSize = true
                };

                label.LinkClicked += (s, e) =>
                {
                    if (ParentForm is MainForm form)
                    {
                        form.Select(subPage);
                    }
                };

                panel.Controls.Add(label);
            }
        }

        private Label GetHeader(string title, int fontSize, int spacing = 8, bool bold = false) => new Label()
        {
            Text = title,
            Font = new Font(this.Font.FontFamily, fontSize, bold ? FontStyle.Bold : FontStyle.Regular, GraphicsUnit.Point),
            AutoSize = true,
            Padding = new Padding(0, 0, 0, spacing),
            Margin = new Padding(0),
        };

        private List<MethodInfo> GetMethods(TweakPage tweakPage) => tweakPage.GetType().GetMethods(BindingFlags.Public | BindingFlags.Static).Where(m =>
        {
            //Only include methods that explicitly want to be included
            BrowsableAttribute a = m.GetAttribute<BrowsableAttribute>();
            return a != null && a.Browsable;
        }).ToList();

        private List<PropertyInfo> GetProperties(TweakPage tweakPage) => tweakPage.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static).ToList();

        private void TweakPageView_Load(object sender, EventArgs e)
        {
            var panel = new FlowLayoutPanel()
            {
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = true
            };

            var header = GetHeader(TweakPage.Name, 14);
            panel.Controls.Add(header);

            AddOptions(TweakPage, panel);

            AddSubPages(TweakPage, panel);

            this.Controls.Add(panel);
            panel.Dock = DockStyle.Fill;
        }
    }
}