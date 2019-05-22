using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NFCSystem.Models
{
    public class StudentStatDTO
    {
        [KeyAttribute]
        public string LectureID { get; set; }
        public string LectureName { get; set; }

        public int AttendanceLab { get; set; }
        public int TotalLab { get; set; }
        public int VisitedLab { get; set; }
    

        public int AttendanceTheory { get; set; }
        public int TotalTheory { get; set; }
        public int VisitedTheory { get; set; }

        public int AttendancePractice { get; set; }
        public int TotalPractice {get;set;}
        public int VisitedPractice {get;set;}
    }
}