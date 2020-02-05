using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nekretnine.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nekretnine.Web.ViewModels
{
    public class UgovorDodajVM
    {
        public int UgovorID { get; set; }
        public string Oznaka { get; set; }

        [Required(ErrorMessage ="Ovo polje je obavezno!")]
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DatumVrijeme { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!")]

        public int NekretninaID { get; set; }
        public List<SelectListItem> Nekretnina { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!")]

        public int VrstaUgovoraID { get; set; }
        public List<SelectListItem> VrstaUgovora { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!")]

        public int KupacID { get; set; }
        public List<SelectListItem> Kupac { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!")]

        public IFormFile PDF { get; set; }

    }
}
