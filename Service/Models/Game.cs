
namespace Service.Models
{
    public class Game
    { 
        public Guid id { get; set; }
        public required string rutaImagen { get; set; }
        public required string descripcion { get; set; }
        public DateTime fechaLanzamiento { get; set; }
        public int valoracion { get; set; }

        //Cambiar a un objeto en el futuro
        public required string plataforma { get; set; }
        //Cambiar a un objeto en el futuro
        public required string genero { get; set; }

    }
}
