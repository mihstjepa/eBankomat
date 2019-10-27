using AutomatedTellerMachine.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomatedTellerMachine.Controllersa
{
    [Authorize] // Pristup samo logiranim korisnicima
    public class TekuciRacunController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TekuciRacun
        public ActionResult Index()
        {
            return View();
        }

        // GET: TekuciRacun/Details
        public ActionResult Details()
        {
            // Dohvaćamo UserId ulogiranog korisnika
                var userId = User.Identity.GetUserId();

            // Tražimo korisnike koji imaju u tablici TekuciRacun stupac ApplicationUserId 
            // isti kao userId (iznad) ulogiranog korisnika
                var tekuciRacun = db.TekuciRacuns.Where(c => c.ApplicationUserId == userId).First();

            return View(tekuciRacun);
        }


        // GET: TekuciRacun/DetailsForAdmin
        [Authorize(Roles = "Admin")]
        public ActionResult DetailsForAdmin(int id)
        {
            // Trazimo tekuci racun koji ima Id isti kao naš ulazni parametar (id)
                var tekuciRacun = db.TekuciRacuns.Find(id);

            // Prosljeđuje akciju na "Details" view (ne na DetailsForAdmin) jer 
            // ga mozemo ponovno koristiti + prosljeđuje traženi tekuciRacun
            return View("Details", tekuciRacun);
        }

        // GET: TekuciRacun/List
        [Authorize(Roles = "Admin")]
        public ActionResult List()
        {
            // Vraća prikaz svih tekućih računa iz baze
                return View(db.TekuciRacuns.ToList());
        }

        // GET: TekuciRacun/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TekuciRacun/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TekuciRacun/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TekuciRacun/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TekuciRacun/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TekuciRacun/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
