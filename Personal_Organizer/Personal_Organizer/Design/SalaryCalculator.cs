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

    public partial class SalaryCalculator : Form
    {
        User user;
        public SalaryCalculator(User _user)
        {
            InitializeComponent();
            InitializeComboBoxItems();
            LoadSalaryData();
            user = _user;
        }

        private void InitializeComboBoxItems()
        {
            // Initialize ExperienceListBox
            ExperienceBox.Items.Add(new ComboBoxItem("Experience Duration (Years) 2-4 - Coefficient: 0.60", 0.60));
            ExperienceBox.Items.Add(new ComboBoxItem("Experience Duration (Years) 5-9 - Coefficient: 1.00", 1.00));
            ExperienceBox.Items.Add(new ComboBoxItem("Experience Duration (Years) 10-14 - Coefficient: 1.20", 1.20));
            ExperienceBox.Items.Add(new ComboBoxItem("Experience Duration (Years) 15-20 - Coefficient: 1.35", 1.35));
            ExperienceBox.Items.Add(new ComboBoxItem("Experience Duration (Years) Over 20 - Coefficient: 1.50", 1.50));

            // Initialize LocationListBox
            LocationBox.Items.Add(new ComboBoxItem("TR10: Istanbul - Coefficient: 0.30", 0.30));
            LocationBox.Items.Add(new ComboBoxItem("TR51: Ankara - Coefficient: 0.20", 0.20));
            LocationBox.Items.Add(new ComboBoxItem("TR31: Izmir - Coefficient: 0.20", 0.20));
            LocationBox.Items.Add(new ComboBoxItem("TR42: Kocaeli/Sakarya/Duzce/Bolu/Yalova - Coefficient: 0.10", 0.10));
            LocationBox.Items.Add(new ComboBoxItem("TR21: Edirne/Kirklareli/Tekirdag - Coefficient: 0.10", 0.10));
            LocationBox.Items.Add(new ComboBoxItem("TR90: Trabzon/Ordu/Giresun/Rize/Artvin/Gumushane - Coefficient: 0.05", 0.05));
            LocationBox.Items.Add(new ComboBoxItem("TR41: Bursa/Eskisehir/Bilecik - Coefficient: 0.05", 0.05));
            LocationBox.Items.Add(new ComboBoxItem("TR32: Aydin/Denizli/Mugla - Coefficient: 0.05", 0.05));
            LocationBox.Items.Add(new ComboBoxItem("TR62: Adana/Mersin - Coefficient: 0.05", 0.05));
            LocationBox.Items.Add(new ComboBoxItem("TR22: Balikesir/Canakkale - Coefficient: 0.05", 0.05));
            LocationBox.Items.Add(new ComboBoxItem("TR61: Antalya/Isparta/Burdur - Coefficient: 0.05", 0.05));
            LocationBox.Items.Add(new ComboBoxItem("Other Cities - Coefficient: 0.00", 0.00));

            // Initialize HigherEducationListBox
            EducationBox.Items.Add(new ComboBoxItem("Academic Degree Related to Profession -> Master's - Coefficient: 0.10", 0.10));
            EducationBox.Items.Add(new ComboBoxItem("Academic Degree Related to Profession -> PhD - Coefficient: 0.30", 0.30));
            EducationBox.Items.Add(new ComboBoxItem("Academic Degree Related to Profession -> Associate Professor - Coefficient: 0.35", 0.35));
            EducationBox.Items.Add(new ComboBoxItem("Academic Degree Unrelated to Profession -> Master's, Coefficient - 0.05", 0.05));
            EducationBox.Items.Add(new ComboBoxItem("Academic Degree Unrelated to Profession -> PhD/Associate Professor - Coefficient: 0.15", 0.15));

            // Initialize LanguageListBox
            LanguagesBox.Items.Add(new ComboBoxItem("Documented English Proficiency - Coefficient: 0.20", 0.20));
            LanguagesBox.Items.Add(new ComboBoxItem("Graduation from English-School - Coefficient: 0.20", 0.20));
            LanguagesBox.Items.Add(new ComboBoxItem("Documented Other Foreign Language Proficiency (per language) - Coefficient: 0.05", 0.05));

            // Initialize ManagerialPositionListBox
            ManagerialPositionBox.Items.Add(new ComboBoxItem("Team Leader/Group Manager/Technical Manager/Software Architect - Coefficient: 0.50", 0.50));
            ManagerialPositionBox.Items.Add(new ComboBoxItem("Project Manager - Coefficient: 0.75", 0.75));
            ManagerialPositionBox.Items.Add(new ComboBoxItem("Director/Project Director - Coefficient: 0.85", 0.85));
            ManagerialPositionBox.Items.Add(new ComboBoxItem("CTO/General Manager - Coefficient: 1.00", 1.00));
            ManagerialPositionBox.Items.Add(new ComboBoxItem("IT Supervisor/Manager (if IT department has up to 5 personnel) - Coefficient: 0.40", 0.40));
            ManagerialPositionBox.Items.Add(new ComboBoxItem("IT Supervisor/Manager (if IT department has more than 5 personnel) - Coefficient: 0.60", 0.60));

            // Initialize FamilyBox
            FamilyBox.Items.Add(new ComboBoxItem("Married and spouse not working - Coefficient: 0.20", 0.20));
            FamilyBox.Items.Add(new ComboBoxItem("Children aged 0-6 - Coefficient: 0.20", 0.20));
            FamilyBox.Items.Add(new ComboBoxItem("Children aged 7-18 - Coefficient: 0.30", 0.30));
            FamilyBox.Items.Add(new ComboBoxItem("Children over 18 (Must be a university undergraduate or associate degree student) - Coefficient: 0.40", 0.40));

            RoleBox.Items.Add("User");
            RoleBox.Items.Add("Part-Time User");
            RoleBox.Items.Add("Admin");
        }

        public class ComboBoxItem
        {
            public string Description { get; set; }
            public double Coefficient { get; set; }

            public ComboBoxItem(string description, double coefficient)
            {
                Description = description;
                Coefficient = coefficient;
            }

            public override string ToString()
            {
                return $"{Description}";
            }
        }

        private void CalculateBtn_Click(object sender, EventArgs e)
        {
            // Check if any ComboBox is not selected
            if (ExperienceBox.SelectedItem == null || LocationBox.SelectedItem == null ||
                EducationBox.SelectedItem == null || LanguagesBox.SelectedItem == null ||
                ManagerialPositionBox.SelectedItem == null || FamilyBox.SelectedItem == null ||
                RoleBox.SelectedItem == null)
            {
                MessageBox.Show("Please select values from all ComboBoxes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if MinWageTxt is empty
            if (string.IsNullOrWhiteSpace(MinWageTxt.Text))
            {
                MessageBox.Show("Please enter the minimum wage.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if MinWageTxt contains only numbers
            if (!double.TryParse(MinWageTxt.Text, out double minWage))
            {
                MessageBox.Show("Please enter a valid minimum wage (only numbers).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Calculate sum of coefficients from selected ComboBox items
            double coefficientSums = GetCoefficientSums();

            // Display coefficient sum in CoefficientSumLbl
            CoefficientSumsLbl.Text = coefficientSums.ToString();

            // Calculate engineer wage
            double engineerWage = (minWage * 2) * (1 + coefficientSums);

            // Will be change when the login session is completed.
            if (RoleBox.SelectedItem.ToString() == "Part-Time User")
            {
                engineerWage /= 2;
            }


            // Display engineer wage
            EngineerWageLbl.Text = engineerWage.ToString();

            SalaryUpdate(minWage, coefficientSums, engineerWage);
        }

        private double GetCoefficientSums()
        {
            double sum = 0.0;

            foreach (var comboBox in Controls.OfType<ComboBox>())
            {
                if (comboBox.SelectedItem != null && comboBox.SelectedItem is ComboBoxItem item)
                {
                    sum += item.Coefficient;
                }
            }

            return sum;
        }



        private void SalaryUpdate(double minWage, double coefficientSums, double engineerWage)
        {
            var csvOperations = new CSVOperations();
            var updatedSalary = new Salary
            {
                Id = 1, // Assuming the ID is 1 for now, this should be dynamic based on your requirements.
                GrossMinWage = minWage,
                Experience = ExperienceBox.SelectedItem.ToString(),
                Location = LocationBox.SelectedItem.ToString(),
                Education = EducationBox.SelectedItem.ToString(),
                Languages = LanguagesBox.SelectedItem.ToString(),
                ManagerialPosition = ManagerialPositionBox.SelectedItem.ToString(),
                FamilyStatus = FamilyBox.SelectedItem.ToString(),
                CoefficientSum = coefficientSums,
                EngineerMinWage = engineerWage,
            };

            csvOperations.UpdateSalary(updatedSalary);
        }

        private void LoadSalaryData()
        {
            var csvOperations = new CSVOperations();
            Salary salary = csvOperations.ReadSalary(1); // Assuming we are loading the salary record with ID 1

            if (salary != null)
            {
                // Set the controls based on the loaded data
                MinWageTxt.Text = salary.GrossMinWage.ToString();
                ExperienceBox.SelectedItem = ExperienceBox.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Description.StartsWith(salary.Experience));
                LocationBox.SelectedItem = LocationBox.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Description.StartsWith(salary.Location));
                EducationBox.SelectedItem = EducationBox.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Description.StartsWith(salary.Education));
                LanguagesBox.SelectedItem = LanguagesBox.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Description.StartsWith(salary.Languages));
                ManagerialPositionBox.SelectedItem = ManagerialPositionBox.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Description.StartsWith(salary.ManagerialPosition));
                FamilyBox.SelectedItem = FamilyBox.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Description.StartsWith(salary.FamilyStatus));
                CoefficientSumsLbl.Text = salary.CoefficientSum.ToString();
                EngineerWageLbl.Text = salary.EngineerMinWage.ToString();
            }
        }

        private void homebtn_Click(object sender, EventArgs e)
        {
            AraYuz araYuz = new AraYuz(user);
            araYuz.ShowDialog();
            this.Close();
        }

        private void personalinfobtn_Click(object sender, EventArgs e)
        {
            PersonalInformation personalInformation = new PersonalInformation(user);
            personalInformation.ShowDialog();
            this.Close();
        }

        private void phonebookbtn_Click(object sender, EventArgs e)
        {
            PhoneBook phoneBook = new PhoneBook(user);
            phoneBook.ShowDialog();
            this.Close();
        }

        private void notesbtn_Click(object sender, EventArgs e)
        {
            Notes notes = new Notes(user);
            notes.ShowDialog();
            this.Close();
        }

        private void reminderbtn_Click(object sender, EventArgs e)
        {
            Reminder reminder = new Reminder(user);
            reminder.ShowDialog();
            this.Close();
        }

        private void usermanagmentbtn_Click(object sender, EventArgs e)
        {
            UserManagament userManagament = new UserManagament();
            userManagament.ShowDialog();
            this.Close();
        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            AraYuz araYuz = new AraYuz(user);
            araYuz.ShowDialog();
            this.Close();
        }

       
    }
}