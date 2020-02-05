using Agencija_za_promet_nekretninama.Models;

namespace Nekretnine.Data.Models
{
    public class NekretninaKarakteristike
    {
        public int NekretninaKarakteristikeID { get; set; }
        public int NekretninaID { get; set; }
        public Nekretnina Nekretnina { get; set; }
        public int KarakteristikeID { get; set; }
        public Karakteristike Karakteristike { get; set; }
    }
}
