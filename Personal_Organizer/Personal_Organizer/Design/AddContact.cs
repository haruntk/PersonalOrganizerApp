using Personal_Organizer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personal_Organizer.Design
{
    public partial class AddContact : Form
    {
        private List<Phonebook> phonebookList;
        private CSVOperations _csvOperations;
        private DataGridView gridView;
        private User user;
        public AddContact(DataGridView dataGrid, ref List<Phonebook> phonebooks, User _user)
        {
            InitializeComponent();
            _csvOperations = new CSVOperations();
            gridView = dataGrid;
            phonebookList = phonebooks;
            user = _user;
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            if (!IsValidEmail(txtBoxEmail.Text))
            {
                MessageBox.Show("Geçersiz Email!");
                return;
            }
            if (!IsValidPhoneNumber(txtBoxNumber.Text))
            {
                MessageBox.Show("Geçersiz Telefon Numarası!");
                return;
            }
            phonebookList.Add(new Phonebook()
            {
                UserId = user.Id,
                Name = txtBoxName.Text,
                Surname = txtBoxSurname.Text,
                Address = txtBoxAddress.Text,
                Description = txtBoxDescription.Text,
                Email = txtBoxEmail.Text,
                PhoneNumber = txtBoxNumber.Text,
            });
            _csvOperations.WritePhonebook(phonebookList);
            gridView.DataSource = null;
            gridView.DataSource = phonebookList;
            this.Hide();
        }
        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            string pattern = @"^(\+90|0)?\s*(\(\d{3}\)[\s-]*\d{3}[\s-]*\d{2}[\s-]*\d{2}|\(\d{3}\)[\s-]*\d{3}[\s-]*\d{4}|\(\d{3}\)[\s-]*\d{7}|\d{3}[\s-]*\d{3}[\s-]*\d{4}|\d{3}[\s-]*\d{3}[\s-]*\d{2}[\s-]*\d{2})$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(phoneNumber);
        }
    }
}
