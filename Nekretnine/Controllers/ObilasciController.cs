using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agencija_za_promet_nekretninama.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nekretnine.Data.Models;
using Nekretnine.Web.Helper;
using Nekretnine.Web.ViewModels;

namespace Nekretnine.Web.Controllers
{
    public class ObilasciController : Controller
    {
        private MyContext db;
       
        public ObilasciController(MyContext context)
        {
            db = context;
        }
        public IActionResult UrediDatum(int ObilazakID, DateTime datum)
        {
            
            Obilazak x = db.Obilasci
                .Include(i => i.Lokacija)
                .Where(i => i.ObilazakID == ObilazakID)
                .FirstOrDefault();
            Notifikacija y = db.Notifikacije
               .Where(i => i.ObilazakID == x.ObilazakID).FirstOrDefault();

            if (!ModelState.IsValid)
                return Redirect("/Notifikacija/Detalji?NotifikacijaID=" + y.NotifikacijaID);

            x.DatumVrijemeStart = datum;
            db.SaveChanges();

           


            return RedirectToAction("/Notifikacija/Detalji?NotifikacijaID=" + y.NotifikacijaID);
        }
        public IActionResult UrediAdresu(int ObilazakID, string adresa)
        {
            Obilazak x = db.Obilasci
                .Include(i => i.Lokacija)
                .Where(i => i.ObilazakID == ObilazakID)
                .FirstOrDefault();
            Notifikacija y = db.Notifikacije
                .Where(i => i.ObilazakID == x.ObilazakID).FirstOrDefault();

            if (!ModelState.IsValid)
                return Redirect("/Notifikacija/Detalji?NotifikacijaID=" + y.NotifikacijaID);
            x.Adresa = adresa;
            db.SaveChanges();

            
          
           
            return RedirectToAction("/Notifikacija/Detalji?NotifikacijaID=" + y.NotifikacijaID);
        }
        public IActionResult OtkazanoMijenjaj(int ObilazakID)
        {
            Obilazak x = db.Obilasci.Find(ObilazakID);
            if (x.Otkazano == false)
            {
                x.Otkazano = true;
                x.Zavrseno = false;
            }
            
            db.SaveChanges();


            return Redirect("/Obilasci/Tabela?AgentID=" + x.UposlenikID);
        }
        public IActionResult ZavrsenoMijenjaj(int ObilazakID)
        {
            Obilazak x = db.Obilasci.Find(ObilazakID);
            if (x.Zavrseno == false)
            {
                x.Zavrseno = true;
                x.Otkazano = false;
            }
            
            db.SaveChanges();

            
            return Redirect("/Obilasci/Tabela?AgentID=" + x.UposlenikID);
        }
        public IActionResult Tabela() 
        {
            //ako je obilazak te notifikacije nepročitan

            var notifikacije = db.Notifikacije.Where(i =>i.Status == "Pročitana").Select(i=>i.ObilazakID).ToList();

            ObilasciPrikazVM model = new ObilasciPrikazVM
            {
                UposlenikID = db.Uposlenici.Where(i => i.Korisnik.KorisnickiNalogID == HttpContext.GetLogiraniKorisnik().KorisnickiNalogID && i.Korisnik.Uloga.Naziv == "Uposlenik").Select(u => u.UposlenikID).First(),
                BrojNeprocitanih = db.Notifikacije.Where(i =>i.Status=="Nepročitana" && i.Uposlenik.Korisnik.KorisnickiNalogID == HttpContext.GetLogiraniKorisnik().KorisnickiNalogID).Count(),
                rows = db.Obilasci
                .Where(i=> notifikacije.Contains(i.ObilazakID))
                .Where(i=>i.UposlenikID == HttpContext.GetLogiraniKorisnik().KorisnickiNalogID)
                .Select(i => new ObilasciPrikazVM.Row
                {
                    ObilazakID=i.ObilazakID,
                    Kupac=i.Korisnik.Ime + " " + i.Korisnik.Prezime,
                    Nekretnina=i.Nekretnina.Naziv,
                    LokacijaAdresa=i.Adresa,
                    Grad=i.Lokacija.Grad.Naziv,
                    DatumVrijemeStart=i.DatumVrijemeStart,
                    DatumVrijemeEnd=i.DatumVrijemeStart.AddHours(1),
                    Otkazano=i.Otkazano,
                    Zavrseno=i.Zavrseno,
                    Napomena=i.Napomena
                    

                }).OrderByDescending(i=>i.DatumVrijemeStart).ToList()
            };
            return PartialView(model);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}