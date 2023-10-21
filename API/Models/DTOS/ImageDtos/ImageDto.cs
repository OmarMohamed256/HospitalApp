namespace API.Models.DTOS.ImageDtos
{
    public class ImageDto
    {
        public int Id { get; set; } = 0;
        public string Url { get; set; }
        public int UserId { get; set; }
        public string Category { get; set; }
        public DateTime ImageDate { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string? Type { get; set; }
        public string? Organ { get; set; }
    }
}