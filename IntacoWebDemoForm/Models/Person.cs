using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using IntacoWebDemoForm.Extensions;

namespace IntacoWebDemoForm.Models
{
    public class Person
    {
        public int ID { get; set; }
        [CustomValidation(typeof(ValidationMethods), "ValidateString")]
        [Required(ErrorMessage ="To pole jest wymagane.")]
        public string Name { get; set; }
        [CustomValidation(typeof(ValidationMethods), "ValidateString")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string Username { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Niepoprawny adres email.")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string E_Mail { get; set; }
        [DataType(DataType.Text, ErrorMessage ="Niepoprawna nazwa instytucji.")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string Nazwa_Instytucji { get; set;}
        [DataType(DataType.Text, ErrorMessage ="Niepoprawna nazwa miejscowości")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string Miejscowosc { get; set; }
        [DataType(DataType.Text, ErrorMessage ="Niepoprawna nazwa ulicy.")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string Ulica { get; set; }
        [DataType(DataType.PostalCode, ErrorMessage ="Niepoprawny kod pocztowy.")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string Kod_Pocztowy { get; set; }
        [DataType(DataType.PhoneNumber, ErrorMessage ="Niepoprawny numer telefonu.")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public string Telefon { get; set; }
        [Range(typeof(bool), "true", "true", ErrorMessage = "Musisz zaakceptować warunki RODO.")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public Boolean Zgoda_Rodo { get; set;}
        public string Token { get; set; }
        public bool verified { get; set; } = false;
        public bool email_Verified { get; set; } = false;
        [DataType(DataType.Date)]
        public DateTime verified_date { get; set; }
        public string AddName { get; set; }
    }
}
