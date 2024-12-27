using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DP_manager.Components.Forms
{
    public class LoadingLabel
    {
        public string Loading { get; set; } = "Loading data";

        [Browsable(false)]

        public Timer Timer = new Timer();

        public LoadingLabel() 
        {
            Timer.Interval = 500;
            Timer.Start();
            Timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (Loading.Length == 16)
                Loading = "Loading data";
            else
                Loading += '.';
        }
    }
}
