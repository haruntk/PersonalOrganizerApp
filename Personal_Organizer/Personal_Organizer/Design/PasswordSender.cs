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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Personal_Organizer.Design
{
    public partial class PasswordSender : Form
    {
        private const string EmailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        public PasswordSender()
        {
            InitializeComponent();
            progressBar1.Visible = false;
        }

        private bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, EmailPattern);
        }
        private void sendbtn_Click(object sender, EventArgs e)
        {
            if (!IsValidEmail(emailtxt.Text))
            {
                emailerrorlbl.Text = "Please enter a valid e-mail.";
                emailtxt.Focus();
            }
            else
            {
                emailerrorlbl.Text = "";
                progressBar1.Visible = true;
                emailtxt.Visible = false;
                label2.Visible = false;
                sendbtn.Visible = false;
            }
        }

    }
}
