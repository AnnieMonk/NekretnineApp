using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nekretnine.Web.ViewModels
{
    public class OglasiDodajVM
    {
        public int OglasID { get; set; }

        [Required(ErrorMessage="Ovo polje je obavezno!")]
        public int NekretninaID { get; set; }
        public List<SelectListItem> Nekretnine { get; set; }

       
        public DateTime DatumObjave { get; set; }
        public bool Aktivan { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        public int vrstaOglasaID { get; set; }
        public List<SelectListItem> vrstaOglasa { get; set; }
        public string UposlenikKorisnickoIme { get; internal set; }
    }
}
