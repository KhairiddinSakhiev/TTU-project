using Microsoft.AspNetCore.Http;

namespace Domain.EntitiesDto
{
    public class NewsDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string ImageName { get; set; }
        public IFormFile Image { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public bool Enabled { get; set; }

    }
}
