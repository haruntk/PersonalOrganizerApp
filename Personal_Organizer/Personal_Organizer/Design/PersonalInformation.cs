using Personal_Organizer.Design;
using Personal_Organizer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Personal_Organizer
{
    public partial class PersonalInformation : Form
    {
        private readonly User user;
        private List<User> users;
        private ProfileMemento _initialState; // İlk durumu saklamak için
        private CSVOperations csvOperations = new CSVOperations();
        private readonly ProfileCaretaker _caretaker = new ProfileCaretaker();
        private bool isNavigating = false;
        bool sidebarExpand;
        public PersonalInformation(User _user)
        {
            InitializeComponent();
            users = csvOperations.ReadAllUsers();
            user = _user;
            InitializeInitialState();
            AttachEventHandlers();

            if (user.Role != Roles.Admin)
            {
                usermanagmentbtn.Visible = false;
            }
            byte[] imageBytes = Convert.FromBase64String(user.Base64Photo);
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                circularPicture2.Image = Image.FromStream(ms);
            }
        }

        private void InitializeInitialState()
        {
            adresstxt.Text = user.Address;
            nametxt.Text = user.Name;
            passwordtxt.Text = user.Password;
            phonenumbertxt.Text = user.PhoneNumber;
            surnametxt.Text = user.Surname;
            emailtxt.Text = user.Email;
            _initialState = user.CreateMemento(); // Başlangıç durumunu kaydet
            _caretaker.SaveState(_initialState);
            UpdateFormFields();
        }
        private void AttachEventHandlers()
        {
            adresstxt.TextChanged += textBox_TextChanged;
            nametxt.TextChanged += textBox_TextChanged;
            passwordtxt.TextChanged += textBox_TextChanged;
            phonenumbertxt.TextChanged += textBox_TextChanged;
            surnametxt.TextChanged += textBox_TextChanged;
            emailtxt.TextChanged += textBox_TextChanged;
        }
        private void DetachEventHandlers()
        {
            adresstxt.TextChanged -= textBox_TextChanged;
            nametxt.TextChanged -= textBox_TextChanged;
            passwordtxt.TextChanged -= textBox_TextChanged;
            phonenumbertxt.TextChanged -= textBox_TextChanged;
            surnametxt.TextChanged -= textBox_TextChanged;
            emailtxt.TextChanged -= textBox_TextChanged;
        }
        private void SaveState()
        {
            user.Name = nametxt.Text;
            user.Surname = surnametxt.Text;
            user.PhoneNumber = phonenumbertxt.Text;
            user.Address = adresstxt.Text;
            user.Password = passwordtxt.Text;
            user.Email = emailtxt.Text;

            _caretaker.SaveState(user.CreateMemento());

        }
        private void RestoreState(ProfileMemento memento)
        {
            if (memento != null)
            {
                user.RestoreMemento(memento);
                UpdateFormFields();
            }
        }
        private void UpdateFormFields()
        {
            DetachEventHandlers();
            nametxt.Text = user.Name;
            surnametxt.Text = user.Surname;
            phonenumbertxt.Text = user.PhoneNumber;
            adresstxt.Text = user.Address;
            passwordtxt.Text = user.Password;
            emailtxt.Text = user.Email;
            AttachEventHandlers();
        }



        //sidebar menu minimization
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
        private void PersonalInformation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isNavigating && e.CloseReason == CloseReason.UserClosing)
            {
                AraYuz araYuz = new AraYuz(user);
                araYuz.Show();
            }
        }
        private void menubtn_Click(object sender, EventArgs e)
        {
            sidebartimer.Start();
        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            int index = users.FindIndex(x => x.Id == user.Id);
            if (index == -1)
            {
                MessageBox.Show("Bir hata oluştu!");
            }
            users[index].Email = emailtxt.Text;
            users[index].Password = passwordtxt.Text;
            users[index].Surname = surnametxt.Text;
            users[index].Address = adresstxt.Text;
            users[index].Name = nametxt.Text;
            users[index].PhoneNumber = phonenumbertxt.Text;
            csvOperations.WriteUsers(users);
        }
        private bool ctrlKeyPressed = false;
        private void PersonalInformation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                ctrlKeyPressed = true;
            }
            else if (ctrlKeyPressed && e.KeyCode == Keys.Z)
            {
                var memento = _caretaker.Undo();
                RestoreState(memento);
                ctrlKeyPressed = false;
            }
            else if (ctrlKeyPressed && e.KeyCode == Keys.Y)
            {
                var memento = _caretaker.Redo();
                RestoreState(memento);
                ctrlKeyPressed = false;
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            SaveState();
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
            this.Close();
        }   

        private void personalinfobtn_Click(object sender, EventArgs e)
        {
            NavigateToForm(new PersonalInformation(user));
        }

        private void phonebookbtn_Click(object sender, EventArgs e)
        {
            NavigateToForm(new PhoneBook(user));
        }

        private void notesbtn_Click(object sender, EventArgs e)
        {
            NavigateToForm(new Notes(user));
        }

        private void salarycalcbtn_Click(object sender, EventArgs e)
        {
            NavigateToForm(new SalaryCalculator(user));
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
            homebtn.BackColor = Color.FromArgb(56, 94, 138);
        }

        private void homebtn_MouseLeave(object sender, EventArgs e)
        {
            homebtn.BackColor = Color.SteelBlue;
        }

        private void personalinfobtn_MouseEnter(object sender, EventArgs e)
        {
            personalinfobtn.BackColor = Color.FromArgb(56, 94, 138);
        }

        private void personalinfobtn_MouseLeave(object sender, EventArgs e)
        {
            personalinfobtn.BackColor = Color.SteelBlue;
        }

        private void phonebookbtn_MouseEnter(object sender, EventArgs e)
        {
            phonebookbtn.BackColor = Color.FromArgb(56, 94, 138);
        }

        private void phonebookbtn_MouseLeave(object sender, EventArgs e)
        {
            phonebookbtn.BackColor = Color.SteelBlue;
        }

        private void notesbtn_MouseEnter(object sender, EventArgs e)
        {
            notesbtn.BackColor = Color.FromArgb(56, 94, 138);
        }

        private void notesbtn_MouseLeave(object sender, EventArgs e)
        {
            notesbtn.BackColor = Color.SteelBlue;
        }

        private void salarycalcbtn_MouseEnter(object sender, EventArgs e)
        {
            salarycalcbtn.BackColor = Color.FromArgb(56, 94, 138);
        }

        private void salarycalcbtn_MouseLeave(object sender, EventArgs e)
        {
            salarycalcbtn.BackColor = Color.SteelBlue;
        }

        private void reminderbtn_MouseEnter(object sender, EventArgs e)
        {
            reminderbtn.BackColor = Color.FromArgb(56, 94, 138);
        }

        private void reminderbtn_MouseLeave(object sender, EventArgs e)
        {
            reminderbtn.BackColor = Color.SteelBlue;
        }

        private void usermanagmentbtn_MouseEnter(object sender, EventArgs e)
        {
            usermanagmentbtn.BackColor = Color.FromArgb(56, 94, 138);
        }

        private void usermanagmentbtn_MouseLeave(object sender, EventArgs e)
        {
            usermanagmentbtn.BackColor = Color.SteelBlue;
        }

        private void logoutbtn_MouseEnter(object sender, EventArgs e)
        {
            logoutbtn.BackColor = Color.FromArgb(56, 94, 138);
        }

        private void logoutbtn_MouseLeave(object sender, EventArgs e)
        {
            logoutbtn.BackColor = Color.SteelBlue;
        }
    }
}
