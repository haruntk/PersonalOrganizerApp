using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Organizer.Models
{
    public class ProfileMemento // Gerçek nesnemizin istediğimiz alanlarını tutan sınıf
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Address { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }

        public ProfileMemento(string name, string surname, string phoneNumber, string address, string password, string email)
        {
            Name = name;
            Surname = surname;
            PhoneNumber = phoneNumber;
            Address = address;
            Password = password;
            Email = email;
        }
    }   
}
