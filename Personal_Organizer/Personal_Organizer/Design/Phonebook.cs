﻿using Personal_Organizer.Design;
using Personal_Organizer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personal_Organizer
{

    public partial class PhoneBook : Form
    {
        User user;
        bool sidebarExpand;
        private CSVOperations _csvOperations = new CSVOperations();
        private List<Phonebook> phonebooks;
        List<IReminder> reminders = new List<IReminder>();
        System.Timers.Timer timer;
        private bool isNavigating = false;
        public PhoneBook(User _user, List<IReminder> _reminders)
        {
            InitializeComponent();
            SetPlaceholder();
            phonebooks = _csvOperations.ReadPhoneBooks();
            var userBooks = phonebooks.Where(x => x.UserId == _user.Id).ToList();
            dataGridView1.DataSource = userBooks;
            user = _user;
            label3.Text = user.Username;
            byte[] imageBytes = Convert.FromBase64String(user.Base64Photo);
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                circularPicture2.Image = Image.FromStream(ms);
            }
            reminders = _csvOperations.ReadRemindersFromCsv();
            reminders = _reminders;
            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            timer.Enabled = true;

            if (user.Role != Roles.Admin)
            {
                usermanagmentbtn.Visible = false;
            }
            label1.Text = user.Username;
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
                    _csvOperations.WriteRemindersToCsv(reminders);
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


        private void SetPlaceholder()
        {
            if (string.IsNullOrWhiteSpace(searchtxtbox.Text))
            {
                searchtxtbox.Text = "Search";
                searchtxtbox.ForeColor = Color.Gray;
            }
        }
        private void searchtxtbox_Enter(object sender, EventArgs e)
        {
            if (searchtxtbox.Text == "Search")
            {
                searchtxtbox.Text = "";
                searchtxtbox.ForeColor = Color.Black;
            }
        }

        private void searchtxtbox_Leave(object sender, EventArgs e)
        {
            SetPlaceholder();
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AddContact addcontact = new AddContact(dataGridView1, ref phonebooks, user);
            addcontact.Show();

        }

        private void personalinfobtn_Click(object sender, EventArgs e)
        {
            NavigateToForm(new PersonalInformation(user, reminders));
        }
        private void phonebookbtn_Click(object sender, EventArgs e)
        {
            NavigateToForm(new PhoneBook(user, reminders));
        }
        private void notesbtn_Click(object sender, EventArgs e)
        {
            NavigateToForm(new Notes(user, reminders));
        }

        private void salarycalcbtn_Click(object sender, EventArgs e)
        {
            NavigateToForm(new SalaryCalculator(user, reminders));
        }

        private void reminderbtn_Click(object sender, EventArgs e)
        {
            NavigateToForm(new Reminder(user));
        }
        private void usermanagmentbtn_Click(object sender, EventArgs e)
        {
            NavigateToForm(new UserManagement(user));
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
        private void homebtn_MouseEnter(object sender, EventArgs e)
        {
            homebtn.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void homebtn_MouseLeave(object sender, EventArgs e)
        {
            homebtn.BackColor = Color.DimGray;
        }

        private void personalinfobtn_MouseEnter(object sender, EventArgs e)
        {
            personalinfobtn.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void personalinfobtn_MouseLeave(object sender, EventArgs e)
        {
            personalinfobtn.BackColor = Color.DimGray;
        }

        private void phonebookbtn_MouseEnter(object sender, EventArgs e)
        {
            phonebookbtn.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void phonebookbtn_MouseLeave(object sender, EventArgs e)
        {
            phonebookbtn.BackColor = Color.DimGray;
        }

        private void notesbtn_MouseEnter(object sender, EventArgs e)
        {
            notesbtn.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void notesbtn_MouseLeave(object sender, EventArgs e)
        {
            notesbtn.BackColor = Color.DimGray;
        }

        private void salarycalcbtn_MouseEnter(object sender, EventArgs e)
        {
            salarycalcbtn.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void salarycalcbtn_MouseLeave(object sender, EventArgs e)
        {
            salarycalcbtn.BackColor = Color.DimGray;
        }

        private void reminderbtn_MouseEnter(object sender, EventArgs e)
        {
            reminderbtn.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void reminderbtn_MouseLeave(object sender, EventArgs e)
        {
            reminderbtn.BackColor = Color.DimGray;
        }

        private void usermanagmentbtn_MouseEnter(object sender, EventArgs e)
        {
            usermanagmentbtn.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void usermanagmentbtn_MouseLeave(object sender, EventArgs e)
        {
            usermanagmentbtn.BackColor = Color.DimGray;
        }

        private void logoutbtn_MouseEnter(object sender, EventArgs e)
        {
            logoutbtn.BackColor = Color.FromArgb(64, 64, 64);
        }

        private void logoutbtn_MouseLeave(object sender, EventArgs e)
        {
            logoutbtn.BackColor = Color.DimGray;
        }

        private void deleteContactbtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this contact?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            try
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                if (result == DialogResult.Yes && selectedIndex != -1)
                {
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        {
                            var selectedData = row.DataBoundItem as Phonebook;
                            phonebooks.Remove(selectedData);
                        }
                        dataGridView1.DataSource = null;
                        var userBooks = phonebooks.Where(x => x.UserId == user.Id).ToList();
                        dataGridView1.DataSource = userBooks;
                        _csvOperations.WritePhonebook(phonebooks);
                    }
                    else
                    {
                        MessageBox.Show("Please select a row to delete.");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void editbtn_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedIndex = dataGridView1.SelectedRows[0].Index;
                if (dataGridView1.SelectedRows.Count == 1 && selectedIndex != -1)
                {
                    EditContact editContact = new EditContact(dataGridView1, ref phonebooks, user);
                    editContact.ShowDialog();
                }
                else if (dataGridView1.SelectedRows.Count > 1)
                    MessageBox.Show("Aynı anda en fazla 1 kişinin bilgilerini değiştirebilirsiniz !");
                else
                    MessageBox.Show("Lütfen bir kişi seçiniz !");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void addContactbtn_Click(object sender, EventArgs e)
        {
            try
            {
                AddContact addContact = new AddContact(dataGridView1, ref phonebooks, user);
                addContact.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void searchtxtbox_TextChanged(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource != null && searchtxtbox.Text != string.Empty && searchtxtbox.Text != "Search")
            {
                dataGridView1.DataSource = phonebooks.Where(x => x.Name.Contains(searchtxtbox.Text)).ToList();
            }
            if (searchtxtbox.Text == string.Empty)
            {
                dataGridView1.DataSource = phonebooks;
            }
        }

        private void PhoneBook_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isNavigating && e.CloseReason == CloseReason.UserClosing)
            {
                AraYuz araYuz = new AraYuz(user);
                araYuz.Show();
            }
        }

    }
}
