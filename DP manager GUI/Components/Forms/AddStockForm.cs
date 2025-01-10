using DP_manager.Interfaces;
using System;
using System.Windows.Forms;

namespace DP_manager.Components
{
    public partial class AddStockForm : Form, ResourceForm
    {

        bool cancelled = true;
        public bool Cancelled { get { return cancelled; } }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            cancelled = true;
            Close();
        }

        private void FormIsClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                cancelled = true;
        }

        bool update;

        StockEntry data;
        public object Data
        {
            get { return data; }
            set
            {
                data = (StockEntry)value;
                InitComponentData();
            }
        }

        StockController controller;
        public object Controller
        {
            get => controller;
            set
            {
                controller = (StockController)value;
            }
        }

        new public bool IsDisposed => base.IsDisposed;
        public bool IsVisible => base.Visible;

        string ConfirmMsg => update ?
            "Are you sure you want to add this entry?" :
            "Are you sure you want to update this entry? The original will be moved to the archive.";

        public AddStockForm(StockController controller, bool update) : base()
        {
            this.update = update;
            this.controller = controller;
            InitializeComponent();
            this.dtp.Format = DateTimePickerFormat.Custom;
            FormClosing += FormIsClosing;
        }

        public Form Reconstruct()
        {
            return new AddStockForm(controller, update);
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
            if (!update)
            {
                lb_reason.Visible = false;
                rtb_reason.Visible = false;
                return;
            }

            tb_worker.Text = data.Worker;
            dtp.Value = DateTime.UtcNow;
            tb_lab.Text = data.Lab;
            tb_location.Text = data.Location;
            nud_recipients.Text = data.Recipients.ToString();
            nud_ppr.Text = data.Ppr.ToString();
            nud_category.Text = data.Category.ToString();
            nud_phase.Text = data.Phase.ToString();
            nud_health.Text = data.Health.ToString();
            tb_history.Text = data.History;
            tb_remarks.Text = data.Remarks;
            cb_plantCode.Text = data.PlantCode;
            cb_mediumId.Text = data.MediumId.ToString();
        }

        private StockEntry ReadData()
        {
            return new StockEntry()
            {
                Id = data.Id,
                Worker = tb_worker.Text,
                Timestamp = dtp.Value,
                Lab = tb_lab.Text,
                Location = tb_location.Text,
                Recipients = (int)nud_recipients.Value,
                Ppr = (int)nud_ppr.Value,
                Category = (Category)nud_category.Value,
                Phase = (Phase)(int)nud_phase.Value,
                Health = (Health)nud_health.Value,
                History = tb_history.Text,
                Remarks = tb_remarks.Text,
                PlantCode = cb_plantCode.Text,
                MediumId = int.Parse(cb_mediumId.Text),
            };
        }

        private async void btn_confirm_Click(object sender, EventArgs e)
        {
            if (data != null)
            {
                DialogResult result = MessageBox.Show(ConfirmMsg, "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Cancel)
                    return;

                if (update)
                    await controller.UpdateEntry(ReadData(), rtb_reason.Text ?? default);
                else
                    await controller.InsertEntry(ReadData());
            }

            Close();
        }

        private void rtb_reason_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            ((RichTextBox)sender).Height = e.NewRectangle.Height + 5;
        }
    }
}
