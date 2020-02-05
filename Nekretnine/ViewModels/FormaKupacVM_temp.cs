using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nekretnine.Web.ViewModels
{
    public class FormaKupacVM_temp
    {
        public int ObilazakID { get; set; }

        public string KupacKorisnickoIme { get; set; }
        public DateTime DatumObilaska { get; set; }
        public int GradID { get; set; }
        public List<SelectListItem> Grad { get; set; }
        public string Adresa { get; set; }
        public int NekretninaID { get; set; }
        public List<SelectListItem> Nekretnine { get; set; }

        public int AgentID { get; set; }
        public List<SelectListItem> Agenti { get; set; }

        
        

        
    }
}
