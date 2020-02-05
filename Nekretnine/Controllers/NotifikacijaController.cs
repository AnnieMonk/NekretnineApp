using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agencija_za_promet_nekretninama.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Nekretnine.Data.Models;
using Nekretnine.Web.Helper;
using Nekretnine.Web.Hubs;
using Nekretnine.Web.ViewModels;

namespace Nekretnine.Web.Controllers
{
    public class NotifikacijaController : Controller
    {

        private MyContext db;
        private readonly IHubContext<NotificationHub> _hubcontext;
        private IConfiguration _configuration;

        public NotifikacijaController(MyContext context, IHubContext<NotificationHub> hubcontext, IConfiguration configuration)
        {
            db = context;
            _hubcontext = hubcontext;
            _configuration = configuration;

        }

        public IActionResult PromijeniUProcitano(int NotifikacijaID)
        {
            Notifikacija x = db.Notifikacije.Find(NotifikacijaID);
            x.Status = "Pročitana";
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Detalji(int NotifikacijaID) {

            Notifikacija x = db.Notifikacije
                .Include(i=>i.Korisnik)
                .Include(i=>i.Obilazak)
                .Include(i=>i.Obilazak.Nekretnina)
                .Include(i=>i.Obilazak.Lokacija)
                .Include(i=>i.Obilazak.Lokacija.Grad)
                .Where(i => i.NotifikacijaID == NotifikacijaID).FirstOrDefault();
          
            NotifikacijaDetaljiVM model = new NotifikacijaDetaljiVM
            {
                NotifikacijaID = x.NotifikacijaID,
                KupacID = x.KorisnikID,
                LokacijaID = x.Obilazak.LokacijaID,
                NekretninaID = x.Obilazak.NekretninaID,
                ObilazakID = x.ObilazakID,
                Adresa = x.Obilazak.Adresa,
                Grad = x.Obilazak.Lokacija.Grad.Naziv,
                endVrijeme = x.Obilazak.DatumVrijemeEnd,
                startVrijeme = x.Obilazak.DatumVrijemeStart,
                KupacNaziv = x.Korisnik.Ime,
                NekretninaNaziv = x.Obilazak.Nekretnina.Naziv



            };
        
                return View(model);
        }

        public IActionResult Index(string pretraga = null)
        {
            
                var model = db.Notifikacije
                .Where(i => i.UposlenikID == db.Uposlenici.Where(s => s.Korisnik.KorisnickiNalogID == HttpContext.GetLogiraniKorisnik().KorisnickiNalogID).FirstOrDefault().UposlenikID)
                .Select
                (
                    i => new NotifikacijaIndexVM
                    {
                        NotifikacijaID = i.NotifikacijaID,
                        Vidjeno=i.Vidjeno,
                        Datum = i.DatumNotifikacije,
                        Status = i.Status,
                        Kupac = i.Korisnik.Ime + " " + i.Korisnik.Prezime,
                        Uposlenik = i.Uposlenik.Korisnik.Ime + " " + i.Uposlenik.Korisnik.Prezime,
                        Text = i.TextNotifikacije
                    }
                ).OrderBy(i => i.Status)
                .ThenByDescending(i => i.Datum)
                
                .ToList();

                return View("Index", model);
            
            
        }

      
        


    }
}