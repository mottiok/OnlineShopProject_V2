using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShopProject.Models
{
    public class BillingDetailsModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string ZipCode { get; set; }
        public CountryModel Country { get; set; }
        [Required]
        public int CountryId { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string CreditCardNumber { get; set; }
        [Required]
        public int ExpirationMonth { get; set; }
        [Required]
        public int ExpirationYear { get; set; }
        [Required]
        public int CVV2 { get; set; }
    }
}