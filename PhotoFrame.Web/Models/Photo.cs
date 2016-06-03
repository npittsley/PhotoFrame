using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhotoFrame.Web.Models
{
    public class Photo
    {
        public int Id { get;set;}
        [DisplayName("Original Filename")]
        public string FileName { get; set; }
        [DisplayName("Name")]
        public string FriendlyName { get; set; }
        [DisplayName("Uploaded")]
        public DateTime UploadDate { get; set; }
        public byte[] Bytes { get; set; }
        public string MimeType { get; set; }
        public string FileExtension { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Attributes> Attributes {get; set; }
        public virtual ICollection<Holiday> Holidays { get; set; }
        public virtual ICollection<PersonalDay> PersonalDays { get; set; }

    }
}