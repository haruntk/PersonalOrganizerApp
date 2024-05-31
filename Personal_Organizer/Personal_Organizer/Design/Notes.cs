using Personal_Organizer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personal_Organizer
{
    public partial class Notes : Form
    {
        User user;
        bool sidebarExpand;
        private CSVOperations CSVOperations;
        private string originalUpdateText;
        public Notes(User _user)
        {
            user = _user;
            InitializeComponent();
            CSVOperations = new CSVOperations();
            LoadNotesListBox();
            updateNoteBtn.Visible = false;
            noteTxt.Visible = false;
            donebtn.Visible = false;
            donebtn.Enabled = false;
            updateTextBox.Visible = false;
        }

        private void Notes_FormClosing(object sender, FormClosingEventArgs e)
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


        private void LoadNotesListBox()
        {
            // Clear the ListBox
            notesListBox.Items.Clear();

            // Read notes from CSV
            var notes = CSVOperations.ReadNotes(user.Id);

            // Add notes to ListBox
            foreach (var note in notes)
            {
                notesListBox.Items.Add($"{note.UserID},{note.Date},{note.Text}");
            }
        }
        private void addNote_Click(object sender, EventArgs e)
        {
            noteTxt.Visible=true;
            noteTxt.Clear();
            donebtn.Visible=true;
            updateTextBox.Visible=false;
            updateNoteBtn.Visible=false;
        }
        private void deleteNote_Click(object sender, EventArgs e)
        {
            // Check if any item is selected
            if (notesListBox.SelectedItem != null)
            {
                // Parse the selected item into a Note object
                string selectedItem = notesListBox.SelectedItem.ToString();
                info.Text = selectedItem;
                string[] parts = selectedItem.Split(',');
                info2.Text = parts[2];
                Note noteToDelete = new Note
                {
                    UserID = int.Parse(parts[0]),
                    Date = DateTime.Parse(parts[1]),
                    Text = parts[2]
                };

                // Delete the note
                CSVOperations.DeleteNote(noteToDelete,user.Id);

                // Refresh the notesListBox
                LoadNotesListBox();
            }
            else
            {
                MessageBox.Show("Please select a note to delete.");
            }
        }

        private void updateNoteBtn_Click(object sender, EventArgs e)
        {
            // Check if any item is selected
            if (notesListBox.SelectedItem != null)
            {
                // Get the selected note details
                string selectedItem = notesListBox.SelectedItem.ToString();
                string[] parts = selectedItem.Split(',');
                int userID = int.Parse(parts[0]);
                DateTime date = DateTime.Now;
                string originalText = parts[2];

                // Update the text of the selected note with the text from updateTextBox
                string updatedText = updateTextBox.Text.Trim();
                if (!string.IsNullOrEmpty(updatedText))
                {
                    // Delete the original note
                    Note noteToDelete = new Note { UserID = userID, Date = date, Text = originalText };
                    CSVOperations.DeleteNote(noteToDelete, user.Id);

                    // Create an updated note with the modified text and original date
                    Note updatedNote = new Note { UserID = userID, Date = date, Text = updatedText };

                    // Write the updated note to the CSV file
                    CSVOperations.WriteNote(updatedNote);

                    // Refresh the notesListBox
                    LoadNotesListBox();

                    // Clear the updateTextBox
                    updateTextBox.Clear();
                    updateNoteBtn.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Please enter an updated note text.");
                }
            }
            else
            {
                MessageBox.Show("Please select a note to update.");
            }
            
        }

        private void notesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected note details
            string selectedItem = notesListBox.SelectedItem.ToString();
            string[] parts = selectedItem.Split(',');
            updateNoteBtn.Visible = true;
            updateTextBox.Visible = true;
            updateTextBox.Text = parts[2];
            originalUpdateText = parts[2];
            updateNoteBtn.Enabled = false;
        }

        private void homebtn_MouseEnter(object sender, EventArgs e)
        {
            homebtn.BackColor = Color.Goldenrod;
        }

        private void homebtn_MouseLeave(object sender, EventArgs e)
        {
            homebtn.BackColor = Color.Gold;
        }

        private void personalinfobtn_MouseEnter(object sender, EventArgs e)
        {
            personalinfobtn.BackColor = Color.Goldenrod;
        }

        private void personalinfobtn_MouseLeave(object sender, EventArgs e)
        {
            personalinfobtn.BackColor = Color.Gold;
        }

        private void phonebookbtn_MouseEnter(object sender, EventArgs e)
        {
            phonebookbtn.BackColor = Color.Goldenrod;
        }

        private void phonebookbtn_MouseLeave(object sender, EventArgs e)
        {
            phonebookbtn.BackColor = Color.Gold;
        }

        private void notesbtn_MouseEnter(object sender, EventArgs e)
        {
            notesbtn.BackColor = Color.Goldenrod;
        }

        private void notesbtn_MouseLeave(object sender, EventArgs e)
        {
            notesbtn.BackColor = Color.Gold;
        }

        private void salarycalcbtn_MouseEnter(object sender, EventArgs e)
        {
            salarycalcbtn.BackColor = Color.Goldenrod;
        }

        private void salarycalcbtn_MouseLeave(object sender, EventArgs e)
        {
            salarycalcbtn.BackColor = Color.Gold;
        }

        private void reminderbtn_MouseLeave(object sender, EventArgs e)
        {
            reminderbtn.BackColor = Color.Gold;
        }

        private void reminderbtn_MouseEnter(object sender, EventArgs e)
        {
            reminderbtn.BackColor = Color.Goldenrod;
        }

        private void usermanagmentbtn_MouseEnter(object sender, EventArgs e)
        {
            usermanagmentbtn.BackColor = Color.Goldenrod;
        }

        private void usermanagmentbtn_MouseLeave(object sender, EventArgs e)
        {
            usermanagmentbtn.BackColor = Color.Gold;
        }

        private void logoutbtn_MouseEnter(object sender, EventArgs e)
        {
            logoutbtn.BackColor = Color.Goldenrod;
        }

        private void logoutbtn_MouseLeave(object sender, EventArgs e)
        {
            logoutbtn.BackColor = Color.Gold;
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

        private void donebtn_Click(object sender, EventArgs e)
        {
            string text = noteTxt.Text.Trim();

            if (!string.IsNullOrEmpty(text))
            {
                // Create a Note object
                Note note = new Note
                {
                    UserID = user.Id, // Assuming UserID is fixed for now
                    Date = DateTime.Now,
                    Text = text
                };

                // Write the note to CSV
                CSVOperations.WriteNote(note);

                // Refresh the notesListBox
                LoadNotesListBox();
                noteTxt.Visible = false;
                donebtn.Visible = false;
            }
            else
            {
                MessageBox.Show("Please enter a note text.");
            }
        }

        private void noteTxt_TextChanged(object sender, EventArgs e)
        {
            if (noteTxt.Text.Length > 0)
            {
                donebtn.Enabled = true;
            }
        }

        private void updateTextBox_TextChanged(object sender, EventArgs e)
        {
            updateNoteBtn.Enabled = updateTextBox.Text != originalUpdateText;

        }

        private void homebtn_Click(object sender, EventArgs e)
        {
            AraYuz arayuz = new AraYuz(user);
            arayuz.Show();
            this.Hide();
        }

        private void personalinfobtn_Click(object sender, EventArgs e)
        {
            PersonalInformation personalInformation = new PersonalInformation(user);
            personalInformation.Show();
            this.Hide();
        }

        private void reminderbtn_Click(object sender, EventArgs e)
        {
            Reminder reminder = new Reminder(user);
            reminder.Show();
            this.Hide();
        }
    }
}
