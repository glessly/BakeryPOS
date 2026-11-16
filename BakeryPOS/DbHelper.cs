using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BakeryPOS
{
    public static class DbHelper
    {
        public static string GetConnectionString()
        {
            var setting = ConfigurationManager.ConnectionStrings["BakeryPOS_DB"];
            if (setting == null || string.IsNullOrWhiteSpace(setting.ConnectionString))
            {
                throw new InvalidOperationException("Connection string 'BakeryPOS_DB' was not found in App.config.");
            }
            return setting.ConnectionString;
        }

        public static string GetProductImagesFolder()
        {
            string folder = Path.Combine(Application.StartupPath, "ProductImages");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            return folder;
        }

        public static Image LoadProductImage(string imagePath)
        {
            if (!string.IsNullOrWhiteSpace(imagePath))
            {
                string fullPath = imagePath;
                if (!Path.IsPathRooted(imagePath))
                {
                    fullPath = Path.Combine(Application.StartupPath, imagePath);
                }

                if (File.Exists(fullPath))
                {
                    try
                    {
                        using (var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                        {
                            return Image.FromStream(stream);
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.LogError("DbHelper.LoadProductImage", ex);
                    }
                }
            }

            return CreatePlaceholderImage();
        }

        private static Image CreatePlaceholderImage()
        {
            Bitmap bmp = new Bitmap(120, 100);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.FromArgb(245, 235, 220));
                using (Pen pen = new Pen(Color.FromArgb(160, 120, 90), 2))
                {
                    g.DrawRectangle(pen, 5, 5, 110, 90);
                }
                using (Font font = new Font("Segoe UI", 9, FontStyle.Bold))
                using (Brush brush = new SolidBrush(Color.FromArgb(120, 80, 50)))
                {
                    StringFormat sf = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    g.DrawString("No Image", font, brush, new RectangleF(0, 0, 120, 100), sf);
                }
            }
            return bmp;
        }
    }
}