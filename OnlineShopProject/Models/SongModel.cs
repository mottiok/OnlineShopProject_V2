using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopProject.Models
{
    public class SongModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AlbumModel Album { get; set; }
        public int AlbumId { get; set; }
        public int Duration { get; set; }
    }
}