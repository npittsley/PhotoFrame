using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using PhotoFrame.Web.Models;

namespace PhotoFrame.Web.ViewModel
{
    public class PhotoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "required")]
        [DisplayName("Name")]
        public string FriendlyName { get; set; }

        [DisplayName("Assigned Personal Days")]
        public virtual ICollection<PersonalDay> PersonalDays { get; set; }

        [DisplayName("Assigned Holidays")]
        public virtual ICollection<Holiday> Holidays { get; set; }

        //[DisplayName("All Personal Days")]
        //public virtual ICollection<PersonalDay> AllPersonalDays { get; set; }

        //[DisplayName("All Holidays")]
        //public virtual ICollection<PersonalDay> AllHolidays { get; set; }
    }
}