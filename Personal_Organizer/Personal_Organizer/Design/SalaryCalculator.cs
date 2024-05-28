using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personal_Organizer.Design
{
    public partial class SalaryCalculator : Form
    {
        public SalaryCalculator()
        {
            InitializeComponent();
        }

        private void homebtn_Click(object sender, EventArgs e)
        {
            AraYuz araYuz = new AraYuz();
            araYuz.ShowDialog();
            this.Close();
        }

        private void personalinfobtn_Click(object sender, EventArgs e)
        {
            PersonalInformation personalInformation = new PersonalInformation();
            personalInformation.ShowDialog();
            this.Close();
        }

        private void phonebookbtn_Click(object sender, EventArgs e)
        {
            PhoneBook phoneBook = new PhoneBook(); 
            phoneBook.ShowDialog();
            this.Close();
        }

        private void notesbtn_Click(object sender, EventArgs e)
        {
            Notes notes = new Notes();
            notes.ShowDialog();
            this.Close();
        }

        private void reminderbtn_Click(object sender, EventArgs e)
        {
            Reminder reminder = new Reminder();
            reminder.ShowDialog();
            this.Close();
        }

        private void usermanagmentbtn_Click(object sender, EventArgs e)
        {
            UserManagament userManagament = new UserManagament();
            userManagament.ShowDialog();
            this.Close();
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            AraYuz araYuz=new AraYuz();
            araYuz.ShowDialog();
            this.Close();
        }
    }
}
