using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class Wedding
    {
        public int WeddingId {get;set;}
        [Required(ErrorMessage="is required")]
        public string WedderOne {get;set;}
        [Required(ErrorMessage="is required")]
        public string WedderTwo {get;set;}
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date {get;set;}
        [Required(ErrorMessage="is required")]
        [Display(Name="Address")]
        public string WeddingAddress {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        // many to many relationship
        // navigation property
        // Wedding can have many Users as guests
        public List<UserWeddingGuest> Guests {get;set;}
        // one to many relationship
        public int UserId {get;set;}
        // navigation property
        public User Creator {get;set;}
    }
}