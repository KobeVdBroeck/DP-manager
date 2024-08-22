using GraphQL.Client.Http;
using GraphQL;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQL.Client.Abstractions;
using DP_manager.Components;
using System.Media;

namespace DP_manager
{
    public partial class Form1 : Form
    {
        ResourceDataGridView<StockResponse, StockEntry> dgv_stock;
        ResourceDataGridView<ArchiveResponse, ArchiveEntry> dgv_archive;

        public Form1(string addr)
        {
            GrpcService.Address = addr;

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
