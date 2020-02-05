using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nekretnine.Web.ViewModels
{
    public class UgovorPrikazVM
    {

        public List<Row> rows { get; set; }
        public class Row
        {
            public int UgovorID { get; set; }
            public string Oznaka { get; set; }
            public DateTime DatumVrijeme { get; set; }
            public int NekretninaID { get; set; }
            public string Nekretnina { get; set; }
            public int VrstaUgovoraID { get; set; }
            public string VrstaUgovora { get; set; }
            public int KupacID { get; set; }
            public string Kupac { get; set; }
           

        }
    }
}
