using Personal_Organizer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Organizer.Models
{
    public interface IReminder : ISubject
    {
        int UserID { get; set; }
        DateTime Date { get; set;}
        TimeSpan Time { get;set;}
        string Title { get; set; }
        string Summary { get; set; }
        string Description { get; set; }
        bool IsTriggered { get; set; }
    }
}
