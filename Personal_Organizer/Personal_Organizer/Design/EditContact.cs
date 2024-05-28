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
        public EditContact(DataGridView dataGridView, ref List<Phonebook> phonebooks)
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
            gridView.DataSource = phonebookList;
            _csvOperations.WritePhonebook(phonebookList);
            this.Hide();
        }
    }
}
