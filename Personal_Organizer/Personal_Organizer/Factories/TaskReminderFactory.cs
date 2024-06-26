﻿using Personal_Organizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Organizer.Factories
{
    public class TaskReminderFactory : IReminderFactory
    {
            public IReminder CreateReminder(int reminderid,int userid, DateTime date, TimeSpan time, string title, string description, string summary, bool isTriggered)
            {
                return new TaskReminder(reminderid,userid,date, time, title, description, summary,isTriggered);
            }
    }
}
