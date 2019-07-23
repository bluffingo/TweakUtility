using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TweakUtility.Helpers;
using TweakUtility.TweakPages;

namespace TweakUtility.Forms
{
    public partial class SplashForm : Form
    {
        public SplashForm() => this.InitializeComponent();

        private void SplashForm_Shown(object sender, EventArgs e)
        {
            SetStatus("Retrieving OS Version...");
            _ = OperatingSystemVersions.GetCurrentVersion();

            SetStatus("Retrieving folder icon...");
            Program.FolderIcon = NativeHelpers.GetIconFromGroup(@"%SystemRoot%\System32\shell32.dll", -4);

            SetStatus("Initializing pages...");
            Program.Pages.AddRange(new List<TweakPage>()
            {
                new CustomizationPage(),
                new InternetExplorerPage(),
                new SnippingToolPage(),
                new AdvancedPage(),
                new Windows10Page(),
                new UncategorizedPage()
            });

#if DEBUG
            SetStatus("Unlocking debug page...");
            Program.Pages.Add(new DebugPage());
#endif

            this.Close();
        }

        public void SetStatus(string status)
        {
            this.statusLabel.Text = status;
            this.statusLabel.Refresh();
            Application.DoEvents();
        }
    }
}