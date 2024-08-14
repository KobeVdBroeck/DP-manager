using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DP_manager.Components
{
    public class FormBoundMenuItem : MenuItem
    {
        public ResourceForm Form { get; set; }

        public FormBoundMenuItem(string name, ResourceForm form) : base(name) 
        {
            Name = name;
            Form = form;
        }

        private void ShowForm(object sender, EventArgs e)
        {
            Form.Show();
        }
    }
}
