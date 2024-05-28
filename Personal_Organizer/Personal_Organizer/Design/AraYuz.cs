using Personal_Organizer.Design;
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
        public AraYuz()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            roundedButton1.BackColor = Color.FromArgb(227, 238, 241);
            label1.BackColor = Color.FromArgb(227, 238, 241);


        }


        private void btnPersonal_Click(object sender, EventArgs e)
        {
            PersonalInformation personalInformation = new PersonalInformation();
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
            Reminder reminder = new Reminder();
            reminder.ShowDialog();
            this.Hide();
        }

        private void btnNotes_Click(object sender, EventArgs e)
        {
            Notes notes = new Notes();
            notes.ShowDialog();
            this.Hide();
        }

        private void btnPhone_Click(object sender, EventArgs e)
        {

            PhoneBook phonebook = new PhoneBook();
            phonebook.ShowDialog();
            this.Hide();
        }

        private void btnSalary_Click(object sender, EventArgs e)
        {
            SalaryCalculator salaryCalculator = new SalaryCalculator();
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
