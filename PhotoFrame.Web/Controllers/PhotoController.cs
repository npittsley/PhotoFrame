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
    [Authorize]
    public class PhotoController : Controller
    {
        private ApplicationDbContext db;
        private UserManager<ApplicationUser> manager;

        public PhotoController()
        {
            db = new ApplicationDbContext();
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }
        // GET: /Photo/
        public async Task<ActionResult> Index()
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId()); 
            return View(currentUser.Photos);
        }

        // GET: /Photo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            //Student student = db.Students.Include(s => s.Files).SingleOrDefault(s => s.ID == id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // GET: /Photo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Photo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FileName,FriendlyName,UploadDate,Bytes,MimeType,FileExtension")] Photo photo, HttpPostedFileBase upload)
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId()); 
            
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    int index = upload.FileName.LastIndexOf(".");
                    string extension = upload.FileName.Substring(index);
                    
                    photo.User = currentUser;
                    photo.FileName = System.IO.Path.GetFileName(upload.FileName);
                    photo.FriendlyName=photo.FriendlyName;
                    photo.UploadDate=DateTime.Now;
                    photo.FileExtension = extension;
                    photo.MimeType = upload.ContentType;
                    
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        photo.Bytes = reader.ReadBytes(upload.ContentLength);
                    }
                }
                if (photo != null)
                {
                    db.Photos.Add(photo);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            return View(photo);
        }

        // GET: /Photo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            PhotoViewModel vm = new PhotoViewModel
            {
                Id = photo.Id,
                FriendlyName = photo.FriendlyName,
                PersonalDays = photo.PersonalDays,
                Holidays=photo.Holidays
            };
            PopulateAssignedPersonalDays(ref vm, photo);
            PopulateAssignedHolidays(ref vm, photo);
            
            /* This works
            Photo photo = db.Photos.Find(id);
            PhotoViewModel vm = new PhotoViewModel
            {
                Id = photo.Id,
                FriendlyName = photo.FriendlyName,
                PersonalDays=photo.PersonalDays
            };
            PopulateAssignedPersonalDays(ref vm,photo);
            PopulateAssignedHolidays(ref vm, photo);
            if (photo == null)
            {
                return HttpNotFound();
            }
             * /
             */
            return View(vm);
        }
        private void PopulateAssignedPersonalDays(ref PhotoViewModel vm,Photo photo)
        {
            var msPDs=db.PersonalDays.Select(c => new{
                Id=c.Id,
                Name=c.Name
            }).ToList();
            ViewBag.AllPersonalDays = new MultiSelectList(msPDs,"Id","Name",vm.PersonalDays);
        }
        private void PopulateAssignedHolidays(ref PhotoViewModel vm, Photo photo)
        {
            var msHDs = db.Holidays.Select(c => new
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
            ViewBag.AllHolidays = new MultiSelectList(msHDs, "Id", "Name",vm.Holidays);
        }
        // POST: /Photo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPhotoViewModel([Bind(Include = "Id,FriendlyName")] PhotoViewModel vm, int[] personalDaysIds,int[]holidaysIds, HttpPostedFileBase upload)
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());
            Photo photo = (from p in db.Photos where p.Id == vm.Id select p).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (photo != null)
                {
                    //Handle FriendlyName
                    if (photo.FriendlyName != vm.FriendlyName)
                    {
                        photo.FriendlyName = vm.FriendlyName;
                    }
                    //Handle updated image
                    if (upload != null && upload.ContentLength > 0)
                    {
                        int index = upload.FileName.LastIndexOf(".");
                        string extension = upload.FileName.Substring(index);

                        photo.User = currentUser;
                        photo.FileName = System.IO.Path.GetFileName(upload.FileName);
                        photo.FriendlyName = vm.FriendlyName;
                        photo.UploadDate = DateTime.Now;
                        photo.FileExtension = extension;
                        photo.MimeType = upload.ContentType;

                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            photo.Bytes = reader.ReadBytes(upload.ContentLength);
                        }
                    }
                    //Handle Personal Days
                    if (personalDaysIds != null && personalDaysIds.Length > 0)
                    {
                        List<PersonalDay> days = (from pd in db.PersonalDays where personalDaysIds.Contains(pd.Id) select pd).ToList<PersonalDay>();
                        foreach (PersonalDay day in days)
                        {
                            //Is it in there already?
                            if (!photo.PersonalDays.Contains(day))
                            {
                                photo.PersonalDays.Add(day);
                            }
                        }
                    }
                    //Handle HoliDays
                    if (holidaysIds != null && holidaysIds.Length > 0)
                    {
                        List<Holiday> days = (from hd in db.Holidays where holidaysIds.Contains(hd.Id) select hd).ToList<Holiday>();
                        foreach (Holiday day in days)
                        {
                            //Is it in there already?
                            if (!photo.Holidays.Contains(day))
                            {
                                photo.Holidays.Add(day);
                            }
                        }
                    }
                }
                db.Entry(photo).State = EntityState.Modified;
                db.SaveChanges();               
                return RedirectToAction("Index");
            }
            return View(photo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FileName,FriendlyName,UploadDate,Bytes,MimeType,FileExtension")] Photo photo, HttpPostedFileBase upload)
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());

            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    int index = upload.FileName.LastIndexOf(".");
                    string extension = upload.FileName.Substring(index);

                    photo.User = currentUser;
                    photo.FileName = System.IO.Path.GetFileName(upload.FileName);
                    photo.FriendlyName = photo.FriendlyName;
                    photo.UploadDate = DateTime.Now;
                    photo.FileExtension = extension;
                    photo.MimeType = upload.ContentType;

                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        photo.Bytes = reader.ReadBytes(upload.ContentLength);
                    }
                    db.Entry(photo).State = EntityState.Modified;
                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }
            //PopulateAssignedPersonalDays(photo);
            return View(photo);
        }

        // GET: /Photo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Photo photo = db.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View(photo);
        }

        // POST: /Photo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Photo photo = db.Photos.Find(id);
            db.Photos.Remove(photo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
