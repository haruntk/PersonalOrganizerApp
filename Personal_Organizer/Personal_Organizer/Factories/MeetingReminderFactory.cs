using Personal_Organizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Organizer.Factories
{
    public class MeetingReminderFactory : IReminderFactory
    {
        public IReminder CreateReminder(int reminderid, int userid,DateTime date, TimeSpan time, string title, string description, string summary)
        {
            return new MeetingReminder(reminderid,userid,date, time, title, description, summary);
        }
    }
}
