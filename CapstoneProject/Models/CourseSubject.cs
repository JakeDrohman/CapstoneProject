using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CapstoneProject.Models
{
    public class CourseSubject
    {
        [Key]
        public int Id { get; set; }
        public string Subject { get; set; }
    }
}