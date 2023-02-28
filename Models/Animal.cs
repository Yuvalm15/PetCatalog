using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace PetShopProject.Models
{
    public class Animal
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You must input a name")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "You must input an age")]
        [IntegerValidator(MinValue = 1)]
        public int Age { get; set; }
        [Required(ErrorMessage = "You must add a Description")]
        [MinLength(1)]
        public string? Description { get; set; }
        [Required(ErrorMessage = "You must choose a category")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "You must choose a picture")]
        public string? ImagePath { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        [NotMapped]
        public IFormFile? Picture { get; set; }

    }
}
