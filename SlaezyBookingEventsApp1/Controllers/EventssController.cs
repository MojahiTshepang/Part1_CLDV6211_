using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SlaezyBookingEventsApp1.Models;

namespace SlaezyBookingEventsApp1.Controllers
{
    public class EventssController : Controller
    {
        private SleazyEventDBContext db = new SleazyEventDBContext();

        // GET: Eventss
        public ActionResult Index()
        {
            return View(db.Eventsses.ToList());
        }

        // GET: Eventss/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eventss eventss = db.Eventsses.Find(id);
            if (eventss == null)
            {
                return HttpNotFound();
            }
            return View(eventss);
        }

        // GET: Eventss/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Eventss/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventssID,Name,Surname,Email")] Eventss eventss)
        {
            if (ModelState.IsValid)
            {
                db.Eventsses.Add(eventss);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eventss);
        }

        // GET: Eventss/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eventss eventss = db.Eventsses.Find(id);
            if (eventss == null)
            {
                return HttpNotFound();
            }
            return View(eventss);
        }

        // POST: Eventss/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventssID,Name,Surname,Email")] Eventss eventss)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventss).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eventss);
        }

        // GET: Eventss/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eventss eventss = db.Eventsses.Find(id);
            if (eventss == null)
            {
                return HttpNotFound();
            }
            return View(eventss);
        }

        // POST: Eventss/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Eventss eventss = db.Eventsses.Find(id);
            db.Eventsses.Remove(eventss);
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
