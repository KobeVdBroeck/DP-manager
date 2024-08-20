using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DP_manager.Components
{
    public partial class PageControl : UserControl
    {
        int page;
        public int Page {
            get => page;
            set
            {
                page = value;
                UpdateLabel();
            }
        }
        public int PageLimit;
        int pageCount;
        public int PageCount
        {
            get => pageCount;
            set
            {
                pageCount = value;
                UpdateLabel();
            }
        }

        public event EventHandler PageChanged;

        public PageControl() : this(50) { }

        public PageControl(int pageLimit)
        {
            InitializeComponent();
            button1.Click += Button1_Click;
            button2.Click += Button2_Click;
            button3.Click += Button3_Click;
            button4.Click += Button4_Click;
            button1.Click += OnPageChanged;
            button2.Click += OnPageChanged;
            button3.Click += OnPageChanged;
            button4.Click += OnPageChanged;
            PageLimit = pageLimit;
            PageCount = 1;
            Page = 1;
        }

        protected virtual void OnPageChanged(object sender, EventArgs e)
        {
            PageChanged?.Invoke(this, e);
        }

        private void UpdateLabel()
        {
            currentPageLabel.Text = Page.ToString() + "/" + PageCount.ToString();
        }

        private void Button4_Click(object sender, EventArgs e) => Page = 1;
        private void Button3_Click(object sender, EventArgs e) => Page -= 1;
        private void Button2_Click(object sender, EventArgs e) => Page += 1;
        private void Button1_Click(object sender, EventArgs e) => Page = PageCount;

        private void currentPageLabel_DoubleClick(object sender, EventArgs e)
        {
            var form = new SetPageForm(PageCount);
            form.ShowDialog();
            var res = form.GetResult();

            if (res != null)
                Page = res.Value;
        }
    }
}
