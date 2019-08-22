using System;
using System.Windows.Forms;

namespace TweakUtility.Forms
{
    public partial class HostsItemForm : Form
    {
        public HostsItemForm()
        {
            InitializeComponent();

            ipLabel.Text = Properties.Strings.Hosts_IPAddress + ":";
            hostLabel.Text = Properties.Strings.Hosts_Host + ":";

            ipTextBox.TextChanged += this.TextBox_TextChanged;
            hostTextBox.TextChanged += this.TextBox_TextChanged;
        }

        private void TextBox_TextChanged(object sender, EventArgs e) => this.mainButton.Enabled = (!string.IsNullOrWhiteSpace(ipTextBox.Text) && !string.IsNullOrWhiteSpace(hostTextBox.Text));

        public static string[] ShowDialog(Form parent, params string[] values)
        {
            bool editMode = values.Length != 0;

            using (var form = new HostsItemForm())
            {
                if (editMode)
                {
                    form.ipTextBox.Text = values[0];
                    form.hostTextBox.Text = values[1];
                }

                form.Text = editMode ? Properties.Strings.Hosts_EditTitle : Properties.Strings.Hosts_AddTitle;
                form.mainButton.Text = editMode ? Properties.Strings.Button_Apply : Properties.Strings.Button_OK;

                if (((Form)form).ShowDialog(parent) == DialogResult.OK)
                {
                    return new string[2] { form.ipTextBox.Text, form.hostTextBox.Text };
                }
                else
                {
                    return null;
                }
            }
        }

        private void MainButton_Click(object sender, System.EventArgs e) => this.Close();

        private void HostsItemForm_Load(object sender, EventArgs e)
        {
        }
    }
}