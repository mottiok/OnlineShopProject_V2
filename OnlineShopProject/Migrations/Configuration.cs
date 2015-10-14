namespace OnlineShopProject.Migrations
{
    using Microsoft.AspNet.Identity;
    using OnlineShopProject.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OnlineShopProject.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(OnlineShopProject.Models.ApplicationDbContext context)
        {
            System.Diagnostics.Debugger.Launch();

            GenreModel genre = new GenreModel { Name = "Pop" };
            context.GenreModels.AddOrUpdate(genre);

            ArtistModel artist = new ArtistModel { Name = "Maroon 5" };
            context.ArtistModels.AddOrUpdate(artist);

            CartModel cart1 = new CartModel();
            CartModel cart2 = new CartModel();
            CartModel cart3 = new CartModel();
            CartModel cart4 = new CartModel();

            #region Maroon 5

            #region V

            AlbumModel vDelux = new AlbumModel
                {
                    Genre = genre,
                    ReleaseDate = DateTime.Now,
                    Artist = artist,
                    Name = "V (Delux)",
                    Price = 12.99,
                    ImagePath = "/Uploads/Albums/CD_Covers/V.jpg"
                };

            vDelux.Songs = new System.Collections.Generic.List<SongModel>();

            vDelux.Songs.Add(new SongModel { Album = vDelux, Duration = 180 + 9, Name = "Maps" });
            vDelux.Songs.Add(new SongModel { Album = vDelux, Duration = 180 + 51, Name = "Animals" });
            vDelux.Songs.Add(new SongModel { Album = vDelux, Duration = 180 + 59, Name = "It Was Always You" });
            vDelux.Songs.Add(new SongModel { Album = vDelux, Duration = 180 + 58, Name = "Unkiss Me" });
            vDelux.Songs.Add(new SongModel { Album = vDelux, Duration = 180 + 55, Name = "Sugar [Explicit]" });
            vDelux.Songs.Add(new SongModel { Album = vDelux, Duration = 180 + 23, Name = "Leaving California" });
            vDelux.Songs.Add(new SongModel { Album = vDelux, Duration = 180 + 39, Name = "In Your Pocket [Explicit]" });
            vDelux.Songs.Add(new SongModel { Album = vDelux, Duration = 180 + 16, Name = "New Love [Explicit]" });
            vDelux.Songs.Add(new SongModel { Album = vDelux, Duration = 180 + 47, Name = "Coming Back For You" });
            vDelux.Songs.Add(new SongModel { Album = vDelux, Duration = 180 + 14, Name = "Feelings [Explicit]" });
            vDelux.Songs.Add(new SongModel { Album = vDelux, Duration = 180 + 56, Name = "My Heart Is Open [feat. Gwen Stefani]" });

            vDelux.Reviews = new System.Collections.Generic.List<ReviewModel>();

            vDelux.Reviews.Add(new ReviewModel
            {
                AlbumModel = vDelux,
                Content = "LOVE it! I listen to Maroon 5 when I go out walking/running ... their music energizes/motivates me and makes the time fly by. There isn't a song on here I don't like which rarely happens....usually have to skip through some. I especially like \"Maps\" but \"It Was Always You\" has to be my favorite!",
                CreatedAt = DateTime.Now,
                Rating = 5,
                ApplicationUser = new ApplicationUser { UserName = "Paula", PasswordHash = new PasswordHasher().HashPassword("Password1"), Email = "paula@gmail.com", CartModel = cart1 }
            });

            vDelux.Reviews.Add(new ReviewModel
            {
                AlbumModel = vDelux,
                Content = "This is, in my opinion, the best Maroon 5 album of their career! That's saying a lot, as every album has been solid. There is something to be said for taking a year or two between albums. If you notice, those always seem to be the biggest hits. I believe Maroon 5 has another hit on their hands with V. Their first single, \"Maps\", tells a gripping story that's helped by a fantastic video. \"Animals\" is probably my favorite track. Then again, there's the track \"In Your Pocket\", which is so catchy. ",
                CreatedAt = DateTime.Now,
                Rating = 5,
                ApplicationUser = new ApplicationUser { UserName = "Andy", PasswordHash = new PasswordHasher().HashPassword("Password1"), Email = "andy@gmail.com", CartModel = cart2 }
            });

            context.AlbumModels.AddOrUpdate(vDelux);

            #endregion

            #region Over Exposed

            AlbumModel overExposed = new AlbumModel
                {
                    Genre = genre,
                    ReleaseDate = DateTime.Now,
                    Artist = artist,
                    Name = "Overexposed",
                    Price = 9.49,
                    ImagePath = "/Uploads/Albums/CD_Covers/Overexposed.jpg"
                };

            overExposed.Songs = new System.Collections.Generic.List<SongModel>();

            overExposed.Songs.Add(new SongModel { Album = overExposed, Duration = 180 + 39, Name = "One More Night" });
            overExposed.Songs.Add(new SongModel { Album = overExposed, Duration = 180 + 51, Name = "Payphone" });
            overExposed.Songs.Add(new SongModel { Album = overExposed, Duration = 180 + 45, Name = "Daylight" });
            overExposed.Songs.Add(new SongModel { Album = overExposed, Duration = 180 + 5, Name = "Lucky Strike" });
            overExposed.Songs.Add(new SongModel { Album = overExposed, Duration = 180 + 25, Name = "The Man Who Never Lied" });
            overExposed.Songs.Add(new SongModel { Album = overExposed, Duration = 180 + 49, Name = "Love Somebody" });
            overExposed.Songs.Add(new SongModel { Album = overExposed, Duration = 120 + 44, Name = "Ladykiller" });
            overExposed.Songs.Add(new SongModel { Album = overExposed, Duration = 180 + 23, Name = "Fortune Teller" });
            overExposed.Songs.Add(new SongModel { Album = overExposed, Duration = 180 + 14, Name = "Sad" });

            overExposed.Reviews = new System.Collections.Generic.List<ReviewModel>();

            overExposed.Reviews.Add(new ReviewModel
            {
                AlbumModel = overExposed,
                Content = "The irony of titling their new album \"Overexposed\" must have been lost on Adam Levine and Maroon 5. The album is overproduced, over-compressed and over-just-about-everything.",
                CreatedAt = DateTime.Now,
                Rating = 3,
                ApplicationUser = new ApplicationUser { UserName = "Tim", PasswordHash = new PasswordHasher().HashPassword("Password1"), Email = "tim@gmail.com", CartModel = cart3 }
            });

            context.AlbumModels.AddOrUpdate(overExposed);

            #endregion

            #region Songs About Jane

            AlbumModel songsAboutJane = new AlbumModel
                {
                    Genre = genre,
                    ReleaseDate = new DateTime(2002, 6, 25),
                    Artist = artist,
                    Name = "Songs About Jane",
                    Price = 5.0,
                    ImagePath = "/Uploads/Albums/CD_Covers/songsaboutjane.jpg"
                };

            songsAboutJane.Songs = new System.Collections.Generic.List<SongModel>();

            songsAboutJane.Songs.Add(new SongModel { Album = songsAboutJane, Duration = 120 + 55, Name = "Harder to Breathe" });
            songsAboutJane.Songs.Add(new SongModel { Album = songsAboutJane, Duration = 240 + 26, Name = "This Love" });
            songsAboutJane.Songs.Add(new SongModel { Album = songsAboutJane, Duration = 180, Name = "Shiver" });
            songsAboutJane.Songs.Add(new SongModel { Album = songsAboutJane, Duration = 240 + 17, Name = "She Will Be Loved" });
            songsAboutJane.Songs.Add(new SongModel { Album = songsAboutJane, Duration = 180 + 19, Name = "Tangled" });
            songsAboutJane.Songs.Add(new SongModel { Album = songsAboutJane, Duration = 240 + 11, Name = "The Sun" });
            songsAboutJane.Songs.Add(new SongModel { Album = songsAboutJane, Duration = 240 + 36, Name = "Must Get Out" });
            songsAboutJane.Songs.Add(new SongModel { Album = songsAboutJane, Duration = 240 + 6, Name = "Sunday Morning" });
            songsAboutJane.Songs.Add(new SongModel { Album = songsAboutJane, Duration = 240 + 55, Name = "Secret" });
            songsAboutJane.Songs.Add(new SongModel { Album = songsAboutJane, Duration = 180 + 1, Name = "Through with You" });
            songsAboutJane.Songs.Add(new SongModel { Album = songsAboutJane, Duration = 240 + 24, Name = "Not Coming Home" });
            songsAboutJane.Songs.Add(new SongModel { Album = songsAboutJane, Duration = 240 + 31, Name = "Sweetest Goodbye" });

            songsAboutJane.Reviews = new System.Collections.Generic.List<ReviewModel>();

            songsAboutJane.Reviews.Add(new ReviewModel
            {
                AlbumModel = songsAboutJane,
                Content = "Okay, not everyone will agree with me when I call this 'pop', but personally I think Maroon 5 is just about the best pop music around at the moment.",
                CreatedAt = new DateTime(2005, 6, 5),
                Rating = 3,
                ApplicationUser = new ApplicationUser { UserName = "T.MOBS", PasswordHash = new PasswordHasher().HashPassword("Password1"), Email = "tmobs@gmail.com", CartModel = cart4 }
            });

            context.AlbumModels.AddOrUpdate(songsAboutJane);

            #endregion 

            #endregion

            context.CartModels.AddOrUpdate(cart1, cart2, cart3);
        }

    }
}
