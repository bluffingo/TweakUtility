using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using TweakUtility.Attributes;
using TweakUtility.Controls;
using TweakUtility.Forms;
using TweakUtility.Helpers;
using TweakUtility.Theming;

namespace TweakUtility
{
    //TODO: Fix categories, (control order, non-categorized @ top)
    public partial class TweakPageView : UserControl
    {
        public TweakPageView(TweakPage tweakPage)
        {
            this.TweakPage = tweakPage;

            this.InitializeComponent();
        }

        public TweakPage TweakPage { get; }

        private void AddBooleanEntry(TweakOption option, Control panel)
        {
            var checkBox = new CheckBox()
            {
                Text = option.Name,
                AutoSize = true,
                Padding = new Padding(4, 0, 0, 0)
            };

            try
            {
                checkBox.Checked = option.GetValue<bool>();
                checkBox.Enabled = option.CanWrite;
            }
            catch
            {
                checkBox.Enabled = false;
                checkBox.CheckState = CheckState.Indeterminate;
            }

            checkBox.CheckedChanged += (s, e2) =>
            {
                option.SetValue(checkBox.Checked);
                this.CheckRefresh(option);
            };

            panel.Controls.Add(checkBox);
        }

        private void AddIntegerEntry(TweakOption option, Control panel)
        {
            var parent = new LabeledControl()
            {
                Text = option.Name,
                AutoSize = true
            };

            var upDown = new NumericUpDown()
            {
                Minimum = int.MinValue,
                Maximum = int.MaxValue
            };

            try
            {
                upDown.Value = option.GetValue<int>();
                upDown.Enabled = option.CanWrite;
            }
            catch
            {
                upDown.Enabled = false;
            }

            upDown.TextChanged += (s, e2) =>
            {
                option.SetValue((int)upDown.Value);
                this.CheckRefresh(option);
            };

            parent.Child = upDown;

            panel.Controls.Add(parent);
        }

        private void AddStringEntry(TweakOption option, Control panel)
        {
            var parent = new LabeledControl()
            {
                Text = option.Name,
                AutoSize = true
            };

            var textBox = new TextBox();

            try
            {
                textBox.Text = option.GetValue<string>();
                textBox.Enabled = option.CanWrite;
            }
            catch
            {
                textBox.Enabled = false;
            }

            textBox.TextChanged += (s, e2) =>
            {
                option.SetValue(textBox.Text);
                this.CheckRefresh(option);
            };

            parent.Child = textBox;

            panel.Controls.Add(parent);
        }

        private void AddEnumEntry(TweakOption option, Control panel)
        {
            var parent = new LabeledControl()
            {
                Text = option.Name,
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
                string valueDisplayName = item.GetAttribute<DisplayNameAttribute>()?.DisplayName ?? item.ToString();

                e2.Graphics.DrawString(valueDisplayName, comboBox.Font, new SolidBrush(e2.ForeColor), e2.Bounds.X, e2.Bounds.Y);
            };

            try
            {
                foreach (Enum value in Enum.GetValues(option.Type))
                {
                    comboBox.Items.Add(value);
                }

                comboBox.SelectedItem = option.GetValue<object>();

                comboBox.Enabled = option.CanWrite;
            }
            catch
            {
                comboBox.Enabled = false;
            }

            comboBox.SelectedValueChanged += (s, e2) =>
            {
                option.SetValue(comboBox.SelectedItem);
                this.CheckRefresh(option);
            };

            parent.Child = comboBox;

            panel.Controls.Add(parent);
        }

        private void AddColorEntry(TweakOption option, Control panel)
        {
            var colorButton = new ColorField()
            {
                Text = option.Name,
                AutoSize = true
            };

            try
            {
                colorButton.Color = option.GetValue<Color>();
            }
            catch
            {
                colorButton.Color = Color.Transparent;
                colorButton.Enabled = false;
            }

            colorButton.ColorChanged += (s, e2) =>
            {
                option.SetValue(colorButton.Color);
                this.CheckRefresh(option);
            };

            panel.Controls.Add(colorButton);
        }

        private void AddAction(TweakAction action, Control panel)
        {
            var button = new CommandControl()
            {
                Text = action.Name,
                AutoSize = true
            };

            button.Click += (s, e2) =>
            {
                action.Invoke();
                this.CheckRefresh(action);
            };

            panel.Controls.Add(button);
        }

        private void AddEntry(TweakEntry entry, Control panel)
        {
            if (!entry.Visible)
            {
                return;
            }

            try
            {
                if (entry.GetAttribute<NoticeAttribute>() is NoticeAttribute noticeAttribute)
                {
                    var control = new NoticeControl(noticeAttribute);
                    panel.Controls.Add(control);
                }

                if (entry is TweakAction action)
                {
                    this.AddAction(action, panel);
                    return;
                }

                if (entry is TweakOption option)
                {
                    if (option.Type == typeof(bool))
                        this.AddBooleanEntry(option, panel);
                    else if (option.Type == typeof(int))
                        this.AddIntegerEntry(option, panel);
                    else if (option.Type == typeof(string))
                        this.AddStringEntry(option, panel);
                    else if (option.Type.BaseType == typeof(Enum))
                        this.AddEnumEntry(option, panel);
                    else if (option.Type == typeof(Color))
                        this.AddColorEntry(option, panel);
                    else
                    //Display a fallback message to let the user know
                    {
                        panel.Controls.Add(new Label()
                        {
                            AutoSize = true,
                            Text = $"Unsupported property type {option.Type.ToString()} on property {option.Name}",
                            ForeColor = Color.Red
                        });
                    }
                }

                if (entry.GetAttribute<DescriptionAttribute>() is DescriptionAttribute descriptionAttribute)
                {
                    var control = new Label()
                    {
                        Text = descriptionAttribute.Description,
                        ForeColor = SystemColors.GrayText,
                        AutoSize = true,
                        Padding = new Padding(0, 0, 0, Constants.Design_Description_Padding_Bottom)
                    };

                    panel.Controls.Add(control);
                }
            }
            catch
            {
                panel.Controls.Add(new Label()
                {
                    AutoSize = true,
                    Text = $"Error displaying entry {entry.Name}",
                    ForeColor = Color.Red
                });
            }
        }

        private void CheckRefresh(TweakEntry entry)
        {
            RefreshRequiredAttribute attribute = entry.GetAttribute<RefreshRequiredAttribute>();

            if (attribute == null)
            {
                return;
            }

            if (attribute.Type == RestartType.ExplorerRestart)
            {
                DialogResult result = MessageBox.Show(Properties.Strings.Reload_ExplorerRestart, Properties.Strings.Application_Name, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Program.RestartExplorer();
                }
            }
            else if (attribute.Type == RestartType.SystemRestart)
            {
                DialogResult result = MessageBox.Show(Properties.Strings.Reload_SystemRestart, Properties.Strings.Application_Name, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    NativeMethods.ExitWindowsEx(NativeMethods.ExitWindows.Reboot, NativeMethods.ShutdownReason.MinorReconfig);
                }
            }
            else if (attribute.Type == RestartType.Logoff)
            {
                DialogResult result = MessageBox.Show(Properties.Strings.Reload_LogOff, Properties.Strings.Application_Name, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    NativeMethods.ExitWindowsEx(NativeMethods.ExitWindows.LogOff, NativeMethods.ShutdownReason.MinorReconfig);
                }
            }
            else if (attribute.Type == RestartType.TweakUtility)
            {
                DialogResult result = MessageBox.Show(Properties.Strings.Reload_TweakUtility, Properties.Strings.Application_Name, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Application.Restart();
                }
            }
            else if (attribute.Type == RestartType.Unknown)
            {
                MessageBox.Show(Properties.Strings.Reload_Unknown, Properties.Strings.Application_Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AddOptions(TweakPage tweakPage, Control panel)
        {
            var categories = new Dictionary<string, List<object>>();

            foreach (var entry in tweakPage.Entries)
            {
                string categoryName = entry.GetAttribute<CategoryAttribute>()?.Category;

                if (categoryName == null)
                {
                    categoryName = "";
                }

                if (!categories.ContainsKey(categoryName))
                {
                    categories[categoryName] = new List<object>();
                }

                categories[categoryName].Add(entry);
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
                            Font = Theme.CategoryFont,
                            AutoSize = true,
                            Padding = Constants.Design_Category_Padding,
                            Margin = new Padding(0),
                            ForeColor = Theme.CategoryForeground
                        });
                    }

                    foreach (TweakEntry entry in categories[category])
                    {
                        this.AddEntry(entry, panel);
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
                Font = Theme.CategoryFont,
                AutoSize = true,
                Padding = Constants.Design_Category_Padding,
                Margin = new Padding(0),
                ForeColor = Theme.CategoryForeground
            });

            //Links
            foreach (TweakPage subPage in this.TweakPage.SubPages)
            {
                var label = new CommandControl()
                {
                    Text = subPage.Name,
                    AutoSize = true
                };

                label.Click += (s, e) =>
                {
                    if (this.ParentForm is MainForm form)
                    {
                        form.Select(subPage);
                    }
                };

                panel.Controls.Add(label);
            }
        }

        private void TweakPageView_Load(object sender, EventArgs e)
        {
            var panel = new FlowLayoutPanel()
            {
                AutoScroll = true,
                WrapContents = false, //We don't intend controls to flow over to the right.
                FlowDirection = FlowDirection.TopDown,
                Dock = DockStyle.Fill,
                Padding = new Padding(SystemInformation.VerticalScrollBarWidth)
            };

            panel.HorizontalScroll.Enabled = false;

            panel.Controls.Add(new Label()
            {
                Text = this.TweakPage.Name,
                Font = Theme.TitleFont,
                AutoSize = true,
                Padding = new Padding(0, 0, 0, 8),
                Margin = new Padding(0),
                ForeColor = Theme.TitleForeground
            });

            if (this.TweakPage.GetType().GetAttribute<DescriptionAttribute>() is DescriptionAttribute descriptionAttribute)
            {
                var control = new Label()
                {
                    Text = descriptionAttribute.Description,
                    ForeColor = SystemColors.GrayText,
                    AutoSize = true,
                    Padding = new Padding(0, 0, 0, Constants.Design_Description_Padding_Bottom)
                };

                panel.Controls.Add(control);
            }

            if (this.TweakPage.GetType().GetAttribute<NoticeAttribute>() is NoticeAttribute noticeAttribute)
            {
                var control = new NoticeControl(noticeAttribute);
                panel.Controls.Add(control);
            }

            this.AddOptions(this.TweakPage, panel);

            this.AddSubPages(this.TweakPage, panel);

            this.Controls.Add(panel);
        }
    }
}