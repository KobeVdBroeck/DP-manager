using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DP_manager.Components
{
    internal class StockContextMenu : ContextMenu
    {
        public StockContextMenu() : base()
        {
            InitMenuItems();
        }

        private void InitMenuItems()
        {
            MenuItem menuItem = new MenuItem("Update");
            menuItem.Click += UpdateStock;
            MenuItems.Add(menuItem);
        }

        private void UpdateStock(object sender, EventArgs e)
        {
            
        }
    }
}
