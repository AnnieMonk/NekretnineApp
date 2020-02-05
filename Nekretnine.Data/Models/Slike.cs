using Agencija_za_promet_nekretninama.Models;
using System;

namespace Nekretnine.Data.Models
{
    public class Slike
    {
        public Guid SlikeID { get; set; }
        public string MyImage { get; set; }
        public string Ekstenzija { get; set; }
        public int NekretninaID { get; set; }
        public Nekretnina Nekretnina { get; set; }
    }
}
