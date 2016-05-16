using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoFrame.Web.Models
{
    public class PersonalDay
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime InstanceDate { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int OriginalYear { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }

    }
}