using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutomatedTellerMachine.Models
{
    public class Transakcija
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Iznos { get; set; }

        [Required]
        public int TekuciRacunId { get; set; }

        public virtual TekuciRacun TekuciRacun { get; set; }
    }
}