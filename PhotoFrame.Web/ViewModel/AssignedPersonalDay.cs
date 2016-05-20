using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using PhotoFrame.Web.Models;
using System.Web.Mvc;
namespace PhotoFrame.Web.ViewModel
{
    public class AssignedPersonalDay
    {
        private readonly List<PersonalDay> _days;

        public int Id { get; set; }
        [Display(Name = "Personal Day")]
        public string Name { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Year { get; set; }

        public IEnumerable<SelectListItem> PersonalDayItems
        {
            get { return new SelectList(_days, "Id", "Name"); }
        }
    }
}