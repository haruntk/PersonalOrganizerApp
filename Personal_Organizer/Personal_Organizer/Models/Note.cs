﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Organizer.Models
{
    public class Note
    {
        public int UserID { get; set; }
        //public string Header { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
    }
}
