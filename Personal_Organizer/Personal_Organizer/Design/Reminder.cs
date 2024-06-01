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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personal_Organizer
{
    public partial class Reminder : Form
    {
        User user = new User();
        bool sidebarExpand;
        List<IReminder> reminders = new List<IReminder>();
        List<IReminder> allReminders = new List<IReminder>();
        MeetingReminderFactory meetingFactory = new MeetingReminderFactory();
        TaskReminderFactory taskFactory = new TaskReminderFactory();
        CSVOperations csvOperations = new CSVOperations();
        System.Timers.Timer timer;
        string filter = "all";
        public Reminder(User _user)
        {
            InitializeComponent();
            user = _user;
            allReminders = csvOperations.ReadRemindersFromCsv();
            foreach (IReminder reminder in allReminders)
            {
                if (user.Id == reminder.UserID)
                {
                    reminders.Add(reminder);
                    if (reminder.GetType().Name == "MeetingReminder")
                        reminder.Attach(new MeetingReminderObserver());
                    else
                        reminder.Attach(new MeetingReminderObserver());
                }
            }
            AddRemindersToDGW();
            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        void AddRemindersToDGW()
        {
            reminderdatagridview.Rows.Clear();
            foreach (IReminder reminder in reminders)
            {
                if(filter=="all")
                    reminderdatagridview.Rows.Add(reminder.ReminderID,reminder.IsTriggered, reminder.Title, reminder.Description, reminder.Summary, reminder.Date.ToShortDateString(), reminder.Time.ToString(@"hh\:mm"), reminder.GetType().Name);
                if (filter == "task")
                {
                    if (reminder.GetType().Name == "TaskReminder")
                        reminderdatagridview.Rows.Add(reminder.ReminderID,reminder.IsTriggered, reminder.Title, reminder.Description, reminder.Summary, reminder.Date.ToShortDateString(), reminder.Time.ToString(@"hh\:mm"), reminder.GetType().Name);
                }
                if (filter == "meeting")
                {
                    if (reminder.GetType().Name == "MeetingReminder")
                        reminderdatagridview.Rows.Add(reminder.ReminderID,reminder.IsTriggered, reminder.Title, reminder.Description, reminder.Summary, reminder.Date.ToShortDateString(), reminder.Time.ToString(@"hh\:mm"), reminder.GetType().Name);
                }
            }
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
            AraYuz araYuz = new AraYuz(user);
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
                int lastId = 0;
                if (allReminders.Count != 0) { 
                     lastId = allReminders[allReminders.Count-1].ReminderID;
                }
                if(addReminder.ReminderType == "meeting")
                {
                    IReminder reminder = meetingFactory.CreateReminder(lastId + 1, user.Id, addReminder.ReminderDate, addReminder.ReminderTime, addReminder.Title, addReminder.Description, addReminder.Summary);
                    allReminders.Add(reminder);
                    reminders.Add(reminder);
                    reminders[reminders.Count - 1].Attach(new MeetingReminderObserver());
                }
                else
                {
                    IReminder reminder = taskFactory.CreateReminder(lastId + 1, user.Id, addReminder.ReminderDate, addReminder.ReminderTime, addReminder.Title, addReminder.Description, addReminder.Summary);
                    allReminders.Add(reminder);
                    reminders.Add(reminder);
                    reminders[reminders.Count - 1].Attach(new TaskReminderObserver()); 

                }
                csvOperations.WriteRemindersToCsv(allReminders);
                AddRemindersToDGW();
            }

        }

        private void personalinfobtn_Click(object sender, EventArgs e)
        {
            PersonalInformation personalInformation = new PersonalInformation(user);
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
                    if (this.InvokeRequired)
                    {
                        this.Invoke((MethodInvoker)delegate {
                            AddRemindersToDGW();
                        });
                    }
                    else
                    {
                        AddRemindersToDGW();
                    }
                }
            }

        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            if (reminderdatagridview.Rows.Count == 0) return;
            int ReminderID = (int)reminderdatagridview.CurrentRow.Cells["ReminderID"].Value;
            IReminder selectedReminder = null;
            for (int i = allReminders.Count - 1; i >= 0; i--)
            {
                IReminder reminder = allReminders[i];
                if (reminder.ReminderID == ReminderID)
                {
                    allReminders.Remove(reminder);
                    reminders.Remove(reminder);
                }
            }

            csvOperations.WriteRemindersToCsv(allReminders);
            AddRemindersToDGW();
        }

        private void reminderdatagridview_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            if (e.ColumnIndex == reminderdatagridview.Columns["Done"].Index)
            {
                bool isChecked = (bool)reminderdatagridview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (isChecked)
                {
                    reminders[e.RowIndex].IsTriggered = true;
                }
                else
                    reminders[e.RowIndex].IsTriggered = false;
            }
        }

        private void reminderdatagridview_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            if (e.ColumnIndex == reminderdatagridview.Columns["Done"].Index)
                reminderdatagridview.EndEdit();

        }

        private void mettingbtn_Click(object sender, EventArgs e)
        {
            filter = "meeting";
            AddRemindersToDGW();
        }

        private void tasksbtn_Click(object sender, EventArgs e)
        {
            filter = "task";
            AddRemindersToDGW();
        }

        private void allbtn_Click(object sender, EventArgs e)
        {
            filter = "all";
            AddRemindersToDGW();
        }

        private void updatereminderbtn_Click(object sender, EventArgs e)
        {
            if (reminderdatagridview.Rows.Count == 0) return;
            int ReminderID = (int)reminderdatagridview.CurrentRow.Cells["ReminderID"].Value;
            IReminder selectedReminder = null;
            foreach(IReminder reminder in allReminders)
            {
                if (reminder.ReminderID == ReminderID) selectedReminder = reminder;
            }
            UpdateReminder updateReminder = new UpdateReminder(selectedReminder);
            if(updateReminder.ShowDialog() == DialogResult.OK)
            {
                selectedReminder = updateReminder.reminder;
            }
            csvOperations.WriteRemindersToCsv(allReminders);
            AddRemindersToDGW();
        }
    }
}
