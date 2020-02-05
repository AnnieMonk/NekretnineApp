using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nekretnine.Web.ViewModels
{
    public class UplataOdaberiVM
    {
        public int KupacID { get; set; }
        public List<SelectListItem> Kupci { get; set; }
    }
}
