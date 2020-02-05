
using Agencija_za_promet_nekretninama.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nekretnine.Data.Models;
using Nekretnine.Web.Helper;
using Nekretnine.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static Nekretnine.Web.ViewModels.NekretninePrikazVM;

namespace Nekretnine.Web.Controllers
{
    [Autorizacija(true,false)]
    public class NekretninaController : Controller
    {
        private MyContext db;
        private readonly IHostingEnvironment host;
        public NekretninaController(MyContext context, IHostingEnvironment _host)
        {
            db = context;
            host = _host;
        }

        

        [HttpDelete]
        public JsonResult DeleteFile(string id)
        {
            //za slike
            if (String.IsNullOrEmpty(id))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Result = "Error" });
            }
            try
            {
                Guid guid = new Guid(id);

                //nadji tu sliku
                Slike fileDetail = db.Slike.Find(guid);
                if (fileDetail == null)
                {
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return Json(new { Result = "Error" });
                }

                //Obrisi je iz baze
                db.Slike.Remove(fileDetail);
                db.SaveChanges();

                //obriši path na kojem se nalazi
                var path = Path.Combine("~/img/", fileDetail.SlikeID + fileDetail.Ekstenzija);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpGet]
        public IActionResult Detalji(int NekretninaID)
        {

            Nekretnina x = db.Nekretnine
          .Include(g => g.Kategorija)
          .Include(t => t.Lokacija)
          .Include(k => k.Lokacija.Grad)
          .Include(t => t.Lokacija.Grad.Drzava)

          .Where(x => x.NekretninaID == NekretninaID).FirstOrDefault();



            NekretninaDodajVm model = new NekretninaDodajVm
            {
                NekretninaID = NekretninaID,
                Naziv = x.Naziv,
                Opis = x.Opis,
                Kvadratura = x.Kvadratura,
                Cijena = x.Cijena,
                BrojSoba = x.BrojSoba,
                LokacijaID = x.LokacijaID,
                GradID = x.Lokacija.Grad.GradID,
                DrzavaID = x.Lokacija.Grad.Drzava.DrzavaID,
                KategorijeID = x.KategorijaID,




                Kategorije = db.Kategorije.Select(k => new SelectListItem
                {
                    Value = k.KategorijaID.ToString(),
                    Text = k.KategorijaNaziv
                }).ToList(),
                Lokacija = db.Lokacije.Select(k => new SelectListItem
                {
                    Value = k.LokacijaID.ToString(),
                    Text = k.Naziv
                }).ToList(),
                Grad = db.Gradovi.Select(k => new SelectListItem
                {
                    Value = k.GradID.ToString(),
                    Text = k.Naziv
                }).ToList(),
                Drzava = db.Drzave.Select(k => new SelectListItem
                {
                    Value = k.DrzavaID.ToString(),
                    Text = k.Naziv
                }).ToList(),
                Karakteristike = db.NekretninaKarakteristike
                .Where(i => i.NekretninaID == NekretninaID)
                .Select(k => new SelectListItem
                {
                    Value = k.Karakteristike.KarakteristikeID.ToString(),
                    Text = k.Karakteristike.Naziv
                }).ToList(),
                Slike = db.Slike.Where(k => k.NekretninaID == NekretninaID).ToList()


            };

            return View(model);
        }


        //validirano fe i be
        [HttpPost]
        public IActionResult Snimi(NekretninaDodajVm input)
        {
            Nekretnina x;

            

            if (input.NekretninaID == 0)
            {
                //ako je dodavanje
                x = new Nekretnina();
                //db.Nekretnine.Add(x);
                if (!ModelState.IsValid)
                    return Redirect("Dodaj");

                List<Slike> fileDetails = new List<Slike>();
                    foreach (var slike in Request.Form.Files)

                    {


                        if (slike != null && slike.Length > 0)
                        {
                            var fileName = Path.GetFileName(slike.FileName);
                            Slike fileDetail = new Slike()
                            {
                                MyImage = fileName,
                                Ekstenzija = Path.GetExtension(fileName),
                                SlikeID = Guid.NewGuid()
                            };
                            fileDetails.Add(fileDetail);
                            string uploadsFolder = Path.Combine(host.WebRootPath, "img");
                            var path = Path.Combine(uploadsFolder, fileDetail.SlikeID + fileDetail.Ekstenzija);
                            slike.CopyTo(new FileStream(path, FileMode.Create));
                        }
                    }

                    x.Slike = fileDetails;
                    db.Nekretnine.Add(x);
                    //db.SaveChanges();

                


            }

            else
            {
                //ako je uređivanje
                x = db.Nekretnine
                     .Include(i => i.Lokacija)
                     .Include(i => i.Lokacija.Grad)
                     .Include(i => i.Lokacija.Grad.Drzava)

                     .Where(i => i.NekretninaID == input.NekretninaID)
                     .FirstOrDefault();
                if (!ModelState.IsValid)
                    return Redirect("Uredi?NekretninaID="+ input.NekretninaID);

                List<Slike> fileDetails = new List<Slike>();
                    foreach (var slike in Request.Form.Files)

                    {


                        if (slike != null && slike.Length > 0)
                        {
                            var fileName = Path.GetFileName(slike.FileName);
                            Slike fileDetail = new Slike()
                            {
                                MyImage = fileName,
                                Ekstenzija = Path.GetExtension(fileName),
                                SlikeID = Guid.NewGuid(),
                                NekretninaID = input.NekretninaID //nekretnina za koju se mijenja slika
                            };
                            fileDetails.Add(fileDetail);
                            string uploadsFolder = Path.Combine(host.WebRootPath, "img");
                            var path = Path.Combine(uploadsFolder, fileDetail.SlikeID + fileDetail.Ekstenzija);
                            slike.CopyTo(new FileStream(path, FileMode.Create));

                            db.Entry(fileDetail).State = EntityState.Added; //db.Slike.Add(fileDetail);
                        }
                    }

                    db.Entry(x).State = EntityState.Modified; 

                    db.SaveChanges();

                



            }



            x.NekretninaID = input.NekretninaID;
            x.KategorijaID = input.KategorijeID;
            x.LokacijaID = input.LokacijaID;
            x.BrojSoba = input.BrojSoba ?? 0;
            x.Cijena = input.Cijena ?? 0;
            x.Kvadratura = input.Kvadratura ?? 0;
            x.Naziv = input.Naziv;
            x.Opis = input.Opis;


            
            db.SaveChanges();


            Lokacija y = db.Lokacije.Find(input.LokacijaID);

            y.GradID = input.GradID;
           

            db.SaveChanges();

            Grad z = db.Gradovi.Find(y.GradID);

            z.DrzavaID = input.DrzavaID;

            db.SaveChanges();


            var nekretninaKarakteristike = db.NekretninaKarakteristike
                .Include(i => i.Karakteristike)
                .Include(i => i.Nekretnina)
                .Where(i => i.NekretninaID == input.NekretninaID)
               .Select(i => i.KarakteristikeID)
                .ToList();

            var inputedKarakteristike = input.KarakteristikeID.ToList();


            if (input.NekretninaID != 0 && inputedKarakteristike != null)
            {
                //prvo obrisati višak
                var visakNekretnina = nekretninaKarakteristike.Except(inputedKarakteristike);
                foreach (var brisi in visakNekretnina)
                {

                    var zaBrisanje = db.NekretninaKarakteristike.Where(i => i.KarakteristikeID == brisi && i.NekretninaID==input.NekretninaID).FirstOrDefault();

                    db.Remove(zaBrisanje);
                }


                db.SaveChanges();


                //sada proci kroz sve inpute i provjeravati da li su karakteristike vec unesene, ako nisu unijeti ih, ako jesu - nista,neka ih
                foreach (var k in inputedKarakteristike)
                {
                    if (!nekretninaKarakteristike.Contains(k))
                    {
                        NekretninaKarakteristike nova = new NekretninaKarakteristike
                        {
                            NekretninaID = input.NekretninaID,
                            KarakteristikeID = k
                        };
                        db.NekretninaKarakteristike.Add(nova);
                    }



                }


                db.SaveChanges();

            }
            else
            {
                foreach (var k in inputedKarakteristike)
                {

                    NekretninaKarakteristike nova = new NekretninaKarakteristike
                    {
                        NekretninaID = x.NekretninaID,
                        KarakteristikeID = k
                    };
                    db.NekretninaKarakteristike.Add(nova);

                }


                db.SaveChanges();
            }


            TempData["WarningMessage"] = "Uspješno ste snimili nekretninu";
            return Redirect("/Nekretnina/Prikazi");


        }

        public IActionResult Uredi(int NekretninaID)
        {
            if (NekretninaID == 0)
            {
                return StatusCode(400);
            }


            Nekretnina x = db.Nekretnine
           .Include(g => g.Kategorija)
           .Include(t => t.Lokacija)
           .Include(k => k.Lokacija.Grad)
           .Include(t => t.Lokacija.Grad.Drzava)
           .Include(t => t.Slike)
           .Where(x => x.NekretninaID == NekretninaID).FirstOrDefault();

            if (x == null)
            {
                return StatusCode(404);
            }

            var selected = db.NekretninaKarakteristike.Where(i => i.NekretninaID == x.NekretninaID).Select(i => i.KarakteristikeID).ToList();

            NekretninaDodajVm model = new NekretninaDodajVm
            {
                NekretninaID = NekretninaID,
                Naziv = x.Naziv,
                Opis = x.Opis,
                Kvadratura = x.Kvadratura,
                Cijena = x.Cijena,
                BrojSoba = x.BrojSoba,
                LokacijaID = x.LokacijaID,
                GradID = x.Lokacija.Grad.GradID,
                DrzavaID = x.Lokacija.Grad.Drzava.DrzavaID,
                KategorijeID = x.KategorijaID,



                Kategorije = db.Kategorije.Select(k => new SelectListItem
                {
                    Value = k.KategorijaID.ToString(),
                    Text = k.KategorijaNaziv
                }).ToList(),
                Lokacija = db.Lokacije.Select(k => new SelectListItem
                {
                    Value = k.LokacijaID.ToString(),
                    Text = k.Naziv
                }).ToList(),
                Grad = db.Gradovi.Select(k => new SelectListItem
                {
                    Value = k.GradID.ToString(),
                    Text = k.Naziv
                }).ToList(),
                Drzava = db.Drzave.Select(k => new SelectListItem
                {
                    Value = k.DrzavaID.ToString(),
                    Text = k.Naziv
                }).ToList(),

                Karakteristike =

                db.Karakteristike

                .Select(k => new SelectListItem
                {
                    Value = k.KarakteristikeID.ToString(),
                    Text = k.Naziv,
                    Selected = selected.Contains(k.KarakteristikeID)

                }).ToList(),
                Slike = db.Slike.Where(k => k.NekretninaID == NekretninaID).ToList()

            };

            return View("Dodaj", model);
        }
        //Drugi sprint
        public IActionResult Dodaj()
        {


            NekretninaDodajVm model = new NekretninaDodajVm
            {
                Kategorije = db.Kategorije.Select(k => new SelectListItem
                {
                    Value = k.KategorijaID.ToString(),
                    Text = k.KategorijaNaziv
                }).ToList(),
                Lokacija = db.Lokacije.Select(k => new SelectListItem
                {
                    Value = k.LokacijaID.ToString(),
                    Text = k.Naziv
                }).ToList(),
                Grad = db.Gradovi.Select(k => new SelectListItem
                {
                    Value = k.GradID.ToString(),
                    Text = k.Naziv
                }).ToList(),
                Drzava = db.Drzave.Select(k => new SelectListItem
                {
                    Value = k.DrzavaID.ToString(),
                    Text = k.Naziv
                }).ToList(),
                Karakteristike = db.Karakteristike.Select(k => new SelectListItem
                {
                    Value = k.KarakteristikeID.ToString(),
                    Text = k.Naziv
                }).ToList()



            };


            return View(model);
        }

       
        public IActionResult Obrisi(int NekretninaID)
        {


            Nekretnina zaBrisanje = db.Nekretnine.Find(NekretninaID);
            Oglas oglas = db.Oglas.Where(i => i.NekretninaID == zaBrisanje.NekretninaID).FirstOrDefault();

            
            var obilasci = db.Obilasci.Where(i => i.NekretninaID == zaBrisanje.NekretninaID).ToList();
            var noti = db.Notifikacije.ToList();
            Ugovor ugovore = db.Ugovori.Where(i => i.NekretninaID == zaBrisanje.NekretninaID).FirstOrDefault();
            Uplata uplata = db.Uplate.Where(i => i.NekretninaID == zaBrisanje.NekretninaID).FirstOrDefault();

            if (zaBrisanje != null)
            {
                if (ugovore != null || uplata != null)
                {
                    TempData["warningMessage"] = "Ovu nekretninu je nemoguće obrisati!";
                    return RedirectToAction("Prikazi");
                }
                else
                {
                    if (obilasci != null)
                    {
                        //prodji kroz sve notifikacije sa tim obilaskom i obriši ih
                       
                           foreach(var o in obilasci)
                            {
                                foreach (var n in noti)
                                {
                                if (n.ObilazakID == o.ObilazakID)
                                {
                                    db.Notifikacije.Remove(n);
                                    db.SaveChanges();
                                }
                            }
                        }
                        //prodji kroz sve obilaske te  nekretnine i obriši ih
                        foreach(var o in obilasci)
                        {
                            
                                db.Obilasci.Remove(o);
                                db.SaveChanges();
                            
                        }

                    }
                    
                    if (oglas != null)
                    {
                        db.Remove(oglas);
                        db.SaveChanges();

                    }

                    db.Remove(zaBrisanje);
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Prikazi");

        }

        public IActionResult Prikazi()
        {

            NekretninePrikazVM model = new NekretninePrikazVM

            {
                rows = db.Nekretnine.Select(n => new Row
                {
                    NekretninaID = n.NekretninaID,

           
                    Naziv = n.Naziv,
                    Kategorija = n.Kategorija.KategorijaNaziv
                }).ToList()
            };

            

            return View(model);
        }

        public int BrojKategorija()
        {
            List<Kategorija> kategorije = db.Kategorije.ToList();
            int broj = kategorije.Count();
            return broj;
        }
    }
}