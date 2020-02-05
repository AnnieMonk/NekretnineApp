using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agencija_za_promet_nekretninama.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nekretnine.Data.Models;
using Nekretnine.Web.Helper;
using Nekretnine.Web.ViewModels;

namespace Nekretnine.Web.Controllers
{
    public class AutentifikacijaController : Controller
    {
       
            private MyContext db;
            public AutentifikacijaController(MyContext context)
            {
                db = context;

            }
            public IActionResult Index()
        {


            return View(new LoginVM()
            {
                ZapamtiMe = true
            });
        }

        public IActionResult Login(LoginVM input)
        {
            
            //nadji unesene podatke
            KorisnickiNalog nalog = db.KorisnickiNalozi.SingleOrDefault(i => i.KorisnickoIme == input.KorisnickoIme && i.Lozinka == input.Lozinka);

           
            if (nalog == null)
            {
                TempData["errorMessage"] = "Niste unijeli ispravne podatke za prijavu.";
                return View("Index",input);
            }
            var uposlenik = db.Korisnici.Where(i => i.KorisnickiNalogID == nalog.KorisnickiNalogID && i.Uloga.Naziv == "Uposlenik").FirstOrDefault();
            var kupac = db.Korisnici.Where(i => i.KorisnickiNalogID == nalog.KorisnickiNalogID && i.Uloga.Naziv=="Kupac").FirstOrDefault();
            if (nalog !=null) //ovdje je jos potrebno za admina dodati
            {
                //ako je uposlenik u pitanju
                HttpContext.SetLogiraniKorisnik(nalog, input.ZapamtiMe);

                if (uposlenik != null)
                    return RedirectToAction("Index", "Home");
                else if (kupac != null)
                    return RedirectToAction("Index", "Kupac");


            }

            TempData["errorMessage"] = "Niste unijeli ispravne podatke za prijavu.";
            return View("Index",input);

        }

        public IActionResult Logout()
        {
            HttpContext.SetLogiraniKorisnik(null);
            return RedirectToAction("Index");
        }
    }
}