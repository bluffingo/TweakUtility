using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Shell;
using TweakUtility.Enums;

namespace TweakUtility.Tweaks.Forms
{
    public partial class SystemRestoreForm : Form
    {
        public string Description => descriptionTextBox.Text;

        public RestorePointEventType EventType =>
            (RestorePointEventType) Enum.Parse(typeof(RestorePointEventType), eventTypeComboBox.Text);

        public RestorePointType RestorePointType =>
            (RestorePointType) Enum.Parse(typeof(RestorePointType), restoreTypeComboBox.Text);

        public SystemRestoreForm()
        {
            InitializeComponent();
            eventTypeComboBox.Items.AddRange(
                Enum.GetNames(typeof(RestorePointEventType)).Select(x => (object) x).ToArray());
            restoreTypeComboBox.Items.AddRange(
                Enum.GetNames(typeof(RestorePointType)).Select(x => (object) x).ToArray());
        }

        private void OkButtonClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}