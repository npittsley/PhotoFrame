using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Drawing2D;
using PhotoFrame.Web;
using PhotoFrame.Web.Models;
using System.Threading.Tasks;

using System.IO;

namespace PhotoFrame.Web.Controllers
{
    public class FileController : Controller
    {
        const int THUMB_IMAGE_SIZE = 120;
        const int SMALL_IMAGE_SIZE = 240;
        const int MEDIUM_IMAGE_SIZE = 480;
        const int LARGE_IMAGE_SIZE = 768;

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: File
        public ActionResult Index(int id)
        {
            FileContentResult retVal = null;
            var file = db.Photos.Find(id);
            if (file.Bytes == null || file.Bytes.Length == 0)
            {
                Photo missingFile = db.Photos.First(p => p.FriendlyName == "Photo Not Found");
                retVal = File(missingFile.Bytes, missingFile.MimeType);
            }
            else
            {
                retVal=File(file.Bytes, file.MimeType);
            }
            return retVal;
        }
        public ActionResult GetThumbnail(int id)
        {
            int maxSize = THUMB_IMAGE_SIZE;
            FileContentResult retVal = null;
            var file = db.Photos.Find(id);
            if (file.Bytes == null || file.Bytes.Length == 0)
            {
                Photo missingFile = db.Photos.First(p => p.FriendlyName == "Photo Not Found");
                //retVal = File(missingFile.Bytes, missingFile.MimeType);
                retVal = File(ResizePhoto(missingFile, maxSize), missingFile.MimeType);
            }
            else
            {
                retVal = File(ResizePhoto(file, maxSize), file.MimeType);
            }
            return retVal;
        }
        public ActionResult GetSmallImage(int id)
        {
            return GetImage(id, SMALL_IMAGE_SIZE);
        }
        public ActionResult GetMediumImage(int id)
        {
            return GetImage(id, MEDIUM_IMAGE_SIZE);
        }
        public ActionResult GetLargeImage(int id)
        {
            return GetImage(id, LARGE_IMAGE_SIZE);
        }
        
        public ActionResult GetImage(int id, int maxSize)
        {
            //int size = maxSize;
            FileContentResult retVal = null;
            var file = db.Photos.Find(id);
            if (file.Bytes == null || file.Bytes.Length == 0)
            {
                Photo missingFile = db.Photos.First(p => p.FriendlyName == "Photo Not Found");
                //retVal = File(missingFile.Bytes, missingFile.MimeType);
                retVal = File(ResizePhoto(missingFile, maxSize), missingFile.MimeType);
            }
            else
            {
                retVal = File(ResizePhoto(file, maxSize), file.MimeType);
            }
            return retVal;
        }
        private Byte[] ResizePhoto(Photo p, int maxSize)
        {
            int maxWidth = maxSize;
            int maxHeight = maxSize;
            //Byte[] thumb = null;
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