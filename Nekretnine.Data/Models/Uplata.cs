using Agencija_za_promet_nekretninama.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nekretnine.Data.Models
{
    public class Uplata
    {
        public int UplataID { get; set; }
        public DateTime DatumUplate { get; set; }
        public bool Kasnjenje { get; set; }
        public double? MjesecnaRata { get; set; }
        public double UkupanIznosPDV { get; set; }
        public double UkupanIznosBezPDV { get; set; }
        public int NacinPlacanjaID { get; set; }
        public NacinPlacanja NacinPlacanja { get; set; }
        public int KorisnikID { get; set; }
        public Korisnik Kupac { get; set; }
        public int UposlenikID { get; set; }
        public Uposlenik Uposlenik { get; set; }
        public int UgovorID { get; set; }
        public Ugovor Ugovor { get; set; }
        public int NekretninaID { get; set; }
        public Nekretnina Nekretnina { get; set; }

    }
}
