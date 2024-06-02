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
using System.Net;
using System.Net.Mail;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Threading;

namespace Personal_Organizer.Design
{
    public partial class UserManagement : Form
    {
        User user = new User();
        private CSVOperations csvOperations = new CSVOperations();
        string _userEmail = string.Empty;
        bool sidebarExpand;
        List<IReminder> reminders = new List<IReminder>();
        System.Timers.Timer timer;
        private bool isNavigating = false;
        
        
        public UserManagement(User _user)
        {
            InitializeComponent();
            user = _user;
            label1.Text=user.Name;
            LoadUsersIntoDataGridView();

            userCurrentRoleLbl.Visible = false;
            userNewRoleLbl.Visible=false;
            currentRoleLbl.Visible = false;
            roleCombobox.Visible = false;
            saveBtn.Visible = false;
            saveBtn.Enabled = false;

            newPasswordLbl.Visible = false;
            passTextBox.Visible = false;
            passTextBox.Enabled = false;
            passSendBtn.Visible = false;
            passSendBtn.Enabled = false;

            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.Visible = false;

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
        private void NavigateToForm(Form form)
        {
            isNavigating = true;
            this.Close();
            form.FormClosed += (s, args) => isNavigating = false;
            form.Show();
        }
        private void LoadUsersIntoDataGridView()
        {
            // Read all users
            var users = csvOperations.ReadAllUsers();

            // Clear existing rows
            usersDataGridView.Rows.Clear();

            // Populate the DataGridView
            foreach (var user in users)
            {
                usersDataGridView.Rows.Add(user.Id.ToString(),user.Name.ToString(),user.Surname.ToString(), user.Role.ToString(), user.Email.ToString(), user.IsForgotten.ToString());
            }
        }
        private void usersDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (usersDataGridView.SelectedRows.Count > 0)
            {
                userCurrentRoleLbl.Visible = true;
                userNewRoleLbl.Visible = true;
                currentRoleLbl.Visible = true;
                roleCombobox.Visible = true;
                saveBtn.Visible = true;
                saveBtn.Enabled = true;

                var selectedRow = usersDataGridView.SelectedRows[0];

                currentRoleLbl.Text = selectedRow.Cells["userRole"].Value.ToString();
                _userEmail = selectedRow.Cells["userEmail"].Value.ToString();
                string isforgetten = selectedRow.Cells["userIsForgotten"].ToString();

                if (selectedRow.Cells["userIsForgotten"].Value.ToString() == "True")
                {
                    newPasswordLbl.Visible = true;
                    passTextBox.Visible = true;
                    passTextBox.Enabled = true;
                    passSendBtn.Visible = true;
                    passSendBtn.Enabled = true;
                }
                else
                {
                    newPasswordLbl.Visible = false;
                    passTextBox.Visible = false;
                    passTextBox.Enabled = false;
                    passSendBtn.Visible = false;
                    passSendBtn.Enabled = false;
                }
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (usersDataGridView.SelectedRows.Count > 0)
            {
                var selectedRow = usersDataGridView.SelectedRows[0];
                int userId = int.Parse(selectedRow.Cells["userId"].Value.ToString());
                var selectedRole = (Roles)Enum.Parse(typeof(Roles), roleCombobox.SelectedItem.ToString());

                csvOperations.UpdateUserRole(userId, selectedRole);
                LoadUsersIntoDataGridView();
                MessageBox.Show("User's role is updated successfully");
            }
            else
            {
                MessageBox.Show("Please select a user to update their role.", "No User Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            userCurrentRoleLbl.Visible = false;
            userNewRoleLbl.Visible = false;
            currentRoleLbl.Visible = false;
            roleCombobox.Visible = false;
            saveBtn.Visible = false;
            saveBtn.Enabled = false;
        }
        private async void passSendBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (passTextBox.Text.Length < 5)
                {
                    MessageBox.Show("Password must be at least 5 characters long.", "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    progressBar.Visible = false;
                    return;
                }

                var selectedRow = usersDataGridView.SelectedRows[0];
                int userId = int.Parse(selectedRow.Cells["userId"].Value.ToString());
                // Retrieve the current password of the selected user
                var users = csvOperations.ReadAllUsers();
                var selectedUser = users.FirstOrDefault(u => u.Id == userId);

                if (selectedUser != null && selectedUser.Password == passTextBox.Text)
                {
                    MessageBox.Show("The new password cannot be the same as the previous password.", "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    progressBar.Visible = false;
                    return;
                }

                progressBar.Visible = true;

                // Set up the email message
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("pofrom2626@gmail.com");
                mail.To.Add(_userEmail);
                mail.Subject = "PO New Password";
                mail.Body = $"This is your new password please do not share with anyone : {passTextBox.Text} - given by {user.Name} {user.Surname} ";

                // Configure the SMTP client
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.Port = 587; // Use the appropriate port
                smtpClient.Credentials = new NetworkCredential("pofrom2626@gmail.com", "cknwzbypidtuzdkk");
                smtpClient.EnableSsl = true; // Enable SSL if required by your SMTP server

                // Send the email 
                await smtpClient.SendMailAsync(mail);
                // Update the user's password
                csvOperations.UpdateUserPassword(userId, passTextBox.Text);
                LoadUsersIntoDataGridView();

                // Hide password-related controls after successful sending
                newPasswordLbl.Visible = false;
                passTextBox.Visible = false;
                passTextBox.Enabled = false;
                passSendBtn.Visible = false;
                passSendBtn.Enabled = false;

                MessageBox.Show("Email sent successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending email: " + ex.Message);
            }
            finally
            {
                progressBar.Visible = false;
            }
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
        private void homebtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void homebtn_MouseEnter(object sender, EventArgs e)
        {
            homebtn.BackColor = Color.SeaGreen;
        }

        private void homebtn_MouseLeave(object sender, EventArgs e)
        {
            homebtn.BackColor = Color.MediumSeaGreen;
        }

        private void personalinfobtn_Click(object sender, EventArgs e)
        {
            NavigateToForm(new PersonalInformation(user));
        }

        private void personalinfobtn_MouseEnter(object sender, EventArgs e)
        {
            personalinfobtn.BackColor = Color.SeaGreen;
        }

        private void personalinfobtn_MouseLeave(object sender, EventArgs e)
        {
            personalinfobtn.BackColor = Color.MediumSeaGreen;
        }

        private void phonebookbtn_Click(object sender, EventArgs e)
        {
            NavigateToForm(new PhoneBook(user));
        }

        private void phonebookbtn_MouseEnter(object sender, EventArgs e)
        {
            phonebookbtn.BackColor = Color.SeaGreen;
        }

        private void phonebookbtn_MouseLeave(object sender, EventArgs e)
        {
            phonebookbtn.BackColor= Color.MediumSeaGreen;
        }

        private void notesbtn_Click(object sender, EventArgs e)
        {
            NavigateToForm(new PhoneBook(user));
        }

        private void notesbtn_MouseEnter(object sender, EventArgs e)
        {
            notesbtn.BackColor = Color.SeaGreen;
        }

        private void notesbtn_MouseLeave(object sender, EventArgs e)
        {
            notesbtn.BackColor = Color.MediumSeaGreen;
        }

        private void salarycalcbtn_Click(object sender, EventArgs e)
        {
            NavigateToForm(new SalaryCalculator(user));
        }

        private void salarycalcbtn_MouseEnter(object sender, EventArgs e)
        {
            salarycalcbtn.BackColor = Color.SeaGreen;
        }

        private void salarycalcbtn_MouseLeave(object sender, EventArgs e)
        {
            salarycalcbtn.BackColor = Color.MediumSeaGreen;
        }

        private void reminderbtn_Click(object sender, EventArgs e)
        {
            NavigateToForm(new Reminder(user));
        }

        private void reminderbtn_MouseEnter(object sender, EventArgs e)
        {
            reminderbtn.BackColor = Color.SeaGreen;
        }

        private void reminderbtn_MouseLeave(object sender, EventArgs e)
        {
            reminderbtn.BackColor = Color.MediumSeaGreen;
        }

        private void usermanagmentbtn_MouseEnter(object sender, EventArgs e)
        {
            usermanagmentbtn.BackColor = Color.SeaGreen;
        }

        private void usermanagmentbtn_MouseLeave(object sender, EventArgs e)
        {
            usermanagmentbtn.BackColor = Color.MediumSeaGreen;
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
                Application.Restart();
            }
        }

        private void logoutbtn_MouseEnter(object sender, EventArgs e)
        {
            logoutbtn.BackColor = Color.SeaGreen;
        }

        private void logoutbtn_MouseLeave(object sender, EventArgs e)
        {
            logoutbtn.BackColor= Color.MediumSeaGreen;
        }

        private void UserManagement_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isNavigating && e.CloseReason == CloseReason.UserClosing)
            {
                AraYuz araYuz = new AraYuz(user);
                araYuz.Show();
            }
        }
    }
}
