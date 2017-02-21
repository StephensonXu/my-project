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
    public partial class DemoCar : Form
    {
        public DemoCar()
        {
            InitializeComponent();
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
