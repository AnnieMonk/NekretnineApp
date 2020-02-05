using System.Collections.Generic;

namespace Nekretnine.Web.ViewModels
{
    public class NekretninePrikazVM
    {

        public List<Row> rows { get; set; }

        public class Row
        {
            public int NekretninaID { get; set; }
            public string Naziv { get; set; }
            public int KategorijaID { get; set; }
            public string Kategorija { get; set; }
        }
    }
}
