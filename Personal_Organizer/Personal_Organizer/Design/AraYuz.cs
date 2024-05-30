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
        User user;
        public AraYuz(User _user)
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            roundedButton1.BackColor = Color.FromArgb(227, 238, 241);
            label1.BackColor = Color.FromArgb(227, 238, 241);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            this.user = _user;

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
            UserManagament userManagament=new UserManagament();
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
