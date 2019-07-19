using System;
using System.Windows.Forms;

namespace TweakUtility.Forms
{
    public partial class SplashForm : Form
    {
        public SplashForm() => this.InitializeComponent();

        private void SplashForm_Shown(object sender, EventArgs e)
        {
            SetStatus("Retrieving OS Version...");
            _ = OperatingSystemVersions.GetCurrentVersion();

            ///SetStatus("Processing attributes...");
            ///foreach (TweakPage tweakPage in Program.Pages)
            ///{
            ///    ProcessPage(tweakPage);
            ///}

            this.Close();
        }

        ///public void ProcessPage(TweakPage tweakPage)
        ///{
        ///    foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(tweakPage))
        ///    {
        ///        Debug.WriteLine($"Processing attributes... ({tweakPage.GetType().Name}/{descriptor.Name})");
        ///        SetStatus($"Processing attributes... ({tweakPage.GetType().Name}/{descriptor.Name})");
        ///
        ///        #region Description Info Append
        ///
        ///        DefaultValueAttribute defaultValueAttribute = descriptor.GetAttribute<DefaultValueAttribute>();
        ///
        ///        if (defaultValueAttribute != null)
        ///        {
        ///            object defaultValue = defaultValueAttribute.GetHiddenValue("value");
        ///
        ///            DescriptionAttribute descriptionAttribute = descriptor.GetAttribute<DescriptionAttribute>();
        ///
        ///            if (descriptionAttribute != null)
        ///            {
        ///                descriptionAttribute.SetHiddenValue("description", $"{descriptionAttribute.GetHiddenValue<string>("description")}\nDefaults to {defaultValue.ToString()}");
        ///            }
        ///        }
        ///
        ///        #endregion Description Info Append
        ///    }
        ///}

        public void SetStatus(string status)
        {
            this.statusLabel.Text = status;
            this.statusLabel.Refresh();
            Application.DoEvents();
        }
    }
}