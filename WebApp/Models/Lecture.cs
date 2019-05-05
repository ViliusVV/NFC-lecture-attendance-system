using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace NFCSystem.Models
{
    public class Lecture
    {
        [Required]
        [KeyAttribute]
        public string StudentId {get;set;}
        [Required]
        public string Title {get;set;}
        [Required]
        public string Details {get;set;}
        [Required]
        public DateTime Start {get;set;}
        [Required]
        public DateTime Finish {get;set;}
        [Required]
        public bool IsVisited {get;set;}
    }
}