using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MicroApi.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            var userIdentity = new ClaimsIdentity(authenticationType);

            // Add standard claims
            var claims = await manager.GetClaimsAsync(this);
            userIdentity.AddClaims(claims);

            return userIdentity;
        }
    }
    public class UserLogin
    {
        [Key]
        public int ID { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserLogin> UserLogin { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize identity table names if needed
            builder.Entity<ApplicationUser>(entity => {
                entity.ToTable("Users"); // Example: Change the table name for users
            });
            builder.Entity<UserLogin>(entity =>
            {
                entity.ToTable("TB_USER_LOGIN"); // Example: Change the table name for user logins
            });
        }
    }
}