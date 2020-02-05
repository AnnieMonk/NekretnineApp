using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nekretnine.Data.Models
{
    public class Grad
    {
        public int GradID { get; set; }

        public string Naziv { get; set; }

        public int DrzavaID { get; set; }
        public Drzava Drzava { get; set; }
    }
}
