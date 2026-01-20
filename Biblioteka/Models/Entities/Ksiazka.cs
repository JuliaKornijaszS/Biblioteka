namespace Biblioteka.Models.Entities
{
    public class Ksiazka
    {
        public int Id { get; set; }

        public string Tytul { get; set; }

        public int RokWydania { get; set; }

        // Klucze obce
        public int AutorId { get; set; }
        public int KategoriaId { get; set; }

        // Relacje (nawigacja)
        public Autor Autor { get; set; }
        public Kategoria Kategoria { get; set; }
    }
}
