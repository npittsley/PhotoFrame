using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoFrame.Web.Models
{
    public class Photo
    {
        public int Id { get;set;}
        public string FileName { get; set; }
        public string FriendlyName { get; set; }
        public DateTime UploadDate { get; set; }
        public byte[] Bytes { get; set; }
        public string MimeType { get; set; }
        public string FileExtension { get; set; }

        public virtual ICollection<Holiday> Holidays { get; set; }
        public virtual ICollection<PersonalDay> PersonalDays { get; set; }

    }
}