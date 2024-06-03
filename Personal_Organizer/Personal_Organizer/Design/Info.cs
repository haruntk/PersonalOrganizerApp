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
using System.Diagnostics;
using System.IO;

namespace Personal_Organizer.Design
{
    public partial class Info : Form
    {
        User user;
        bool sidebarExpand;
        private bool isNavigating = false;
        private List<IReminder> reminders = new List<IReminder>();
        public Info(User _user, List<IReminder> _reminders)
        {
            user = _user;
            InitializeComponent();
            if (user.Role != Roles.Admin)
            {
                usermanagmentbtn.Visible = false;
            }
            this.StartPosition = FormStartPosition.CenterScreen;
            this.TopMost = true;
            label10.Text = user.Username;
            byte[] imageBytes = Convert.FromBase64String(user.Base64Photo);
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                circularPicture2.Image = Image.FromStream(ms);
            }
            this.WindowState = FormWindowState.Normal;
            this.Show();

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
            NavigateToForm(new PersonalInformation(user, reminders));
        }

        private void phonebookbtn_Click(object sender, EventArgs e)
        {
            NavigateToForm(new PhoneBook(user, reminders));
        }

        private void notesbtn_Click(object sender, EventArgs e)
        {
            NavigateToForm(new Notes(user,reminders));
        }

        private void salarycalcbtn_Click(object sender, EventArgs e)
        {
            NavigateToForm(new SalaryCalculator(user, reminders));
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

        private void menubtn_Click(object sender, EventArgs e)
        {
            sidebartimer.Start();
        }

        private void homebtn_MouseEnter(object sender, EventArgs e)
        {
            homebtn.BackColor = Color.FromArgb(175, 43, 86);
        }

        private void homebtn_MouseLeave(object sender, EventArgs e)
        {
            homebtn.BackColor = Color.PaleVioletRed;
        }

        private void personalinfobtn_MouseEnter(object sender, EventArgs e)
        {
            personalinfobtn.BackColor = Color.FromArgb(175, 43, 86);
        }

        private void personalinfobtn_MouseLeave(object sender, EventArgs e)
        {
            personalinfobtn.BackColor = Color.PaleVioletRed;
        }

        private void phonebookbtn_MouseEnter(object sender, EventArgs e)
        {
            phonebookbtn.BackColor = Color.FromArgb(175, 43, 86);
        }

        private void phonebookbtn_MouseLeave(object sender, EventArgs e)
        {
            phonebookbtn.BackColor = Color.PaleVioletRed;
        }

        private void notesbtn_MouseEnter(object sender, EventArgs e)
        {
            notesbtn.BackColor = Color.FromArgb(175, 43, 86);
        }

        private void notesbtn_MouseLeave(object sender, EventArgs e)
        {
            notesbtn.BackColor = Color.PaleVioletRed;
        }

        private void salarycalcbtn_MouseEnter(object sender, EventArgs e)
        {
            salarycalcbtn.BackColor = Color.FromArgb(175, 43, 86);
        }

        private void salarycalcbtn_MouseLeave(object sender, EventArgs e)
        {
            salarycalcbtn.BackColor = Color.PaleVioletRed;
        }

        private void reminderbtn_MouseEnter(object sender, EventArgs e)
        {
            reminderbtn.BackColor = Color.FromArgb(175, 43, 86);
        }

        private void reminderbtn_MouseLeave(object sender, EventArgs e)
        {
            reminderbtn.BackColor = Color.PaleVioletRed;
        }

        private void usermanagmentbtn_MouseEnter(object sender, EventArgs e)
        {
            usermanagmentbtn.BackColor = Color.FromArgb(175, 43, 86);
        }

        private void usermanagmentbtn_MouseLeave(object sender, EventArgs e)
        {
            usermanagmentbtn.BackColor = Color.PaleVioletRed;
        }

        private void logoutbtn_MouseEnter(object sender, EventArgs e)
        {
            logoutbtn.BackColor = Color.FromArgb(175, 43, 86);
        }

        private void logoutbtn_MouseLeave(object sender, EventArgs e)
        {
            logoutbtn.BackColor = Color.PaleVioletRed;
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
        private void OpenLink(string url)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        private void LinkLabel10_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenLink("https://www.linkedin.com/in/alperen-g%C3%BCltekin-4aa514205?utm_source=share&utm_campaign=share_via&utm_content=profile&utm_medium=ios_app");
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenLink("https://github.com/CVE-2002-1215");
 
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenLink("https://www.linkedin.com/in/arda-%C3%A7abuk-8355b2253?utm_source=share&utm_campaign=share_via&utm_content=profile&utm_medium=ios_app ");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenLink("https://github.com/ardacbk");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenLink("https://www.linkedin.com/in/ceren-ad%C4%B1yaman/");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenLink("https://github.com/CerenAdiyaman");
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenLink("https://www.linkedin.com/in/gamzedag/");
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenLink("https://github.com/gmzdag");
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenLink("https://www.linkedin.com/in/harun-taha-kepenek-521722258?utm_source=share&utm_campaign=share_via&utm_content=profile&utm_medium=ios_app ");
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenLink("https://github.com/haruntk");
        }

        private void Info_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isNavigating && e.CloseReason == CloseReason.UserClosing)
            {
                AraYuz araYuz = new AraYuz(user);
                araYuz.Show();
            }
        }
    }
}
