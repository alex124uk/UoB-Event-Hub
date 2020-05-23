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
using ExtensionMethods;

namespace UOBEVENT_Code_first_2.Controllers
{
    public class EventsController : Controller
    {
        private EventDBContext db = new EventDBContext();

        // GET: Events
        [Authorize]
        public ActionResult Index(string Category, string Location, string searchString,  DateTime? startdate, DateTime? enddate)
        {

        


            var CategoryList = new List<string>();

            var Catqry = from c in db.Events
                         orderby c.EventCategory
                         select c.EventCategory;

            CategoryList.AddRange(Catqry.Distinct());
            ViewBag.Category = new SelectList(CategoryList);




            var events = from e in db.Events
                         orderby e.EventDate
                         where e.EventDate > System.DateTime.Now

                         select e;

            if (!String.IsNullOrEmpty(searchString))
            {
                events = events.Where(s => s.EventName.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(Category))
            {
                events = events.Where(y => y.EventCategory == Category);
            }

            var LocationList = new List<string>();

            var Locqry = from c in db.Events
                         orderby c.EventLocation
                         select c.EventLocation;

            LocationList.AddRange(Locqry.Distinct());
            ViewBag.Location = new SelectList(LocationList);

            if (!string.IsNullOrEmpty(Location))
            {
                events = events.Where(y => y.EventLocation == Location);
            }



            var DateList = new List<string>();

            var date = from d in db.Events
                       where d.EventDate >= startdate && d.EventDate <= enddate
                       select d;

            ViewBag.Date = new SelectList(DateList);

      








            return View(events);
        }

        // GET: Events/Details/5
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




        public ActionResult Join(int id)
        {


            string UserId = User.Identity.GetUserName();
            string UserCourse = User.Identity.GetUserCourse();
            //var url = HttpContext.Request.RequestContext.RouteData.Values["id"]; ;
            //int Eid = System.Convert.ToInt32(url);



            var a = new Attending
            {
                EventID = id,
                UserID = UserId,
                UserCourse = UserCourse

            };
            db.Attendings.Add(a);
            db.SaveChanges();

            return RedirectToAction("Success");


        }

        public ActionResult Success()
        {
            return View();
    }

        public ActionResult Success2()
        {
            return View();
        }


     

        //GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventID,EventName,EventDate,EventTime,EventLocation,EventRoom,EventDescription,EventCategory,CreationDT,EventUserID")] Event @event)
        {
            if (ModelState.IsValid)
            {

                string UserId = User.Identity.GetUserName();
                @event.EventUserID = UserId;

                @event.CreationDT = DateTime.Now;

                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Success2");
            }

            return View(@event);
        }

        // GET: Events/Edit/5
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

           

            if (User.Identity.GetUserName() != @event.EventUserID)
            {
                return new HttpUnauthorizedResult();
            }

            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventID,EventName,EventDate,EventTime,EventLocation,EventDescription,EventCategory,CreationDT,EventUserID")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: Events/Delete/5
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

            if (User.Identity.GetUserName() != @event.EventUserID)
            {
                return new HttpUnauthorizedResult();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
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

        public ActionResult GuestsPV(int id)
        {




            var Guests = from p in db.Attendings.Include(a => a.Event)
                         orderby p.AttendID
                         where p.EventID == id
                         select p;


            return View(Guests);






        }


    }
}


