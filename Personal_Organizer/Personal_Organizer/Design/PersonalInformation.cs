using Personal_Organizer.Design;
using Personal_Organizer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personal_Organizer
{
    public partial class PersonalInformation : Form
    {
        User user;
        List<IReminder> reminders = new List<IReminder>();
        System.Timers.Timer timer;
        CSVOperations csvOperations = new CSVOperations();

        public PersonalInformation(User _user)
        {
            InitializeComponent();
            user = _user;
            reminders = csvOperations.ReadRemindersFromCsv();
            List<IReminder> _reminders = new List<IReminder>();
            foreach (IReminder reminder in reminders)
            {
                if (user.Id == reminder.UserID)
                {
                    _reminders.Add(reminder);
                    if (reminder.GetType().Name == "MeetingReminder")
                        reminder.Attach(new MeetingReminderObserver());
                    else
                        reminder.Attach(new MeetingReminderObserver());
                }
            }
            reminders = _reminders;
            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
        }
        bool sidebarExpand;

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            foreach (IReminder reminder in reminders)
            {
                DateTime now = DateTime.Now;
                DateTime reminderDate = reminder.Date + reminder.Time;
                if (reminderDate.Year == now.Year &&
            reminderDate.Month == now.Month &&
            reminderDate.Day == now.Day &&
            reminderDate.Hour == now.Hour &&
            reminderDate.Minute == now.Minute && !reminder.IsTriggered)
                {
                    reminder.Notify(this);
                    Notification not = new Notification(reminder);
                    not.ShowDialog();

                }
            }

        }
        private void PersonalInformation_Load(object sender, EventArgs e)
        {

        }

        private void homebtn_Click(object sender, EventArgs e)
        {
            AraYuz araYuz = new AraYuz(user);
            araYuz.ShowDialog();
            this.Hide();
        }

        //sidebar menu minimization
        private void sidebartimer_Tick(object sender, EventArgs e)
        {
            if(sidebarExpand)
            {
                sidebarflowLayoutPanel.Width -= 10;
                if(sidebarflowLayoutPanel.Width == sidebarflowLayoutPanel.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    sidebartimer.Stop();
                }
            }
            else
            {
                sidebarflowLayoutPanel.Width += 10;
                if(sidebarflowLayoutPanel.Width == sidebarflowLayoutPanel.MaximumSize.Width)
                {
                    sidebarExpand = true;
                    sidebartimer.Stop();
                }
            }
        }



        private void PersonalInformation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void menubtn_Click(object sender, EventArgs e)
        {
           sidebartimer.Start();
        }

        private void savebtn_Click(object sender, EventArgs e)
        {

        }
    }
}
