using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nekretnine.Web.ViewModels
{
    public class LoginVM
    {
        public int KorisnickiNalogID { get; set; }
        [Display(Name = "Korisničko ime")]
        [Required(ErrorMessage = "Korisničko ime je obavezno polje.")]
        public string KorisnickoIme { get; set; }

        [Display(Name = "Lozinka")]
        [Required(ErrorMessage = "Lozinka je obavezno polje.")]
        public string Lozinka { get; set; }

        [Display(Name = "Zapamti me")]
        public bool ZapamtiMe { get; set; }

       
    }
}
