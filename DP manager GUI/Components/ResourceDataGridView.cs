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
    internal class ResourceDataGridView<TResponse, TEntity> : DataGridView where TResponse : IGrpcResponse
    {
        private BindingSource bindingSource = new BindingSource();
        private ResourceController<TResponse, TEntity> resourceController;
        private PageControl pageControl;
        private string sortDirection = "";
        private int sortedColumn = -1;

        public ResourceDataGridView(PageControl pageControl, ResourceController<TResponse, TEntity> controller) : base()
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

                m.MenuItems.AddRange(resourceController.MenuItems.ToArray());
                foreach (var menuItem in m.MenuItems)
                {
                    var item = (FormBoundMenuItem) menuItem;
                    item.Click += Form_Click;
                }

                int currentMouseOverRow = HitTest(e.X, e.Y).RowIndex;


                if (currentMouseOverRow >= 0)
                {
                    m.MenuItems.Add(new MenuItem(string.Format("Do something to row {0}", currentMouseOverRow.ToString())));
                }

                m.Show(this, new Point(e.X, e.Y));
            }
        }

        private void Form_Click(object sender, EventArgs e)
        {
            var count = Rows.GetRowCount(DataGridViewElementStates.Selected);

            foreach (var item in SelectedRows)
            {
                var index = ((DataGridViewRow) item).Index;
                var entity = (TEntity) bindingSource[index];

                var formMenuItem = (FormBoundMenuItem) sender;
                var form = formMenuItem.Form;

                if (form.IsDisposed || form.IsVisible)
                    formMenuItem.Form = (ResourceForm) form.Reconstruct();

                formMenuItem.Form.Data = entity;
                formMenuItem.Form.Show();
                formMenuItem.Form.Close += Form_Close;
            }
        }

        private void Form_Close(object sender, EventArgs e)
        {
            UpdateData();
        }
    }
}
