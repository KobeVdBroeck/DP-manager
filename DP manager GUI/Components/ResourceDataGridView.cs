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
    internal class ResourceDataGridView<TController, T> : DataGridView where TController : ResourceController<T> where T : IGrpcResponse
    {
        private BindingSource bindingSource = new BindingSource();
        private TController resourceController;
        private PageControl pageControl;
        private string sortDirection = "";
        private int sortedColumn = -1;

        public ResourceDataGridView(PageControl pageControl, TController controller) : base()
        {
            this.resourceController = controller;
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
            resourceController.SetPaging(pageControl.Page, pageControl.PageLimit);
            UpdateData();
        }

        private void OnColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int column = e.ColumnIndex;

            
            if (sortDirection != "")
            {
                string curText = Columns[sortedColumn].HeaderText;
                Columns[sortedColumn].HeaderText = curText.Substring(0, curText.Length - 1);
            }

            if (column != sortedColumn)
            {
                sortDirection = "asc";
                sortedColumn = column;
                resourceController.SetSort(Columns[column].Name, "asc");
            }
            else if (sortedColumn < 0)
            {
                sortDirection = "asc";
                sortedColumn = column;
                resourceController.SetSort(Columns[column].Name, "asc");
            }
            else if(sortDirection == "asc")
            {
                sortDirection = "desc";
                sortedColumn = column;
                resourceController.SetSort(Columns[column].Name, "desc");
            }
            else
            {
                sortDirection = "";
                sortedColumn = -1;
                resourceController.RemoveSort();
            }

            UpdateData();
        }

        public async void UpdateData()
        {
            var data = await resourceController.GetEntries();
            var (page, pageCount) = data.GetPageInfo();

            pageControl.Page = page;
            pageControl.PageCount = pageCount;

            bindingSource.DataSource = new BindingList<object>(data.GetData().Cast<object>().ToList());
            Refresh();

            if (sortDirection != "")
                Columns[sortedColumn].HeaderText += sortDirection == "asc" ? "▲" : "▼";
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
