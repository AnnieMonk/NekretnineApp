using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nekretnine.Data.Models
{
    public class Lokacija
    {
        public int LokacijaID { get; set; }
      
        public string Naziv { get; set; }
       
       
        public int GradID { get; set; }
        public Grad Grad { get; set; }
    }
}
