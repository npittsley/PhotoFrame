using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoFrame.Web;
using PhotoFrame.Web.Models;


namespace PhotoFrame.Web.Controllers
{
    public class FileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: File
        public ActionResult Index(int id)
        {
            FileContentResult retVal = null;
            var file = db.Photos.Find(id);
            if (file.Bytes == null || file.Bytes.Length == 0)
            {
                //Student student = db.Students.Include(s => s.Files).SingleOrDefault(s => s.ID == id);
                Photo missingFile = db.Photos.First(p => p.FriendlyName == "FileNotFound");
                //var missingFile = db.Photos.Find(1);
                retVal = File(missingFile.Bytes, missingFile.MimeType);
            }
            else
            {
                retVal=File(file.Bytes, file.MimeType);
            }
            return retVal;
        }
    }
}