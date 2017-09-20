using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Azure.Sample.Models;

namespace Azure.Sample.Controllers
{
    public class TodosController : Controller
    {
        private readonly MyDatabaseContext db = new MyDatabaseContext();

        // GET: Todos
        public ActionResult Index()
        {
            Trace.WriteLine("GET /Todos/Index");
            return View(db.Todoes.ToList());
        }

        // GET: Todos/Details/5
        public ActionResult Details(int? id)
        {
            Trace.WriteLine("GET /Todos/Details/" + id);
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var todo = db.Todoes.Find(id);
            if (todo == null)
                return HttpNotFound();
            return View(todo);
        }

        // GET: Todos/Create
        public ActionResult Create()
        {
            Trace.WriteLine("GET /Todos/Create");
            return View(new Todo {CreatedDate = DateTime.Now});
        }

        // POST: Todos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Description,CreatedDate,Done")] Todo todo)
        {
            Trace.WriteLine("POST /Todos/Create");
            if (ModelState.IsValid)
            {
                db.Todoes.Add(todo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(todo);
        }

        // GET: Todos/Edit/5
        public ActionResult Edit(int? id)
        {
            Trace.WriteLine("GET /Todos/Edit/" + id);
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var todo = db.Todoes.Find(id);
            if (todo == null)
                return HttpNotFound();
            return View(todo);
        }

        // POST: Todos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Description,CreatedDate,Done")] Todo todo)
        {
            Trace.WriteLine("POST /Todos/Edit/" + todo.ID);
            if (ModelState.IsValid)
            {
                db.Entry(todo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(todo);
        }

        // GET: Todos/Delete/5
        public ActionResult Delete(int? id)
        {
            Trace.WriteLine("GET /Todos/Delete/" + id);
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var todo = db.Todoes.Find(id);
            if (todo == null)
                return HttpNotFound();
            return View(todo);
        }

        // POST: Todos/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trace.WriteLine("POST /Todos/Delete/" + id);
            var todo = db.Todoes.Find(id);
            db.Todoes.Remove(todo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET Todos/Attachment/5
        [HttpGet]
        public ActionResult Attachment(int? id)
        {
            Trace.WriteLine("GET /Todos/Attachment/" + id);
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View();
        }

        // POST Todos/Attachment/5
        [HttpPost]
        [ActionName("Attachment")]
        [ValidateAntiForgeryToken]
        public ActionResult UploadAttachment(int id, HttpPostedFileBase attachment)
        {
            Trace.WriteLine("POST /Todos/Attachment/" + id);
            if (attachment == null || attachment.ContentLength == 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}
