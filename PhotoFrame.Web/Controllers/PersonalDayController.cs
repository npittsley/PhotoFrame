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

namespace PhotoFrame.Web.Controllers
{
    [Authorize]
    public class PersonalDayController : Controller
    {
        private ApplicationDbContext db;
        private UserManager<ApplicationUser> manager;

        public PersonalDayController(){
            db = new ApplicationDbContext();
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }
        
        // GET: /PersonalDay/
        public ActionResult Index()
        {
            return View(db.PersonalDays.ToList());
        }

        // GET: /PersonalDay/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalDay personalday = db.PersonalDays.Find(id);
            if (personalday == null)
            {
                return HttpNotFound();
            }
            return View(personalday);
        }

        // GET: /PersonalDay/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /PersonalDay/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include="Id,Name,InstanceDate,Month,Day,OriginalYear")] PersonalDay personalday)
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId()); 
            if (ModelState.IsValid)
            {
                personalday.User = currentUser;
                db.PersonalDays.Add(personalday);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(personalday);
        }

        // GET: /PersonalDay/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalDay personalday = db.PersonalDays.Find(id);
            if (personalday == null)
            {
                return HttpNotFound();
            }
            return View(personalday);
        }

        // POST: /PersonalDay/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,InstanceDate,Month,Day,OriginalYear")] PersonalDay personalday)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personalday).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personalday);
        }

        // GET: /PersonalDay/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalDay personalday = db.PersonalDays.Find(id);
            if (personalday == null)
            {
                return HttpNotFound();
            }
            return View(personalday);
        }

        // POST: /PersonalDay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonalDay personalday = db.PersonalDays.Find(id);
            db.PersonalDays.Remove(personalday);
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
