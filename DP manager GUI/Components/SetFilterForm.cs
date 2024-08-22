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
    public partial class SetFilterForm<TResponse, TEntity> : Form, ResourceForm
    {
        (StockEntry entry, string field) data;
        public object Data
        {
            get { return data; }
            set
            {
                var tupleVal = ((object entry, string field)) value;
                data = ((StockEntry) tupleVal.entry, tupleVal.field);
                InitComboBox();
            }
        }

        ResourceController<TResponse, TEntity> controller;
        public object Controller
        {
            get => controller;
            set
            {
                controller = (ResourceController<TResponse, TEntity>) value;
            }
        }

        public SetFilterForm(ResourceController<TResponse, TEntity> controller)
        {
            InitializeComponent();
            Controller = controller;
        }


        new public bool IsDisposed => base.IsDisposed;
        public bool IsVisible => base.Visible;

        event EventHandler<EventArgs> ResourceForm.Close
        {
            add
            {
                this.FormClosed += new FormClosedEventHandler(value);
            }

            remove
            {
                this.FormClosed -= new FormClosedEventHandler(value);
            }
        }

        public Form Reconstruct()
        {
            return new SetFilterForm<TResponse, TEntity>(controller);
        }

        private void InitComboBox()
        {
            List<string> strings = data.entry.GetType().GetProperties().Select(p => p.Name).Where(s => !(s == "Id")).ToList();
            cb_fields.Items.Clear();
            cb_fields.Items.Add("No filter");
            cb_fields.Items.AddRange(strings.ToArray());
            int a = strings.IndexOf(data.field);
            cb_fields.SelectedIndex = strings.IndexOf(data.field);
        }

        private void btn_confirm_Click(object sender, EventArgs e)
        {
            if(cb_fields.SelectedIndex == 0)
            {
                controller.RemoveFilter();
                Close();
            }

            if(cb_fields.SelectedIndex == -1)
            {
                MessageBox.Show("Choose a valid column or cancel.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            controller.SetFilter((string)cb_fields.SelectedItem, tb_value.Text);

            Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
