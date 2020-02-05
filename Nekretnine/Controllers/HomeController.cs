using Agencija_za_promet_nekretninama.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nekretnine.Data.Models;
using Nekretnine.Web;
using Nekretnine.Web.Helper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Nekretnine.Controllers
{
    public class HomeController : Controller
    {
        private MyContext db;
        
        public HomeController(MyContext context)
        {
            db = context;
        

        }
       


        [Autorizacija(true,false)]
        public IActionResult Index()
        {
           


            KorisnickiNalog nalog = HttpContext.GetLogiraniKorisnik();
            if (nalog == null)
            {
                TempData["errorMessage"] = "Nemate pravo pristupa";
                return RedirectToAction("Index", "Autentifikacija");
            }

           
           
            var brojOglasa = db.Oglas.ToList().Count();
            var brojApartmana = db.Oglas.Where(i => i.Nekretnina.Kategorija.KategorijaNaziv == "Apartman").ToList().Count();
            var brojVikendica = db.Oglas.Where(i => i.Nekretnina.Kategorija.KategorijaNaziv == "Vikendica").ToList().Count();
            var brojZemljista = db.Oglas.Where(i => i.Nekretnina.Kategorija.KategorijaNaziv == "Zemljista").ToList().Count();
            var brojStanova = db.Oglas.Where(i => i.Nekretnina.Kategorija.KategorijaNaziv == "Stan").ToList().Count();
            var brojKuca = db.Oglas.Where(i => i.Nekretnina.Kategorija.KategorijaNaziv == "Kuća").ToList().Count();
            var brojPprostora = db.Oglas.Where(i => i.Nekretnina.Kategorija.KategorijaNaziv == "Poslovni prostor").ToList().Count();

            ViewData["brojOglasa"] = brojOglasa;
            ViewData["brojApartmana"] = brojApartmana;
            ViewData["brojVikendica"] = brojVikendica;
            ViewData["brojZemljista"] = brojZemljista;
            ViewData["brojStanova"] = brojStanova;
            ViewData["brojKuca"] = brojKuca;
            ViewData["brojPprostora"] = brojPprostora;
          

            return View();
        }
      

        }
    }
