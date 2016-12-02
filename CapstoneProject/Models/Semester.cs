using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CapstoneProject.Models
{
    public class Semester
    {
        [Key]
        public int Id { get; set; }
        public string Season { get; set; }
        public int Year { get; set; }
    }
}