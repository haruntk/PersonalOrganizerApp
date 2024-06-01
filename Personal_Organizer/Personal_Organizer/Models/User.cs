using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Organizer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Roles Role { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PhotoPath { get; set; }

        public double Salary { get; set; }
        public ProfileMemento CreateMemento() // Originator Class (Saklamak istediğimiz gerçek nesne)
        {
            return new ProfileMemento(Name, Surname, PhoneNumber, Address, Password, Email);
        }

        public void RestoreMemento(ProfileMemento memento)
        {
            Name = memento.Name;
            Surname = memento.Surname;
            PhoneNumber = memento.PhoneNumber;
            Address = memento.Address;
            Password = memento.Password;
            Email = memento.Email;
        }

    }
    public enum Roles
    {
        Admin = 1,
        User,
        Part_Time_User
    }
}
