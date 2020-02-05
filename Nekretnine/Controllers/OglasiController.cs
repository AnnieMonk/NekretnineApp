using Agencija_za_promet_nekretninama.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nekretnine.Web.Helper;
using Nekretnine.Web.ViewModels;
using System;
using System.Linq;

namespace Nekretnine.Web.Controllers
{
    public class OglasiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private MyContext db;


        public OglasiController(MyContext context)
        {
            db = context;
        }
        [Autorizacija(true, false)]
        public IActionResult Detalji(int OglasID)
        {
            Oglas x = db.Oglas
                .Include(i => i.Nekretnina)
                .Include(i => i.Uposlenik)
                .Include(i => i.Uposlenik.Korisnik)
                .Include(i => i.vrstaOglasa)
                .Where(i => i.OglasID == OglasID).FirstOrDefault();
            OglasiDetaljiVM model = new OglasiDetaljiVM
            {
                OglasID = x.OglasID,
                UposlenikID = x.UposlenikID,
                NekretninaID = x.NekretninaID,
                Aktivan = x.Aktivan,
                DatumObjave = x.DatumVrijemeObjave,
                NazivNekretnine = x.Nekretnina.Naziv,
                UposlenikImePrezime = x.Uposlenik.Korisnik.Ime + " " + x.Uposlenik.Korisnik.Prezime,
                vrstaOglasa = x.vrstaOglasa.Naziv,
                Cijena = x.Nekretnina.Cijena
            };
            return View(model);
        }
        public IActionResult Snimi(OglasiDodajVM input)
        {
            if (!ModelState.IsValid)
                return Redirect("DodajIznova");
            Oglas novi = new Oglas
            {
                NekretninaID = input.NekretninaID,
                UposlenikID = db.Uposlenici.Where(i=>i.Korisnik.KorisnickiNalogID == HttpContext.GetLogiraniKorisnik().KorisnickiNalogID && i.Korisnik.Uloga.Naziv =="Uposlenik").Select(u=>u.UposlenikID).First(),
                vrstaOglasaID = input.vrstaOglasaID,
                DatumVrijemeObjave =input.DatumObjave,
                Aktivan = true

            };
            db.Add(novi);
            db.SaveChanges();
            db.Dispose();

            return Redirect("/Oglasi/Prikazi");
        }
        [Autorizacija(true, false)]
        public IActionResult DodajIznova()
        {
            var oglasi = db.Oglas.Select(i => i.NekretninaID).ToList();

            OglasiDodajVM model = new OglasiDodajVM
            {

                DatumObjave = DateTime.Now,

                //prikazi samo nekretnine koje nisu u oglasima
                Nekretnine = db.Nekretnine
                .Where(i => !oglasi.Contains(i.NekretninaID))
                .Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = i.NekretninaID.ToString(),
                    Text = i.Naziv
                }).ToList(),
                
                UposlenikKorisnickoIme = HttpContext.GetLogiraniKorisnik().KorisnickoIme,
                
                vrstaOglasa = db.VrsteOglasa.Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = i.VrstaOglasaID.ToString(),
                    Text = i.Naziv

                }).ToList()

            };

            return View("Dodaj", model);
        }
        [Autorizacija(true, false)]
        public IActionResult Dodaj(int NekretninaID)
        {
            //ovo je kada biramo iz Uredi ili Detalji Nekretnine
            Nekretnina x = db.Nekretnine.Find(NekretninaID);
            Oglas postojiLi = db.Oglas.Where(i => i.NekretninaID == NekretninaID).FirstOrDefault();
            if (postojiLi != null)
            {
                TempData["WarningMessage"] = "Nekretnina je već objavljena!";
                return Redirect("/Nekretnina/Prikazi");
            }
            else
            {
                OglasiDodajVM model = new OglasiDodajVM
                {
                    NekretninaID = x.NekretninaID,
                    Nekretnine=db.Nekretnine.Where(i=>i.NekretninaID == NekretninaID).Select(k=> new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { 
                        Value=k.NekretninaID.ToString(),
                        Text=k.Naziv
                    }).ToList(),
                    Aktivan = true,
                    DatumObjave = DateTime.Now,


                    UposlenikKorisnickoIme =HttpContext.GetLogiraniKorisnik().KorisnickoIme,
                    vrstaOglasa = db.VrsteOglasa.Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                    {
                        Value = i.VrstaOglasaID.ToString(),
                        Text = i.Naziv

                    }).ToList()

                };
                return View(model);

            }
        }

        //Prvi sprint
        [Autorizacija(true, false)]
        public IActionResult Obrisi(int OglasID)
        {
            Oglas oglasZaBrisanje = db.Oglas.Find(OglasID);
            int x = oglasZaBrisanje.UposlenikID;
            db.Remove(oglasZaBrisanje);
            db.SaveChanges();

            return Redirect("/Oglasi/Prikazi");

        }

        //Prvi sprint
        public IActionResult Prikazi(string pretraga = null)
        {
           

            if (pretraga != null)
            {

                //prikazat ce se oglasi samo ulogiranog agenta, tj. oni koje je on objavio
                OglasiPrikazVM model = new OglasiPrikazVM
                {

                    rows = db.Oglas
                    .Where(ag => ag.UposlenikID == HttpContext.GetLogiraniKorisnik().KorisnickiNalogID && ag.Nekretnina.Kategorija.KategorijaNaziv == pretraga)

                    .Select(og => new OglasiPrikazVM.Row
                    {
                        OglasID = og.OglasID,
                        DatumVrijemeObjave = og.DatumVrijemeObjave,
                        Uposlenik = og.Uposlenik.Korisnik.Ime + " " + og.Uposlenik.Korisnik.Prezime,
                        Aktivan = og.Aktivan,
                        Nekretnina = og.Nekretnina.Naziv,
                        vrstaOglasa = og.vrstaOglasa.Naziv,
                        Cijena = og.Nekretnina.Cijena,
                        SlikaID = og.Nekretnina.Slike.Select(i => i.SlikeID).First(),
                        Ekstenzija = og.Nekretnina.Slike.Select(i => i.Ekstenzija).First(),
                        Opis = og.Nekretnina.Opis
                    }).ToList()
                };
                return View(model);
            }
            else
            {
                OglasiPrikazVM model = new OglasiPrikazVM
                {

                    rows = db.Oglas
                  .Where(ag => ag.UposlenikID == HttpContext.GetLogiraniKorisnik().KorisnickiNalogID)

                  .Select(og => new OglasiPrikazVM.Row
                  {
                      OglasID = og.OglasID,
                      DatumVrijemeObjave = og.DatumVrijemeObjave,
                      Uposlenik = og.Uposlenik.Korisnik.Ime + " " + og.Uposlenik.Korisnik.Prezime,
                      Aktivan = og.Aktivan,
                      Nekretnina = og.Nekretnina.Naziv,
                      vrstaOglasa = og.vrstaOglasa.Naziv,
                      Cijena = og.Nekretnina.Cijena,
                      SlikaID = og.Nekretnina.Slike.Select(i => i.SlikeID).First(),
                      Ekstenzija = og.Nekretnina.Slike.Select(i => i.Ekstenzija).First(),
                      Opis = og.Nekretnina.Opis
                  }).ToList()
                };

                return View(model);
            }
            
        }

    }
}