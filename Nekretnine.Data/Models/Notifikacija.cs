using Agencija_za_promet_nekretninama.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nekretnine.Data.Models
{
    public class Notifikacija
    {
        public int NotifikacijaID { get; set; }
        public string TextNotifikacije { get; set; }
        public bool Vidjeno { get; set; } 
        public string Status { get; set; } //procitana ili ne
        public DateTime DatumNotifikacije { get; set; }
        public int UposlenikID { get; set; }
        public Uposlenik Uposlenik { get; set; }
        public int KorisnikID { get; set; }
        public Korisnik Korisnik { get; set; }

        public int ObilazakID { get; set; }
        public Obilazak Obilazak { get; set; }
    }
}
