using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Organizer.Models
{
    public class Salary
    {
        public int Id { get; set; }
        public double GrossMinWage { get; set; }
        public string Experience { get; set; }
        public string Location { get; set; }
        public string Education { get; set; }
        public string Languages { get; set; }
        public string ManagerialPosition { get; set; }
        public string FamilyStatus { get; set; }
        public double CoefficientSum { get; set; }
        public double EngineerMinWage { get; set; }
    }
}
