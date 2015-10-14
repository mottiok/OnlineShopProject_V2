using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopProject.Models
{
    public class BillingDetailsModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public short ZipCode { get; set; }
        public CountryModel Country { get; set; }
        public int CountryId { get; set; }
        public int Phone { get; set; }
    }
}