using Microsoft.AspNetCore.Http;
using Nekretnine.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agencija_za_promet_nekretninama.Models;
using Microsoft.EntityFrameworkCore;

namespace Nekretnine.Web.Helper
{
    public static class Autentifikacija
    {
        private const string LogiraniKorisnik = "logirani_korisnik";

        public static void SetLogiraniKorisnik(this HttpContext context, KorisnickiNalog nalog, bool snimiUCookie = false)
        {

            MyContext db = context.RequestServices.GetService<MyContext>();

            string stariToken = context.Request.GetCookieJson<string>(LogiraniKorisnik);

            if (stariToken != null)
            {
                AutorizacijskiToken obrisi = db.Tokeni.FirstOrDefault(i => i.Vrijednost == stariToken);

                if (obrisi != null)
                {
                    db.Tokeni.Remove(obrisi);
                    db.SaveChanges();
                }
            }

            if (nalog != null)
            {
                string token = Guid.NewGuid().ToString();

                db.Tokeni.Add(new AutorizacijskiToken
                {
                    Vrijednost = token,
                    KorisnickiNalogID = nalog.KorisnickiNalogID,
                    VrijemeEvidentiranja = DateTime.Now
                });

                db.SaveChanges();
                context.Response.SetCookieJson(LogiraniKorisnik, token);
            }
           
        } 
    

            public static KorisnickiNalog GetLogiraniKorisnik(this HttpContext context)
        {
            
                MyContext db = context.RequestServices.GetService<MyContext>();
            string token = context.Request.GetCookieJson<string>(LogiraniKorisnik);

            if (token == null)
                return null;

            return db.Tokeni
                .Where(x => x.Vrijednost == token)
                .Select(s => s.KorisnickiNalog)
                .SingleOrDefault();
        }
    }
}
