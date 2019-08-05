using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TweakUtility.Forms;

namespace TweakUtility
{
    public class ProgressIndicator : IDisposable
    {
        private ProgressForm form;

        public int Maximum { get; private set; }

        public void Dispose() => form.Dispose();

        public void Initialize(int maximum)
        {
            if (form != null)
            {
                form = new ProgressForm();
                this.Maximum = maximum;
            }
        }

        public void SetProgress(int value, string status = "") => form.SetProgress(value, this.Maximum, status);
    }
}