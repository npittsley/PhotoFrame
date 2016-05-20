﻿using System;
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
        public string FriendlyName { get; set; }

        public int SelectedValue { get; set; }
        [DisplayName("Assigned Personal Days")]
        public virtual ICollection<PersonalDay> PersonalDays { get; set; }

        [DisplayName("All Personal Days")]
        public virtual ICollection<PersonalDay> AllPersonalDays { get; set; }
    }
}