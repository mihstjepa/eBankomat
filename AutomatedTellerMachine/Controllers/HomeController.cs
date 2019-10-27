using AutomatedTellerMachine.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutomatedTellerMachine.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET /home/index
        [Authorize]                             // Ovaj authorize force-a korisnika da ode na login ukoliko nije registriran
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var tekuciRacunId = db.TekuciRacuns.Where(c => c.ApplicationUserId == userId).First().Id; //dohvaćamo Id tekuceg racuna u DbSetu<TekuciRacuns> koji se poklapa sa tablicom User
            ViewBag.TekuciRacunId = tekuciRacunId;  // Stavljamo id u ViewBag koji će se koristiti u /Transakcija/Uplata

            // Ovo je dio sa UserManagerom - pokušavamo dohvatit Pin (stupac koji smo kreirali) iz tablice AspNetUsers,
            // zatim ga stavit u ViewBag koji šaljemo dalje u Index View (Index.cshtml) na početnoj stranici
            var manager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = manager.FindById(userId);
            ViewBag.Pin = user.Pin;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "eBankomat";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(string poruka)
        {
            // TODO - Pošalji poruku na mail

            ViewBag.Poruka = "Hvala na interesu! Vaša poruka je uspješno poslana.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Poruka = "Imate pitanja? Pošaljite nam poruku.";

            return View();
        }

        public ActionResult Foo()
        {
            return View("Contact");

            // Ne vraća view sa nazivom "Foo", već sa nazivom "Contact"
            // Ne postoji "Foo" view...
        }


        /// <summary>
        /// Vraća serijski broj našeg bankomata.
        /// Po defaultu vraća ALL CAPS rezultat (kao što je defaultno podešeno u ruti /SerijskiBroj).
        /// Ako u ruti navedemo /SerijskiBroj/lower onda vraća u lower case-u.
        /// </summary>
        /// <param name="letterCase"></param>
        /// <returns>Serijski broj bankomata.</returns>
        public ActionResult SerijskiBroj(string letterCase)
        {
            var serijskiBroj = "ASPNETMVC5ATM1969812691000421";    // Ovo je izmišljeni serijski broj našeg bankomata
            if (letterCase == "lower")
            {
                return Content(serijskiBroj.ToLower());
            }
            else
            {
                return Content(serijskiBroj);
            }
        }
    }
}