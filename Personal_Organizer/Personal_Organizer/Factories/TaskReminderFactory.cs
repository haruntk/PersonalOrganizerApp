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
        public IReminder CreateReminder()
        {
            return new TaskReminder();
        }
    }
}
