using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nekretnine.Web.ViewModels
{
    public class NotifikacijaIndexVM
    {
        
         public int NotifikacijaID { get; set; }
      
            public DateTime Datum { get; set; }
            public string Status { get; set; }
            public bool Vidjeno { get; set; }
            public string Kupac { get; set; }
            public string Uposlenik { get; set; }
            public string Text { get; set; }
        

    }
}
