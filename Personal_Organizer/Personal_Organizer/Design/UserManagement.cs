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
        public UserManagement(User _user)
        {
            InitializeComponent();
            user = _user;

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
    }
}
