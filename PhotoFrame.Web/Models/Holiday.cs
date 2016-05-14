﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoFrame.Web.Models
{
    public class Holiday
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime InstanceDate { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }

    }
}