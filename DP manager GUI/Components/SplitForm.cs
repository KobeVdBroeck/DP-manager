﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DP_manager.Components
{
    public partial class SplitForm : Form, ResourceForm
    {
        BindingSource bindingSource = new BindingSource();

        public SplitForm(StockController controller)
        {
            this.controller = controller;
            bindingSource.DataSource = new BindingList<StockEntry>();
            InitializeComponent();
            dgv_entries.AllowUserToResizeRows = false;
            dgv_entries.AllowUserToAddRows = true;
            dgv_entries.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        StockEntry data;
        public object Data
        {
            get { return data; }
            set
            {
                data = (StockEntry)value;
                InitData();
            }
        }

        private void InitData()
        {
            dgv_entries.DataSource = bindingSource;
            bindingSource.Add(data);
            dgv_entries.Columns[0].Visible = false;
        }

        StockController controller;
        public object Controller
        {
            get => controller;
            set
            {
                controller = (StockController)value;
            }
        }

        new public bool IsDisposed => base.IsDisposed;
        public bool IsVisible => base.Visible;

        public Form Reconstruct()
        {
            return new SplitForm(controller);
        }

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

        private void btn_confirm_Click(object sender, EventArgs e)
        {
            controller.FormatStockList(((BindingList<StockEntry>)bindingSource.DataSource).ToList());
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgv_entries_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            int row = e.RowIndex;
            int column = e.ColumnIndex;

            dgv_entries.Rows[row].DefaultCellStyle = new DataGridViewCellStyle() { BackColor = Color.OrangeRed };

            if(row == 0)
                dgv_entries.EndEdit();
        }

        private void dgv_entries_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex != 0)
            {
                if (dgv_entries.Rows[0].Cells[e.ColumnIndex].Value is string)
                {
                    e.Cancel = false;
                    return;
                }
                if(((string) e.FormattedValue) == "")
                {
                    e.Cancel = false;
                    dgv_entries.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                    return;
                }
                else if (!int.TryParse(Convert.ToString(e.FormattedValue), out _))
                {
                    e.Cancel = true;
                    SystemSounds.Beep.Play();
                    MessageBox.Show("Input must be a valid number");
                }
            }
        }
    }
}
