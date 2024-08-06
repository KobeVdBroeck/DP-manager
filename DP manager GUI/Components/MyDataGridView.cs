using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DP_manager.Components
{
    internal class MyDataGridView : DataGridView
    {
        private BindingSource bindingSource = new BindingSource();
        private StockController stockController = new StockController();
        private PageControl pageControl;
        private string sortDirection = "";
        private int sortedColumn = -1;

        public MyDataGridView(PageControl pageControl) : base()
        {
            this.pageControl = pageControl;

            Dock = DockStyle.Fill;
            RowHeadersVisible = false;
            AllowUserToResizeRows = false;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            EditMode = DataGridViewEditMode.EditProgrammatically;

            DefaultCellStyle.ApplyStyle(new DataGridViewCellStyle() { 
                SelectionBackColor = Color.LightGreen, 
                SelectionForeColor = Color.Black 
            });

            ColumnHeaderMouseClick += OnColumnHeaderMouseClick;
            CellMouseClick += OnRightMouseClick;
            pageControl.PageChanged += PageControl_PageChanged;

            AutoGenerateColumns = true;
            DataSource = bindingSource;
            UpdateData();
        }

        private void PageControl_PageChanged(object sender, EventArgs e)
        {
            stockController.SetPaging(pageControl.Page, pageControl.PageLimit);
            UpdateData();
        }

        private void OnColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int column = e.ColumnIndex;

            if(column != sortedColumn)
            {
                sortDirection = "asc";
                sortedColumn = column;
                stockController.SetSort(Columns[column].Name, "asc");
            }
            else if (sortedColumn < 0)
            {
                sortDirection = "asc";
                sortedColumn = column;
                stockController.SetSort(Columns[column].Name, "asc");
            }
            else if(sortDirection == "asc")
            {
                sortDirection = "desc";
                sortedColumn = column;
                stockController.SetSort(Columns[column].Name, "desc");
            }
            else
            {
                sortDirection = "";
                sortedColumn = -1;
            }

            UpdateData();
        }

        public async void UpdateData()
        {
            var data = await stockController.GetEntries();
            pageControl.Page = data.Stock.CurrentPage;
            pageControl.PageCount = data.Stock.PageCount;
            bindingSource.DataSource = new BindingList<StockEntry>(data.Stock.Result.ToList());
            Refresh();
        }

        private void OnRightMouseClick(object sender, MouseEventArgs e)
        {
            // paygrid.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                
                MenuItem item = new MenuItem("Cut");
                item.Click += Update_Resource_Click;

                m.MenuItems.Add(item);


                m.MenuItems.Add(new MenuItem("Copy"));
                m.MenuItems.Add(new MenuItem("Paste"));

                int currentMouseOverRow = HitTest(e.X, e.Y).RowIndex;


                if (currentMouseOverRow >= 0)
                {
                    m.MenuItems.Add(new MenuItem(string.Format("Do something to row {0}", currentMouseOverRow.ToString())));
                }

                m.Show(this, new Point(e.X, e.Y));
            }
        }

        private void Update_Resource_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
