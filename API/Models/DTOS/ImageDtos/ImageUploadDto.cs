namespace API.Models.DTOS.ImageDtos
{
    public class ImageUploadDto
    {
        public IFormFile File { get; set; }
        public int UserId { get; set; }
        public string Category { get; set; }
        public DateTime ImageDate { get; set; }
        public string? Type { get; set; }
        public string? Organ { get; set; }
    }
}