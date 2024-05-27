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
    public partial class Phonebook : Form
    {
        bool sidebarExpand;
        public Phonebook()
        {
            InitializeComponent();
            SetPlaceholder();
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
            AraYuz araYuz = new AraYuz();
            araYuz.Show();
            this.Hide();
        }

        private void Phonebook_FormClosing(object sender, FormClosingEventArgs e)
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AddContact addcontact = new AddContact();
            addcontact.ShowDialog();

        }

        private void personalinfobtn_Click(object sender, EventArgs e)
        {
            PersonalInformation personalInformation = new PersonalInformation();
            personalInformation.ShowDialog();
            this.Hide();
        }

        private void notesbtn_Click(object sender, EventArgs e)
        {
            Notes notes = new Notes();
            notes.ShowDialog();
            this.Hide();
        }

        private void salarycalcbtn_Click(object sender, EventArgs e)
        {

        }

        private void reminderbtn_Click(object sender, EventArgs e)
        {
            Reminder reminder = new Reminder();
            reminder.ShowDialog();
            this.Hide();
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

            if (result == DialogResult.Yes)
            {

            }
        }

        private void editbtn_Click(object sender, EventArgs e)
        {
            AddContact addContact = new AddContact();
            addContact.ShowDialog();
        }

        private void addContactbtn_Click(object sender, EventArgs e)
        {
            AddContact addContact = new AddContact();
            addContact.ShowDialog();
        }
    }
}
