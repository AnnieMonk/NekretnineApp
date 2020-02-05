using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agencija_za_promet_nekretninama.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nekretnine.Data.Models;
using Nekretnine.Web.Helper;
using Nekretnine.Web.ViewModels;

namespace Nekretnine.Web.Controllers
{
    public class UplataController : Controller
    {
        private MyContext db;


        public UplataController(MyContext context)
        {
            db = context;
        }

        public IActionResult Snimi(UplataDodajVM input)
        {
            if (!ModelState.IsValid)
                return Redirect("Dodaj?KupacID=" + input.KupacID);

            var cijenaSaPDV = db.Nekretnine.Where(i => i.NekretninaID == input.NekretninaID).Select(i => i.Cijena).FirstOrDefault();
            var cijenaBezPDV = cijenaSaPDV - (cijenaSaPDV * 0.17);

            Uplata x = new Uplata
            {
                DatumUplate = input.DatumUplate,
                KorisnikID = input.KupacID,
                MjesecnaRata = input.MjesecnaRata,
                NacinPlacanjaID = input.NacinPlacanjaID,
                UgovorID = input.UgovorID,
                NekretninaID = input.NekretninaID,
                UkupanIznosBezPDV = cijenaBezPDV ,
                UkupanIznosPDV = cijenaBezPDV ,
                UposlenikID = db.Uposlenici.Where(i => i.Korisnik.KorisnickiNalogID == HttpContext.GetLogiraniKorisnik().KorisnickiNalogID && i.Korisnik.Uloga.Naziv == "Uposlenik").Select(u => u.UposlenikID).First()
            };
            db.Add(x);
            db.SaveChanges();
            TempData["WarningMessage"] = "Uspješno ste dodali uplatu!";

            return Redirect("/Uplata/IndexUplate?KupacID=" + x.KorisnikID);
        }
        public IActionResult Dodaj(int KupacID)
        {
            var ugovori = db.Ugovori.Where(i => i.KorisnikID == KupacID).Select(i=>i.NekretninaID).ToList();

            //Uplata x = db.Uplate
            //    .Include(i=>i.Kupac)
            //    .Include(i=>i.Nekretnina)
            //    .Where(i => i.KorisnikID == KupacID).FirstOrDefault();

            Korisnik x = db.Korisnici.Where(i => i.KorisnikID == KupacID).FirstOrDefault();

            UplataDodajVM model = new UplataDodajVM
            {
                KupacID = x.KorisnikID,
                Kupac=x.Ime+ " "+ x.Prezime,
                //UkupanIznosPDV=x.Nekretnina.Cijena,
                //UkupanIznosBezPDV=x.Nekretnina.Cijena - (x.Nekretnina.Cijena *0.17),
                OznakaUgovora = db.Ugovori.Where(i => i.KorisnikID == KupacID).Select(i => new SelectListItem { 
                    Value=i.UgovorID.ToString(),
                    Text=i.Oznaka
                
                }).ToList(),
                NacinPlacanja = db.NaciniPlacanja.Select(i => new SelectListItem
                {
                    Value = i.NacinPlacanjaID.ToString(),
                    Text = i.Naziv

                }).ToList(),
                Nekretnina = db.Nekretnine
                .Where(i => ugovori.Contains(i.NekretninaID))
                .Select(i => new SelectListItem
                {
                    Value = i.NekretninaID.ToString(),
                    Text = i.Naziv

                }).ToList()

            };
            return View(model);
        }

        //prikazi samo ono sto je odabrano u modalu
        public IActionResult IndexUplate(UplataOdaberiVM input)
        {

                //nadji sve uplate odabranog korisnika
                Uplata x = db.Uplate
                    .Include(i => i.Nekretnina)
                    .Include(i => i.Ugovor)
                    .Include(i => i.Kupac)
                    .Include(i => i.NacinPlacanja)
                    .Where(i => i.KorisnikID == input.KupacID).FirstOrDefault();

                 Korisnik korisnik = db.Korisnici.Where(i => i.KorisnikID == input.KupacID).FirstOrDefault();
                
            UplataIndexVM model = new UplataIndexVM
                {
                    
                    KupacID = korisnik.KorisnikID,

                    Kupac = korisnik.Ime + " " + korisnik.Prezime,

                    rows = db.Uplate
                    .Where(i => i.KorisnikID == korisnik.KorisnikID)
                    .Select(i => new UplataIndexVM.Row
                    {
                        UplataID = i.UplataID,
                        DatumUplate = i.DatumUplate,
                        Nekretnina = i.Nekretnina.Naziv,
                        OznakaUgovora = i.Ugovor.Oznaka,
                        NekretninaID = i.NekretninaID,
                        Kasnjenje = i.Kasnjenje,
                        NacinPlacanjaID = i.NacinPlacanjaID,
                        NacinPlacanja = i.NacinPlacanja.Naziv,
                        MjesecnaRata = i.MjesecnaRata,
                        UkupanIznosPDV = i.Nekretnina.Cijena,
                        UkupanIznosBezPDV = i.Nekretnina.Cijena - (i.Nekretnina.Cijena * 0.17)

                    }).ToList()
                };

                return View(model);
            

        }

        //za odabir kupca za kojeg ce se prikazati uplate
        public IActionResult Odaberi()
        {
            var ugovori = db.Ugovori.Select(i=>i.KorisnikID).ToList();
            UplataOdaberiVM model = new UplataOdaberiVM
            {
                Kupci = db.Korisnici
                .Where(i=> ugovori.Contains(i.KorisnikID) && i.Uloga.Naziv =="Kupac")
                .Select(i => new SelectListItem
                {
                    Value = i.KorisnikID.ToString(),
                    Text = i.Ime + " " + i.Prezime
                }).ToList()
            };
            return View(model);
        }
    }
}