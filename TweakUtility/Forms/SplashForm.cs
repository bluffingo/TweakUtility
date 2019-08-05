using System;
using System.Diagnostics;
using System.Windows.Forms;
using TweakUtility.Attributes;
using TweakUtility.Helpers;
using TweakUtility.TweakPages;

namespace TweakUtility.Forms
{
    internal partial class SplashForm : Form
    {
        internal SplashForm() => this.InitializeComponent();

        /// <summary>
        /// This prevents the application showing the main form if an error occurred while launching.
        /// </summary>
        private bool formInitiatedClose = false;

        private void SplashForm_Shown(object sender, EventArgs e)
        {
            this.SetStatus("Creating folders...");
            Program.CreateFolders();

            this.SetStatus("Retrieving OS Version...");
            _ = OperatingSystemVersions.CurrentVersion;

            this.SetStatus("Retrieving folder icon...");
            Program.FolderIcon = NativeHelpers.ExtractIcon(@"%SystemRoot%\System32\shell32.dll", -4);

            InitializePages();

#if DEBUG
            this.SetStatus("Unlocking debug page...");
            Program.Pages.Add(new DebugPage());
#endif

            formInitiatedClose = true;
            this.Close();
        }

        public void SetStatus(string status)
        {
            statusLabel.Text = status;
            statusLabel.Refresh();
            Application.DoEvents();
        }

        private void SplashForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!formInitiatedClose)
            {
                Application.Exit();
            }
        }

        public void InitializePages()
        {
            this.SetStatus("Initializing pages...");
            foreach (Type pageType in new Type[] {
                typeof(CustomizationPage),
                typeof(InternetExplorerPage),
                typeof(SnippingToolPage),
                typeof(AdvancedPage),
                typeof(Windows10Page),
                typeof(MsnMessengerPage),
                typeof(UncategorizedPage)
            })
            {
                OperatingSystemSupportedAttribute osAttribute = pageType.GetAttribute<OperatingSystemSupportedAttribute>();
                if (osAttribute != null && !osAttribute.IsSupported)
                {
                    continue;
                }

                RegistryKeyRequiredAttribute keyAttribute = pageType.GetAttribute<RegistryKeyRequiredAttribute>();
                if (keyAttribute != null && !keyAttribute.Exists)
                {
                    continue;
                }

                this.SetStatus($"Initializing pages... ({pageType.Name})");
                object instance = Activator.CreateInstance(pageType, true);

                Debug.Assert(instance is TweakPage);

                Program.Pages.Add(instance as TweakPage);
            }
        }
    }
}