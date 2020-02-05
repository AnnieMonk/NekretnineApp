using System;

namespace Nekretnine.Web.ViewModels
{
    public class OglasiDetaljiVM
    {
        public int OglasID { get; set; }
        public DateTime DatumObjave { get; set; }
        public int NekretninaID { get; set; }
        public string NazivNekretnine { get; set; }
        public int UposlenikID { get; set; }
        public string UposlenikImePrezime { get; set; }
        public bool Aktivan { get; set; }

        public string vrstaOglasa { get; set; }
        public double Cijena { get; set; }
    }
}
