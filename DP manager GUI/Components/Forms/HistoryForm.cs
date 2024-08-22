using DP_manager.Controllers;
using DP_manager.Models;
using System;
using System.Windows.Forms;

namespace DP_manager.Components
{
    public partial class HistoryForm : Form, ResourceForm
    {
        ResourceDataGridView<HistoryResponse, ArchiveEntry> dgv_stock;

        public HistoryForm(HistoryController controller)
        {
            this.pageControl1 = new PageControl(50);
            Controller = controller;
            InitializeComponent();
        }

        StockEntry data;
        public object Data
        {
            get { return data; }
            set
            {
                data = (StockEntry)value;
            }
        }

        HistoryController controller;
        public object Controller
        {
            get => controller;
            set
            {
                controller = (HistoryController)value;
            }
        }

        public bool IsVisible => base.Visible;
        new public bool IsDisposed => base.IsDisposed;

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
            return new HistoryForm(controller);
        }

        new public void Show()
        {
            controller.History = data.History;

            dgv_stock = new ResourceDataGridView<HistoryResponse, ArchiveEntry>(pageControl1, controller);
            tlp_stock.Controls.Add(dgv_stock, 0, 0);
            base.Show();
        }
    }
}
