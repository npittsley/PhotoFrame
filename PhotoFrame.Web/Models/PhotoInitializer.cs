using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;
using System.Data.Entity.Migrations;

using CsvHelper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PhotoFrame.Web.Models
{
    public class PhotoInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            InitializeIdentityForEF(context);
            InitHolidays(context);
            InitPhotos(context);
            base.Seed(context);
            
        }
        private void InitializeIdentityForEF(ApplicationDbContext context)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var myinfo = new UserProfile() { FirstName = "PhotoFrame", LastName = "Admin" };
            string name = "Admin";
            string password = "123456";
            string test = "test";

            //Create Role Test and User Test
            RoleManager.Create(new IdentityRole(test));
            UserManager.Create(new ApplicationUser() { UserName = test });

            //Create Role Admin if it does not exist
            if (!RoleManager.RoleExists(name))
            {
                var roleresult = RoleManager.Create(new IdentityRole(name));
            }

            //Create User=Admin with password=123456
            var user = new ApplicationUser();
            user.UserName = name;
            //user. = "Seattle";
            user.Profile = myinfo;
            var adminresult = UserManager.Create(user, password);

            //Add User Admin to Role Admin
            if (adminresult.Succeeded)
            {
                var result = UserManager.AddToRole(user.Id, name);
            }
        }
        private void InitPhotos(ApplicationDbContext context)
        {
            //var photos = new List<Photo>
            //{
            //new Photo{FileName="one",FriendlyName="Wedding1",UploadDate=DateTime.Parse("2015-05-01"),FileExtension="jpg",MimeType="image"},
            //new Photo{FileName="two",FriendlyName="Wedding2",UploadDate=DateTime.Parse("2015-05-01"),FileExtension="jpg",MimeType="image"}
            //};

            //photos.ForEach(s => context.Photos.Add(s));
            //context.SaveChanges();
            //var personaldays = new List<PersonalDay>
            //{
            //new PersonalDay{Name="Wedding",InstanceDate=DateTime.Parse("2015-05-01"),Month=5,Day=1,OriginalYear=2015},
            //new PersonalDay{Name="Anniversary",InstanceDate=DateTime.Parse("2016-05-01"),Month=5,Day=1,OriginalYear=2016}
            //};
            //personaldays.ForEach(s => context.PersonalDays.Add(s));
            //context.SaveChanges();

            //var holidays = new List<Holiday>
            //{
            //new Holiday{Name="Wedding",InstanceDate=DateTime.Parse("2015-05-01"),Month=5,Day=1},
            //new Holiday{Name="Anniversary",InstanceDate=DateTime.Parse("2016-05-01"),Month=5,Day=1}
            //};
            //holidays.ForEach(s => context.Holidays.Add(s));
            //context.SaveChanges();

        }
        private void InitHolidays(ApplicationDbContext context)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "PhotoFrame.Web.Models.SeedData.USHolidays.csv";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    CsvReader csvReader = new CsvReader(reader);
                    csvReader.Configuration.WillThrowOnMissingField = false;
                    var holidays = csvReader.GetRecords<Holiday>().ToArray();
                    context.Holidays.AddOrUpdate(h => h.Name, holidays);
                }
            }
        }
    }
}