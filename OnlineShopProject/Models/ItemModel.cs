using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopProject.Models
{
    public abstract class ItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImagePath { get; set; }
        public List<ReviewModel> Reviews { get; set; }
    }
}