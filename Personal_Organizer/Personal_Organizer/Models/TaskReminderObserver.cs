using Personal_Organizer.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personal_Organizer.Models
{
    public class TaskReminderObserver : IObserver
    {
        public void Shake(ISubject subject,Form form)
        {
            form.BeginInvoke((Action)(() =>
            {
                var original = form.Location;
                var rnd = new Random(1337);
                const int shake_amplitude = 10;
                for (int i = 0; i < 50; i++)
                {
                    form.Location = new Point(original.X + rnd.Next(-shake_amplitude, shake_amplitude), original.Y + rnd.Next(-shake_amplitude, shake_amplitude));
                    System.Threading.Thread.Sleep(20);
                }
                form.Location = original;
            }));
        }
    }
}
