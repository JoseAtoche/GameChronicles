using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class Game
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int Rating { get; set; }

        [Required]
        public string? Platform { get; set; }

        public string? Genre { get; set; }

        public string? ImagePath { get; set; }

        public ICollection<UserGame>? SavedByUsers { get; set; }
    }
}