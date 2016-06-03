using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace PhotoFrame.Web.Models
{
    public class PersonalDay
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "First Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime InstanceDate { get; set; }
        [Display(Name = "Month")]
        public int Month { get; set; }
        [Display(Name = "Day")]
        public int Day { get; set; }
        [Display(Name = "First Year")]
        public int OriginalYear { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }

    }
}