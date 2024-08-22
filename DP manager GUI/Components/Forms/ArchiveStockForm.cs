using System;
using System.Windows.Forms;

namespace DP_manager.Components
{
    public partial class ArchiveStockForm : Form, ResourceForm
    {
        int data = 0;
        public object Data
        {
            get { return data; }
            set
            {
                data = ((StockEntry)value).Id;
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

        public ArchiveStockForm(StockController controller)
        {
            this.controller = controller;
            InitializeComponent();
        }

        public Form Reconstruct()
        {
            return new ArchiveStockForm(controller);
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

        private async void btn_confirm_ClickAsync(object sender, EventArgs e)
        {
            if (data != 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this entry and move it to the archive?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.Cancel)
                    return;

                await controller.RemoveEntry(data, rtb_reason.Text ?? "");
            }

            Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void rtb_reason_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            ((RichTextBox)sender).Height = e.NewRectangle.Height + 5;
        }
    }
}
