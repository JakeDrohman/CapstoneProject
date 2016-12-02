using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CapstoneProject.Models
{
    public class StudentInformation
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Student")]
        public string StudentId { get; set; }
        public ApplicationUser Student { get; set; }
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Alternate First Name")]
        public string AlternateFirstName { get; set; }
        [Display(Name = "Alternate Last Name")]
        public string AlternateLastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [Display(Name = "Zip Code")]
        public string Zipcode { get; set; }
        [Display(Name = "Alternate Street")]
        public string AlternateStreet { get; set; }
        [Display(Name = "Alternate City")]
        public string AlternateCity { get; set; }
        [Display(Name = "Alternate State")]
        public string AlternateState { get; set; }
        [Display(Name = "Alternate Zip Code")]
        public string AlternateZipcode { get; set; }
        [Display(Name = "E-mail Address")]
        public string EmailAddress { get; set; }
        [Display(Name ="Date Admitted")]
        public DateTime DateAdmitted { get; set; }
        public string Track { get; set; }
        [Display(Name ="Projected Date of Graduation")]
        public DateTime? GraduationDate { get; set; }
        public bool? Graduated { get; set; }
        public bool? Withdrawn { get; set; }
        public bool? Dismissed { get; set; }
        [Display(Name ="Credits Needed")]
        public decimal CreditsNeeded { get; set; }
        [Display(Name ="Credits Completed")]
        public decimal CreditsCompleted { get; set; }
    }
}