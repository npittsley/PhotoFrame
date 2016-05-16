using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PhotoFrame.Web.Models
{
    public class ApplicationUser : IdentityUser
    {

        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<PersonalDay> PersonalDays { get; set; }

        public virtual UserProfile Profile { get; set; }
    }
    public class UserProfile
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }
    public class AppModel
    {

    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection") { }

        public DbSet<UserProfile> Profile { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<PersonalDay> PersonalDays { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Change the name of the table to be Users instead of AspNetUsers
            modelBuilder.Entity<IdentityUser>()
                .ToTable("Users");
            modelBuilder.Entity<ApplicationUser>()
                .ToTable("Users");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}