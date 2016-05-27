using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PhotoFrame.Web.Models
{
    public class Holiday
    {
        //Table Data
        //http://www.timeanddate.com/holidays/us/
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Holiday Type")]
        public string HolidayType { get; set; }
        [Display(Name = "Month")]
        public int Month { get; set; }
        [Display(Name = "Day")]
        public int Day { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }

    }
}