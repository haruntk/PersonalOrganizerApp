using Personal_Organizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Organizer.Factories
{
    public interface IReminderFactory
    {
        IReminder CreateReminder(int reminderid,int userid,DateTime date, TimeSpan time, string title, string description, string summary,bool isTriggered);
    }
}
