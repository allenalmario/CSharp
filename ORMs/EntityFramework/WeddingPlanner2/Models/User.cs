using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner2.Models
{
    public class User
    {
        public int UserId {get;set;}
        [Required(ErrorMessage = "is required")]
        [Display(Name="First Name")]
        public string FirstName {get;set;}
        [Required(ErrorMessage = "is required")]
        [Display(Name="Last Name")]
        public string LastName {get;set;}
        [Required(ErrorMessage = "is required")]
        [EmailAddress]
        public string Email {get;set;}
        [Required(ErrorMessage = "is required")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage="must be at least 8 characters")]
        public string Password {get;set;}
        [NotMapped]
        [Required(ErrorMessage = "is required")]
        [Display(Name="Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage="Passwords don't match!")]
        public string ConfirmPassword {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        // many to many relationship
        // Navigation property
        // User can attend many weddings
        public List<UserWeddingRSVP> RSVPS {get;set;}
        // one to many relationship
        // Navigation property
        public List<Wedding> CreatedWeddings {get;set;}
    }
}