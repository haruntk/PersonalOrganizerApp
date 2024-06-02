using Personal_Organizer.Design;
using Personal_Organizer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personal_Organizer
{
    public partial class AraYuz : Form
    {
        User user = new User();
        List<IReminder> reminders = new List<IReminder>();
        System.Timers.Timer timer;
        CSVOperations csvOperations = new CSVOperations();
        bool sidebarExpand;


        public AraYuz(User _user)
        {
            this.user = _user;
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            roundedButton1.BackColor = Color.FromArgb(227, 238, 241);
            lblWelcome.BackColor = Color.FromArgb(227, 238, 241);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;


            // Check user role and set userManagementBtn visibility
            if (user.Role != Roles.Admin)
            {
                userManagementBtn.Visible = false;
            }

            this.user = _user;
            lblWelcome.Text = $"Welcome, {user.Name} {user.Surname}";
            lblAd.Text = user.Username;
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
            byte[] imageBytes = Convert.FromBase64String(user.Base64Photo);
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                circularPicture1.Image = Image.FromStream(ms);
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
                    csvOperations.WriteRemindersToCsv(reminders);

                }
            }

        }

        private void NavigateToForm(Form form)
        {
            this.Hide();
            //form.FormClosed += (s, args) => this.Show();
            form.Show();

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
                else
                {
                    Application.Exit();
                }
            }
        }
        private void btnPersonal_Click(object sender, EventArgs e)
        {
            NavigateToForm(new PersonalInformation(user,reminders));
        }
        private void btnReminder_Click(object sender, EventArgs e)
        {
            NavigateToForm(new Reminder(user));
        }

        private void btnNotes_Click(object sender, EventArgs e)
        {
            NavigateToForm(new Notes(user, reminders));
        }

        private void btnPhone_Click(object sender, EventArgs e)
        {
            NavigateToForm(new PhoneBook(user, reminders));
        }

        private void btnSalary_Click(object sender, EventArgs e)
        {
            NavigateToForm(new SalaryCalculator(user, reminders));
        }

        private void userManagementBtn_Click(object sender, EventArgs e)
        {
            NavigateToForm(new UserManagement(user));
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Restart();

            }
        }

        private void infobtn_Click(object sender, EventArgs e)
        {
            NavigateToForm(new Info(user, reminders));
        }

        private void homebtn_MouseEnter(object sender, EventArgs e)
        {
            homebtn.BackColor = Color.CadetBlue;
        }

        private void homebtn_MouseLeave(object sender, EventArgs e)
        {
            homebtn.BackColor = Color.FromArgb(173, 213, 216);
        }

        private void infobtn_MouseEnter(object sender, EventArgs e)
        {
            infobtn.BackColor = Color.CadetBlue;
        }

        private void infobtn_MouseLeave(object sender, EventArgs e)
        {
            infobtn.BackColor = Color.FromArgb(173, 213, 216);
        }

        private void logoutbtn_MouseEnter(object sender, EventArgs e)
        {
            logoutbtn.BackColor = Color.CadetBlue;
        }

        private void logoutbtn_MouseLeave(object sender, EventArgs e)
        {
            logoutbtn.BackColor = Color.FromArgb(173, 213, 216);
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

        private void AraYuz_Load(object sender, EventArgs e)
        {
            if(user.Role==Roles.Admin)
            {
                userManagementBtn.Visible = true;
                roundedButton2.Visible = false;
            }
            else if(user.Role==Roles.User||user.Role==Roles.Part_Time_User)
            {
                roundedButton2.Visible = true;
                userManagementBtn.Visible=false;
                userManagementBtn.Enabled=false;
            }
        }
    }
}
