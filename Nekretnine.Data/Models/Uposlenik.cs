using Nekretnine.Data.Models;
using System;

namespace Agencija_za_promet_nekretninama.Models
{
    public class Uposlenik
    {
        public int UposlenikID { get; set; }
        public DateTime DatumZaposlenja { get; set; }
        public string Zvanje { get; set; }
        public string Opis { get; set; }
        public double RatingStars { get; set; }
        public int KorisnikID { get; set; }
        public Korisnik Korisnik { get; set; }
      
    }
}