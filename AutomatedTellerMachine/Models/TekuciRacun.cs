using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutomatedTellerMachine.Models
{
    public class TekuciRacun
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Unos IBAN-a je obavezan.")]
        [StringLength(10)]
        [Column(TypeName ="nvarchar")]
        [RegularExpression(@"\d{6,15}", ErrorMessage ="IBAN mora imati između 6 i 15 znakova.")]
        [Display(Name = "IBAN")]
        public string IBAN { get; set; }

        [Required(ErrorMessage = "Unos imena je obavezan.")]
        [RegularExpression(@"[A-Za-z]{2,40}", ErrorMessage = "Ime mora imati između 2 i 40 slova.")]
        [Display(Name = "Ime")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Unos prezimena je obavezan.")]
        [RegularExpression(@"[A-Za-z]{2,40}", ErrorMessage = "Prezime mora imati između 2 i 40 slova.")]
        [Display(Name = "Prezime")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Unos stanja je obavezan.")]
        [RegularExpression(@"[0-9]+(.[0-9]{2})?", ErrorMessage = "Unesen iznos mora biti cijeli broj ili decimalan broj sa dva decimalna mjesta.")]
        [DataType(DataType.Currency)]
        public decimal Stanje { get; set; }

        public virtual ApplicationUser User { get; set; }   //Vanjski kljuc (objekt)

        [Required]
        [Display(Name ="Šifra korisnika")]
        public string ApplicationUserId { get; set; }       //Vanjski kljuc (id)

        public virtual ICollection<Transakcija> Transakcije { get; set; }
    }
}