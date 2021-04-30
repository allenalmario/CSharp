using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner2.Models
{
    public class Wedding
    {
        // primary key
        public int WeddingId {get;set;}
        [Required]
        [Display(Name="Wedder One")]
        public string WedderOne {get;set;}
        [Required]
        [Display(Name="Wedder Two")]
        public string WedderTwo {get;set;}
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date {get;set;}
        [Required]
        [Display(Name="Wedding Address")]
        public string WeddingAddress {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        // foreign key
        // one to many relationship,
        // being that weddings can be made
        // many times by one user
        public int UserId {get;set;}
        // navigation property
        public User CreatedBy {get;set;}
        // many to many list
        // where a wedding can have many attendees
        // navigation property
        public List<UserWeddingRSVP> Attendees {get;set;}
    }
}