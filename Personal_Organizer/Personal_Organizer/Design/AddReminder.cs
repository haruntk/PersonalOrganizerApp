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

namespace Personal_Organizer.Design
{
    public partial class AddReminder : Form
    {

        public AddReminder()
        {
            InitializeComponent();
            addbtn.Enabled =false;
        }

        private void CheckFields()
        {
            // doluluk kontrol etme
            bool fieldsFilled = titletextbox.Text.Length > 0 &&
                                descriptiontextbox.Text.Length > 0 &&
                                summarytextbox.Text.Length > 0 &&
            dateTimePicker1.Value != null &&
            (meetingradiobtn.Checked || taskradiobtn.Checked); 
            addbtn.Enabled = fieldsFilled;
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void titletextbox_TextChanged(object sender, EventArgs e)
        {
            CheckFields();
        }

        private void descriptiontextbox_TextChanged(object sender, EventArgs e)
        {
            CheckFields();
        }

        private void summarytextbox_TextChanged(object sender, EventArgs e)
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
