using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Personal_Organizer
{
    public partial class AddReminder : Form
    {
        public AddReminder()
        {
            InitializeComponent();
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void AddReminder_Load(object sender, EventArgs e)
        {
            addbtn.Enabled = false;
        }

        private void CheckFields()
        {
            // Alanların doluluğunu kontrol et
            bool fieldsFilled = titletextbox.Text.Length > 0 && descriptiontxtbox.Text.Length > 0 &&
            summarytextbox.Text.Length > 0 && (meetingradiobtn.Checked || taskradiobtn.Checked); ;
            addbtn.Enabled = fieldsFilled;
        }
        private void titletextbox_TextChanged(object sender, EventArgs e)
        {
            CheckFields();
        }

        private void descriptiontxtbox_TextChanged(object sender, EventArgs e)
        {
            CheckFields();
        }

        private void summarytextbox_TextChanged(object sender, EventArgs e)
        {
            CheckFields();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            CheckFields();
        }

        private void meetingradiobtn_CheckedChanged(object sender, EventArgs e)
        {
            CheckFields();
        }

        private void taskradiobtn_CheckedChanged(object sender, EventArgs e)
        {
            CheckFields();
        }
    }
}
