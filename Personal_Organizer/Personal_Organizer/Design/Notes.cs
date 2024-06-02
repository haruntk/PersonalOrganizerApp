using Personal_Organizer.Design;
using Personal_Organizer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personal_Organizer
{
    public partial class Notes : Form
    {
        User user = new User();
        bool sidebarExpand;
        private CSVOperations CSVOperations = new CSVOperations();
        private Note selectedNote;
        List<IReminder> reminders = new List<IReminder>();
        System.Timers.Timer timer;
        private bool isNavigating = false;
        public Notes(User _user,List<IReminder> _reminders)
        {
            user = _user;
            InitializeComponent();
            CSVOperations = new CSVOperations();
            LoadNotesDataGridView();
            updateNoteBtn.Visible = false;
            headerTxt.Visible = false;
            noteTxt.Visible = false;
            donebtn.Visible = false;
            donebtn.Enabled = false;
            headerLbl.Visible = false;
            updateTextBox.Visible = false;
            updateHeaderLbl.Visible = false;
            updateHeaderTxt.Visible = false;
            reminders = CSVOperations.ReadRemindersFromCsv();
            byte[] imageBytes = Convert.FromBase64String(user.Base64Photo);
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                circularPicture2.Image = Image.FromStream(ms);
            }
            label3.Text = user.Username;
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

            // Wire up event handlers
            notesDataGridView.SelectionChanged += notesDataGridView_SelectionChanged;
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
                    CSVOperations.WriteRemindersToCsv(reminders);
                }
            }

        }
        private void LoadNotesDataGridView()
        {
            // Clear the DataGridView
            notesDataGridView.Rows.Clear();

            // Read notes from CSV
            var notes = CSVOperations.ReadNotes(user.Id);

            // Add notes to DataGridView
            foreach (var note in notes)
            {
                notesDataGridView.Rows.Add(note.Header, note.Date, note.Text);
            }
        }

        private void addNote_Click(object sender, EventArgs e)
        {
            noteTxt.Visible = true;
            noteTxt.Clear();
            headerLbl.Visible=true;
            headerTxt.Visible = true; // Assuming you added a TextBox for Header input
            headerTxt.Clear();
            donebtn.Visible = true;
            updateHeaderLbl.Visible = false;
            updateHeaderTxt.Visible = false;
            updateTextBox.Visible = false;
            updateNoteBtn.Visible = false;
        }

        private void deleteNote_Click(object sender, EventArgs e)
        {
            // Check if any row is selected
            if (notesDataGridView.SelectedRows.Count > 0)
            {
                // Get the selected note details
                var selectedRow = notesDataGridView.SelectedRows[0];
                int userID = user.Id;
                string header = selectedRow.Cells[0].Value.ToString();
                DateTime date = DateTime.Parse(selectedRow.Cells[1].Value.ToString());
                string text = selectedRow.Cells[2].Value.ToString();

                Note noteToDelete = new Note
                {
                    UserID = userID,
                    Header = header,
                    Date = date,
                    Text = text
                };

                // Delete the note
                CSVOperations.DeleteNote(noteToDelete, user.Id);

                // Refresh the DataGridView
                LoadNotesDataGridView();
            }
            else
            {
                MessageBox.Show("Please select a note to delete.");
            }


            updateNoteBtn.Visible = false;
            updateHeaderLbl.Visible = false;
            updateHeaderTxt.Visible = false;
            updateTextBox.Visible = false;
            updateNoteBtn.Visible = false;
            noteTxt.Visible = false;
            headerLbl.Visible = false;
            headerTxt.Visible = false;
            updateNoteBtn.Enabled = false;
        }

        private void updateNoteBtn_Click(object sender, EventArgs e)
        {
            // Check if any row is selected
            if (selectedNote != null)
            {
                // Update the text of the selected note with the text from updateTextBox
                string updatedText = updateTextBox.Text.Trim();
                string updatedHeader = updateHeaderTxt.Text.Trim();
                if (!string.IsNullOrEmpty(updatedText))
                {
                    // Delete the original note
                    CSVOperations.DeleteNote(selectedNote, user.Id);

                    // Create an updated note with the modified text and original date
                    Note updatedNote = new Note
                    {
                        UserID = user.Id,
                        Header = updatedHeader,
                        Date = DateTime.Now, // Keep the original date
                        Text = updatedText
                    };

                    // Write the updated note to the CSV file
                    CSVOperations.WriteNote(updatedNote);

                    // Refresh the DataGridView
                    LoadNotesDataGridView();

                    // Clear the updateTextBox
                    updateTextBox.Clear();
                    updateHeaderTxt.Clear();
                    updateTextBox.Visible = false;
                    updateHeaderLbl.Visible = false;
                    updateHeaderTxt.Visible = false;
                    updateNoteBtn.Visible = false;
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

        private void notesDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (notesDataGridView.SelectedRows.Count > 0)
            {
                // Get the selected note details
                var selectedRow = notesDataGridView.SelectedRows[0];
                selectedNote = new Note
                {
                    UserID = user.Id,
                    Header = selectedRow.Cells[0].Value.ToString(),
                    Date = DateTime.Parse(selectedRow.Cells[1].Value.ToString()),
                    Text = selectedRow.Cells[2].Value.ToString()
                };

                updateNoteBtn.Visible = true;
                updateTextBox.Visible = true;
                updateHeaderLbl.Visible = true;
                updateHeaderTxt.Visible = true;
                donebtn.Visible = false;
                noteTxt.Visible = false;
                headerLbl.Visible = false;
                headerTxt.Visible = false;
                updateHeaderTxt.Text = selectedNote.Header;
                updateTextBox.Text = selectedNote.Text;
                updateNoteBtn.Enabled = false;
            }
        }

        private void donebtn_Click(object sender, EventArgs e)
        {
            string text = noteTxt.Text.Trim();
            string header = headerTxt.Text.Trim(); // Assuming you added a TextBox for Header input

            if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(header))
            {
                // Read notes from CSV
                var notes = CSVOperations.ReadNotes(user.Id);

                // Check if a note with the same header already exists
                bool duplicateHeader = notes.Any(note => note.Header.Equals(header, StringComparison.OrdinalIgnoreCase));

                if (duplicateHeader)
                {
                    MessageBox.Show("A note with the same header already exists. Please use a different header.");
                }
                else
                {
                    // Create a Note object
                    Note note = new Note
                    {
                        UserID = user.Id,
                        Header = header,
                        Date = DateTime.Now,
                        Text = text
                    };

                    // Write the note to CSV
                    CSVOperations.WriteNote(note);

                    // Refresh the DataGridView
                    LoadNotesDataGridView();
                    noteTxt.Visible = false;
                    donebtn.Visible = false;
                    headerTxt.Visible = false;
                    headerLbl.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("Please enter both header and note text.");
            }
        }

        private void noteTxt_TextChanged(object sender, EventArgs e)
        {
            donebtn.Enabled = (noteTxt.Text.Length > 0 && headerTxt.Text.Length > 0);
        }

        private void updateTextBox_TextChanged(object sender, EventArgs e)
        {
            updateNoteBtn.Enabled = updateTextBox.Text != selectedNote.Text;
        }

        private void headerTxt_TextChanged(object sender, EventArgs e)
        {
            donebtn.Enabled = (noteTxt.Text.Length > 0 && headerTxt.Text.Length > 0);
        }

        private void updateHeaderTxt_TextChanged(object sender, EventArgs e)
        {
            updateNoteBtn.Enabled = updateHeaderTxt.Text != selectedNote.Header;
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

        private void NavigateToForm(Form form)
        {
            isNavigating = true;
            this.Close();
            form.FormClosed += (s, args) => isNavigating = false;
            form.Show();
        }

        private void homebtn_Click(object sender, EventArgs e)
        {
            NavigateToForm(new AraYuz(user));
        }

        private void personalinfobtn_Click(object sender, EventArgs e)
        {
            NavigateToForm(new PersonalInformation(user, reminders));
        }

        private void reminderbtn_Click(object sender, EventArgs e)
        {
            NavigateToForm(new Reminder(user));
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

        private void Notes_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isNavigating && e.CloseReason == CloseReason.UserClosing)
            {
                AraYuz araYuz = new AraYuz(user);
                araYuz.Show();
            }
        }
    }
}
