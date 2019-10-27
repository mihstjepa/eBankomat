using AutomatedTellerMachine.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomatedTellerMachine.Controllers
{
    // Authorize - Zabranjuje neregistriranim korisnicima da koriste ovaj kontroler i njegove akcije
    [Authorize] 
    public class TransakcijaController : Controller
    {
        
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Transakcija/Uplata
        public ActionResult Uplata(int tekuciRacunId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Uplata(Transakcija transakcija)
        {
            if (ModelState.IsValid) // Provjerava da li je model validan na server strani
            {
                var uplaceniIznos = transakcija.Iznos;
                db.Transakcije.Add(transakcija);

                var userId = User.Identity.GetUserId();
                var tekuciRacunLogiranogKorisnika = db.TekuciRacuns.Where(c => c.ApplicationUserId == userId).First();

                if (tekuciRacunLogiranogKorisnika != null)
                {
                    tekuciRacunLogiranogKorisnika.Stanje = tekuciRacunLogiranogKorisnika.Stanje + uplaceniIznos;
                }

                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();          // Ako nije validan rendera UplataView kako bi se ispisale error poruke
        }


        // GET: Transakcija/Isplata
        public ActionResult Isplata(int tekuciRacunId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Isplata(Transakcija transakcija)
        {
            if (ModelState.IsValid) // Provjerava da li je model validan na server strani
            {
                var zeljeniIznos = transakcija.Iznos - 2*(transakcija.Iznos);
                transakcija.Iznos = zeljeniIznos;
                db.Transakcije.Add(transakcija);

                var userId = User.Identity.GetUserId();
                var tekuciRacunLogiranogKorisnika = db.TekuciRacuns.Where(c => c.ApplicationUserId == userId).First();

                if (tekuciRacunLogiranogKorisnika != null)
                {
                    tekuciRacunLogiranogKorisnika.Stanje = tekuciRacunLogiranogKorisnika.Stanje + zeljeniIznos;
                }

                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();          // Ako nije validan rendera UplataView kako bi se ispisale error poruke
        }

        // GET: Transakcija/BrzaIsplata
        public ActionResult BrzaIsplata()
        {
            var userId = User.Identity.GetUserId();
            var tekuciRacunLogiranogKorisnika = db.TekuciRacuns.Where(c => c.ApplicationUserId == userId).First();

            if (tekuciRacunLogiranogKorisnika.Stanje > 100)
            {
                Transakcija brzaTransakcija = new Transakcija()
                {
                    Iznos = -100,
                    TekuciRacunId = tekuciRacunLogiranogKorisnika.Id
                };
                db.Transakcije.Add(brzaTransakcija);
                tekuciRacunLogiranogKorisnika.Stanje = tekuciRacunLogiranogKorisnika.Stanje - 100;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Error");
            }
        }

        // GET: Transakcija/IspisTransakcija
        public ActionResult IspisTransakcija()
        {
                var userId = User.Identity.GetUserId();
                var tekuciRacunLogiranogKorisnika = db.TekuciRacuns.Where(c => c.ApplicationUserId == userId).First();

                var rezultat = db.Transakcije.Where(c => c.TekuciRacunId == tekuciRacunLogiranogKorisnika.Id).ToList();

                return View(rezultat);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult IspisTransakcijaDetails(int id)
        {
            var tekuciIzabranogKorisnika = db.TekuciRacuns.Where(c => c.Id == id).First();

            var rezultat = db.Transakcije.Where(c => c.TekuciRacunId == tekuciIzabranogKorisnika.Id).ToList();
            IEnumerable<Transakcija> lista = db.Transakcije.Where(c => c.TekuciRacunId == tekuciIzabranogKorisnika.Id);

            return View("IspisTransakcija", lista);
        }


        // GET: Transakcija/Prijenos
        public ActionResult Prijenos(int tekuciRacunId)
        {
                return View();
        }

        [HttpPost]
        public ActionResult Prijenos(Transakcija transakcija, string IbanPrimatelja)
        {
            var userId = User.Identity.GetUserId();
            var tekuciRacunLogiranogKorisnika = db.TekuciRacuns.Where(c => c.ApplicationUserId == userId).First();

            if (transakcija.Iznos < 0)
            {
                return View("Error");
            }
            if (tekuciRacunLogiranogKorisnika.Stanje > transakcija.Iznos)
            {
                // SKIDANJE IZNOSA SA POŠILJATELJA
                var negativanIznos = transakcija.Iznos - 2 * (transakcija.Iznos);
                var pozitivanIznos = transakcija.Iznos;
                transakcija.Iznos = negativanIznos;
                db.Transakcije.Add(transakcija);

                tekuciRacunLogiranogKorisnika.Stanje = tekuciRacunLogiranogKorisnika.Stanje - pozitivanIznos;

                // DODAVANJE IZNOSA PRIMATELJU
                var tekuciRacunPrimatelja = db.TekuciRacuns.Where(c => c.IBAN == IbanPrimatelja).First();
                Transakcija transakcijaPrimatelja = new Transakcija
                {
                    Iznos = pozitivanIznos,
                    TekuciRacunId = tekuciRacunPrimatelja.Id
                };
                db.Transakcije.Add(transakcijaPrimatelja);
                tekuciRacunPrimatelja.Stanje = tekuciRacunPrimatelja.Stanje + pozitivanIznos;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("Error");
            }
        }

    }
}