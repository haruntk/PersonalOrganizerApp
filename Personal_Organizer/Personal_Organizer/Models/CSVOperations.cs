﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Globalization;
using static System.Windows.Forms.LinkLabel;
using Personal_Organizer.Factories;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Windows.Forms;


namespace Personal_Organizer.Models
{
    public class CSVOperations
    {
        private string FilePath = ConfigurationManager.AppSettings["DataPath"];
        private string NotesFilePath = ConfigurationManager.AppSettings["NotesDataPath"];
        private string PhoneBookDataPath = ConfigurationManager.AppSettings["PhoneBookDataPath"];
        private string ReminderDataPath = ConfigurationManager.AppSettings["ReminderDataPath"];
        private string SalaryDataPath = ConfigurationManager.AppSettings["SalaryDataPath"];

        private TaskReminderFactory TaskFactory = new TaskReminderFactory();
        private MeetingReminderFactory MeetingFactory = new MeetingReminderFactory();

        public List<User> ReadAllUsers()
        {
            var users = new List<User>();
            var lines = File.ReadAllLines(FilePath);

            foreach (var line in lines.Skip(1)) // Skip the header line
            {
                var values = line.Split(',');
                User user = new User()
                {
                    Id = int.Parse(values[0]),
                    Username = values[1],
                    Name = values[2],
                    Surname = values[3],
                    Password = values[4],
                    Email = values[5],
                    Role = (Roles)Enum.Parse(typeof(Roles), values[6]),
                    PhoneNumber = values[7],
                    Address = values[8],
                    Salary = double.Parse(values[9]),
                    IsForgotten = bool.Parse(values[10]),
                    Base64Photo = values[11],

                };
                users.Add(user);
            }

            return users;
        }


        public void WriteUsers(List<User> users)
        {
            var lines = new List<string> { "Id,Username,Name,Surname,Password,Email,Role,PhoneNumber,Address,Salary,IsForgotten,Base64Photo" };
            lines.AddRange(users.Select(u =>
            {
                return $"{u.Id},{u.Username},{u.Name},{u.Surname},{u.Password},{u.Email},{u.Role},{u.PhoneNumber},{u.Address},{u.Salary},{u.IsForgotten},{u.Base64Photo}";

            }));
            File.WriteAllLines(FilePath, lines);
        }

        // Notes CSV Operations
        public List<Note> ReadNotes(int userId)
        {
            List<Note> notes = new List<Note>();

            using (var reader = new StreamReader(NotesFilePath))
            {
                string line;
                bool isFirstLine = true;

                while ((line = reader.ReadLine()) != null)
                {
                    if (isFirstLine)
                    {
                        isFirstLine = false;
                        continue; // Skip the header line
                    }

                    var fields = line.Split(',');
                    var note = new Note
                    {
                        UserID = int.Parse(fields[0]),
                        Header = fields[1],
                        Date = DateTime.ParseExact(fields[2], "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                        Text = fields[3]
                    };

                    if (note.UserID == userId)
                    {
                        notes.Add(note);
                    }
                }
            }

            return notes;
        }

        public void WriteNote(Note note)
        {
            using (var writer = new StreamWriter(NotesFilePath, append: true))
            {
                var line = $"{note.UserID},{note.Header},{note.Date.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture)},{note.Text}";
                writer.WriteLine(line);
            }
        }

        public void DeleteNote(Note noteToDelete, int userId)
        {
            var notes = new List<Note>();

            // Read the CSV file and load notes
            using (var reader = new StreamReader(NotesFilePath))
            {
                // Skip the header line
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var note = new Note
                    {
                        UserID = int.Parse(values[0]),
                        Header = values[1],
                        Date = DateTime.ParseExact(values[2], "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                        Text = values[3]
                    };

                    // Add to list if it doesn't match the note to be deleted
                    if (note.UserID != userId || note.Date != noteToDelete.Date || note.Header != noteToDelete.Header)
                    {
                        notes.Add(note);
                    }
                }
            }

            // Rewrite the CSV file with the updated list of notes
            using (var writer = new StreamWriter(NotesFilePath, false))
            {
                writer.WriteLine("UserID,Header,Date,Text"); // Rewrite the header line

                foreach (var note in notes)
                {
                    var line = $"{note.UserID},{note.Header},{note.Date.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture)},{note.Text}";
                    writer.WriteLine(line);
                }
            }
        }

        // PhoneBook CSV Operations
        public List<Phonebook> ReadPhoneBooks()
        {
            var phonebookList = new List<Phonebook>();
            using (var reader = new StreamReader(PhoneBookDataPath))
            {
                string headerLine = reader.ReadLine();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',');
                    var phonebookEntry = new Phonebook
                    {
                        Name = values[0],
                        Surname = values[1],
                        PhoneNumber = values[2],
                        Address = values[3],
                        Description = values[4],
                        Email = values[5],
                        UserId = int.Parse(values[6])
                    };
                    phonebookList.Add(phonebookEntry);
                }
            }
            return phonebookList;
        }
        public void WritePhonebook(List<Phonebook> phonebookList)
        {
            using (var writer = new StreamWriter(PhoneBookDataPath))
            {
                writer.WriteLine("Name,Surname,PhoneNumber,Address,Description,Email,UserId");
                foreach (var entry in phonebookList)
                {
                    var line = $"{entry.Name},{entry.Surname},{entry.PhoneNumber},{entry.Address},{entry.Description},{entry.Email},{entry.UserId}";
                    writer.WriteLine(line);
                }
            }
        }

        public void WriteRemindersToCsv(List<IReminder> reminders)
        {
            var lines = new List<string> { "ReminderID,UserID,Date,Time,Title,Summary,Description,Type,IsTriggered" };
            lines.AddRange(reminders.Select(r => $"{r.ReminderID},{r.UserID},{r.Date.ToString("dd.MM.yyyy")},{r.Time.ToString(@"hh\:mm")},{r.Title},{r.Summary},{r.Description},{r.GetType().Name},{r.IsTriggered}"));
            File.WriteAllLines(ReminderDataPath, lines);
        }

        public List<IReminder> ReadRemindersFromCsv()
        {
            var lines = File.ReadAllLines(ReminderDataPath).Skip(1); // Skip header line
            var reminders = new List<IReminder>();

            foreach (var line in lines)
            {
                var values = line.Split(',');
                if (values[6] == "TaskReminder")
                {
                    IReminder reminder = TaskFactory.CreateReminder(int.Parse(values[0]), int.Parse(values[1]),DateTime.ParseExact(values[2], "dd.MM.yyyy", null).Date,
                        TimeSpan.Parse(values[3]),
                        values[4],
                        values[5],
                        values[6],
                        bool.Parse(values[8]));
                    reminders.Add(reminder);
                }
                else
                {
                    IReminder reminder = MeetingFactory.CreateReminder(int.Parse(values[0]), int.Parse(values[1]), DateTime.ParseExact(values[2], "dd.MM.yyyy", null).Date,
                        TimeSpan.Parse(values[3]),
                        values[4],
                        values[5],
                        values[6], 
                        bool.Parse(values[8]));
                    reminders.Add(reminder);
                }
            }

            return reminders;
        }

        public void WriteSalary(Salary salary)
        {
            bool fileExists = File.Exists(SalaryDataPath);

            using (var writer = new StreamWriter(SalaryDataPath, true))
            {
                if (!fileExists)
                {
                    // Write header if file does not exist
                    writer.WriteLine("Id,GrossMinWage,Experience,Location,Education,Languages,ManagerialPosition,FamilyStatus,CoefficientSum,EngineerMinWage");
                }

                // Write salary record
                writer.WriteLine($"{salary.Id},{salary.GrossMinWage},{salary.Experience},{salary.Location},{salary.Education},{salary.Languages},{salary.ManagerialPosition},{salary.FamilyStatus},{salary.CoefficientSum},{salary.EngineerMinWage}");
            }
        }

        public Salary ReadSalary(int id)
        {
            if (!File.Exists(SalaryDataPath))
            {
                return null;
            }

            using (var reader = new StreamReader(SalaryDataPath))
            {
                string header = reader.ReadLine(); // Skip the header line

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (values.Length == 10 && int.Parse(values[0]) == id)
                    {
                        return new Salary
                        {
                            Id = int.Parse(values[0]),
                            GrossMinWage = double.Parse(values[1]),
                            Experience = values[2],
                            Location = values[3],
                            Education = values[4],
                            Languages = values[5],
                            ManagerialPosition = values[6],
                            FamilyStatus = values[7],
                            CoefficientSum = double.Parse(values[8]),
                            EngineerMinWage = double.Parse(values[9])
                        };
                    }
                }
            }

            return null;
        }

        public void UpdateSalary(Salary updatedSalary)
        {
            if (!File.Exists(SalaryDataPath))
            {
                MessageBox.Show("No salary data file found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var lines = new List<string>();
            bool recordFound = false;

            using (var reader = new StreamReader(SalaryDataPath))
            {
                string header = reader.ReadLine();
                lines.Add(header); // Keep the header

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (values.Length == 10 && int.Parse(values[0]) == updatedSalary.Id)
                    {
                        // Update the record
                        var updatedLine = $"{updatedSalary.Id},{updatedSalary.GrossMinWage},{updatedSalary.Experience},{updatedSalary.Location},{updatedSalary.Education},{updatedSalary.Languages},{updatedSalary.ManagerialPosition},{updatedSalary.FamilyStatus},{updatedSalary.CoefficientSum},{updatedSalary.EngineerMinWage}";
                        lines.Add(updatedLine);
                        recordFound = true;
                    }
                    else
                    {
                        lines.Add(line);
                    }
                }
            }

            if (!recordFound)
            {
                WriteSalary(updatedSalary);
                return;
                //MessageBox.Show($"No salary record found for ID {updatedSalary.Id}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
            }

            using (var writer = new StreamWriter(SalaryDataPath, false)) // Overwrite the file
            {
                foreach (var line in lines)
                {
                    writer.WriteLine(line);
                }
            }
        }
        public void UpdateUserSalary(int userId, double newSalary)
        {
            var users = ReadAllUsers();

            // Find the user to update
            var userToUpdate = users.FirstOrDefault(u => u.Id == userId);

            if (userToUpdate != null)
            {
                // Update the user's salary
                userToUpdate.Salary = newSalary;

                // Rewrite all users to the file
                WriteUsers(users);
            }
            else
            {
                Console.WriteLine($"User with ID {userId} not found.");
            }
        }

        public void UpdateUserIsForgotten(int userId, bool isForgotten)
        {
            var users = ReadAllUsers();

            var userToUpdate = users.FirstOrDefault(u => u.Id == userId);

            if (userToUpdate != null)
            {
                userToUpdate.IsForgotten = isForgotten;
                WriteUsers(users);
            }
            else
            {
                Console.WriteLine($"User with ID {userId} not found.");
            }
        }

        public void UpdateUserRole(int userId, Roles newRole)
        {
            var users = ReadAllUsers();

            var userToUpdate = users.FirstOrDefault(u => u.Id == userId);

            if (userToUpdate != null)
            {
                userToUpdate.Role = newRole;
                userToUpdate.IsForgotten = false;
                WriteUsers(users);
            }
            else
            {
                Console.WriteLine($"User with ID {userId} not found.");
            }
        }

        public void UpdateUserPassword(int userId, string password)
        {
            var users = ReadAllUsers();

            var userToUpdate = users.FirstOrDefault(u => u.Id == userId);

            if (userToUpdate != null)
            {
                userToUpdate.Password = password;
                userToUpdate.IsForgotten = false;
                WriteUsers(users);
            }
            else
            {
                Console.WriteLine($"User with ID {userId} not found.");
            }
        }


    }

}
