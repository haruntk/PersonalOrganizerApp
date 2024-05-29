using Personal_Organizer.Models;
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
    public partial class UpdateReminder : Form
    {
        public IReminder reminder { get; }
        public UpdateReminder(IReminder _reminder)
        {
            reminder = _reminder;
            InitializeComponent();
            addbtn.Enabled =false;
            timePicker.Format = DateTimePickerFormat.Custom;
            timePicker.CustomFormat = "HH:mm";
            titletextbox.Text = _reminder.Title;
            summarytextbox.Text = _reminder.Summary;
            descriptiontextbox.Text = _reminder.Description;
            datePicker.Value = _reminder.Date;
            timePicker.Value = DateTime.Today.Add(_reminder.Time);
        }

        private void CheckFields()
        {
            // doluluk kontrol etme
            bool fieldsFilled = titletextbox.Text.Length > 0 &&
                                descriptiontextbox.Text.Length > 0 &&
                                summarytextbox.Text.Length > 0 &&
            datePicker.Value != null; 
            addbtn.Enabled = fieldsFilled;
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            reminder.Date = datePicker.Value.Date;
            reminder.Time = timePicker.Value.TimeOfDay;
            reminder.Summary = summarytextbox.Text;
            reminder.Title = titletextbox.Text;
            reminder.Description = descriptiontextbox.Text;
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
    }
}
