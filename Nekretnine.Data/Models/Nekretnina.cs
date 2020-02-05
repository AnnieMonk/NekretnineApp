using Nekretnine.Data.Models;
using System.Collections.Generic;

namespace Agencija_za_promet_nekretninama.Models
{
    public class Nekretnina
    {
        public int NekretninaID { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public double Kvadratura { get; set; }
        public double Cijena { get; set; }
        public int BrojSoba { get; set; }

        public int KategorijaID { get; set; }
        public Kategorija Kategorija { get; set; }
        public int LokacijaID { get; set; }
        public Lokacija Lokacija { get; set; }
        public virtual ICollection<Slike> Slike { get; set; }


    }
}
