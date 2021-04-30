using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogReg.Models
{
    public class User
    {
        public int UserId {get;set;}
        [Required]
        [MinLength(2, ErrorMessage="must be at least 2 characters")]
        [Display(Name="First Name")]
        public string FirstName {get;set;}
        [Required]
        [MinLength(2, ErrorMessage="must be at least 2 characters")]
        [Display(Name="Last Name")]
        public string LastName {get;set;}
        [EmailAddress] // Validates that the field is in the form of a valid email address
        [Required]
        public string Email {get;set;}
        [Required]
        [MinLength(8, ErrorMessage="must be at least 8 characters")]
        [DataType(DataType.Password)]
        public string Password {get;set;}
        [NotMapped]
        [Display(Name="Confirm Password")]
        [Compare("Password", ErrorMessage = "doesn't match password")] // must be the same
        [DataType(DataType.Password)]
        public string ConfirmPassword {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}