using OnlineShopProject.CurrencyReference;

namespace OnlineShopProject.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
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
            #region Countries

            CountryModel israel = new CountryModel { Country = "Israel" };

            context.CountryModels.AddOrUpdate(israel,
                    new CountryModel() { Country = "Spain" },
                    new CountryModel() { Country = "Canada" },
                    new CountryModel() { Country = "Brazil" },
                    new CountryModel() { Country = "Egypt" },
                    new CountryModel() { Country = "Japan" });

            #endregion

            #region Currency

            CurrencyModel defaultCurrency = new CurrencyModel { Sign = "$", Currency = Currency.USD };
            context.CurrencyModels.AddOrUpdate(defaultCurrency, new CurrencyModel { Sign = "₪", Currency = Currency.ILS }, new CurrencyModel { Sign = "€", Currency = Currency.EUR });

            #endregion

            #region Genres

            GenreModel genre1 = new GenreModel { Name = "Pop" };
            GenreModel genre2 = new GenreModel { Name = "Rock" };
            GenreModel genre3 = new GenreModel { Name = "R&B" };
            GenreModel genre4 = new GenreModel { Name = "Country" };
            context.GenreModels.AddOrUpdate(genre1, genre2, genre3, genre4);

            #endregion

            #region Artists

            ArtistModel artist = new ArtistModel { Name = "Maroon 5" };
            ArtistModel artist2 = new ArtistModel { Name = "Taylor Swift" };
            ArtistModel artist3 = new ArtistModel { Name = "Ed Sheeran" };
            ArtistModel artist4 = new ArtistModel { Name = "Lana Del Rey" };
            ArtistModel artist5 = new ArtistModel { Name = "Metallica" };
            ArtistModel artist6 = new ArtistModel { Name = "AC / DC" };
            ArtistModel artist7 = new ArtistModel { Name = "Stevie Wonder" };
            ArtistModel artist8 = new ArtistModel { Name = "Bruno Mars" };
            ArtistModel artist9 = new ArtistModel { Name = "Clint Black" };
            ArtistModel artist10 = new ArtistModel { Name = "Beyonce" };
            ArtistModel artist11 = new ArtistModel { Name = "Rihanna" };

            context.ArtistModels.AddOrUpdate(artist, artist2, artist3, artist4, artist5, artist6, artist7, artist8, artist9, artist10, artist11);

            #endregion

            #region Users

            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(context);
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(userStore);

            ApplicationUser paula = new ApplicationUser { UserName = "paula@gmail.com", Email = "paula@gmail.com", CartModel = new CartModel(), CurrencyModel = defaultCurrency };
            ApplicationUser andy = new ApplicationUser { UserName = "andy@gmail.com", Email = "andy@gmail.com", CartModel = new CartModel(), CurrencyModel = defaultCurrency };
            ApplicationUser tim = new ApplicationUser { UserName = "tim@gmail.com", Email = "tim@gmail.com", CartModel = new CartModel(), CurrencyModel = defaultCurrency };
            ApplicationUser mobs = new ApplicationUser { UserName = "tmobs@gmail.com", Email = "tmobs@gmail.com", CartModel = new CartModel(), CurrencyModel = defaultCurrency };

            userManager.Create(paula, "Password1!");
            userManager.Create(andy, "Password1!");
            userManager.Create(tim, "Password1!");
            userManager.Create(mobs, "Password1!");

            #endregion

            #region Admin Role & Power User

            ApplicationUser admin = new ApplicationUser { UserName = "admin@gmail.com", Email = "admin@gmail.com", CartModel = new CartModel(), CurrencyModel = defaultCurrency };
            userManager.Create(admin, "password");
            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole { Name = "Admins" });
            context.SaveChanges();

            userManager.AddToRole(admin.Id, "Admins");

            #endregion

            #region Maroon 5

            #region V

            AlbumModel vDelux = new AlbumModel
            {
                Genre = genre1,
                ReleaseDate = new DateTime(2015, 01, 01),
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
                ApplicationUser = paula
            });

            vDelux.Reviews.Add(new ReviewModel
            {
                AlbumModel = vDelux,
                Content = "This is, in my opinion, the best Maroon 5 album of their career! That's saying a lot, as every album has been solid. There is something to be said for taking a year or two between albums. If you notice, those always seem to be the biggest hits. I believe Maroon 5 has another hit on their hands with V. Their first single, \"Maps\", tells a gripping story that's helped by a fantastic video. \"Animals\" is probably my favorite track. Then again, there's the track \"In Your Pocket\", which is so catchy. ",
                CreatedAt = DateTime.Now,
                Rating = 5,
                ApplicationUser = andy
            });

            context.AlbumModels.AddOrUpdate(vDelux);

            #endregion

            #region Over Exposed

            AlbumModel overExposed = new AlbumModel
            {
                Genre = genre1,
                ReleaseDate = new DateTime(2014, 01, 01),
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
                ApplicationUser = tim
            });

            context.AlbumModels.AddOrUpdate(overExposed);

            #endregion

            #region Songs About Jane

            AlbumModel songsAboutJane = new AlbumModel
            {
                Genre = genre1,
                ReleaseDate = new DateTime(2002, 6, 25),
                Artist = artist,
                Name = "Songs About Jane",
                Price = 5.0,
                ImagePath = "/Uploads/Albums/CD_Covers/SongsAboutJane.jpg"
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
                ApplicationUser = mobs
            });

            context.AlbumModels.AddOrUpdate(songsAboutJane);

            #endregion

            #endregion

            #region 1989

            AlbumModel taylor = new AlbumModel
            {
                Genre = genre1,
                ReleaseDate = new DateTime(2015, 04, 01),
                Artist = artist2,
                Name = "1989",
                Price = 12.38,
                ImagePath = "/Uploads/Albums/CD_Covers/1989.jpg"
            };

            taylor.Songs = new System.Collections.Generic.List<SongModel>();

            taylor.Songs.Add(new SongModel { Album = taylor, Duration = 180 + 9, Name = "Welcome To New York" });
            taylor.Songs.Add(new SongModel { Album = taylor, Duration = 180 + 51, Name = "Blank Space" });
            taylor.Songs.Add(new SongModel { Album = taylor, Duration = 180 + 59, Name = "Style" });
            taylor.Songs.Add(new SongModel { Album = taylor, Duration = 180 + 58, Name = "Out Of The Woods" });
            taylor.Songs.Add(new SongModel { Album = taylor, Duration = 180 + 55, Name = "All You Had To Do Was Stay" });

            taylor.Reviews = new System.Collections.Generic.List<ReviewModel>();

            taylor.Reviews.Add(new ReviewModel
            {
                AlbumModel = taylor,
                Content = "LOVE it!",
                CreatedAt = DateTime.Now,
                Rating = 5,
                ApplicationUser = mobs
            });

            context.AlbumModels.AddOrUpdate(taylor);
            #endregion

            #region X
            AlbumModel X = new AlbumModel
            {
                Genre = genre1,
                ReleaseDate = new DateTime(2013, 04, 07),
                Artist = artist3,
                Name = "X",
                Price = 11.59,
                ImagePath = "/Uploads/Albums/CD_Covers/X.jpg"
            };

            X.Songs = new System.Collections.Generic.List<SongModel>();

            X.Songs.Add(new SongModel { Album = X, Duration = 180 + 9, Name = "One" });
            X.Songs.Add(new SongModel { Album = X, Duration = 180 + 51, Name = "I'm A Mess" });
            X.Songs.Add(new SongModel { Album = X, Duration = 180 + 59, Name = "Sing" });
            X.Songs.Add(new SongModel { Album = X, Duration = 180 + 58, Name = "Don't" });
            X.Songs.Add(new SongModel { Album = X, Duration = 180 + 55, Name = "Nina" });

            X.Reviews = new System.Collections.Generic.List<ReviewModel>();

            X.Reviews.Add(new ReviewModel
            {
                AlbumModel = X,
                Content = "This album is amazing. It shows Ed's ability and diversity.",
                CreatedAt = DateTime.Now,
                Rating = 4,
                ApplicationUser = mobs
            });

            context.AlbumModels.AddOrUpdate(X);

            #endregion

            # region DooWopsHooligans
            AlbumModel DooWopsHooligans = new AlbumModel
            {
                Genre = genre3,
                ReleaseDate = new DateTime(1970, 08, 08),
                Artist = artist8,
                Name = "Doo - Wops & Hooligans",
                Price = 5.99,
                ImagePath = "/Uploads/Albums/CD_Covers/DooWops&Hooligans.jpg"
            };

            DooWopsHooligans.Songs = new System.Collections.Generic.List<SongModel>();

            DooWopsHooligans.Songs.Add(new SongModel { Album = DooWopsHooligans, Duration = 180 + 9, Name = "Grenade" });
            DooWopsHooligans.Songs.Add(new SongModel { Album = DooWopsHooligans, Duration = 180 + 51, Name = "Just The Way You Are" });
            DooWopsHooligans.Songs.Add(new SongModel { Album = DooWopsHooligans, Duration = 180 + 59, Name = "Our First Time" });
            DooWopsHooligans.Songs.Add(new SongModel { Album = DooWopsHooligans, Duration = 180 + 58, Name = "Runaway Baby" });
            DooWopsHooligans.Songs.Add(new SongModel { Album = DooWopsHooligans, Duration = 180 + 55, Name = "The Lazy Song" });
            DooWopsHooligans.Reviews = new System.Collections.Generic.List<ReviewModel>();

            DooWopsHooligans.Reviews.Add(new ReviewModel
            {
                AlbumModel = DooWopsHooligans,
                Content = "mega hits",
                CreatedAt = DateTime.Now,
                Rating = 5,
                ApplicationUser = mobs
            });

            context.AlbumModels.AddOrUpdate(DooWopsHooligans);
            #endregion

            #region Honeymoon
            AlbumModel Honeymoon = new AlbumModel
            {
                Genre = genre2,
                ReleaseDate = new DateTime(1990, 07, 07),
                Artist = artist4,
                Name = "Honeymoon",
                Price = 10.62,
                ImagePath = "/Uploads/Albums/CD_Covers/Honeymoon.jpg"
            };

            Honeymoon.Songs = new System.Collections.Generic.List<SongModel>();

            Honeymoon.Songs.Add(new SongModel { Album = Honeymoon, Duration = 180 + 9, Name = "Honeymoon" });
            Honeymoon.Songs.Add(new SongModel { Album = Honeymoon, Duration = 180 + 51, Name = "Music To Watch Boys To" });
            Honeymoon.Songs.Add(new SongModel { Album = Honeymoon, Duration = 180 + 59, Name = "Terrence Loves You" });
            Honeymoon.Songs.Add(new SongModel { Album = Honeymoon, Duration = 180 + 58, Name = "God Knows I Tried" });
            Honeymoon.Songs.Add(new SongModel { Album = Honeymoon, Duration = 180 + 55, Name = " High By The Beach" });

            Honeymoon.Reviews = new System.Collections.Generic.List<ReviewModel>();

            Honeymoon.Reviews.Add(new ReviewModel
            {
                AlbumModel = Honeymoon,
                Content = "Great!",
                CreatedAt = DateTime.Now,
                Rating = 4,
                ApplicationUser = mobs
            });

            context.AlbumModels.AddOrUpdate(Honeymoon);

            #endregion

            #region Metallica
            AlbumModel Metallica = new AlbumModel
            {
                Genre = genre2,
                ReleaseDate = new DateTime(1985, 05, 05),
                Artist = artist5,
                Name = "Metallica",
                Price = 5.00,
                ImagePath = "/Uploads/Albums/CD_Covers/Metallica.jpg"
            };

            Metallica.Songs = new System.Collections.Generic.List<SongModel>();

            Metallica.Songs.Add(new SongModel { Album = Metallica, Duration = 180 + 9, Name = "Enter Sandman" });
            Metallica.Songs.Add(new SongModel { Album = Metallica, Duration = 180 + 51, Name = "Sad But True" });
            Metallica.Songs.Add(new SongModel { Album = Metallica, Duration = 180 + 59, Name = "The Unforgiven" });
            Metallica.Songs.Add(new SongModel { Album = Metallica, Duration = 180 + 58, Name = "Wherever I May Roam" });
            Metallica.Songs.Add(new SongModel { Album = Metallica, Duration = 180 + 55, Name = "Through The Never" });

            Metallica.Reviews = new System.Collections.Generic.List<ReviewModel>();

            Metallica.Reviews.Add(new ReviewModel
            {
                AlbumModel = Metallica,
                Content = "Metallica's breakthrough album.",
                CreatedAt = DateTime.Now,
                Rating = 4,
                ApplicationUser = mobs
            });

            context.AlbumModels.AddOrUpdate(Metallica);

            #endregion

            #region Back in Black
            AlbumModel BackInBlack = new AlbumModel
            {
                Genre = genre2,
                ReleaseDate = new DateTime(1999, 04, 04),
                Artist = artist6,
                Name = "Back in Black",
                Price = 9.00,
                ImagePath = "/Uploads/Albums/CD_Covers/BackInBlack.jpg"
            };

            BackInBlack.Songs = new System.Collections.Generic.List<SongModel>();

            BackInBlack.Songs.Add(new SongModel { Album = BackInBlack, Duration = 180 + 9, Name = "Hells Bells" });
            BackInBlack.Songs.Add(new SongModel { Album = BackInBlack, Duration = 180 + 51, Name = "Shoot To Thrill" });
            BackInBlack.Songs.Add(new SongModel { Album = BackInBlack, Duration = 180 + 59, Name = "What Do You Do For Money Honey" });
            BackInBlack.Songs.Add(new SongModel { Album = BackInBlack, Duration = 180 + 58, Name = "Givin The Dog A Bone" });
            BackInBlack.Songs.Add(new SongModel { Album = BackInBlack, Duration = 180 + 55, Name = "Let Me Put My Love Into You" });

            BackInBlack.Reviews = new System.Collections.Generic.List<ReviewModel>();

            BackInBlack.Reviews.Add(new ReviewModel
            {
                AlbumModel = BackInBlack,
                Content = "This album is amazing. It shows Ed's ability and diversity.",
                CreatedAt = DateTime.Now,
                Rating = 4,
                ApplicationUser = mobs
            });

            context.AlbumModels.AddOrUpdate(BackInBlack);

            #endregion

            #region TheDefinitiveCollection
            AlbumModel TheDefinitiveCollection = new AlbumModel
            {
                Genre = genre3,
                ReleaseDate = new DateTime(2005, 01, 01),
                Artist = artist7,
                Name = "The Definitive Collection",
                Price = 7.76,
                ImagePath = "/Uploads/Albums/CD_Covers/TheDefinitiveCollection.jpg"
            };

            TheDefinitiveCollection.Songs = new System.Collections.Generic.List<SongModel>();

            TheDefinitiveCollection.Songs.Add(new SongModel { Album = TheDefinitiveCollection, Duration = 180 + 9, Name = "Fingertips" });
            TheDefinitiveCollection.Songs.Add(new SongModel { Album = TheDefinitiveCollection, Duration = 180 + 51, Name = "Uptight" });
            TheDefinitiveCollection.Songs.Add(new SongModel { Album = TheDefinitiveCollection, Duration = 180 + 59, Name = "Hey Love" });
            TheDefinitiveCollection.Songs.Add(new SongModel { Album = TheDefinitiveCollection, Duration = 180 + 58, Name = "I Was Made To Love Her" });
            TheDefinitiveCollection.Songs.Add(new SongModel { Album = TheDefinitiveCollection, Duration = 180 + 55, Name = "For Once In My Life" });

            TheDefinitiveCollection.Reviews = new System.Collections.Generic.List<ReviewModel>();

            TheDefinitiveCollection.Reviews.Add(new ReviewModel
            {
                AlbumModel = TheDefinitiveCollection,
                Content = "This album is amazing. It shows Ed's ability and diversity.",
                CreatedAt = DateTime.Now,
                Rating = 4,
                ApplicationUser = mobs
            });

            context.AlbumModels.AddOrUpdate(TheDefinitiveCollection);

            #endregion

            #region On Purpose
            AlbumModel OnPurpose = new AlbumModel
            {
                Genre = genre4,
                ReleaseDate = new DateTime(2007, 07, 07),
                Artist = artist9,
                Name = "On Purpose",
                Price = 10.00,
                ImagePath = "/Uploads/Albums/CD_Covers/OnPurpose.jpg"
            };

            OnPurpose.Songs = new System.Collections.Generic.List<SongModel>();

            OnPurpose.Songs.Add(new SongModel { Album = OnPurpose, Duration = 180 + 9, Name = "Time For That" });
            OnPurpose.Songs.Add(new SongModel { Album = OnPurpose, Duration = 180 + 51, Name = "Doing It Now" });
            OnPurpose.Songs.Add(new SongModel { Album = OnPurpose, Duration = 180 + 59, Name = "One Way" });
            OnPurpose.Songs.Add(new SongModel { Album = OnPurpose, Duration = 180 + 58, Name = "Making You Smile" });
            OnPurpose.Songs.Add(new SongModel { Album = OnPurpose, Duration = 180 + 55, Name = "Better and Worse" });

            OnPurpose.Reviews = new System.Collections.Generic.List<ReviewModel>();

            OnPurpose.Reviews.Add(new ReviewModel
            {
                AlbumModel = OnPurpose,
                Content = "So great to hear new Clint Black. He hasn't changed",
                CreatedAt = DateTime.Now,
                Rating = 4,
                ApplicationUser = mobs
            });

            context.AlbumModels.AddOrUpdate(OnPurpose);

            #endregion

            #region Beyonce
            AlbumModel Beyonce = new AlbumModel
            {
                Genre = genre3,
                ReleaseDate = new DateTime(2001, 01, 01),
                Artist = artist10,
                Name = "Beyonce",
                Price = 08.46,
                ImagePath = "/Uploads/Albums/CD_Covers/Beyonce.jpg"
            };

            Beyonce.Songs = new System.Collections.Generic.List<SongModel>();

            Beyonce.Songs.Add(new SongModel { Album = Beyonce, Duration = 180 + 9, Name = "Pretty Hurts" });
            Beyonce.Songs.Add(new SongModel { Album = Beyonce, Duration = 180 + 51, Name = "Haunted" });
            Beyonce.Songs.Add(new SongModel { Album = Beyonce, Duration = 180 + 59, Name = "Drunk in Love" });
            Beyonce.Songs.Add(new SongModel { Album = Beyonce, Duration = 180 + 58, Name = "Blow" });
            Beyonce.Songs.Add(new SongModel { Album = Beyonce, Duration = 180 + 55, Name = "Angel" });

            Beyonce.Reviews = new System.Collections.Generic.List<ReviewModel>();

            Beyonce.Reviews.Add(new ReviewModel
            {
                AlbumModel = Beyonce,
                Content = "This album is amazing",
                CreatedAt = DateTime.Now,
                Rating = 5,
                ApplicationUser = mobs
            });

            context.AlbumModels.AddOrUpdate(Beyonce);

            #endregion

            #region 4
            AlbumModel four = new AlbumModel
            {
                Genre = genre3,
                ReleaseDate = new DateTime(1993, 04, 04),
                Artist = artist10,
                Name = "4",
                Price = 5.86,
                ImagePath = "/Uploads/Albums/CD_Covers/4.jpg"
            };

            four.Songs = new System.Collections.Generic.List<SongModel>();

            four.Songs.Add(new SongModel { Album = four, Duration = 180 + 9, Name = "Love On Top" });
            four.Songs.Add(new SongModel { Album = four, Duration = 180 + 51, Name = "Party" });
            four.Songs.Add(new SongModel { Album = four, Duration = 180 + 59, Name = "Schoolin' Life" });
            four.Songs.Add(new SongModel { Album = four, Duration = 180 + 58, Name = "Countdown" });
            four.Songs.Add(new SongModel { Album = four, Duration = 180 + 55, Name = "I Miss You" });

            four.Reviews = new System.Collections.Generic.List<ReviewModel>();

            four.Reviews.Add(new ReviewModel
            {
                AlbumModel = four,
                Content = "This album is amazing",
                CreatedAt = DateTime.Now,
                Rating = 5,
                ApplicationUser = mobs
            });

            context.AlbumModels.AddOrUpdate(four);

            #endregion

            #region IAmSashaFierce
            AlbumModel IAmSashaFierce = new AlbumModel
            {
                Genre = genre3,
                ReleaseDate = new DateTime(2005, 05, 05),
                Artist = artist10,
                Name = "I Am Sasha Fierce",
                Price = 5.92,
                ImagePath = "/Uploads/Albums/CD_Covers/IAmSashaFierce.jpg"
            };

            IAmSashaFierce.Songs = new System.Collections.Generic.List<SongModel>();

            IAmSashaFierce.Songs.Add(new SongModel { Album = IAmSashaFierce, Duration = 180 + 9, Name = "If I Were A Boy" });
            IAmSashaFierce.Songs.Add(new SongModel { Album = IAmSashaFierce, Duration = 180 + 51, Name = "Halo" });
            IAmSashaFierce.Songs.Add(new SongModel { Album = IAmSashaFierce, Duration = 180 + 59, Name = "Disappear" });
            IAmSashaFierce.Songs.Add(new SongModel { Album = IAmSashaFierce, Duration = 180 + 58, Name = "Broken-Hearted Girl" });
            IAmSashaFierce.Songs.Add(new SongModel { Album = IAmSashaFierce, Duration = 180 + 55, Name = "Ave Maria" });

            IAmSashaFierce.Reviews = new System.Collections.Generic.List<ReviewModel>();

            IAmSashaFierce.Reviews.Add(new ReviewModel
            {
                AlbumModel = IAmSashaFierce,
                Content = "This album is amazing",
                CreatedAt = DateTime.Now,
                Rating = 5,
                ApplicationUser = mobs
            });

            context.AlbumModels.AddOrUpdate(IAmSashaFierce);

            #endregion

            #region DangerouslyinLove
            AlbumModel DangerouslyinLove = new AlbumModel
            {
                Genre = genre3,
                ReleaseDate = new DateTime(1989, 01, 01),
                Artist = artist10,
                Name = "Dangerously in Love",
                Price = 4.99,
                ImagePath = "/Uploads/Albums/CD_Covers/DangerouslyinLove.jpg"
            };

            DangerouslyinLove.Songs = new System.Collections.Generic.List<SongModel>();

            DangerouslyinLove.Songs.Add(new SongModel { Album = DangerouslyinLove, Duration = 180 + 9, Name = "Daddy" });
            DangerouslyinLove.Songs.Add(new SongModel { Album = DangerouslyinLove, Duration = 180 + 51, Name = "Crazy In Love (Featuring Jay-Z)" });
            DangerouslyinLove.Songs.Add(new SongModel { Album = DangerouslyinLove, Duration = 180 + 59, Name = "Naughty Girl" });
            DangerouslyinLove.Songs.Add(new SongModel { Album = DangerouslyinLove, Duration = 180 + 58, Name = "Baby Boy (Featuring Sean Paul)" });
            DangerouslyinLove.Songs.Add(new SongModel { Album = DangerouslyinLove, Duration = 180 + 55, Name = "Hip Hop Star (Featuring Big Boi And Sleepy Brown)" });

            DangerouslyinLove.Reviews = new System.Collections.Generic.List<ReviewModel>();

            DangerouslyinLove.Reviews.Add(new ReviewModel
            {
                AlbumModel = DangerouslyinLove,
                Content = "This album is amazing",
                CreatedAt = DateTime.Now,
                Rating = 5,
                ApplicationUser = mobs
            });

            context.AlbumModels.AddOrUpdate(DangerouslyinLove);

            #endregion

            #region UnorthodoxJukebox
            AlbumModel UnorthodoxJukebox = new AlbumModel
            {
                Genre = genre3,
                ReleaseDate = new DateTime(1995, 05, 05),
                Artist = artist8,
                Name = "Unorthodox Jukebox",
                Price = 10.49,
                ImagePath = "/Uploads/Albums/CD_Covers/UnorthodoxJukebox.jpg"
            };

            UnorthodoxJukebox.Songs = new System.Collections.Generic.List<SongModel>();

            UnorthodoxJukebox.Songs.Add(new SongModel { Album = UnorthodoxJukebox, Duration = 180 + 9, Name = "Young Girls" });
            UnorthodoxJukebox.Songs.Add(new SongModel { Album = UnorthodoxJukebox, Duration = 180 + 51, Name = "Locked Out Of Heaven" });
            UnorthodoxJukebox.Songs.Add(new SongModel { Album = UnorthodoxJukebox, Duration = 180 + 59, Name = "Gorilla [Explicit]" });
            UnorthodoxJukebox.Songs.Add(new SongModel { Album = UnorthodoxJukebox, Duration = 180 + 58, Name = "Treasure [Explicit]" });
            UnorthodoxJukebox.Songs.Add(new SongModel { Album = UnorthodoxJukebox, Duration = 180 + 55, Name = "Moonshine" });

            UnorthodoxJukebox.Reviews = new System.Collections.Generic.List<ReviewModel>();

            UnorthodoxJukebox.Reviews.Add(new ReviewModel
            {
                AlbumModel = UnorthodoxJukebox,
                Content = "This album is amazing",
                CreatedAt = DateTime.Now,
                Rating = 5,
                ApplicationUser = mobs
            });

            context.AlbumModels.AddOrUpdate(UnorthodoxJukebox);

            #endregion

            #region Red
            AlbumModel Red = new AlbumModel
            {
                Genre = genre1,
                ReleaseDate = new DateTime(2005, 05, 05),
                Artist = artist2,
                Name = "Red",
                Price = 10.60,
                ImagePath = "/Uploads/Albums/CD_Covers/Red.jpg"
            };

            Red.Songs = new System.Collections.Generic.List<SongModel>();

            Red.Songs.Add(new SongModel { Album = Red, Duration = 180 + 9, Name = "State Of Grace" });
            Red.Songs.Add(new SongModel { Album = Red, Duration = 180 + 51, Name = "Red" });
            Red.Songs.Add(new SongModel { Album = Red, Duration = 180 + 59, Name = "Treacherous" });
            Red.Songs.Add(new SongModel { Album = Red, Duration = 180 + 58, Name = "I Knew You Were Trouble." });
            Red.Songs.Add(new SongModel { Album = Red, Duration = 180 + 55, Name = "All Too Well" });

            Red.Reviews = new System.Collections.Generic.List<ReviewModel>();

            Red.Reviews.Add(new ReviewModel
            {
                AlbumModel = Red,
                Content = "This album is amazing",
                CreatedAt = DateTime.Now,
                Rating = 5,
                ApplicationUser = mobs
            });

            context.AlbumModels.AddOrUpdate(Red);

            #endregion

            #region SpeakNow
            AlbumModel SpeakNow = new AlbumModel
            {
                Genre = genre1,
                ReleaseDate = new DateTime(2008, 08, 08),
                Artist = artist2,
                Name = "Speak Now",
                Price = 9.59,
                ImagePath = "/Uploads/Albums/CD_Covers/SpeakNow.jpg"
            };

            SpeakNow.Songs = new System.Collections.Generic.List<SongModel>();

            SpeakNow.Songs.Add(new SongModel { Album = SpeakNow, Duration = 180 + 9, Name = "Mine" });
            SpeakNow.Songs.Add(new SongModel { Album = SpeakNow, Duration = 180 + 51, Name = "Sparks Fly" });
            SpeakNow.Songs.Add(new SongModel { Album = SpeakNow, Duration = 180 + 59, Name = "Back To December" });
            SpeakNow.Songs.Add(new SongModel { Album = SpeakNow, Duration = 180 + 58, Name = "Speak Now" });
            SpeakNow.Songs.Add(new SongModel { Album = SpeakNow, Duration = 180 + 55, Name = "Dear John" });

            SpeakNow.Reviews = new System.Collections.Generic.List<ReviewModel>();

            SpeakNow.Reviews.Add(new ReviewModel
            {
                AlbumModel = SpeakNow,
                Content = "This album is amazing",
                CreatedAt = DateTime.Now,
                Rating = 5,
                ApplicationUser = mobs
            });

            context.AlbumModels.AddOrUpdate(SpeakNow);

            #endregion

            #region TaylorSwift
            AlbumModel TaylorSwift = new AlbumModel
            {
                Genre = genre1,
                ReleaseDate = new DateTime(2011, 01, 01),
                Artist = artist2,
                Name = "Taylor Swift",
                Price = 7.99,
                ImagePath = "/Uploads/Albums/CD_Covers/TaylorSwift.jpg"
            };

            TaylorSwift.Songs = new System.Collections.Generic.List<SongModel>();

            TaylorSwift.Songs.Add(new SongModel { Album = TaylorSwift, Duration = 180 + 9, Name = "Tim McGraw" });
            TaylorSwift.Songs.Add(new SongModel { Album = TaylorSwift, Duration = 180 + 51, Name = "Picture To Burn (Album Version)" });
            TaylorSwift.Songs.Add(new SongModel { Album = TaylorSwift, Duration = 180 + 59, Name = "Teardrops On My Guitar (Radio Single Remix)" });
            TaylorSwift.Songs.Add(new SongModel { Album = TaylorSwift, Duration = 180 + 58, Name = "A Place in this World" });
            TaylorSwift.Songs.Add(new SongModel { Album = TaylorSwift, Duration = 180 + 55, Name = "Cold As You" });

            TaylorSwift.Reviews = new System.Collections.Generic.List<ReviewModel>();

            TaylorSwift.Reviews.Add(new ReviewModel
            {
                AlbumModel = TaylorSwift,
                Content = "This album is amazing",
                CreatedAt = DateTime.Now,
                Rating = 5,
                ApplicationUser = mobs
            });

            context.AlbumModels.AddOrUpdate(TaylorSwift);

            #endregion

            #region ItWontBeSoonBeforeLong

            AlbumModel ItWontBeSoonBeforeLong = new AlbumModel
            {
                Genre = genre1,
                ReleaseDate = new DateTime(2003, 04, 04),
                Artist = artist,
                Name = "It Won't Be Soon Before Long",
                Price = 5.00,
                ImagePath = "/Uploads/Albums/CD_Covers/ItWontBeSoonBeforeLong.jpg"
            };

            ItWontBeSoonBeforeLong.Songs = new System.Collections.Generic.List<SongModel>();

            ItWontBeSoonBeforeLong.Songs.Add(new SongModel { Album = ItWontBeSoonBeforeLong, Duration = 180 + 9, Name = "Maps" });
            ItWontBeSoonBeforeLong.Songs.Add(new SongModel { Album = ItWontBeSoonBeforeLong, Duration = 180 + 51, Name = "Animals" });
            ItWontBeSoonBeforeLong.Songs.Add(new SongModel { Album = ItWontBeSoonBeforeLong, Duration = 180 + 59, Name = "It Was Always You" });
            ItWontBeSoonBeforeLong.Songs.Add(new SongModel { Album = ItWontBeSoonBeforeLong, Duration = 180 + 58, Name = "Unkiss Me" });
            ItWontBeSoonBeforeLong.Songs.Add(new SongModel { Album = ItWontBeSoonBeforeLong, Duration = 180 + 55, Name = "Sugar [Explicit]" });

            ItWontBeSoonBeforeLong.Reviews = new System.Collections.Generic.List<ReviewModel>();

            ItWontBeSoonBeforeLong.Reviews.Add(new ReviewModel
            {
                AlbumModel = ItWontBeSoonBeforeLong,
                Content = "LOVE it!",
                CreatedAt = DateTime.Now,
                Rating = 5,
                ApplicationUser = paula
            });

            context.AlbumModels.AddOrUpdate(ItWontBeSoonBeforeLong);

            #endregion

            #region HandsAllOver

            AlbumModel HandsAllOver = new AlbumModel
            {
                Genre = genre1,
                ReleaseDate = new DateTime(2005, 05, 05),
                Artist = artist,
                Name = "Hands All Over",
                Price = 9.49,
                ImagePath = "/Uploads/Albums/CD_Covers/HandsAllOver.jpg"
            };

            HandsAllOver.Songs = new System.Collections.Generic.List<SongModel>();

            HandsAllOver.Songs.Add(new SongModel { Album = HandsAllOver, Duration = 180 + 9, Name = "Maps" });
            HandsAllOver.Songs.Add(new SongModel { Album = HandsAllOver, Duration = 180 + 51, Name = "Animals" });
            HandsAllOver.Songs.Add(new SongModel { Album = HandsAllOver, Duration = 180 + 59, Name = "It Was Always You" });
            HandsAllOver.Songs.Add(new SongModel { Album = HandsAllOver, Duration = 180 + 58, Name = "Unkiss Me" });
            HandsAllOver.Songs.Add(new SongModel { Album = HandsAllOver, Duration = 180 + 55, Name = "Sugar [Explicit]" });

            HandsAllOver.Reviews = new System.Collections.Generic.List<ReviewModel>();

            HandsAllOver.Reviews.Add(new ReviewModel
            {
                AlbumModel = HandsAllOver,
                Content = "LOVE it!",
                CreatedAt = DateTime.Now,
                Rating = 5,
                ApplicationUser = paula
            });

            context.AlbumModels.AddOrUpdate(HandsAllOver);

            #endregion

            #region GoodGirlGoneBad

            AlbumModel GoodGirlGoneBad = new AlbumModel
            {
                Genre = genre3,
                ReleaseDate = new DateTime(2001, 01, 01),
                Artist = artist11,
                Name = "Good Girl Gone Bad",
                Price = 5.00,
                ImagePath = "/Uploads/Albums/CD_Covers/GoodGirlGoneBad.jpg"
            };

            GoodGirlGoneBad.Songs = new System.Collections.Generic.List<SongModel>();

            GoodGirlGoneBad.Songs.Add(new SongModel { Album = GoodGirlGoneBad, Duration = 180 + 9, Name = "Umbrella feat. JAY-Z" });
            GoodGirlGoneBad.Songs.Add(new SongModel { Album = GoodGirlGoneBad, Duration = 180 + 51, Name = "Push Up On Me" });
            GoodGirlGoneBad.Songs.Add(new SongModel { Album = GoodGirlGoneBad, Duration = 180 + 59, Name = "Don't Stop The Music" });
            GoodGirlGoneBad.Songs.Add(new SongModel { Album = GoodGirlGoneBad, Duration = 180 + 58, Name = "Breakin' Dishes" });
            GoodGirlGoneBad.Songs.Add(new SongModel { Album = GoodGirlGoneBad, Duration = 180 + 55, Name = "Shut Up and Drive" });

            GoodGirlGoneBad.Reviews = new System.Collections.Generic.List<ReviewModel>();

            GoodGirlGoneBad.Reviews.Add(new ReviewModel
            {
                AlbumModel = GoodGirlGoneBad,
                Content = "LOVE it!",
                CreatedAt = DateTime.Now,
                Rating = 5,
                ApplicationUser = paula
            });

            context.AlbumModels.AddOrUpdate(GoodGirlGoneBad);

            #endregion

            #region Orders

            context.OrderModels.AddOrUpdate(new OrderModel
            {
                ApplicationUser = andy,
                CreatedAt = DateTime.Now,
                BillingDetails = new BillingDetailsModel
                {
                    Address = "Degania 46",
                    City = "Netanya",
                    Country = israel,
                    CreditCardNumber = "123456",
                    CVV2 = 1,
                    ExpirationMonth = 1,
                    ExpirationYear = 2015,
                    FirstName = "Andy",
                    LastName = "Mika",
                    Phone = "12114",
                    ZipCode = "123123"
                }
            },
            new OrderModel
            {
                ApplicationUser = andy,
                CreatedAt = DateTime.Now,
                BillingDetails = new BillingDetailsModel
                {
                    Address = "Uzi Hitman 5",
                    City = "Holon",
                    Country = israel,
                    CreditCardNumber = "123456",
                    CVV2 = 1,
                    ExpirationMonth = 1,
                    ExpirationYear = 2015,
                    FirstName = "Andy",
                    LastName = "Mika",
                    Phone = "12114",
                    ZipCode = "123123"
                }
            },
            new OrderModel
            {
                ApplicationUser = andy,
                CreatedAt = DateTime.Now,
                BillingDetails = new BillingDetailsModel
                {
                    Address = "Borochov 27",
                    City = "Holon",
                    Country = israel,
                    CreditCardNumber = "123456",
                    CVV2 = 1,
                    ExpirationMonth = 1,
                    ExpirationYear = 2015,
                    FirstName = "Andy",
                    LastName = "Mika",
                    Phone = "12114",
                    ZipCode = "123123"
                }
            },
            new OrderModel
            {
                ApplicationUser = andy,
                CreatedAt = DateTime.Now,
                BillingDetails = new BillingDetailsModel
                {
                    Address = "Herzl Street 5",
                    City = "Yehud",
                    Country = israel,
                    CreditCardNumber = "123456",
                    CVV2 = 1,
                    ExpirationMonth = 1,
                    ExpirationYear = 2015,
                    FirstName = "Andy",
                    LastName = "Mika",
                    Phone = "12114",
                    ZipCode = "123123"
                }
            },
            new OrderModel
            {
                ApplicationUser = andy,
                CreatedAt = DateTime.Now,
                BillingDetails = new BillingDetailsModel
                {
                    Address = "Herzl Street 5",
                    City = "Ness Ziyyona",
                    Country = israel,
                    CreditCardNumber = "123456",
                    CVV2 = 1,
                    ExpirationMonth = 1,
                    ExpirationYear = 2015,
                    FirstName = "Andy",
                    LastName = "Mika",
                    Phone = "12114",
                    ZipCode = "123123"
                }
            },
            new OrderModel
            {
                ApplicationUser = andy,
                CreatedAt = DateTime.Now,
                BillingDetails = new BillingDetailsModel
                {
                    Address = "Aluf Albert Mendler 24",
                    City = "Tel Aviv-Yafo",
                    Country = israel,
                    CreditCardNumber = "123456",
                    CVV2 = 1,
                    ExpirationMonth = 1,
                    ExpirationYear = 2015,
                    FirstName = "Andy",
                    LastName = "Mika",
                    Phone = "12114",
                    ZipCode = "123123"
                }
            },
            new OrderModel
            {
                ApplicationUser = andy,
                CreatedAt = DateTime.Now,
                BillingDetails = new BillingDetailsModel
                {
                    Address = "Aluf Albert Mendler 8",
                    City = "Tel Aviv-Yafo",
                    Country = israel,
                    CreditCardNumber = "123456",
                    CVV2 = 1,
                    ExpirationMonth = 1,
                    ExpirationYear = 2015,
                    FirstName = "Andy",
                    LastName = "Mika",
                    Phone = "12114",
                    ZipCode = "123123"
                }
            },
            new OrderModel
            {
                ApplicationUser = andy,
                CreatedAt = DateTime.Now,
                BillingDetails = new BillingDetailsModel
                {
                    Address = "Herzl St 5",
                    City = "Ness Ziyyona",
                    Country = israel,
                    CreditCardNumber = "123456",
                    CVV2 = 1,
                    ExpirationMonth = 1,
                    ExpirationYear = 2015,
                    FirstName = "Andy",
                    LastName = "Mika",
                    Phone = "12114",
                    ZipCode = "123123"
                }
            },
            new OrderModel
            {
                ApplicationUser = andy,
                CreatedAt = DateTime.Now,
                BillingDetails = new BillingDetailsModel
                {
                    Address = "Ben Gurion St",
                    City = "Ness Ziyyona",
                    Country = israel,
                    CreditCardNumber = "123456",
                    CVV2 = 1,
                    ExpirationMonth = 1,
                    ExpirationYear = 2015,
                    FirstName = "Andy",
                    LastName = "Mika",
                    Phone = "12114",
                    ZipCode = "123123"
                }
            },
            new OrderModel
            {
                ApplicationUser = andy,
                CreatedAt = DateTime.Now,
                BillingDetails = new BillingDetailsModel
                {
                    Address = "Yehuda ha-Levi Street 101",
                    City = "Tel Aviv-Yafo",
                    Country = israel,
                    CreditCardNumber = "123456",
                    CVV2 = 1,
                    ExpirationMonth = 1,
                    ExpirationYear = 2015,
                    FirstName = "Andy",
                    LastName = "Mika",
                    Phone = "12114",
                    ZipCode = "123123"
                }
            },
            new OrderModel
            {
                ApplicationUser = andy,
                CreatedAt = DateTime.Now,
                BillingDetails = new BillingDetailsModel
                {
                    Address = "Herzl 2",
                    City = "Rehovot",
                    Country = israel,
                    CreditCardNumber = "123456",
                    CVV2 = 1,
                    ExpirationMonth = 1,
                    ExpirationYear = 2015,
                    FirstName = "Andy",
                    LastName = "Mika",
                    Phone = "12114",
                    ZipCode = "123123"
                }
            });

            #endregion

        }

    }
}