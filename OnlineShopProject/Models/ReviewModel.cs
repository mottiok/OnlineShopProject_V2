using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopProject.Models
{
    public class ReviewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public AlbumModel AlbumModel { get; set; }
        public int AlbumModelId { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}