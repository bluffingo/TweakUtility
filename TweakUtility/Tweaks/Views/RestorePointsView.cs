using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using TweakUtility.Enums;

namespace TweakUtility.Tweaks.Views
{
    public partial class RestorePointsView : UserControl
    {
        public RestorePointsView()
        {
            InitializeComponent();
        }

        private void RestorePointsViewLoad(object sender, EventArgs e)
        {
            var sysRestores = new ManagementClass("\\\\.\\root\\default", "systemrestore",
                new System.Management.ObjectGetOptions());
            var restores = sysRestores.GetInstances();

            var eventTypes =
                ((RestorePointEventType[]) Enum.GetValues(typeof(RestorePointEventType))).Select(x => (uint) x);
            var restoreTypes =
                ((RestorePointType[]) Enum.GetValues(typeof(RestorePointType))).Select(x => (uint) x);

            foreach (var restore in restores)
            {
                var eventType = (uint) restore["eventtype"];
                var restoreType = (uint) restore["restorepointtype"];
                var item = new ListViewItem(new string[]
                {
                    (string) restore["description"],
                    DateTime.ParseExact(((string) restore["creationtime"]).Split('.')[0],
                            "yyyyMMddHHmmss", CultureInfo.CurrentCulture, DateTimeStyles.AssumeUniversal)
                        .ToString("dd.MM.yyyy hh:mm:ss"),
                    ((uint) restore["sequencenumber"]).ToString(),
                    eventTypes.Any(x => x == eventType)
                        ? ((RestorePointEventType) eventType).ToString()
                        : eventType.ToString(),
                    restoreTypes.Any(x => x == restoreType)
                        ? ((RestorePointType) restoreType).ToString()
                        : restoreType.ToString()
                });
                listView.Items.Add(item);
            }
        }

        private void AddButtonClick(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void EditButtonClick(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void RemoveButtonClick(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void ListViewItemActivate(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void ListViewMouseClick(object sender, MouseEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void ListViewKeyUp(object sender, KeyEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void ListViewItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}