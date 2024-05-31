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
    public partial class PersonalInformation : Form
    {
        User user;
        public PersonalInformation(User _user)
        {
            InitializeComponent();
            user = _user;
        }
        bool sidebarExpand;

        private void PersonalInformation_Load(object sender, EventArgs e)
        {

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
            if(sidebarExpand)
            {
                sidebarflowLayoutPanel.Width -= 10;
                if(sidebarflowLayoutPanel.Width == sidebarflowLayoutPanel.MinimumSize.Width)
                {
                    sidebarExpand = false;
                    sidebartimer.Stop();
                }
            }
            else
            {
                sidebarflowLayoutPanel.Width += 10;
                if(sidebarflowLayoutPanel.Width == sidebarflowLayoutPanel.MaximumSize.Width)
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
    }
}
