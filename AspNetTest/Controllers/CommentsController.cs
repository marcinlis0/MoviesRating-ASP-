using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AspNetTest.Models;
using Microsoft.AspNet.Identity;

namespace AspNetTest.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Movie).Include(c => c.User);
            return View(comments.OrderBy(c => c.Created).ToList());
        }

        // GET: Comments/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: Comments/Create
        [Authorize]
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = new Comment();
            comment.MovieId = Convert.ToInt32(id);

            ViewBag.MovieId = new SelectList(db.Movies, "Id", "Title");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View(comment);
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Rating,Content,Created,MovieId,UserId")] Comment comment)
        {

            ApplicationUser actualUser = null;

            foreach (var user in db.Users)
            {
                if (user.Id == User.Identity.GetUserId())
                    actualUser = user;
            }

            if (actualUser == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (actualUser.Comments.Any(comm => comm.MovieId == comment.MovieId))
                return RedirectToAction("CommentError");

            comment.Created = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details", new { Controller = "Movies", Id = comment.MovieId });
            }

            ViewBag.MovieId = new SelectList(db.Movies, "Id", "Title", comment.MovieId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", comment.UserId);
            return View(comment);
        }

        // GET: Comments/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.MovieId = new SelectList(db.Movies, "Id", "Title", comment.MovieId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", comment.UserId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,Rating,Content,Created,MovieId,UserId")] Comment comment)
        {
            if (comment.UserId != User.Identity.GetUserId() && !User.IsInRole("Admin"))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (ModelState.IsValid)
            {
                comment.Created = DateTime.Now;
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { Controller = "Movies", Id = comment.MovieId });
            }
            ViewBag.MovieId = new SelectList(db.Movies, "Id", "Title", comment.MovieId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", comment.UserId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);

            if (comment.UserId != User.Identity.GetUserId() && !User.IsInRole("Admin"))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Details", new { Controller = "Movies", Id = comment.MovieId });
        }

        public ActionResult CommentError()
        {
            return View();
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
