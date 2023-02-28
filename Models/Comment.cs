

namespace PetShopProject.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string? Text { get; set; }
        public int AnimalId { get; set; }
        public string? UserName { get; set; }
        public string? UserIcon { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}