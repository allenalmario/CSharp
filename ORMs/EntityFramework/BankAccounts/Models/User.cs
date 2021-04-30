using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAccounts.Models
{
    public class User
    {
        public int UserId {get;set;}
        [Required]
        [MinLength(2, ErrorMessage="must be at least 2 characters")]
        [MaxLength(45, ErrorMessage="must not be more than 45 characters")]
        [Display(Name="First Name")]
        public string FirstName {get;set;}
        [Required]
        [MinLength(2, ErrorMessage="must be at least 2 characters")]
        [MaxLength(45, ErrorMessage="must not be more than 45 characters")]
        [Display(Name="Last Name")]
        public string LastName {get;set;}
        [Required]
        [EmailAddress]
        [MaxLength(45, ErrorMessage="must not be more than 45 characters")]
        public string Email {get;set;}
        [Required]
        [MinLength(8, ErrorMessage="must be at least 8 characters")]
        [DataType(DataType.Password)]
        public string Password {get;set;}
        public Double CurrentBalance {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        [NotMapped]
        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name="Confirm Password")]
        public string ConfirmPassword {get;set;}
        // Navigation Property for related Transaction objects
        public List<Transaction> Transactions {get;set;}
        // Constructor
        public User()
        {
            CurrentBalance = 0.00;
        }
    }
}