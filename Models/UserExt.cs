using System.Security.Claims;

namespace PetShopProject.Models
{
    public static class UserExt
    {
        public static string? IconPath { get; set; } = "~/UserPictures/Default.jpg";
        public static DateTime? CreatedAt { get; set; }
        public static string? SetIconPath(this ClaimsPrincipal user, string? path)
        {
            return IconPath = path;
        }

        public static DateTime? SetDateTime(this ClaimsPrincipal user, DateTime? date)
        {
            return CreatedAt = date;
        }

        public static string? GetIconPath(this ClaimsPrincipal user)
        {
            return IconPath;
        }

        public static DateTime? GetDateTime(this ClaimsPrincipal user)
        {
            return CreatedAt;
        }

    }
}
