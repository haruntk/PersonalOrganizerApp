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

        private CSVOperations CSVOperations;
        public Notes()
        {
            InitializeComponent();
            CSVOperations = new CSVOperations();
            LoadNotesListBox();
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
        private void homebtn_MouseLeave(object sender, EventArgs e)
        {
            homebtn.BackColor = Color.Gold;
        }

        private void homebtn_MouseEnter(object sender, EventArgs e)
        {
            homebtn.BackColor = Color.Goldenrod;
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

        private void reminderbtn_MouseEnter(object sender, EventArgs e)
        {
            reminderbtn.BackColor = Color.Goldenrod;
        }

        private void reminderbtn_MouseLeave(object sender, EventArgs e)
        {
            reminderbtn.BackColor = Color.Gold;
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

        private void LoadNotesListBox()
        {
            // Clear the ListBox
            notesListBox.Items.Clear();

            // Read notes from CSV
            var notes = CSVOperations.ReadNotes();

            // Add notes to ListBox
            foreach (var note in notes)
            {
                notesListBox.Items.Add($"{note.UserID},{note.Date},{note.Text}");
            }
        }
        private void addNote_Click(object sender, EventArgs e)
        {
            string text = noteTxt.Text.Trim();

            if (!string.IsNullOrEmpty(text))
            {
                // Create a Note object
                Note note = new Note
                {
                    UserID = 1, // Assuming UserID is fixed for now
                    Date = DateTime.Now,
                    Text = text
                };

                // Write the note to CSV
                CSVOperations.WriteNote(note);

                // Refresh the notesListBox
                LoadNotesListBox();
            }
            else
            {
                MessageBox.Show("Please enter a note text.");
            }
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
                CSVOperations.DeleteNote(noteToDelete);

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
                DateTime date = DateTime.Parse(parts[1]);
                string originalText = parts[2];

                // Update the text of the selected note with the text from updateTextBox
                string updatedText = updateTextBox.Text.Trim();
                if (!string.IsNullOrEmpty(updatedText))
                {
                    // Delete the original note
                    Note noteToDelete = new Note { UserID = userID, Date = date, Text = originalText };
                    CSVOperations.DeleteNote(noteToDelete);

                    // Create an updated note with the modified text and original date
                    Note updatedNote = new Note { UserID = userID, Date = date, Text = updatedText };

                    // Write the updated note to the CSV file
                    CSVOperations.WriteNote(updatedNote);

                    // Refresh the notesListBox
                    LoadNotesListBox();

                    // Clear the updateTextBox
                    updateTextBox.Clear();
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
            updateTextBox.Text = parts[2];
        }
    }
}
