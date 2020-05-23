using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UOBEVENT_Code_first_2.Models;
using Microsoft.AspNet.Identity;


namespace UOBEVENT_Code_first_2.Controllers
{
    public class AttendingsController : Controller
    {
        private EventDBContext db = new EventDBContext();

        // GET: Attendings
        [Authorize]
        public ActionResult Index()
        {
            //var attendings = db.Attendings.Include(a => a.Event);
            string UserId = User.Identity.GetUserName();
            var UserEvent = from p in db.Attendings.Include(a => a.Event)
                            orderby p.AttendID
                            where p.UserID == UserId
                            select p;




            return View(UserEvent);
            //return View(attendings.ToList());
        }

        public ActionResult Leave(int id)
        {
            string UserId = User.Identity.GetUserName();

            var LeaveEvent = from l in db.Attendings
                             where /*l.UserID == UserId &&*/ l.AttendID == id
                             select l;

            foreach (var l in LeaveEvent)
            {
                db.Attendings.Remove(l);
            }

            db.SaveChanges();

            return View();
        }



        // GET: Attendings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attending attending = db.Attendings.Find(id);
            if (attending == null)
            {
                return HttpNotFound();
            }
            return View(attending);
        }

        // GET: Attendings/Create
        public ActionResult Create()
        {
            ViewBag.EventID = new SelectList(db.Events, "EventID", "EventName");
            return View();
        }

        // POST: Attendings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AttendID,EventID,UserID")] Attending attending)
        {
            if (ModelState.IsValid)
            {
                db.Attendings.Add(attending);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventID = new SelectList(db.Events, "EventID", "EventName", attending.EventID);
            return View(attending);
        }

        // GET: Attendings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attending attending = db.Attendings.Find(id);
            if (attending == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventID = new SelectList(db.Events, "EventID", "EventName", attending.EventID);
            return View(attending);
        }

        // POST: Attendings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AttendID,EventID,UserID")] Attending attending)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attending).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventID = new SelectList(db.Events, "EventID", "EventName", attending.EventID);
            return View(attending);
        }

        // GET: Attendings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attending attending = db.Attendings.Find(id);
            if (attending == null)
            {
                return HttpNotFound();
            }
            return View(attending);
        }

        // POST: Attendings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attending attending = db.Attendings.Find(id);
            db.Attendings.Remove(attending);
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

        public ActionResult Guests(int id)
        {




            var Guests = from p in db.Attendings.Include(a => a.Event)
                         orderby p.AttendID
                         where p.EventID == id
                         select p;


            return View(Guests);






        }



    }
}
