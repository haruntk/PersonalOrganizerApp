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
    public partial class PhoneBook : Form
    {
        bool sidebarExpand;
        private CSVOperations _csvOperations = new CSVOperations();
        private List<Phonebook> phonebooks;
        public PhoneBook()
        {
            InitializeComponent();
            SetPlaceholder();
            phonebooks = _csvOperations.ReadPhoneBooks();
            dataGridView1.DataSource = phonebooks;
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
            AddContact addcontact = new AddContact(dataGridView1, ref phonebooks);
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
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        var selectedData = row.DataBoundItem as Phonebook;
                        phonebooks.Remove(selectedData);
                    }
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = phonebooks;
                    _csvOperations.WritePhonebook(phonebooks);
                }
                else
                {
                    MessageBox.Show("Please select a row to delete.");
                }
            }
        }

        private void editbtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                EditContact editContact = new EditContact(dataGridView1, ref phonebooks);
                editContact.ShowDialog();
            }
            else if (dataGridView1.SelectedRows.Count > 1)
                MessageBox.Show("Aynı anda en fazla 1 kişinin bilgilerini değiştirebilirsiniz !");
            else
                MessageBox.Show("Lütfen bir kişi seçiniz !");
        }

        private void addContactbtn_Click(object sender, EventArgs e)
        {
            AddContact addContact = new AddContact(dataGridView1, ref phonebooks);
            addContact.ShowDialog();
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
    }
}
