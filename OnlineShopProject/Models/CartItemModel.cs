using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopProject.Models
{
    public class CartItemModel
    {
        public int Id { get; set; }
        public AlbumModel Album { get; set; }
        public int AlbumId { get; set; }
        public int Quantity { get; set; }
        public CartModel CartModel { get; set; }
        public int CartModelId { get; set; }
    }
}