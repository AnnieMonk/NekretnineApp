using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Agencija_za_promet_nekretninama.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nekretnine.Data.Models;
using Nekretnine.Web.Helper;
using Nekretnine.Web.ViewModels;

namespace Nekretnine.Web.Controllers
{
    public class UgovorController : Controller
    {
        private MyContext db;
        [Obsolete]
        private readonly IHostingEnvironment hostingEnvironment;

        [Obsolete]
        public UgovorController(MyContext context, IHostingEnvironment hostingEnvironment)
        {
            db = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Pregled(int UgovorID)
        {
            Ugovor x = db.Ugovori
                .Include(i=>i.PDF)
                .Where(i=>i.UgovorID == UgovorID)
                .FirstOrDefault();


            return View(x);
        }

        [HttpPost]
        [Obsolete]

        public IActionResult Snimi(UgovorDodajVM input)
        {
            if (!ModelState.IsValid)
                return Redirect("Dodaj");

            Ugovor y = new Ugovor();
            KorisnickiNalog x = HttpContext.GetLogiraniKorisnik();

            if (ModelState.IsValid)
            {
                
                var fileName = Path.GetFileName(input.PDF.FileName);
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "files");

                PDF pdfFile = new PDF
                {
                    MyPDF = fileName,
                    Extension = Path.GetExtension(fileName),
                    PDFID = Guid.NewGuid()
                };
                y.PDF = pdfFile;

                
                var filePath = Path.Combine(uploadsFolder, pdfFile.PDFID + pdfFile.Extension);
                input.PDF.CopyTo(new FileStream(filePath, FileMode.Create));

                y.DatumKreiranja = input.DatumVrijeme;
                y.KorisnikID = input.KupacID;
                y.NekretninaID = input.NekretninaID;
                y.UposlenikID = db.Uposlenici.Where(i => i.Korisnik.KorisnickiNalogID == x.KorisnickiNalogID).FirstOrDefault().UposlenikID;
                y.VrstaUgovoraID = input.VrstaUgovoraID;
                y.Oznaka = input.Oznaka;
                db.Add(y);
                db.SaveChanges();

                //sada treba oglas proglasiti neaktivnim
                Oglas og = db.Oglas.Where(i => i.NekretninaID == y.NekretninaID).FirstOrDefault();
                og.Aktivan = false;
                db.SaveChanges();

            }          
            TempData["WarningMessage"] = "Uspješno ste dodali ugovor!";
            return RedirectToAction("Index");

        }
        public IActionResult Dodaj()
        {
            int brojac = db.Ugovori.Count(); //ovoliko ima ugovora
            brojac++;

            var ugovori = db.Ugovori.Select(i => i.NekretninaID).ToList();

            UgovorDodajVM model = new UgovorDodajVM
            {
                Oznaka = brojac.ToString() + "/"+ DateTime.Now.Month,
                VrstaUgovora = db.VrsteUgovora.Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = i.VrstaUgovoraID.ToString(),
                    Text = i.Naziv
                }).ToList(),
                Kupac = db.Korisnici
                .Where(i => i.Uloga.Naziv == "Kupac")
                .Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = i.KorisnikID.ToString(),
                    Text = i.Ime + " " + i.Prezime
                }).ToList(),
                Nekretnina = db.Nekretnine
                .Where(i => !ugovori.Contains(i.NekretninaID))
                .Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = i.NekretninaID.ToString(),
                    Text = i.Naziv
                }).ToList()


            };

            return View(model);
        }
        public IActionResult Index()
        {
            //ag => ag.UposlenikID == HttpContext.GetLogiraniKorisnik().KorisnickiNalogID
            UgovorPrikazVM model = new UgovorPrikazVM
            {
                rows = db.Ugovori.Where(ag => ag.UposlenikID == HttpContext.GetLogiraniKorisnik().KorisnickiNalogID)
                .Select(i => new UgovorPrikazVM.Row
                {
                    KupacID=i.KorisnikID,
                    Kupac= i.Kupac.Ime+ " " + i.Kupac.Prezime,
                     NekretninaID=i.NekretninaID,
                     Nekretnina=i.Nekretnina.Naziv,
                     DatumVrijeme=i.DatumKreiranja,
                     UgovorID=i.UgovorID,
                     VrstaUgovora=i.VrstaUgovora.Naziv,
                     VrstaUgovoraID=i.VrstaUgovoraID,
                     Oznaka=i.Oznaka
                     

                }).ToList()
            };
            return View(model);
        }
    }
}