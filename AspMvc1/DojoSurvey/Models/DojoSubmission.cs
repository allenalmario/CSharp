using System.ComponentModel.DataAnnotations;
namespace DojoSurvey.Models
{
    public class DojoSubmission
    {
        [Required]
        [MinLength(2)]
        [Display(Name = "Your Name:")]
        public string Name {get;set;}
        [Required]
        [Display(Name = "Your Location:")]
        public string Location {get;set;}
        [Required]
        [Display(Name = "Your Favorite Language:")]
        public string Language {get;set;}
        [MaxLength(20)]
        [Display(Name = "Comment (Optional):")]
        public string Comment {get;set;}
    }
}