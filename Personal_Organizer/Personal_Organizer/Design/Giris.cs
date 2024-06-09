using Personal_Organizer.Design;
using Personal_Organizer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Personal_Organizer
{
    public partial class Giris : Form
    {
        readonly CSVOperations csvOperations = new CSVOperations();
        List<User> users = new List<User>();
        private bool isPasswordVisible = false;
        private bool isLoginSuccessful = false;
        public Giris()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            users = csvOperations.ReadAllUsers();
            passwordtxt.PasswordChar = '*';

        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            bool validated = false;
            users = csvOperations.ReadAllUsers();
            foreach (User user in users)
            {
                if (user.Username == usertxt.Text && user.Password == passwordtxt.Text)
                {
                    AraYuz araYuz = new AraYuz(user);
                    araYuz.Show();
                    isLoginSuccessful = true;

                    Thread thread = new Thread(() =>
                    {
                        Application.Run(new AraYuz(user));
                    });
                    thread.Start();


                    this.Close();
                    validated = true;
                   
                }
            }
                if (!validated)
                    MessageBox.Show("Invalid username or password. Please check and try again.", "Invalid Credentials");
        }

        private void Giris_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isLoginSuccessful && e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Kayıt kayıt = new Kayıt();
            kayıt.Show();

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

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PasswordSender passwordSender = new PasswordSender();
            passwordSender.Show();
        }

        private void usertxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                passwordtxt.Focus();
                e.Handled = true;
            }
        }
    }
}
