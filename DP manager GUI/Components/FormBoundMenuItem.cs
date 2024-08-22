using System;
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
