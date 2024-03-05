using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using TweakUtility.Helpers;
using TweakUtility.Tweaks.Model;

namespace TweakUtility.Tweaks.Views
{
    public partial class BackgroundsPageView : UserControl
    {
        public BackgroundsPageView()
        {
            InitializeComponent();
        }

        public static string DesktopBackgroundPath => RegistryHelper.GetValue<string>(@"HKCU\Control Panel\Desktop\Wallpaper");

        public static string LoginBackgroundDirectoryPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), @"oobe\info\backgrounds");

        public static string LoginBackgroundPath => Path.Combine(LoginBackgroundDirectoryPath, "backgroundDefault.jpg");

        public static LogonStyle LogonStyle
        {
            get => (LogonStyle)RegistryHelper.GetValue<int>(@"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\ButtonSet");
            set => RegistryHelper.SetValue(@"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\ButtonSet", (int)value);
        }

        public OpenFileDialog OpenFileDialog { get; } = new OpenFileDialog()
        {
            Title = "Choose a background"
        };

        //Style
        public static WallpaperStyle DesktopStyle
        {
            get
            {
                if (RegistryHelper.GetValue<string>(@"HKCU\Control Panel\Desktop\TileWallpaper") == "1")
                    return WallpaperStyle.Tiled;
                else
                    return (WallpaperStyle)int.Parse(RegistryHelper.GetValue<string>(@"HKCU\Control Panel\Desktop\WallpaperStyle"));
            }
            set
            {
                RegistryHelper.SetValue(@"HKCU\Control Panel\Desktop\TileWallpaper", value == WallpaperStyle.Tiled ? "1" : "0");
                RegistryHelper.SetValue(@"HKCU\Control Panel\Desktop\WallpaperStyle", ((int)value).ToString());
            }
        }

        private static Bitmap ResizeImage(Bitmap bitmap, int width, int height)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException(nameof(bitmap));
            }

            //Check if a resize is needed.
            if (bitmap.Size.Equals(new Size(width, height)))
            {
                return bitmap;
            }

            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            destImage.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);
            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.CompositingQuality = CompositingQuality.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(bitmap, destRect, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return destImage;
        }

        private void BackgroundsPageView_Load(object sender, EventArgs e)
        {
            foreach (WallpaperStyle style in Enum.GetValues(typeof(WallpaperStyle)))
            {
                desktopStyleComboBox.Items.Add(style);
            }

            desktopStyleComboBox.SelectedItem = DesktopStyle;

            foreach (LogonStyle style in Enum.GetValues(typeof(LogonStyle)))
            {
                logonStyleComboBox.Items.Add(style);
            }

            logonStyleComboBox.SelectedItem = LogonStyle;

            setBackgrounds();
        }

        private Image CheckOversizedResolution(Image input)
        {
            var screenSize = Screen.PrimaryScreen.Bounds;
            if (screenSize.Width < input.Width && screenSize.Height < input.Height)
            {
                var result = MessageBox.Show("The image is currently bigger than your current screen resolution.\n\nWould you like to scale it down?", Properties.Strings.Application_Name, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    var percentage = (float)screenSize.Width / (float)input.Width;
                    var width = (int)(input.Width * percentage);
                    var height = (int)(input.Height * percentage);

                    return ResizeImage((Bitmap)input, width, height);
                }
            }

            return input;
        }

        private void desktopPictureBox_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                NativeMethods.SystemParametersInfo(20, 0, OpenFileDialog.FileName, 3);
                setBackgrounds();
            }
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            foreach (var codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        private void loginPictureBox_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                var input = Image.FromFile(OpenFileDialog.FileName);
                input = CheckOversizedResolution(input);

                SetBackgroundWindows7(input);

                setBackgrounds();
            }
        }

        private void loginPreviewLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NativeMethods.LockWorkStation();
        }

        private void loginRestoreLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegistryHelper.SetValue(@"HKLM\Software\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background\OEMBackground", 0);
            setBackgrounds();
        }

        private void qualityNoticeLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Program.OpenURL("https://github.com/bluffingo/TweakUtility/wiki/Logon-Background-Quality");

        private void setBackgrounds()
        {
            if (RegistryHelper.GetValue<int>(@"HKLM\Software\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background\OEMBackground") == 1 &&
                File.Exists(LoginBackgroundPath))
            {
                using (var stream = new MemoryStream(File.ReadAllBytes(LoginBackgroundPath)))
                    loginPictureBox.Image = Image.FromStream(stream);

                loginPictureBox.BorderStyle = BorderStyle.None;
                loginPictureBox.BackColor = Color.Transparent;
            }
            else
            {
                loginPictureBox.Image = null;
                loginPictureBox.BorderStyle = BorderStyle.FixedSingle;
                loginPictureBox.BackColor = Color.Gray;
            }

            if (File.Exists(DesktopBackgroundPath))
            {
                using (var stream = new MemoryStream(File.ReadAllBytes(DesktopBackgroundPath)))
                    desktopPictureBox.Image = Image.FromStream(stream);

                desktopPictureBox.BorderStyle = BorderStyle.None;
                desktopPictureBox.BackColor = Color.Transparent;
            }
            else
            {
                desktopPictureBox.Image = null;
                desktopPictureBox.BorderStyle = BorderStyle.FixedSingle;
                desktopPictureBox.BackColor = Color.Gray;
            }
        }

        private void SetBackgroundWindows7(Image input)
        {
            RegistryHelper.SetValue(@"HKLM\Software\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\Background\OEMBackground", 1);

            if (!Directory.Exists(LoginBackgroundDirectoryPath))
            {
                Directory.CreateDirectory(LoginBackgroundDirectoryPath);
            }

            var jgpEncoder = GetEncoder(ImageFormat.Jpeg);
            var myEncoder = Encoder.Quality;

            using (var myEncoderParameters = new EncoderParameters(1))
            using (var stream = new MemoryStream())
            {
                const long reductionInterval = 5L;
                var quality = 100L + reductionInterval;

                do
                {
                    quality -= reductionInterval;

                    if (quality < 1L)
                    {
                        var width = (int)(input.Width * 0.5);
                        var height = (int)(input.Height * 0.5);

                        var result = MessageBox.Show($"The image is too large to fit under 256 KB.\n\nWould you like to try a lower resolution? ({input.Width}x{input.Height} -> {width}x{height})", Properties.Strings.Application_Name, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (result == DialogResult.Yes)
                        {
                            input = ResizeImage((Bitmap)input, width, height);
                            SetBackgroundWindows7(input);
                        }

                        return;
                    }

                    var myEncoderParameter = new EncoderParameter(myEncoder, 100L);
                    myEncoderParameters.Param[0] = myEncoderParameter;
                    input.Save(stream, jgpEncoder, myEncoderParameters);
                }
                while (stream.Length > 256000);

                try
                {
                    File.WriteAllBytes(LoginBackgroundPath, stream.ToArray());

                    qualityNoticeLinkLabel.Text = $"The background was applied with {quality}% quality. Learn More";
                    qualityNoticeLinkLabel.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Could not save background.\n\n{ex.Message}", "Tweak Utility", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DesktopStyleComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (desktopStyleComboBox.SelectedItem == null)
                return;

            DesktopStyle = (WallpaperStyle)desktopStyleComboBox.SelectedItem;
        }

        private void LogonStyleComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (logonStyleComboBox.SelectedItem == null)
                return;
        }

        private void DesktopStyleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DesktopStyle = (WallpaperStyle)desktopStyleComboBox.SelectedItem;
            NativeMethods.SystemParametersInfo(20, 0, DesktopBackgroundPath, 3);
        }
    }
}