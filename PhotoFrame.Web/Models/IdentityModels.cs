using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PhotoFrame.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    //public class ApplicationUser : IdentityUser
    //{
    //}

    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //{
    //    public ApplicationDbContext()
    //        : base("DefaultConnection"){}

    //    public DbSet<UserProfile> Profile { get; set; }
    //    public DbSet<Photo> Photos { get; set; }
    //    public DbSet<Holiday> Holidays { get; set; }
    //    public DbSet<PersonalDay> PersonalDays { get; set; }

    //    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //    {
    //        base.OnModelCreating(modelBuilder);
    //        // Change the name of the table to be Users instead of AspNetUsers
    //        modelBuilder.Entity<IdentityUser>()
    //            .ToTable("Users");
    //        modelBuilder.Entity<ApplicationUser>()
    //            .ToTable("Users");
    //        modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
    //    }
    //}
}