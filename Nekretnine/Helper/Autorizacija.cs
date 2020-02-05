using Agencija_za_promet_nekretninama.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Nekretnine.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nekretnine.Web.Helper
{
    public class AutorizacijaAttribute : TypeFilterAttribute
    {
        public AutorizacijaAttribute(bool uposlenik, bool kupac)
            : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { uposlenik, kupac };
        }

        public class MyAuthorizeImpl: IAsyncActionFilter
        {
            public MyAuthorizeImpl(bool uposlenik, bool kupac)
            {
                _uposlenik = uposlenik;
                _kupac = kupac;
            }

            private readonly bool _uposlenik;
            private readonly bool _kupac;

            public async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
            {
                KorisnickiNalog k = filterContext.HttpContext.GetLogiraniKorisnik();

                if(k == null)
                {
                    if(filterContext.Controller is Controller controller)
                    {
                        controller.TempData["errorMessage"] = "Niste prijavljeni";
                    }
                   
                    filterContext.Result = new RedirectToActionResult("Index", "Autentifikacija", new { area = "" });
                    return;
                }

                MyContext db = filterContext.HttpContext.RequestServices.GetService<MyContext>();

                if(_uposlenik && db.Korisnici.Any(i=>i.Uloga.Naziv == "Uposlenik" && i.KorisnickiNalogID == k.KorisnickiNalogID))
                {
                    await next();
                    return;
                }
                if (_kupac && db.Korisnici.Any(i => i.Uloga.Naziv == "Kupac" && i.KorisnickiNalogID == k.KorisnickiNalogID))
                {
                    await next();
                    return;
                }

                if(filterContext.Controller is Controller c1){
                    c1.ViewData["errorMessage"] = "Nemate pravo pristupa";
                }

                filterContext.Result = new RedirectToActionResult("Index", "Autentifikacija", new { area = "" });

            }
        }

    }
}
