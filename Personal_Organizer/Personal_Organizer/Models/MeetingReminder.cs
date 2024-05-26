using Personal_Organizer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Organizer.Models
{
    public class MeetingReminder : IReminder
    {
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }

        public void Attach(IObserver observer)
        {

        }

        public void Detach(IObserver observer)
        {

        }

        public void Notify()
        {

        }
    }
}
