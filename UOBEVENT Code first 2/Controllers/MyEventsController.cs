using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UOBEVENT_Code_first_2.Models;

namespace UOBEVENT_Code_first_2.Controllers
{
    public class MyEventsController : Controller
    {
        private EventDBContext db = new EventDBContext();

        // GET: MyEvents
        [Authorize]
        public ActionResult Index()
        {
            string UserId = User.Identity.GetUserName();
            var UserEvent = from p in db.Events
                            orderby p.EventUserID
                            where p.EventUserID == UserId
                            select p;




            return View(UserEvent);
            //return View(db.Events.ToList());
        }

        // GET: MyEvents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: MyEvents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MyEvents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventID,EventName,EventDate,EventTime,EventLocation,EventDescription,EventCategory,CreationDT,EventUserID")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@event);
        }

        // GET: MyEvents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }

            //if (User.Identity.GetUserName() != @event.EventUserID)
            //{
            //    return new HttpUnauthorizedResult();
            //}

            return View(@event);
        }

        // POST: MyEvents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventID,EventName,EventDate,EventTime,EventLocation,EventRoom,EventDescription,EventCategory,CreationDT,EventUserID")] Event @event)
        {
            if (ModelState.IsValid)
            {
                string UserId = User.Identity.GetUserName();
                @event.EventUserID = UserId;

                @event.CreationDT = DateTime.Now;
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (User.Identity.GetUserName() != @event.EventUserID)
            {
                return new HttpUnauthorizedResult();
            }

            return View(@event);
        }

        // GET: MyEvents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: MyEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
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
