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
    public partial class Reminder : Form
    {
        public Reminder()
        {
            InitializeComponent();
        }

        private void homebtn_MouseLeave(object sender, EventArgs e)
        {
            homebtn.BackColor = Color.Firebrick;
        }

        private void homebtn_MouseEnter(object sender, EventArgs e)
        {
            homebtn.BackColor = Color.DarkRed;
        }

        private void personalinfobtn_MouseEnter(object sender, EventArgs e)
        {
            personalinfobtn.BackColor = Color.DarkRed;
        }

        private void personalinfobtn_MouseLeave(object sender, EventArgs e)
        {
            personalinfobtn.BackColor = Color.Firebrick;
        }

        private void phonebookbtn_MouseEnter(object sender, EventArgs e)
        {
            phonebookbtn.BackColor = Color.DarkRed;
        }

        private void phonebookbtn_MouseLeave(object sender, EventArgs e)
        {
            phonebookbtn.BackColor = Color.Firebrick;
        }

        private void notesbtn_MouseEnter(object sender, EventArgs e)
        {
            notesbtn.BackColor = Color.DarkRed;
        }

        private void notesbtn_MouseLeave(object sender, EventArgs e)
        {
            notesbtn.BackColor = Color.Firebrick;
        }

        private void salarycalcbtn_MouseEnter(object sender, EventArgs e)
        {
            salarycalcbtn.BackColor = Color.DarkRed;
        }

        private void salarycalcbtn_MouseLeave(object sender, EventArgs e)
        {
            salarycalcbtn.BackColor = Color.Firebrick;
        }

        private void reminderbtn_MouseEnter(object sender, EventArgs e)
        {
            reminderbtn.BackColor = Color.DarkRed;
        }

        private void reminderbtn_MouseLeave(object sender, EventArgs e)
        {
            reminderbtn.BackColor = Color.Firebrick;
        }

        private void usermanagmentbtn_MouseEnter(object sender, EventArgs e)
        {
            usermanagmentbtn.BackColor = Color.DarkRed;
        }

        private void usermanagmentbtn_MouseLeave(object sender, EventArgs e)
        {
            usermanagmentbtn.BackColor = Color.Firebrick;
        }

        private void logoutbtn_MouseEnter(object sender, EventArgs e)
        {
            logoutbtn.BackColor = Color.DarkRed;
        }

        private void logoutbtn_MouseLeave(object sender, EventArgs e)
        {
            logoutbtn.BackColor = Color.Firebrick;
        }

        private void homebtn_Click(object sender, EventArgs e)
        {

        }
    }
}
