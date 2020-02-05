using System;
using System.Collections.Generic;
using System.Text;

namespace Nekretnine.Data.Models
{
    public class AutorizacijskiToken
    {
        public int AutorizacijskiTokenID { get; set; }
        public string Vrijednost { get; set; }
        public int KorisnickiNalogID { get; set; }
        public KorisnickiNalog KorisnickiNalog { get; set; }
        public DateTime VrijemeEvidentiranja { get; set; }
    }
}
