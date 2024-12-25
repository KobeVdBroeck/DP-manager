using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using DP_manager.Models;
using DP_manager.Properties;


namespace DP_manager.Components
{
    public delegate void Notify();

    public class NotificationScheduler
    {
        static readonly int TIMER_MINIMUM_MINUTES = 15; 

        List<Models.Notification> notifications = new List<Models.Notification>();
        System.Timers.Timer timer = new System.Timers.Timer();

        Icon notificationIcon;
        public int PastDueCount { get; set; } = 0;

        public event Notify Notified;
        NotifyIcon icon;

        bool busy = false;

        public NotificationScheduler()
        {
            notificationIcon = ((Icon) new System.ComponentModel.ComponentResourceManager(typeof(MainForm)).GetObject("$this.Icon"));

            icon = new NotifyIcon();
            icon.BalloonTipTitle = "Reminder to process stock";
            icon.Icon = notificationIcon;
            icon.BalloonTipIcon = ToolTipIcon.Info;
            icon.Visible = true;

            timer.Elapsed += ShowNotification;
        }

        public void ScheduleFutureNotifications(List<Models.Notification> notifications)
        {
            timer.Stop();

            Wait();

            busy = true;
            this.notifications.Clear();
            this.notifications.AddRange(notifications);
            busy = false;

            if (notifications.Any())
                InitTimer();
        }

        public void AddNewNotifications(IEnumerable<Models.Notification> notifications)
        {
            Wait();
            busy = true;
            this.notifications.AddRange(notifications);
            busy = false;
            InitTimer();
        }

        double a;
        List<double> c;
        void InitTimer()
        {
            Wait();

            busy = true;
            PastDueCount += notifications.RemoveAll(n => n == null || GetMsToTime(n) / 1000 / 60 < TIMER_MINIMUM_MINUTES);

            a = GetMsToTime(notifications.First());
            c = notifications.Select(n => GetMsToTime(n)).ToList();


            if (!notifications.Any())
            {
                if (PastDueCount == 0)
                {
                    busy = false;
                    return;
                }

                timer.Interval = TIMER_MINIMUM_MINUTES * 1000 * 60;
            }
            else
            {
                timer.Interval = GetMsToTime(notifications[0]);
            }
            busy = false;

            timer.Start();
        }

        private void ShowNotification(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            icon.BalloonTipText = GetBodyText();
            icon.ShowBalloonTip(2000);

            if (!notifications.Any())
                ScheduleFutureNotifications(notifications);
            
            Notified?.Invoke();
        }

        private string GetBodyText()
        {
            return string.Format("{0} entries past due, {1} scheduled today", 
                PastDueCount, notifications.Count);
        }

        double GetMsToTime(Models.Notification notification)
        {
            return (notification.ProcessBy.ToUniversalTime() - DateTime.UtcNow).TotalMilliseconds;
        }

        void Wait()
        {
            while (busy) ;
        }
    }
}
