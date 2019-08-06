using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

using TweakUtility.Attributes;
using TweakUtility.Extensions;
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
            _ = OperatingSystemVersions.CurrentVersion; //This causes the property to be called

            this.SetStatus("Loading extensions...");
            Program.Loader.LoadExtensions();

            this.SetStatus("Initializing pages...");
            this.InitializePages();

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
            var types = new List<Type>() {
                typeof(CustomizationPage),
                typeof(InternetExplorerPage),
                typeof(SnippingToolPage),
                typeof(AdvancedPage),
                typeof(Windows10Page),
                typeof(MsnMessengerPage),
                typeof(UncategorizedPage)
            };

            foreach (Extension extension in Program.Loader.Extensions)
            {
                types.AddRange(extension.GetTweakPages());
            }

            foreach (Type pageType in types)
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