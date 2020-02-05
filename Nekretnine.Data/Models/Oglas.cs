using System;

namespace Agencija_za_promet_nekretninama.Models
{
    public class Oglas
    {
        public int OglasID { get; set; }
        public DateTime DatumVrijemeObjave { get; set; }
        public bool Aktivan { get; set; }
        public int vrstaOglasaID { get; set; }
        public VrstaOglasa vrstaOglasa { get; set; }
        public int UposlenikID { get; set; }
        public Uposlenik Uposlenik { get; set; }
        public int NekretninaID { get; set; }
        public Nekretnina Nekretnina { get; set; }
    }
}
