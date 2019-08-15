using System;

using TweakUtility.Forms;

namespace TweakUtility
{
    public class ProgressIndicator : IDisposable
    {
        private ProgressForm form;

        public int Maximum { get; private set; }

        public int Value { get; private set; }

        public void Dispose()
        {
            if (form != null)
            {
                form.Dispose();
            }
        }

        public void Initialize(int maximum)
        {
            if (form == null)
            {
                form = new ProgressForm();
                form.Show();
                this.Maximum = maximum;
            }
        }

        public void SetProgress(int value, string status = "")
        {
            if (form != null)
            {
                form.SetProgress(value, this.Maximum, status);
            }
        }
    }
}