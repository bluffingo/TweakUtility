using System;
using System.Windows.Forms;
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
            eventTypeComboBox.Items.AddRange(Enum.GetNames(typeof(RestorePointEventType)));
            restoreTypeComboBox.Items.AddRange(Enum.GetNames(typeof(RestorePointType)));
        }
    }
}