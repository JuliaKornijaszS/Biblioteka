using System;

namespace Biblioteka.Models.Entities
{
    public class Wypozyczenie
    {
        public int Id { get; set; }

        public int KsiazkaId { get; set; }

        public DateTime DataWypozyczenia { get; set; }

        public DateTime? DataZwrotu { get; set; }

        // Relacja
        public Ksiazka Ksiazka { get; set; }
    }
}
