using Agencija_za_promet_nekretninama.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nekretnine.Data.Models
{
    public class Obilazak
    {
        public int ObilazakID { get; set; }
        public DateTime DatumVrijemeStart { get; set; }
        public DateTime DatumVrijemeEnd { get; set; }
        public string Adresa { get; set; }
        public string Napomena { get; set; }
        public bool Otkazano { get; set; }
        public bool Zavrseno { get; set; }
        public int KorisnikID { get; set; }
        public Korisnik Korisnik { get; set; }

        public int UposlenikID { get; set; }
        public Uposlenik Uposlenik { get; set; }
        public int LokacijaID { get; set; }
        public Lokacija Lokacija { get; set; }
        public int NekretninaID { get; set; }
        public Nekretnina Nekretnina { get; set; }
    }
}
