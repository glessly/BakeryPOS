using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BakeryPOS.UI
{
    public static class Styles
    {
        public static Color PrimaryDark { get { return Color.FromArgb(76, 33, 9); } }
        public static Color Primary { get { return Color.FromArgb(139, 69, 19); } }
        public static Color Accent { get { return Color.FromArgb(183, 110, 70); } }
        public static Color WarmBeige { get { return Color.FromArgb(253, 245, 236); } }
        public static Color PanelBeige { get { return Color.FromArgb(245, 222, 179); } }

        // Global Standardized Palette
        public static readonly Color WarmCream = Color.FromArgb(253, 245, 236);
        public static readonly Color DarkEspresso = Color.FromArgb(50, 22, 8);
        public static readonly Color SectionHeaderBrown = Color.FromArgb(123, 63, 26);
        public static readonly Color FieldLabelBrown = Color.FromArgb(50, 22, 8);
        public static readonly Color MediumRoast = Color.FromArgb(101, 61, 34);
        public static readonly Color Caramel = Color.FromArgb(184, 115, 51);
        public static readonly Color Crimson = Color.FromArgb(178, 58, 58);
        public static readonly Color WarmGray = Color.FromArgb(115, 110, 105);
        public static readonly Color GridTextDark = Color.FromArgb(35, 20, 10);
        public static readonly Color GridSelectionBg = Color.FromArgb(235, 210, 180);
        public static readonly Color GridSelectionFg = Color.FromArgb(50, 22, 8);

        private static Image _cachedLogoImage;

        public static Image GetLogoImage()
        {
            if (_cachedLogoImage != null)
                return _cachedLogoImage;

            try
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(frmProducts));
                Image raw = resources.GetObject("picLogo.Image") as Image;
                if (raw is Bitmap bmp && bmp.Width >= 1115 && bmp.Height >= 565)
                {
                    Rectangle cropRect = new Rectangle(420, 108, 695, 455);
                    _cachedLogoImage = bmp.Clone(cropRect, bmp.PixelFormat);
                    return _cachedLogoImage;
                }
                _cachedLogoImage = raw;
                return _cachedLogoImage;
            }
            catch
            {
                return null;
            }
        }

        public static Panel CreateHeaderBanner(string uppercaseTitle)
        {
            Panel pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 85,
                BackColor = DarkEspresso
            };

            PictureBox picLogo = new PictureBox
            {
                Size = new Size(130, 74),
                Location = new Point(16, 5),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent,
                TabStop = false,
                Image = GetLogoImage()
            };

            Label lblHeaderTitle = new Label
            {
                Text = uppercaseTitle,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(160, 27)
            };

            pnlHeader.Controls.Add(picLogo);
            pnlHeader.Controls.Add(lblHeaderTitle);
            return pnlHeader;
        }

        public static void ApplyGridStyle(DataGridView dgv)
        {
            if (dgv == null) return;

            dgv.EnableHeadersVisualStyles = false;
            dgv.RowHeadersVisible = false;
            dgv.BackgroundColor = Color.White;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.MultiSelect = false;

            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 36;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = MediumRoast;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = MediumRoast;
            dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);

            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            dgv.DefaultCellStyle.ForeColor = GridTextDark;
            dgv.DefaultCellStyle.SelectionBackColor = GridSelectionBg;
            dgv.DefaultCellStyle.SelectionForeColor = GridSelectionFg;
            dgv.RowTemplate.Height = 32;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow)
                    row.Height = 32;
            }
        }

        public static void StyleButton(Button btn, Color backColor, int height = 36)
        {
            if (btn == null) return;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btn.ForeColor = Color.White;
            btn.BackColor = backColor;
            btn.Height = height;
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;
        }
    }
}
