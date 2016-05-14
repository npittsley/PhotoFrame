using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoFrame.Web.Models
{
    public class PhotoInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
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
    }
}