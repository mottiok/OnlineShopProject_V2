using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopProject.Models
{
    public class AlbumModel : ItemModel
    {
        public ArtistModel Artist { get; set; }
        public int ArtistId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<SongModel> Songs { get; set; }
        public GenreModel Genre { get; set; }
        public int GenreId { get; set; }
    }
}