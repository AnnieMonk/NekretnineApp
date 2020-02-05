using Nekretnine.Data.Models;
using System.Collections.Generic;

namespace Agencija_za_promet_nekretninama.Models
{
    public class Korisnik
    {

        public int KorisnikID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
      
        public string Email { get; set; }
        public string Telefon { get; set; }
        public int GradID { get; set; }
        public Grad Grad { get; set; }

        public int UlogaID { get; set; }
        public Uloga Uloga { get; set; }

        public int? KorisnickiNalogID { get; set; } 
        public KorisnickiNalog KorisnickiNalog { get; set; }

    }
}
