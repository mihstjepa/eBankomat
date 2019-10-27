using AutomatedTellerMachine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomatedTellerMachine.Services
{
    public class TekuciRacunService
    {
        private ApplicationDbContext db;

        public TekuciRacunService(ApplicationDbContext dbContext)
        {
            db = dbContext;
        } 

        public void KreirajTekuciRacun(string ime, string prezime, string idKorisnika, decimal pocetnoStanjeRacuna)
        {
            var sifraRacuna = (123456 + db.TekuciRacuns.Count()).ToString().PadLeft(10, '0');   // Kreiramo id
            TekuciRacun tekuciRacun = new TekuciRacun
            {
                Ime = ime,
                Prezime = prezime,
                IBAN = sifraRacuna,
                Stanje = 0,
                ApplicationUserId = idKorisnika
            };
            db.TekuciRacuns.Add(tekuciRacun);
            db.SaveChanges();
        }
    }
}