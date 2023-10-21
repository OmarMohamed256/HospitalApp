using webapi.Entities;

namespace API.Models.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int UserId { get; set; }
        public virtual AppUser User { get; set; }
        public string Category { get; set; }
        public DateTime ImageDate { get; set; } = DateTime.Now;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string? Type { get; set; }
        public string? Organ { get; set; }

    }
}