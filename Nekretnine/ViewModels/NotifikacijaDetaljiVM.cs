using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nekretnine.Web.ViewModels
{
    public class NotifikacijaDetaljiVM
    {
        public int NotifikacijaID { get; set; }
        public int ObilazakID { get; set; }
        public int LokacijaID { get; set; }
        public int KupacID { get; set; }
        public int NekretninaID { get; set; }
        public string KupacNaziv { get; set; }

        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime startVrijeme { get; set; }
        public DateTime endVrijeme { get; set; }

        [RegularExpression("^([a-zA-Z]+) [0-9][a-zA-Z]?")]
        public string Adresa { get; set; }
        public string Grad { get; set; }
        public string NekretninaNaziv { get; set; }

    }
}
