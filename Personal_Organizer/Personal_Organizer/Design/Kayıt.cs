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
    public partial class Kayıt : Form
    {
        private List<User> users = new List<User>();
        readonly CSVOperations csvOperations = new CSVOperations();
        public Kayıt()
        {
            InitializeComponent();
            users = csvOperations.ReadAllUsers();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Giris giris = new Giris();
            int lastId = 0;
            if (users.Count > 0 )
                lastId = users[users.Count - 1].Id;
            User user = new User()
            {
                Id = lastId + 1,
                Name = nametxt.Text,
                Surname = surnametxt.Text,
                Password = passwordtxt.Text,
                Email = emailtxt.Text,
                PhoneNumber = phonenumbertxt.Text,
                Address = adresstxt.Text,
                PhotoPath = ""
            };
            if (user.Id == 0)
                user.Role = Roles.Admin;
            users.Add(user);
            csvOperations.WriteUsers(users);
            giris.Show();
            this.Close();
        }
    }
}
