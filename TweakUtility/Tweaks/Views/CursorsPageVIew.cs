using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TweakUtility.Tweaks.Model;

namespace TweakUtility.Tweaks.Views
{
    public partial class CursorsPageView : UserControl
    {
        public CursorsPageView()
        {
            InitializeComponent();
        }

        private void CursorsPageView_Load(object sender, EventArgs e)
        {
            var currentName = CursorScheme.GetCurrentCursorSchemeName();
            var schemes = CursorScheme.GetAvailableCursorSchemes();

            comboBox.Items.AddRange(schemes);

            comboBox.SelectedItem = schemes.FirstOrDefault(cs => cs.Name == currentName);
        }

        private void comboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            void addItem(string name, string path)
            {
                listView.Items.Add(name, name);

                if (!string.IsNullOrWhiteSpace(path))
                {
                    imageList.Images.Add(name, GetImage(path, imageList.ImageSize.Width));
                }
            }

            var cursorScheme = (CursorScheme)comboBox.SelectedItem;

            listView.Items.Clear();
            imageList.Images.Clear();

            addItem("Normal select", cursorScheme.Arrow);
            addItem("Help Select", cursorScheme.Help);
            //addItem("Working In Background", cursorScheme.AppStarting);
            //addItem("Busy", cursorScheme.Wait);
            addItem("Handwriting", cursorScheme.NWPen);
            addItem("Unavailable", cursorScheme.No);
            addItem("Vertical Resize", cursorScheme.SizeNS);
            addItem("Horizontal Resize", cursorScheme.SizeWE);
            addItem("Diagonal Resize 1", cursorScheme.SizeNESW);
            addItem("Diagonal Resize 2", cursorScheme.SizeNWSE);
            addItem("Move", cursorScheme.SizeAll);
            addItem("Alternate Select", cursorScheme.UpArrow);
            addItem("Link Select", cursorScheme.Hand);
        }

        private Image GetImage(string filePath, int iconSize)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("message", nameof(filePath));

            var bitmap = new Bitmap(iconSize, iconSize);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                try
                {
                    using (var cursor = new Cursor(filePath))
                    {
                        cursor.Draw(graphics, new Rectangle(new Point(0, 0), bitmap.Size));
                    }
                }
                catch
                {
                    bitmap.Dispose();
                    return null;
                }
            }

            return bitmap;
        }
    }
}