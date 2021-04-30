using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogReg.Models
{
    [NotMapped] // won't be added to the db, just for form validations for logging in
    public class LoginUser
    {
        [Required(ErrorMessage="is required.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string LoginEmail {get;set;}
        [Required(ErrorMessage="is required.")]
        [DataType(DataType.Password)]
        [Display(Name="Password")]
        public string LoginPassword {get;set;}
    }
}