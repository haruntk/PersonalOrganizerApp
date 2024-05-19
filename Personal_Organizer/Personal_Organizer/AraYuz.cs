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
    public partial class AraYuz : Form
    {
        public AraYuz()
        {
            InitializeComponent();
        }

        private void AraYuz_Load(object sender, EventArgs e)
        {
            
        }

        private void btnPersonal_Click(object sender, EventArgs e)
        {
            PersonalInformation personalInformation = new PersonalInformation();
            personalInformation.Show();
            this.Hide();
        }

        private void AraYuz_FormClosing(object sender, FormClosingEventArgs e)
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

        private void btnReminder_Click(object sender, EventArgs e)
        {
            Reminder reminder = new Reminder();
            reminder.ShowDialog();
            this.Hide();
        }
    }
}
