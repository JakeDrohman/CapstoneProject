using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CapstoneProject.Models
{
    public class StudentCourses
    {
        [Display(Name ="Advised to Take")]
        public bool NeedsToTake { get; set; }
        [Display(Name ="Have Passed")]
        public bool HasPassed { get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public ApplicationUser Student { get; set; }
        [ForeignKey("Professor")]
        public string ProfessorId { get; set; }
        public ApplicationUser Professor { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}