using Personal_Organizer.Design;
using Personal_Organizer.Factories;
using Personal_Organizer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personal_Organizer
{
    public partial class Reminder : Form
    {
        bool sidebarExpand;
        List<IReminder> reminders = new List<IReminder>();
        MeetingReminderFactory meetingFactory = new MeetingReminderFactory();
        TaskReminderFactory taskFactory = new TaskReminderFactory();
        CSVOperations csvOperations = new CSVOperations();
        System.Timers.Timer timer;
        public Reminder()
        {
            InitializeComponent();
            reminders = csvOperations.ReadRemindersFromCsv();
            foreach (IReminder reminder in reminders)
            {
                //reminderListBox.Items.Add($"{reminder.UserID},{reminder.Date.ToString("dd.MM.yyyy")},{reminder.Time.ToString(@"hh\:mm\:ss")},{reminder.Title},{reminder.Summary},{reminder.Description},{reminder.GetType().Name}");
                reminder.Attach(new TaskReminderObserver());
            }
            Notification not  = new Notification();
            not.Show();

        }

        private void homebtn_MouseLeave(object sender, EventArgs e)
        {
            homebtn.BackColor = Color.Firebrick;
        }

        private void homebtn_MouseEnter(object sender, EventArgs e)
        {
            homebtn.BackColor = Color.DarkRed;
        }

        private void personalinfobtn_MouseEnter(object sender, EventArgs e)
        {
            personalinfobtn.BackColor = Color.DarkRed;
        }

        private void personalinfobtn_MouseLeave(object sender, EventArgs e)
        {
            personalinfobtn.BackColor = Color.Firebrick;
        }

        private void phonebookbtn_MouseEnter(object sender, EventArgs e)
        {
            phonebookbtn.BackColor = Color.DarkRed;
        }

        private void phonebookbtn_MouseLeave(object sender, EventArgs e)
        {
            phonebookbtn.BackColor = Color.Firebrick;
        }

        private void notesbtn_MouseEnter(object sender, EventArgs e)
        {
            notesbtn.BackColor = Color.DarkRed;
        }

        private void notesbtn_MouseLeave(object sender, EventArgs e)
        {
            notesbtn.BackColor = Color.Firebrick;
        }

        private void salarycalcbtn_MouseEnter(object sender, EventArgs e)
        {
            salarycalcbtn.BackColor = Color.DarkRed;
        }

        private void salarycalcbtn_MouseLeave(object sender, EventArgs e)
        {
            salarycalcbtn.BackColor = Color.Firebrick;
        }

        private void reminderbtn_MouseEnter(object sender, EventArgs e)
        {
            reminderbtn.BackColor = Color.DarkRed;
        }

        private void reminderbtn_MouseLeave(object sender, EventArgs e)
        {
            reminderbtn.BackColor = Color.Firebrick;
        }

        private void usermanagmentbtn_MouseEnter(object sender, EventArgs e)
        {
            usermanagmentbtn.BackColor = Color.DarkRed;
        }

        private void usermanagmentbtn_MouseLeave(object sender, EventArgs e)
        {
            usermanagmentbtn.BackColor = Color.Firebrick;
        }

        private void logoutbtn_MouseEnter(object sender, EventArgs e)
        {
            logoutbtn.BackColor = Color.DarkRed;
        }

        private void logoutbtn_MouseLeave(object sender, EventArgs e)
        {
            logoutbtn.BackColor = Color.Firebrick;
        }

        private void homebtn_Click(object sender, EventArgs e)
        {
            AraYuz araYuz = new AraYuz();
            araYuz.Show();
            this.Hide();
        }

        private void addlistbtn_Click(object sender, EventArgs e)
        {
            AddList addList = new AddList();
            addList.ShowDialog();
        }

        private void addreminderbtn_Click(object sender, EventArgs e)
        {
            AddReminder addReminder = new AddReminder();
           if(addReminder.ShowDialog() == DialogResult.OK)
            {
                if(addReminder.ReminderType == "meeting")
                {
                    reminders.Add(meetingFactory.CreateReminder(addReminder.ReminderDate, addReminder.ReminderTime, addReminder.Title, addReminder.Description, addReminder.Summary));
                    reminders[reminders.Count - 1].Attach(new MeetingReminderObserver());
                }
                else
                {
                    reminders.Add(taskFactory.CreateReminder(addReminder.ReminderDate, addReminder.ReminderTime, addReminder.Title, addReminder.Description, addReminder.Summary));
                    reminders[reminders.Count - 1].Attach(new TaskReminderObserver()); 

                }
                csvOperations.WriteRemindersToCsv(reminders);
                //reminderListBox.Items.Clear();
                foreach(IReminder reminder in reminders)
                {
                    //reminderListBox.Items.Add($"{reminder.UserID},{reminder.Date.ToString("dd.MM.yyyy")},{reminder.Time.ToString(@"hh\:mm\:ss")},{reminder.Title},{reminder.Summary},{reminder.Description},{reminder.GetType().Name}");
                }
            }

        }

        private void personalinfobtn_Click(object sender, EventArgs e)
        {
            PersonalInformation personalInformation = new PersonalInformation();
            personalInformation.ShowDialog();
            this.Hide();
        }

        private void menubtn_Click(object sender, EventArgs e)
        {
            sidebartimer.Start();
        }

        private void sidebartimer_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sidebarflowLayoutPanel.Width -= 10;
                if (sidebarflowLayoutPanel.Width == sidebarflowLayoutPanel.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    sidebartimer.Stop();
                }
            }
            else
            {
                sidebarflowLayoutPanel.Width += 10;
                if (sidebarflowLayoutPanel.Width == sidebarflowLayoutPanel.MaximumSize.Width)
                {
                    sidebarExpand = true;
                    sidebartimer.Stop();
                }
            }
        }

        private void Reminder_FormClosing(object sender, FormClosingEventArgs e)
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

        private void Reminder_Load(object sender, EventArgs e)
        {
            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

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
            reminderDate.Minute == now.Minute)
                {
                    reminder.Notify(this);
                    reminder.IsTriggered = true;
                }
            }

        }
    }
}
