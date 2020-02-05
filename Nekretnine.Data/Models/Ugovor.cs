using Agencija_za_promet_nekretninama.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nekretnine.Data.Models
{
    public class Ugovor
    {
        public int UgovorID { get; set; }
        public string Oznaka { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public int VrstaUgovoraID { get; set; }
        public VrstaUgovora VrstaUgovora { get; set; }
        public int NekretninaID { get; set; }
        public Nekretnina Nekretnina { get; set; }
        public int KorisnikID { get; set; }
        public Korisnik Kupac { get; set; }
        public int UposlenikID { get; set; }
        public Uposlenik Uposlenik { get; set; }
        public int PDFID { get; set; }
        public PDF PDF { get; set; }
    }
}
