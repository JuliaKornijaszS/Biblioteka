using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Models.Entities
{
    public class Ksiazka
    {
        
        public int Id { get; set; }
        [Required(ErrorMessage = "Tytuł jest wymagany")]
        public string Tytul { get; set; }
        [Range(1455, 2026, ErrorMessage = "Rok wydania - między 1455 a 2026!")]
        public int RokWydania { get; set; }

        // Klucze obce
        public int AutorId { get; set; }
        public int KategoriaId { get; set; }

        // Relacje (nawigacja)
        public Autor Autor { get; set; }
        public Kategoria Kategoria { get; set; }
    }
}
