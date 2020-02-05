using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nekretnine.Web.ViewModels
{
    public class UplataDodajVM
    {
        public int UplataID { get; set; }

       
        public int KupacID { get; set; }
        public string Kupac { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        public int NekretninaID { get; set; }
        public List<SelectListItem> Nekretnina { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DatumUplate { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        public int UgovorID { get; set; }
        public List<SelectListItem> OznakaUgovora { get; set; }
        public double UkupanIznosPDV { get; set; }
        public double UkupanIznosBezPDV { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        public double? MjesecnaRata { get; set; }
        public bool Kasnjenje { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!")]

        public int NacinPlacanjaID { get; set; }
        public List<SelectListItem> NacinPlacanja { get; set; }
        
    }
}
