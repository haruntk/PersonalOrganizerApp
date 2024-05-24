using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Organizer.Models
{
    public class CSVOperations
    {
        //FilePath'i kendinizin path'ini yazın.
        private string FilePath = @"C:\Users\harun_rvth\OneDrive\Desktop\repositories\PersonalOrganizerApp\Personal_Organizer\Personal_Organizer\Data\data.csv";
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
    }
}
