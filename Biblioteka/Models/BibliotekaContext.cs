using Biblioteka.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Biblioteka.Models
{
    public class BibliotekaContext : IdentityDbContext<ApplicationUser>
    {
        public BibliotekaContext(DbContextOptions<BibliotekaContext> options)
            : base(options)
        {
        }

        public DbSet<Autor> Autorzy { get; set; }
        public DbSet<Kategoria> Kategorie { get; set; }
        public DbSet<Ksiazka> Ksiazki { get; set; }
        public DbSet<Wypozyczenie> Wypozyczenia { get; set; }
    }
}
