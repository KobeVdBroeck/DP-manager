using System;
using System.Windows.Forms;

namespace DP_manager.Components
{
    public interface ResourceForm
    {
        object Data { get; set; }
        object Controller { get; set; }

        bool IsDisposed { get; }
        bool IsVisible { get; }

        Form Reconstruct();

        void Show();
        event EventHandler<EventArgs> Close;
    }
}
