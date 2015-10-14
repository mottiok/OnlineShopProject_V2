using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopProject.Models
{
    public class OrderItemModel
    {
        public int Id { get; set; }
        public AlbumModel Album { get; set; }
        public int AlbumId { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public OrderModel OrderModel { get; set; }
        public int OrderModelId { get; set; }
    }
}