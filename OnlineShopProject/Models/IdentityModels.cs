using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace OnlineShopProject.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public CurrencyModel CurrencyModel { get; set; }

        public int CurrenyModelId { get; set; }
        public CartModel CartModel { get; set; }
        public int CartModelId { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<OnlineShopProject.Models.GenreModel> GenreModels { get; set; }

        public System.Data.Entity.DbSet<OnlineShopProject.Models.ArtistModel> ArtistModels { get; set; }

        public System.Data.Entity.DbSet<OnlineShopProject.Models.AlbumModel> AlbumModels { get; set; }

        public System.Data.Entity.DbSet<OnlineShopProject.Models.CountryModel> CountryModels { get; set; }

        public System.Data.Entity.DbSet<OnlineShopProject.Models.BillingDetailsModel> BillingDetailsModels { get; set; }

        public System.Data.Entity.DbSet<OnlineShopProject.Models.CartItemModel> CartItemModels { get; set; }

        public System.Data.Entity.DbSet<OnlineShopProject.Models.CartModel> CartModels { get; set; }

        public System.Data.Entity.DbSet<OnlineShopProject.Models.ImageModel> ImageModels { get; set; }

        public System.Data.Entity.DbSet<OnlineShopProject.Models.OrderModel> OrderModels { get; set; }

        public System.Data.Entity.DbSet<OnlineShopProject.Models.ReviewModel> ReviewModels { get; set; }

        public System.Data.Entity.DbSet<OnlineShopProject.Models.SongModel> SongModels { get; set; }

        public System.Data.Entity.DbSet<OnlineShopProject.Models.OrderItemModel> OrderItemModels { get; set; }

        public System.Data.Entity.DbSet<OnlineShopProject.Models.CurrencyModel> CurrencyModels { get; set; }
    }
}