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

namespace DP_manager
{
    public partial class Form1 : Form
    {
        ResourceDataGridView<StockResponse, StockEntry> dgv_stock;
        ResourceDataGridView<StockResponse, StockEntry> dgv_archive;

        public Form1()
        {
            this.pageControl1 = new PageControl(50);
            InitializeComponent();
            dgv_stock = new ResourceDataGridView<StockResponse, StockEntry>(pageControl1, new StockController());
            dgv_archive = new ResourceDataGridView<StockResponse, StockEntry>(pageControl1, new StockController());
            tlp_stock.Controls.Add(dgv_stock, 0, 0);
            tlp_archive.Controls.Add(dgv_archive, 0, 0);
        }
    }
}
