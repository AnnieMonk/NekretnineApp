using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nekretnine.Web.ViewModels
{
    public class ObilasciPrikazVM
    {
        public int UposlenikID { get; set; }
        public int BrojNeprocitanih { get; set; }
        public List<Row> rows { get; set; }

        public class Row
        {
            public int ObilazakID { get; set; }
            public DateTime DatumVrijemeStart { get; set; }
            public DateTime DatumVrijemeEnd { get; set; }
            public string Napomena { get; set; }
            public bool Otkazano { get; set; }
            public bool Zavrseno { get; set; }
            public int KupacID { get; set; }
            public string Kupac { get; set; }
            public int NekretninaID { get; set; }
            public string Nekretnina { get; set; }
            public int LokacijaID { get; set; }
            public string LokacijaAdresa { get; set; }
            public string Grad { get; set; }
        }
    }
}
