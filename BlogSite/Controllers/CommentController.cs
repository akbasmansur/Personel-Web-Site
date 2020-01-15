using BlogSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogSite.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Comment
        public ActionResult Index()
        {
            return View(_db.Comment.Include("Blog").OrderByDescending(x=>x.CommentId).ToList());
        }
        public ActionResult Edit(int id)
        {
            var yrm = _db.Comment.Include("Blog").Where(x => x.CommentId == id).SingleOrDefault();
            if (yrm == null)
                return HttpNotFound();
            return View(yrm);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Comment comment)
        {
            var yrm = _db.Comment.Where(x => x.CommentId == comment.CommentId).SingleOrDefault();
            yrm.Confirmation = comment.Confirmation;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}