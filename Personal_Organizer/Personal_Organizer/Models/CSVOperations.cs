using System;
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


namespace Personal_Organizer.Models
{
    public class CSVOperations
    {
        private string FilePath = ConfigurationManager.AppSettings["DataPath"];
        private string NotesFilePath = ConfigurationManager.AppSettings["NotesDataPath"];
        private string PhoneBookDataPath = ConfigurationManager.AppSettings["PhoneBookDataPath"];
        private string ReminderDataPath = ConfigurationManager.AppSettings["ReminderDataPath"];
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
                    Name = values[1],
                    Surname = values[2],
                    Password = values[3],
                    Email = values[4],
                    Role = (Roles)Enum.Parse(typeof(Roles), values[5]),
                    PhoneNumber = values[6],
                    Address = values[7],
                    PhotoPath = null,
                };
                users.Add(user);
                string photoBase64 = values[8];

                if (!string.IsNullOrEmpty(photoBase64))
                {
                    byte[] photoBytes = Convert.FromBase64String(photoBase64);
                    user.PhotoPath = Path.Combine("photos", $"{user.Id}_{user.Name}.jpg");
                    File.WriteAllBytes(user.PhotoPath, photoBytes);
                }
            }

            return users;
        }


        public void WriteUsers(List<User> users)
        {
            var lines = new List<string> { "Id,Name,Surname,Password,Email,Role,PhoneNumber,Address,Photo" };
            lines.AddRange(users.Select(u =>
            {
                string photoBase64 = string.Empty;
                if (!string.IsNullOrEmpty(u.PhotoPath) && File.Exists(u.PhotoPath))
                {
                    byte[] photoBytes = File.ReadAllBytes(u.PhotoPath);
                    photoBase64 = Convert.ToBase64String(photoBytes);
                }
                return $"{u.Id},{u.Name},{u.Surname},{u.Password},{u.Email},{u.Role},{u.PhoneNumber},{u.Address},{photoBase64}";
            }));
            File.WriteAllLines(FilePath, lines);
        }

        // Notes CSV Operations
        public List<Note> ReadNotes()
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
                        Date = DateTime.ParseExact(fields[1], "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
                        Text = fields[2]
                    };

                    notes.Add(note);
                }
            }

            return notes;
        }

        public void WriteNote(Note note)
        {
            using (var writer = new StreamWriter(NotesFilePath, append: true))
            {
                var line = $"{note.UserID},{note.Date.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture)},{note.Text}";
                writer.WriteLine(line);
            }
        }

        public void DeleteNote(Note noteToDelete)
        {
            var notes = ReadNotes();
            var updatedNotes = new List<Note>();

            foreach (var note in notes)
            {
                if (note.Date != noteToDelete.Date)
                {
                    updatedNotes.Add(note);
                }
            }

            // Rewrite the CSV file with the updated list of notes
            using (var writer = new StreamWriter(NotesFilePath, false))
            {
                writer.WriteLine("UserID,Date,Text"); // Rewrite the header line

                foreach (var note in updatedNotes)
                {
                    var line = $"{note.UserID},{note.Date.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture)},{note.Text}";
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
                        UserId = 1
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
            var lines = new List<string> { "UserID,Date,Time,Title,Summary,Description,Type" };
            lines.AddRange(reminders.Select(r => $"{r.UserID},{r.Date},{r.Time},{r.Title},{r.Summary},{r.Description},{r.GetType().Name}"));
            File.WriteAllLines(ReminderDataPath,lines);
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
                    IReminder reminder = TaskFactory.CreateReminder(DateTime.ParseExact(values[1], "dd.MM.yyyy HH:mm:ss",null).Date,
                        TimeSpan.Parse(values[2]),
                        values[3],
                        values[4],
                        values[5]);
                    reminders.Add(reminder);
                }
                else
                {
                    IReminder reminder = MeetingFactory.CreateReminder(DateTime.ParseExact(values[1], "dd.MM.yyyy HH:mm:ss", null).Date,
                     TimeSpan.Parse(values[2]),
                     values[3],
                     values[4],
                     values[5]);
                 reminders.Add(reminder);
                }
            }

            return reminders;
        }

    }

}
