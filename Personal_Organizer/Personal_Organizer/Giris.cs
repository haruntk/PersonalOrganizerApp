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
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            AraYuz araYuz = new AraYuz();
            araYuz.Show();
            this.Hide();

        }

        private void Giris_FormClosing(object sender, FormClosingEventArgs e)
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Kayıt kayıt = new Kayıt();
            kayıt.Show();
            this.Hide();

        }
    }
}
