using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UGV_1
{
    public partial class NoCamra : Form
    {
        public NoCamra()
        {
            InitializeComponent();
        }

        private void NoCameraBox_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
