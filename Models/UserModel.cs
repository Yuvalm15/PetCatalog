
using Microsoft.AspNetCore.Identity;

namespace PetShopProject.Models{
    public class UserModel : IdentityUser
    {
        [PersonalData]
        public string? IconImageUrl { get; set; }

        [PersonalData]
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}