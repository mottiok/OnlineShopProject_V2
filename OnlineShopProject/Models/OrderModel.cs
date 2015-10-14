using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopProject.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public BillingDetailsModel BillingDetails { get; set; }
        public int BillingDetailsId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}