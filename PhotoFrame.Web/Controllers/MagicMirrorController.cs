using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhotoFrame.Web.Models;

using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PhotoFrame.Web.ViewModel;

namespace PhotoFrame.Web.Controllers
{
    public class MagicMirrorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserManager<ApplicationUser> manager;

        public MagicMirrorController()
        {
            db = new ApplicationDbContext();
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }
        // GET: MagicMirror
        public ActionResult Index()
        {
            return View(db.Photos.ToList());
        }
        public async Task<ActionResult> GetNextPhoto(int? currentPhotoId)
        {
            if (ViewBag.PhotoCount==null || ViewBag.PhotoCount < 0) ViewBag.PhotoCount = 0;

            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());
            List<Photo> photos = currentUser.Photos.ToList<Photo>();
            int nextPhoto = GetRandomNumber(photos.Count);
            ViewBag.CurrentPhotoId = photos[nextPhoto].Id;
            ViewBag.PhotoCount += 1;
            return View(photos[nextPhoto]);
        }
        private int GetRandomNumber(int maxValue)
        {
            Random r = new Random();
            int nextNum = r.Next(0, maxValue);
            return nextNum;
        }
        // GET: MagicMirror/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Photo photo = db.Photos.Find(id);
        //    if (photo == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(photo);
        //}

        //// GET: MagicMirror/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: MagicMirror/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,FileName,FriendlyName,UploadDate,Bytes,MimeType,FileExtension")] Photo photo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Photos.Add(photo);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(photo);
        //}

        //// GET: MagicMirror/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Photo photo = db.Photos.Find(id);
        //    if (photo == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(photo);
        //}

        //// POST: MagicMirror/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,FileName,FriendlyName,UploadDate,Bytes,MimeType,FileExtension")] Photo photo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(photo).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(photo);
        //}

        //// GET: MagicMirror/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Photo photo = db.Photos.Find(id);
        //    if (photo == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(photo);
        //}

        //// POST: MagicMirror/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Photo photo = db.Photos.Find(id);
        //    db.Photos.Remove(photo);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
