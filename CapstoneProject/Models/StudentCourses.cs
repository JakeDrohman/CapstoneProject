using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CapstoneProject.Models
{
    public class StudentCourses
    {
        public bool HasPassed { get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public ApplicationUser Student { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}