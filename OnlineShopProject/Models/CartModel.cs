using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopProject.Models
{
    public class CartModel
    {
        public int Id { get; set; }
        public List<CartItemModel> CartItems { get; set; }
    }
}