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

namespace Personal_Organizer.Design
{
    public partial class EditContact : Form
    {
        private List<Phonebook> phonebookList;
        private CSVOperations _csvOperations;
        private DataGridView gridView;
        private Phonebook selectedData;
        private User user;
        public EditContact(DataGridView dataGridView, ref List<Phonebook> phonebooks, User _user)
        {
            InitializeComponent();
            _csvOperations = new CSVOperations();
            gridView = dataGridView;
            phonebookList = phonebooks;
            selectedData = gridView.SelectedRows[0].DataBoundItem as Phonebook;
            txtBoxAddress.Text = selectedData.Address;
            txtBoxDescription.Text = selectedData.Description;
            txtBoxEmail.Text = selectedData.Email;
            txtBoxName.Text = selectedData.Name;
            txtBoxNumber.Text = selectedData.PhoneNumber;
            txtBoxSurname.Text = selectedData.Surname;
            phonebooks.Remove(selectedData);
            user = _user;
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            selectedData.Name = txtBoxName.Text;
            selectedData.PhoneNumber = txtBoxNumber.Text;
            selectedData.Email = txtBoxEmail.Text;
            selectedData.Surname = txtBoxSurname.Text;
            selectedData.Address = txtBoxAddress.Text;
            selectedData.Description = txtBoxDescription.Text;

            phonebookList.Add(selectedData);
            
            gridView.DataSource = null;
            var userBooks = phonebookList.Where(x => x.UserId == user.Id).ToList();
            gridView.DataSource = userBooks;
            _csvOperations.WritePhonebook(phonebookList);
            this.Hide();
        }
    }
}
