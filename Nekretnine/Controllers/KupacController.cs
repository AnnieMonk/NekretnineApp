using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agencija_za_promet_nekretninama.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Nekretnine.Data.Models;
using Nekretnine.Web.Helper;
using Nekretnine.Web.Hubs;
using Nekretnine.Web.ViewModels;

namespace Nekretnine.Web.Controllers
{
    [Autorizacija(false,true)] 
    public class KupacController : Controller
    {
        private MyContext db;
        private readonly IHubContext<NotificationHub> _hubcontext;

        public KupacController(MyContext context, IHubContext<NotificationHub> hubcontext)
        {
            db = context;
            _hubcontext = hubcontext;
        }
        
        public IActionResult Forma()
        {
            FormaKupacVM_temp model = new FormaKupacVM_temp
            {
                KupacKorisnickoIme = HttpContext.GetLogiraniKorisnik().KorisnickoIme,
                Agenti = db.Uposlenici.Select(i => new SelectListItem
                {
                    Value = i.UposlenikID.ToString(),
                    Text = i.Korisnik.Ime + " " + i.Korisnik.Prezime
                }).ToList(),
                Nekretnine = db.Nekretnine.Select(i => new SelectListItem
                {
                    Value = i.NekretninaID.ToString(),
                    Text = i.Naziv

                }).ToList(),
                Grad = db.Gradovi.Select(i => new SelectListItem
                {
                    Value = i.GradID.ToString(),
                    Text = i.Naziv
                }).ToList(),

                DatumObilaska = DateTime.Now 
                
            };

            return View(model);
        }
        public IActionResult Index()
        {
           


            return View();
        }




       


    }
}