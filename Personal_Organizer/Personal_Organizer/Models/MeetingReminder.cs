using Personal_Organizer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personal_Organizer.Models
{
    public class MeetingReminder : IReminder
    {
        public int ReminderID { get; set; }
        public int UserID { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Title { get;set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public bool IsTriggered { get; set; }

        public MeetingReminder(int reminderid, int userid, DateTime date, TimeSpan time, string title, string description, string summary,bool isTriggered)
        {
            this.ReminderID = reminderid;
            this.UserID = userid;
            this.Date = date;
            this.Time = time;
            this.Title = title;
            this.Description = description;
            this.Summary = summary;
            this.IsTriggered = isTriggered;
        }

        private List<IObserver> _observers = new List<IObserver>();
        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(Form reminder)
        {
            if (IsTriggered == true) return;
            IsTriggered = true;
            foreach (IObserver observer in _observers)
            {
                observer.Shake(this, reminder);
            }
        }
    }
}
