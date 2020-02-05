using System;
using System.Collections.Generic;

namespace Nekretnine.Web.ViewModels
{
    public class OglasiPrikazVM
    {

        public List<Row> rows { get; set; }
        public class Row
        {
            public int OglasID { get; set; }
            public DateTime DatumVrijemeObjave { get; set; }
            public bool Aktivan { get; set; }
            public string vrstaOglasa { get; set; }
            public int UposlenikID { get; set; }
            public string Uposlenik { get; set; }
            public string Nekretnina { get; set; }
            public double Cijena { get; set; }

            public Guid SlikaID { get; set; }
            public string Ekstenzija { get; set; }
            public string Opis { get; set; }
        }
    }
}

