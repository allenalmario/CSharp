using System; // to use datetime
using System.ComponentModel.DataAnnotations;

namespace Crudelicious.Models
{
    public class Dish // in relation to the DB, related to Dishes table, each
    // instance of this Model/Class is a row in the Dishes Table
    {
        // Each property represents a column in the row instance of the Dishes Table
        public int DishId {get;set;}
        [Required]
        public string Name {get;set;}
        [Required]
        public string Chef {get;set;}
        [Required]
        [Range(1,5)]
        public int Tastiness {get;set;}
        [Required]
        [Range(1, 1000)]
        public int Calories {get;set;}
        [Required]
        public string Description {get;set;}
        [Required]
        public DateTime CreatedAt {get;set;} = DateTime.Now; // set default values
        [Required]
        public DateTime UpdatedAt {get;set;} = DateTime.Now; // set default values
    }
}