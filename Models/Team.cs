using System.ComponentModel.DataAnnotations.Schema;

namespace Lumia.Models
{
    public class Team
    {
        public int Id { get; set; }

        public string Name { get; set; }

        

        public Position Position { get; set; }
        public string? Image { get; set; }

        public int? PositionId { get; set; }
        public string? Title { get; set; }
        [NotMapped]
        public IFormFile? Photo { get; set; }
    }
}
