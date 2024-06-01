using Personal_Organizer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Personal_Organizer
{
    public partial class Kayıt : Form
    {
        private List<User> users = new List<User>();
        readonly CSVOperations csvOperations = new CSVOperations();

        private const string EmailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        private const string PhoneNumberPattern = @"^0\s\(\d{3}\)\s\d{3}-\d{4}$";
        private const string NamePattern = "^[A-Za-zğüşıöçĞÜŞİÖÇ]+$";
        private bool isPasswordVisible = false;
        bool isValid = true;

        public Kayıt()
        {
            InitializeComponent();
            users = csvOperations.ReadAllUsers();
            phonenumbertxt.Mask = "0 (000) 000-0000";
            passwordtxt.PasswordChar = '*';
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, EmailPattern);
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, PhoneNumberPattern);
        }
        private bool IsValidName(string name)
        {
            return Regex.IsMatch(name, NamePattern);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Giris giris = new Giris();
            int lastId = users.Count > 0 ? users[users.Count - 1].Id : 0;

            User user = new User()
            {
                Id = lastId + 1,
                Username = usernametxt.Text,
                Name = nametxt.Text,
                Surname = surnametxt.Text,
                Password = passwordtxt.Text,
                Email = emailtxt.Text,
                PhoneNumber = phonenumbertxt.Text,
                Address = adresstxt.Text,
                PhotoPath = ""
            };

            if (user.Id == 1) // The first user should be an Admin
                user.Role = Roles.Admin;
            else
                user.Role = Roles.User;
            users.Add(user);
            csvOperations.WriteUsers(users);
            giris.Show();
            this.Close();
        }
        private void Kayıt_Load(object sender, EventArgs e)
        {
            nametxt.Focus();
        }

        private void nametxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                surnametxt.Focus();
                e.Handled = true;
            }
        }

        private void surnametxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                emailtxt.Focus();
                e.Handled = true;
            }
        }

        private void emailtxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                passwordtxt.Focus();
                e.Handled = true;
            }
        }


        private void passwordtxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                usernametxt.Focus();
                e.Handled = true;
            }
        }

        private void usernametxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                phonenumbertxt.Focus();
                e.Handled = true;
            }
        }

        private void phonenumbertxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                adresstxt.Focus();
                e.Handled = true;
            }
        }

        private void visiblitybtn_Click(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible; 

            if (isPasswordVisible)
            {
                passwordtxt.PasswordChar = '\0';
            }
            else
            {
                passwordtxt.PasswordChar = '*';
            }
        }

        private void emailtxt_Leave(object sender, EventArgs e)
        {
            if (!IsValidEmail(emailtxt.Text))
            {
                emailerrorlbl.Text = "Please enter a valid e-mail.";
                emailtxt.Focus();
                isValid = false;
            }
            else
            {
                emailerrorlbl.Text = "";
            }
        }

        private void phonenumbertxt_Leave(object sender, EventArgs e)
        {

            if (!IsValidPhoneNumber(phonenumbertxt.Text))
            {
                phonenumbererrorlbl.Text = "Please enter a valid phone number.";
                phonenumbererrorlbl.Focus();
                isValid = false;
            }
            else
            {
                phonenumbererrorlbl.Text = "";
            }

            if (!isValid)
            {
                return;
            }
        }

        private void nametxt_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nametxt.Text) || !IsValidName(nametxt.Text))
            {
                nameerrorlbl.Text = "Please enter a valid name.";
                nametxt.Focus();
                isValid = false;
            }
            else
            {
                nameerrorlbl.Text = "";
            }
        }

        private void surnametxt_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nametxt.Text) || !IsValidName(nametxt.Text))
            {
                nameerrorlbl.Text = "Please enter a valid surname.";
                nametxt.Focus();
                isValid = false;
            }
            else
            {
                nameerrorlbl.Text = "";
            }
        }
    }
}
