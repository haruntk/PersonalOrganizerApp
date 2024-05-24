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
    public partial class Notes : Form
    {
        public Notes()
        {
            InitializeComponent();
         }

        private void Notes_FormClosing(object sender, FormClosingEventArgs e)
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
        private void homebtn_MouseLeave(object sender, EventArgs e)
        {
            homebtn.BackColor = Color.Gold;
        }

        private void homebtn_MouseEnter(object sender, EventArgs e)
        {
            homebtn.BackColor = Color.Goldenrod;
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

        private void reminderbtn_MouseEnter(object sender, EventArgs e)
        {
            reminderbtn.BackColor = Color.Goldenrod;
        }

        private void reminderbtn_MouseLeave(object sender, EventArgs e)
        {
            reminderbtn.BackColor = Color.Gold;
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


    }
}
