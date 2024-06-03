using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
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
        public DateTime ReminderDate { get; set; }
        public TimeSpan ReminderTime { get; set; }
        public string Summary { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ReminderType { get; set; }
        public AddReminder()
        {
            InitializeComponent();
            addbtn.Enabled =false;
            timePicker.Format = DateTimePickerFormat.Custom;
            timePicker.CustomFormat = "HH:mm";
        }

        private void CheckFields()
        {
            // doluluk kontrol etme
            bool fieldsFilled = titletextbox.Text.Length > 0 &&
                                descriptiontextbox.Text.Length > 0 &&
                                summarytextbox.Text.Length > 0 &&
            datePicker.Value != null &&
            (meetingradiobtn.Checked || taskradiobtn.Checked); 
            addbtn.Enabled = fieldsFilled;
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            ReminderDate = datePicker.Value.Date;
            ReminderTime = timePicker.Value.TimeOfDay;
            Summary = summarytextbox.Text;
            Title = titletextbox.Text;
            Description = descriptiontextbox.Text;
            if (meetingradiobtn.Checked)
            {
                ReminderType = "meeting";
            }
            else
                ReminderType = "task";
            this.DialogResult = DialogResult.OK;
            this.Close();
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

        private void titletextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                descriptiontextbox.Focus();
                e.Handled = true;
            }
        }

        private void descriptiontextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                summarytextbox.Focus();
                e.Handled = true;
            }
        }

        private void summarytextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                datePicker.Focus();
                e.Handled = true;
            }
        }
    }
}
