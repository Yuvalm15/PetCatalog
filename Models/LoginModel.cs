using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace PetShopProject.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please input a username")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Please input a password")]
        [MinLength(4, ErrorMessage = "Incorret Password")]
        public string? Password { get; set; }
    }
}
