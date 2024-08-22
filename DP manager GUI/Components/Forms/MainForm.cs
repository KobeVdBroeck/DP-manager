using DP_manager.Components;
using System.Windows.Forms;

namespace DP_manager
{
    public partial class MainForm : Form
    {
        ResourceDataGridView<StockResponse, StockEntry> dgv_stock;
        ResourceDataGridView<ArchiveResponse, ArchiveEntry> dgv_archive;

        public MainForm(string addr)
        {
            GraphQlService.Address = addr;

            this.pageControl1 = new PageControl(50);
            this.pageControl2 = new PageControl(50);

            InitializeComponent();
            dgv_stock = new ResourceDataGridView<StockResponse, StockEntry>(pageControl1, new StockController()) { TabIndex = 1 };
            dgv_archive = new ResourceDataGridView<ArchiveResponse, ArchiveEntry>(pageControl2, new ArchiveController()) { TabIndex = 1 };
            tlp_stock.Controls.Add(dgv_stock, 0, 0);
            tlp_archive.Controls.Add(dgv_archive, 0, 0);
        }
    }
}
