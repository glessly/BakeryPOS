using System;
using System.Drawing;
using System.Windows.Forms;
using BakeryPOS.UI;

namespace BakeryPOS
{
    public partial class frmViewDetails : Form
    {
        public frmViewDetails()
        {
            InitializeComponent();
            this.BackColor = Styles.WarmCream;
            this.Controls.Add(Styles.CreateHeaderBanner("VIEW DETAILS"));
        }
    }
}
