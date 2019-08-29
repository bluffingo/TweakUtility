using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        }

        public new void Close()
        {
            formInitiatedClose = true;
            base.Close();
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
                typeof(SoftwarePage),
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
                //Gets all requirement attributes and checks if there's an invalid one.
                if (!Helpers.Helpers.RequirementsMet(pageType))
                {
                    continue;
                }

                this.SetStatus($"Initializing pages... ({pageType.Name})");

                try
                {
                    object instance = Activator.CreateInstance(pageType, true);

                    Debug.Assert(instance is TweakPage);

                    Program.Pages.Add(instance as TweakPage);
                }
                catch (Exception ex)
                {
                    if (ex.InnerException is UnauthorizedAccessException)
                    {
                        MessageBox.Show(string.Format(Properties.Strings.TweakPage_InsufficientPermissions, pageType.Name), Properties.Strings.Application_Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        Debug.Fail(ex.ToString());
                        MessageBox.Show(string.Format(Properties.Strings.TweakPage_LoadError, pageType.Name, ex.Message), Properties.Strings.Application_Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }
    }
}