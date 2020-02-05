using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nekretnine.Data.Models
{
    public class Drzava
    {
       
        public int DrzavaID { get; set; }

        public string Naziv { get; set; }
    }
}
