using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace PetShopProject.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Please input a username")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Please input a password")]
        [MinLength(4, ErrorMessage = "Password must contain have a minimum length of 4 and must contain numbers, lower case and upper case")]
        public string? Password { get; set; }

        public string? IconImageUrl { get; set; }
    }
}
