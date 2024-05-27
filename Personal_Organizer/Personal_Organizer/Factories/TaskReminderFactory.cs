using Personal_Organizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Organizer.Factories
{
    public class TaskReminderFactory : IReminderFactory
    {
            public IReminder CreateReminder(DateTime date, TimeSpan time, string title, string description, string summary)
            {
                return new MeetingReminder(date, time, title, description, summary);
            }
    }
}
