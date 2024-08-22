using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DP_manager.Components
{
    internal class ResourceDataGridView<TResponse, TEntity> : DataGridView where TResponse : IGraphQlResponse
    {
        private BindingSource bindingSource = new BindingSource();
        private ResourceController<TResponse, TEntity> resourceController;
        private PageControl pageControl;
        private ContextMenu headerContextMenu = new ContextMenu();
        private ContextMenu contextMenu = new ContextMenu();
        private int filteredColumn = -1;
        private string filterValue = "";
        private string sortDirection = "";
        private int sortedColumn = -1;
        private bool columnsInitialized = false;

        public ResourceDataGridView(PageControl pageControl, ResourceController<TResponse, TEntity> controller) : base()
        {
            this.resourceController = controller;
            this.pageControl = pageControl;

            Dock = DockStyle.Fill;
            StandardTab = true;
            RowHeadersVisible = false;
            AllowUserToResizeRows = false;
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            EditMode = DataGridViewEditMode.EditProgrammatically;

            DefaultCellStyle.ApplyStyle(new DataGridViewCellStyle()
            {
                SelectionBackColor = Color.LightGreen,
                SelectionForeColor = Color.Black
            });

            contextMenu.MenuItems.AddRange(resourceController.MenuItems.ToArray());
            foreach (var menuItem in contextMenu.MenuItems)
            {
                var item = (FormBoundMenuItem)menuItem;
                item.Click += MenuItem_Click;
            }

            ColumnHeaderMouseClick += OnColumnHeaderMouseClick;
            CellMouseDown += OnRightMouseClick;
            pageControl.PageChanged += PageControl_PageChanged;
            KeyDown += OnKeyDown;

            AutoGenerateColumns = true;
            DataSource = bindingSource;
            resourceController.SetPaging(pageControl.Page, pageControl.PageLimit);

            headerContextMenu.MenuItems.Add(new FormBoundMenuItem("Add filter", new SetFilterForm<TResponse, TEntity>(resourceController)));
            foreach (var menuItem in headerContextMenu.MenuItems)
            {
                var item = (FormBoundMenuItem)menuItem;
                item.Click += HeaderMenuItem_Click;
            }

            UpdateData();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            Point location = PointToClient(MousePosition);
            var hit = HitTest(location.X, location.Y);
            location.Offset(5, 5);

            if (e.Control)
                contextMenu.Show(this, location);
            if (e.Alt)
                headerContextMenu.Show(this, location);
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
            else if (sortDirection == "asc")
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

            if (!columnsInitialized)
            {
                foreach (var item in Columns)
                    ((DataGridViewColumn)item).MinimumWidth = 40;
                columnsInitialized = true;
            }

            Refresh();

            if (sortDirection != "" && !Columns[sortedColumn].HeaderText.EndsWith("▲") && !Columns[sortedColumn].HeaderText.EndsWith("▼"))
                Columns[sortedColumn].HeaderText += sortDirection == "asc" ? "▲" : "▼";

            foreach (var col in Columns)
                ((DataGridViewColumn)col).MinimumWidth = 2;

            ApplyFilterText();
        }

        private void ApplyFilterText()
        {
            try
            {
                if (filteredColumn != -1)
                {
                    string curText = Columns[filteredColumn].HeaderText;

                    if (filteredColumn != -1 && resourceController.filter.field != "")
                        Columns[filteredColumn].HeaderText = curText.Substring(0, curText.IndexOf(" = "));
                }

                if (resourceController.filter.field != "")
                {
                    filteredColumn = Columns[resourceController.filter.field].DisplayIndex;
                    filterValue = resourceController.filter.value;
                }
            }
            catch (Exception)
            {
                filteredColumn = -1;
            }

            if (filteredColumn != -1 && !Columns[filteredColumn].HeaderText.Contains('='))
            {
                Columns[filteredColumn].HeaderText += " = " + filterValue;
            }
        }

        private void OnRightMouseClick(object sender, MouseEventArgs e)
        {
            Point location = PointToClient(MousePosition);
            var hit = HitTest(location.X, location.Y);

            if (hit.Type == DataGridViewHitTestType.ColumnHeader)
            {
                location.Offset(5, 5);

                if (e.Button == MouseButtons.Right)
                    headerContextMenu.Show(this, location);
            }
            else if (hit.Type == DataGridViewHitTestType.Cell)
            {
                if (!SelectedRows.Cast<DataGridViewRow>().Any(r => r.Index == hit.RowIndex))
                {
                    foreach (var item in SelectedRows.Cast<DataGridViewRow>())
                        item.Selected = false;

                    Rows[hit.RowIndex].Selected = true;
                }

                location.Offset(5, 5);

                if (e.Button == MouseButtons.Right)
                    contextMenu.Show(this, location);
            }
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            var count = Rows.GetRowCount(DataGridViewElementStates.Selected);

            foreach (var item in SelectedRows)
            {
                var index = ((DataGridViewRow)item).Index;
                var entity = (TEntity)bindingSource[index];

                var formMenuItem = (FormBoundMenuItem)sender;
                var form = formMenuItem.Form;

                if (form.IsDisposed || form.IsVisible)
                    formMenuItem.Form = (ResourceForm)form.Reconstruct();

                formMenuItem.Form.Data = entity;
                formMenuItem.Form.Show();
                formMenuItem.Form.Close += Form_Close;
            }
        }

        private void HeaderMenuItem_Click(object sender, EventArgs e)
        {
            Point location = PointToClient(MousePosition);
            var hit = HitTest(location.X, location.Y);

            var count = Rows.GetRowCount(DataGridViewElementStates.Selected);

            var formMenuItem = (FormBoundMenuItem)sender;
            var form = formMenuItem.Form;

            if (form.IsDisposed || form.IsVisible)
                formMenuItem.Form = (ResourceForm)form.Reconstruct();

            formMenuItem.Form.Data = (entry: bindingSource[0], field: Columns[hit.ColumnIndex].HeaderText);
            formMenuItem.Form.Show();
            formMenuItem.Form.Close += Form_Close;
        }

        private void Form_Close(object sender, EventArgs e)
        {
            UpdateData();
        }
    }
}
