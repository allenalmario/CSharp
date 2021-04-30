using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChefsNDishes.Models
{
    public class Chef
    {
        // Primary Key
        public int ChefId {get;set;}
        [Required]
        [DisplayName("First Name")]
        public string FirstName {get;set;}
        [Required]
        [DisplayName("Last Name")]
        public string LastName {get;set;}
        [Required]
        [DisplayName("Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth {get;set;}
        // Navigation property for related Dish objects
        public List<Dish> CreatedDishes {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}