using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChefsNDishes.Models
{
    public class Dish
    {
        public int DishId {get;set;}
        [Required]
        public string Name {get;set;}
        [Required]
        [Range(1, int.MaxValue, ErrorMessage="must be greater than 0")]
        public int Calories {get;set;}
        [Required]
        [Range(1, 5, ErrorMessage="must be between 1-5")]
        public int Tastiness {get;set;}
        [Required]
        public string Description {get;set;}
        // Set Relationship to Chef Database
        // Foriegn Key
        [DisplayName("Chef")]
        public int ChefId {get;set;}
        // Navigation property for related Chef object
        public Chef Creator {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}