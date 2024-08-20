using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DP_manager.Components
{
    public partial class SetPageForm : Form
    {
        int pages;
        int? result;

        public SetPageForm(int pages)
        {
            InitializeComponent();
            this.pages = pages;
            lblLimit.Text = pages.ToString();
            nud_page.Value = 1;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void nud_page_ValueChanged(object sender, EventArgs e)
        {
            if(nud_page.Value <= 0)
                nud_page.Value = 1;

            if(nud_page.Value > pages)
                nud_page.Value = pages;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            result = (int)nud_page.Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        public int? GetResult()
        {
            return result; 
        }
    
    }
}
