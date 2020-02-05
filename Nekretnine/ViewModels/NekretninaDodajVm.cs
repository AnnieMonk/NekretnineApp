using Microsoft.AspNetCore.Mvc.Rendering;
using Nekretnine.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nekretnine.Web.ViewModels
{
    public class NekretninaDodajVm
    {
        public int NekretninaID { get; set; }
        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        public string Naziv { get; set; }

        [StringLength(1000, ErrorMessage ="Opis ne može biti duži od 1000 karaktera! ")]
        public string Opis { get; set; }
        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        public double? Kvadratura { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        [Range(0, 1000000)]
        public double? Cijena { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        
        public int? BrojSoba { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        public int KategorijeID { get; set; }
        public List<SelectListItem> Kategorije { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        public int GradID { get; set; }
        public List<SelectListItem> Grad { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        public int DrzavaID { get; set; }
        public List<SelectListItem> Drzava { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        public int LokacijaID { get; set; }
        public List<SelectListItem> Lokacija { get; set; }

        //public int AdresaID { get; set; }
        //[Required(ErrorMessage = "Ovo polje je obavezno!")]
        //public string Adresa { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno!")]
        public int[] KarakteristikeID { get; set; }
        public List<SelectListItem> Karakteristike { get; set; }
       
        public virtual List<Slike> Slike { get; set; }
    }
}
