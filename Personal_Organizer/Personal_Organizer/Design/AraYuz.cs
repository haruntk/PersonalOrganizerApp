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
    public partial class AraYuz : Form
    {
        List<IReminder> reminders = new List<IReminder>();
        System.Timers.Timer timer;
        CSVOperations csvOperations = new CSVOperations();
        User user = new User();
        public AraYuz(User _user)
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            roundedButton1.BackColor = Color.FromArgb(227, 238, 241);
            label1.BackColor = Color.FromArgb(227, 238, 241);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            this.user = _user;
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

        private void btnPersonal_Click(object sender, EventArgs e)
        {
            PersonalInformation personalInformation = new PersonalInformation(user);
            personalInformation.Show();
            this.Hide();
        }

        private void AraYuz_FormClosing(object sender, FormClosingEventArgs e)
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

        private void btnReminder_Click(object sender, EventArgs e)
        {
            Reminder reminder = new Reminder(user);
            reminder.ShowDialog();
            this.Hide();
        }

        private void btnNotes_Click(object sender, EventArgs e)
        {
            Notes notes = new Notes(user);
            notes.ShowDialog();
            this.Hide();
        }

        private void btnPhone_Click(object sender, EventArgs e)
        {

            PhoneBook phonebook = new PhoneBook(user);
            phonebook.ShowDialog();
            this.Hide();
        }

        private void btnSalary_Click(object sender, EventArgs e)
        {
            SalaryCalculator salaryCalculator = new SalaryCalculator(user);
            salaryCalculator.ShowDialog();
            this.Hide();
        }

        private void rdnManagement_Click(object sender, EventArgs e)
        {
            UserManagament userManagament=new UserManagament(user);
            userManagament.ShowDialog();
            this.Hide();
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Giris giris = new Giris();
                giris.ShowDialog();
                this.Hide();
            }
        }
    }
}
