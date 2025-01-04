using DP_manager.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DP_manager.Components.Forms
{
    public partial class ExportForm : Form, ResourceForm
    {
        public bool Cancelled => false;

        public object Data { get => null; set => _ = value; }
        public object Controller { get => null; set => _ = value; }

        public bool IsVisible => false;

        string table;
        public ExportForm(string table) : base()
        {
            this.table = table;
        }

        new public async void Show()
        {
            string folder = Environment.GetEnvironmentVariable("USERPROFILE");
            var index = folder.LastIndexOf("\\");

            folder = folder.Substring(0, index) + "\\public\\data\\";



            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var file = folder + table + ".csv";
            var res = await ExportController.ExportData(table, file.Replace("\\", "/"));

            if(!res)
            {
                MessageBox.Show("Export data unsuccessful.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string argument = "/select, \"" + file + "\"";
            Process.Start("explorer.exe", argument);
        }

        event EventHandler<EventArgs> ResourceForm.Close
        {
            add
            {

            }

            remove
            {

            }
        }

        public Form Reconstruct()
        {
            return new ExportForm(table);
        }
    }
}
