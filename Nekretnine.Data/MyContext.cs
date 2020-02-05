using Microsoft.EntityFrameworkCore;
using Nekretnine.Data.Models;

namespace Agencija_za_promet_nekretninama.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> x) : base(x) { }

        

        public DbSet<Nekretnina> Nekretnine { get; set; }

        public DbSet<Oglas> Oglas { get; set; }
        public DbSet<VrstaOglasa> VrsteOglasa { get; set; }

        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Uposlenik> Uposlenici { get; set; }

        public DbSet<Drzava> Drzave { get; set; }
        public DbSet<Grad> Gradovi { get; set; }
        public DbSet<Lokacija> Lokacije { get; set; }
        public DbSet<Kategorija> Kategorije { get; set; }
        public DbSet<Karakteristike> Karakteristike { get; set; }
        public DbSet<NekretninaKarakteristike> NekretninaKarakteristike { get; set; }
        public DbSet<Slike> Slike { get; set; }
        public DbSet<Uloga> Uloge { get; set; }
        public DbSet<Obilazak> Obilasci { get; set; }
        public DbSet<Notifikacija> Notifikacije { get; set; }
        public DbSet<KorisnickiNalog> KorisnickiNalozi { get; set; }
        public DbSet<AutorizacijskiToken> Tokeni { get; set; }
        public DbSet<VrstaUgovora> VrsteUgovora { get; set; }
        public DbSet<Ugovor> Ugovori { get; set; }
        public DbSet<Uplata> Uplate { get; set; }
        public DbSet<NacinPlacanja> NaciniPlacanja { get; set; }
        public DbSet<PDF> PDFs { get; set; }
        
      
      
    }
}
