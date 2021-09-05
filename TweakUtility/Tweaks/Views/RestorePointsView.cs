using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Management;
using System.Threading.Tasks;
using TweakUtility.Enums;
using TweakUtility.Helpers;
using TweakUtility.Tweaks.Forms;

namespace TweakUtility.Tweaks.Views
{
    public partial class RestorePointsView : UserControl
    {
        private ListViewItem _selectedItem = null;

        public RestorePointsView()
        {
            InitializeComponent();
        }

        private void RestorePointsViewLoad(object sender, EventArgs e)
        {
            var class_ = new ManagementClass("\\\\.\\root\\default", "systemrestore",
                new System.Management.ObjectGetOptions());
            var instances = class_.GetInstances();

            var eventTypes =
                ((RestorePointEventType[]) Enum.GetValues(typeof(RestorePointEventType))).Select(x => (uint) x)
                .ToArray();
            var restoreTypes =
                ((RestorePointType[]) Enum.GetValues(typeof(RestorePointType))).Select(x => (uint) x).ToArray();

            foreach (var restore in instances)
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
                this.listView.Items.Add(item);
            }
        }

        private void SetWorking(bool working)
        {
            this.listView.Enabled = !working;
            this.addButton.Enabled = !working;
            this.removeButton.Enabled = !working;
            this.progressBar.Value = working ? 100 : 0;
            this.progressBar.Style = working ? ProgressBarStyle.Marquee : ProgressBarStyle.Continuous;
        }

        private void ListViewItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            this.removeButton.Enabled = e.IsSelected;
            this._selectedItem = e.Item;
        }

        private void AddButtonClick(object sender, EventArgs e)
        {
            var diag = new SystemRestoreForm();
            if (diag.ShowDialog() == DialogResult.Cancel) return;
            RunActionAsync(() =>
            {
                var scope = new ManagementScope("\\\\.\\root\\default");
                var path = new ManagementPath("SystemRestore");
                var options = new ObjectGetOptions();
                using (var class_ = new ManagementClass(scope, path, options))
                using (var parameters = class_.GetMethodParameters("CreateRestorePoint"))
                {
                    parameters["Description"] = diag.Description;
                    parameters["EventType"] = (uint) diag.EventType;
                    parameters["RestorePointType"] = (uint) diag.RestorePointType;
                    class_.InvokeMethod("CreateRestorePoint", parameters, null);
                }
            });
        }

        private void RemoveButtonClick(object sender, EventArgs e)
        {
            if (this._selectedItem == null)
            {
                this.removeButton.Enabled = false;
                return;
            }

            var seqNum = uint.Parse(this._selectedItem.SubItems[2].Text);
            if (Properties.Settings.Default.RestorePointDeletionWarning)
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (MessageBox.Show(
                    $"You are about to delete the System Restore Point: '{this._selectedItem.SubItems[0].Text}' ({seqNum})\n" +
                    "This message is a safe guard.\nDo you want to see this message again the next time?",
                    $"Delete {this._selectedItem.SubItems[0].Text}", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Cancel:
                        return;
                    case DialogResult.No:
                        Properties.Settings.Default.RestorePointDeletionWarning = false;
                        break;
                    case DialogResult.Yes:
                        Properties.Settings.Default.RestorePointDeletionWarning = true;
                        break;
                }

            RunActionAsync(() => { NativeMethods.SRRemoveRestorePoint(seqNum); });
        }

        private async void RunActionAsync(Action action)
        {
            SetWorking(true);
            this.statusLabel.Text = "Working... ";
            var workTask = Task.Run(action);
            var working = true;
            var countTask = Task.Run(() =>
            {
                var sw = Stopwatch.StartNew();
                while (working)
                {
                    try
                    {
                        this.statusLabel.Text = $"Working... {Math.Round((sw.Elapsed.TotalMilliseconds / 1000f), 2)}s";
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    Task.Delay(100);
                }

                this.statusLabel.Text = $"Finished within {sw.Elapsed}";
            });

            await Task.WhenAny(workTask, countTask);
            await Task.Delay(5000);
            this.listView.Items.Clear();
            RestorePointsViewLoad(null, null); // Doing some hackery here
            working = false;
            SetWorking(false);
        }

        private void ListViewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                RemoveButtonClick(sender, e); // More hackery
            }
        }
    }
}