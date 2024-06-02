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

        public PersonalInformation(User _user)
        {
            InitializeComponent();
            users = csvOperations.ReadAllUsers();
            user = _user;
            InitializeInitialState();
            AttachEventHandlers();
        }
        bool sidebarExpand;
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

        private void homebtn_Click(object sender, EventArgs e)
        {
            AraYuz araYuz = new AraYuz(user);
            araYuz.ShowDialog();
            this.Hide();
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
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
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

        private void PersonalInformation_Load(object sender, EventArgs e)
        {

        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            SaveState();
        }

    }
}
