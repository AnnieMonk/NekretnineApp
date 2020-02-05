using Agencija_za_promet_nekretninama.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Nekretnine.Data.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nekretnine.Web.Helper;
using Nekretnine.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Nekretnine.Web.Hubs
{
    public class NotificationHub : Hub
    {
        private MyContext db;
       
        public NotificationHub(MyContext context)
        {
            db = context;
        }
        public class User
        {
            public string Id;
            public HashSet<string> connectionIds;
        }

        private static readonly ConcurrentDictionary<string, User> connectedUsers = new ConcurrentDictionary<string, User>();

        public async Task Snimi(string datumObilaska, string gradId, string adresa, string nekretninaId, string agentId)
        {
            

            HttpContext httpContext = Context.GetHttpContext();
            KorisnickiNalog kn = httpContext.GetLogiraniKorisnik();
            Korisnik k = db.Korisnici.Where(x => x.KorisnickiNalogID == kn.KorisnickiNalogID).FirstOrDefault();

            Obilazak x = new Obilazak
            {
                DatumVrijemeStart = DateTime.Parse(datumObilaska),
                DatumVrijemeEnd = DateTime.Parse(datumObilaska).AddHours(1),
                
                KorisnikID = k.KorisnikID, //ovo je kupac
                NekretninaID = Int32.Parse(nekretninaId),
                UposlenikID = Int32.Parse(agentId),
                Lokacija = new Lokacija
                {
                    GradID = Int32.Parse(gradId),
                  
                },
                Adresa=adresa,
                Napomena = null,
                Otkazano = false,
                Zavrseno = false
            };

            db.Obilasci.Add(x);
            db.SaveChanges();

            Notifikacija y = new Notifikacija
            {
                KorisnikID = x.KorisnikID,
                UposlenikID = x.UposlenikID,
                DatumNotifikacije = DateTime.Now,
                ObilazakID = x.ObilazakID,
                Status = "Nepročitana",
                Vidjeno=false,
                TextNotifikacije = " dobili ste novi zahtjev za obilazak od kupca - "
            };
            db.Add(y);
            db.SaveChanges();

            await Notify("added", Int32.Parse(agentId));
        }

        public async Task Notify(string message, int userId)
        {
            
            User receiver;
            if(connectedUsers.TryGetValue(userId.ToString(),out receiver))
            {
                IEnumerable<string> allReceivers;
                lock (receiver.connectionIds)
                {
                    //ako user ima više sesija
                    allReceivers = receiver.connectionIds;
                }
                foreach(var cid in allReceivers) 
                {
                    await Clients.Client(cid).SendAsync("ReceiveNotification", message, userId);
                }
            }
            await Clients.Caller.SendAsync("SuccessfulySubmited");
        }
        
        public override Task OnConnectedAsync()
        {
            HttpContext httpContext = Context.GetHttpContext();
            KorisnickiNalog k = httpContext.GetLogiraniKorisnik();
            Korisnik k1 = db.Korisnici.Where(x => x.KorisnickiNalogID == k.KorisnickiNalogID).FirstOrDefault();
            
            string id;
            string connectionId = Context.ConnectionId;
            
            id = k1.KorisnikID.ToString(); 

            var user = connectedUsers.GetOrAdd(id, _ => new User
            {
                Id = id,
                connectionIds = new HashSet<string>()

            });

            lock (user.connectionIds)
            {
                user.connectionIds.Add(connectionId);
            }

            return base.OnConnectedAsync();
        }

        public async Task ResetNeprocitaneNotifikacije(int? KorisnikID)
        {
            HttpContext httpContext = Context.GetHttpContext();
            KorisnickiNalog kn = httpContext.GetLogiraniKorisnik();
            Korisnik k = db.Korisnici.Where(x => x.KorisnickiNalogID == kn.KorisnickiNalogID).FirstOrDefault();
            Uposlenik u = db.Uposlenici.Where(x => x.KorisnikID == k.KorisnikID).FirstOrDefault();

            //resetuj notifikacije 
            var notifikacijeZaReset = db.Notifikacije.Where(i => i.UposlenikID == u.UposlenikID && i.Vidjeno == false).ToList();

            if (notifikacijeZaReset.Count() > 0)
            {
                foreach (var noti in notifikacijeZaReset)
                {
                    noti.Vidjeno = true;
                  
                }
                db.SaveChanges();
            }

            User receiver;
            if (connectedUsers.TryGetValue(k.KorisnikID.ToString(), out receiver))
            {
                IEnumerable<string> allReceivers;
                lock (receiver.connectionIds)
                {
                    //ako user ima više sesija
                    allReceivers = receiver.connectionIds;
                }
                foreach (var cid in allReceivers)
                {
                    await Clients.Client(cid).SendAsync("ResetNotificationCounter", u.UposlenikID);
                }
            }
        }
    }
}
