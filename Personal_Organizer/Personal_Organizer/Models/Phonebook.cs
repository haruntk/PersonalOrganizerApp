using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Organizer.Models
{
    public class Phonebook
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public override string ToString()
        {
            return $"Name: {Name}, Surname: {Surname}, Phone Number: {PhoneNumber}, Address: {Address}, Description: {Description}, Email: {Email}";
        }
    }
}
