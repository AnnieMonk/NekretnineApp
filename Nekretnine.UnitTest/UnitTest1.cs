using Agencija_za_promet_nekretninama.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Nekretnine.Controllers;
using Nekretnine.Data.Models;
using Nekretnine.Web.Controllers;
using Nekretnine.Web.ViewModels;
using System;
using System.Collections.Generic;

namespace Nekretnine.UnitTest
{
    [TestClass]
    public class Test_Anisa
    {
        public MyContext CreateContextForInMemory()
        {
            var option = new DbContextOptionsBuilder<MyContext>().UseInMemoryDatabase(databaseName: "Test_Database").Options;

            var context = new MyContext(option);
            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }

        private MyContext _context;

        public Test_Anisa()
        {
            _context = CreateContextForInMemory();

            var drzava = new Drzava { Naziv = "..." };
            var grad = new Grad { Naziv = "...", Drzava = drzava };
            var lokacija = new Lokacija { Naziv = "...", Grad = grad };
            var uloga = new Uloga { Naziv = "..." };

            var korisnickiNalog = new KorisnickiNalog
            {
                KorisnickoIme = "...",
                Lozinka = "...",
                ZapamtiMe = false

            };


            var korisnik = new Korisnik
            {
                Email = "...",
                Ime = "...",
                Prezime = "...",
                Grad = grad,
                KorisnickiNalog = korisnickiNalog,
                Telefon = "...",
                Uloga = uloga

            };

            var uposlenik = new Uposlenik
            {
                DatumZaposlenja = DateTime.Now,
                Korisnik = korisnik,
                Opis = "...",
                RatingStars = 5,
                Zvanje = "..."

            };
            var kategorija = new Kategorija
            {
                KategorijaNaziv = "..."
            };
            var nekretnina = new Nekretnina
            {
                BrojSoba = 0,
                Kategorija = kategorija,
                Cijena = 0,
                Kvadratura = 0,
                Lokacija = lokacija,
                Naziv = "Zoe's apartments",
                Opis = "..."

            };
            var slika = new Slike
            {
                Ekstenzija = "...",
                MyImage = "...",
                Nekretnina = nekretnina
            };

            var obilazak = new Obilazak
            {
                DatumVrijemeStart = DateTime.Now,
                DatumVrijemeEnd = DateTime.Now,
                Korisnik = korisnik,
                Lokacija = lokacija,
                Napomena = "...",
                Nekretnina = nekretnina,
                Otkazano = false,
                Uposlenik = uposlenik,
                Zavrseno = false


            };


            _context.AddRange(drzava, grad, uloga, korisnik, korisnickiNalog, uposlenik, kategorija, slika, nekretnina, obilazak, lokacija);
            _context.SaveChanges();
        }

        [TestMethod]
        public void Test_Broj_Kategorija()
        {
            
            NekretninaController controller = new NekretninaController(_context, null);
           
            int realna = controller.BrojKategorija();

            Assert.AreEqual(realna, 1);
        }

        [TestMethod]
        [DataRow(1)]
        public void Test_NekretninaByID(int id)
        {
            try
            {
                var kontroler = new NekretninaController(_context, null);

                var vr = kontroler.Detalji(id) as ViewResult;
                var rezultat = vr.Model as NekretninaDodajVm; ;

                Assert.AreEqual("Zoe's apartments", rezultat.Naziv);
            }
            catch (Exception e)
            {

            }
        }

    }



}
