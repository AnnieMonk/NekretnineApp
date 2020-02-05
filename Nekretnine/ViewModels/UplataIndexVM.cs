using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nekretnine.Web.ViewModels
{
    public class UplataIndexVM
    {
       

        public int KupacID { get; set; }
        public string Kupac { get; set; }
        
       
        public List<Row> rows { get; set; }
        public class Row
        {
            public int UplataID { get; set; }
            public int NekretninaID { get; set; }
            public string Nekretnina { get; set; }
            public DateTime DatumUplate { get; set; }
            public string OznakaUgovora { get; set; }
            public double UkupanIznosPDV { get; set; }
            public double UkupanIznosBezPDV { get; set; }
            public double? MjesecnaRata { get; set; }
            public bool Kasnjenje { get; set; }
            public int NacinPlacanjaID { get; set; }
            public string NacinPlacanja { get; set; }
        }
    }
}
