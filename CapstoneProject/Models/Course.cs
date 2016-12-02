using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CapstoneProject.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }
        public decimal Credits { get; set; }
        [ForeignKey("Subject")]
        public int CourseSubjectId { get; set; }
        public CourseSubject Subject { get; set; }
    }
}