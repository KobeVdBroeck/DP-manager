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
    public partial class StockForm : Form, ResourceForm
    {
        StockEntry data;
        public object Data 
        { 
            get { return data;  }
            set 
            {
                data = (StockEntry) value;
                InitComponentData();
            }
        }

        StockController controller;
        public object Controller
        {
            get => controller;
            set
            {
                controller = (StockController) value;
            }
        }

        new public bool IsDisposed => base.IsDisposed;

        public StockForm(StockController controller) : base()
        {
            this.controller = controller;
            InitializeComponent();
        }

        public Form Reconstruct()
        {
            return new StockForm(controller);
        }

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

        private void InitComponentData()
        {
            tb_worker.Text = data.Worker;
            nud_year.Text = data.Week.Substring(0, 2);
            nud_week.Text = data.Week.Substring(2);
            tb_lab.Text = data.Lab;
            tb_location.Text = data.Location;
            nud_recipients.Text = data.Recipients.ToString();
            nud_ppr.Text = data.Ppr.ToString();
            nud_category.Text = data.Category.ToString();
            nud_phase.Text = data.Phase.ToString();
            nud_health.Text = data.Health.ToString();
            tb_history.Text = data.History;
            tb_remarks.Text = data.Remarks;
            cb_plantCode.Text = "todo";
            cb_mediumId.Text = "todo";

            if(data == null) 
            {
                lb_reason.Enabled = false;
                tb_reason.Enabled = false;
            }
        }

        private async void btn_confirm_Click(object sender, EventArgs e)
        {
            if(data != null)
                await controller.UpdateEntry(data, tb_reason.Text ?? default);

            Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
