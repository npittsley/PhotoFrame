using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace PhotoFrame.Web.Models
{
    public class PhotoThumbsViewModel
    {
        public  ICollection<Photo> thumbnails = new List<Photo>();
        public PhotoThumbsViewModel(ICollection<Photo> photos)
        {
            thumbnails = new List<Photo>();
            foreach(Photo p in photos){
                Photo q = p;
                q.Bytes = GetThumbnail(p);
                thumbnails.Add(q);
            }
        }
        private Byte[] GetThumbnail(Photo p)
        {
            int maxWidth = 60;
            int maxHeight = 60;
            Byte[] thumb = null;
            MemoryStream ms = new MemoryStream(p.Bytes);
            Image img = Image.FromStream(ms);
            var ratioX = (double)maxWidth / img.Width;
            var ratioY = (double)maxHeight / img.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(img.Width * ratio);
            var newHeight = (int)(img.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(img, 0, 0, newWidth, newHeight);

            ms = new MemoryStream();
            newImage.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);

            return ms.ToArray();
        }
    }
}