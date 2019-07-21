using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using TweakUtility.Attributes;
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

            //Hide incompatible entries
            var supportedAttribute = option.GetAttributeReflection<OperatingSystemSupportedAttribute>();
            if (supportedAttribute != null && !supportedAttribute.IsSupported())
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

                    var button = new CommandControl()
                    {
                        Text = displayName,
                        AutoSize = true
                    };

                    button.Click += (s, e2) =>
                    {
                        method.Invoke(this.TweakPage, null);
                        CheckRefresh(method);
                    };

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
                            CheckRefresh(property);
                        };

                        panel.Controls.Add(checkBox);
                    }
                    else if (property.PropertyType == typeof(int))
                    {
                        var parent = new LabeledControl()
                        {
                            Text = displayName,
                            AutoSize = true
                        };

                        var upDown = new NumericUpDown()
                        {
                            Minimum = int.MinValue,
                            Maximum = int.MaxValue
                        };

                        try
                        {
                            upDown.Value = (int)property.GetValue(tweakPage, null);
                            upDown.Enabled = property.CanWrite;
                        }
                        catch
                        {
                            upDown.Enabled = false;
                        }

                        upDown.TextChanged += (s, e2) =>
                        {
                            property.SetValue(tweakPage, (int)upDown.Value, null);
                            CheckRefresh(property);
                        };

                        parent.Child = upDown;

                        panel.Controls.Add(parent);
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
                            CheckRefresh(property);
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
                            CheckRefresh(property);
                        };

                        parent.Child = comboBox;

                        panel.Controls.Add(parent);
                    }
                    else if (property.PropertyType == typeof(Color))
                    {
                        var colorButton = new ColorField()
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
                            CheckRefresh(property);
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

        private void CheckRefresh(object info)
        {
            var attribute = info.GetAttributeReflection<RefreshRequiredAttribute>();

            if (attribute == null)
            {
                return;
            }

            if (attribute.Type == RestartType.ExplorerRestart)
            {
                DialogResult result = MessageBox.Show(
                        "This option requires you to restart Windows Explorer.\nWould you like to do that now?",
                        "Tweak Utility",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Program.RestartExplorer();
                }
            }
            else if (attribute.Type == RestartType.SystemRestart)
            {
                DialogResult result = MessageBox.Show(
                        "This option requires you to restart your system.\nWould you like to do that now?",
                        "Tweak Utility",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    NativeMethods.ExitWindowsEx(NativeMethods.ExitWindows.Reboot, NativeMethods.ShutdownReason.MinorReconfig);
                }
            }
            else if (attribute.Type == RestartType.Logoff)
            {
                DialogResult result = MessageBox.Show(
                        "This option requires you to log off.\nWould you like to do that now?",
                        "Tweak Utility",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    NativeMethods.ExitWindowsEx(NativeMethods.ExitWindows.LogOff, NativeMethods.ShutdownReason.MinorReconfig);
                }
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

                if (category == null)
                {
                    category = "";
                }

                if (!categories.ContainsKey(category))
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
                            Font = new Font(this.Font.FontFamily, Program.Config.CurrentTheme.CategorySize, GraphicsUnit.Point),
                            AutoSize = true,
                            Padding = Constants.DESIGN_CATEGOTY_PADDING,
                            Margin = new Padding(0),
                            ForeColor = Program.Config.CurrentTheme.CategoryForeground
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
            panel.Controls.Add(new Label()
            {
                Text = "Related Tweak Pages",
                Font = new Font(this.Font.FontFamily, Program.Config.CurrentTheme.CategorySize, GraphicsUnit.Point),
                AutoSize = true,
                Padding = Constants.DESIGN_CATEGOTY_PADDING,
                Margin = new Padding(0),
                ForeColor = Program.Config.CurrentTheme.CategoryForeground
            });

            //Links
            foreach (TweakPage subPage in TweakPage.SubPages)
            {
                var label = new CommandControl()
                {
                    Text = subPage.Name,
                    AutoSize = true
                };

                label.Click += (s, e) =>
                {
                    if (ParentForm is MainForm form)
                    {
                        form.Select(subPage);
                    }
                };

                panel.Controls.Add(label);
            }
        }

        private List<MethodInfo> GetMethods(TweakPage tweakPage) => tweakPage.GetType().GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance).Where(m =>
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
                WrapContents = false, //We don't intend controls to flow over to the right.
                FlowDirection = FlowDirection.TopDown,
                Dock = DockStyle.Fill,
                Padding = new Padding(0, 0, SystemInformation.VerticalScrollBarWidth, 0)
            };

            panel.HorizontalScroll.Enabled = false;

            panel.Controls.Add(new Label()
            {
                Text = TweakPage.Name,
                Font = new Font(this.Font.FontFamily, Program.Config.CurrentTheme.TitleSize, FontStyle.Regular, GraphicsUnit.Point),
                AutoSize = true,
                Padding = new Padding(0, 0, 0, 8),
                Margin = new Padding(0),
                ForeColor = Program.Config.CurrentTheme.TitleForeground
            });

            AddOptions(TweakPage, panel);

            AddSubPages(TweakPage, panel);

            this.Controls.Add(panel);
        }
    }
}