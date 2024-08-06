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
        MyDataGridView dataGridView;

        public Form1()
        {
            this.pageControl1 = new PageControl(50);
            InitializeComponent();
            dataGridView = new MyDataGridView(pageControl1);
            tableLayoutPanel1.Controls.Add(dataGridView, 0, 0);
        }
    }
}
