using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CapstoneProject.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Date of Appointment")]
        public DateTime Date { get; set; }
        [Display(Name ="Reason for Appointment")]
        public string ReasonForAppointment { get; set; }
        [ForeignKey("Student")]
        public string StudentId { get; set; }
        public ApplicationUser Student { get; set; }
        [ForeignKey("Advisor")]
        public string AdvisorId { get; set; }
        public ApplicationUser Advisor { get; set; }
    }
}