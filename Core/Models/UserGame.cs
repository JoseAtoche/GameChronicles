using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models
{
    public class UserGame
    {
        // Hacer estos campos opcionales si es necesario
        public int UserId { get; set; }
        public int GameId { get; set; }

        // Propiedades de navegación
        public User? User { get; set; }
        public Game? Game { get; set; }
    }
}
