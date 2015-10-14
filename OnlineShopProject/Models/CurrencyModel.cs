using OnlineShopProject.CurrencyReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopProject.Models
{
    public class CurrencyModel
    {
        public int Id { get; set; }
        public string Sign { get; set; }
        public Currency Currency { get; set; }
    }
}