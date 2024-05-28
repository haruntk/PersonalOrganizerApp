﻿using Personal_Organizer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Organizer.Models
{
    public class TaskReminder : IReminder
    {
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }

        private List<IObserver> _observers = new List<IObserver>();
        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(Reminder reminder)
        {
            foreach(IObserver observer in _observers)
            {
                observer.Shake(this,reminder);
            }
        }
    }
}