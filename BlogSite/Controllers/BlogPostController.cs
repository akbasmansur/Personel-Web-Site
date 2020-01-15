using BlogSite.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BlogSite.Controllers
{
    [Authorize]
    public class BlogPostController : Controller
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        public ActionResult Index()
        {
            _db.Configuration.LazyLoadingEnabled = false;
            return View(_db.Blog.Include("Category").ToList());
        }
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_db.Category, "CategoryId", "CategoryName");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Blog blog, HttpPostedFileBase PicURL)
        {
            if (PicURL != null)
            {
                WebImage blogimage = new WebImage(PicURL.InputStream);

                string imgname = PicURL.FileName;
                blogimage.Resize(300, 300);
                blogimage.Save("~/Uploads/BlogpostImages/" + imgname);

                blog.PicURL = "~/Uploads/BlogpostImages/" + imgname;
            }
            blog.BlogDate = DateTime.UtcNow.ToString("MMMM yyyy", new CultureInfo("tr-TR"));
            _db.Blog.Add(blog);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            ViewBag.CategoryId = new SelectList(_db.Category, "CategoryId", "CategoryName");
            var myBlog = _db.Blog.Where(x => x.BlogId == id).SingleOrDefault();
            if (myBlog == null)
                return HttpNotFound();
            return View(myBlog);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Blog myblog, HttpPostedFileBase PicURL)
        {
            var _blog = _db.Blog.Where(x => x.BlogId == myblog.BlogId).SingleOrDefault();
            if (PicURL != null)
            {
                if (System.IO.File.Exists(Server.MapPath(_blog.PicURL)))
                {
                    System.IO.File.Delete(Server.MapPath(_blog.PicURL));
                }
                WebImage webImage = new WebImage(PicURL.InputStream);
                string filename = PicURL.FileName;
                webImage.Resize(300, 300);
                webImage.Save("~/Uploads/BlogpostImages/" + filename);
                _blog.PicURL = "~/Uploads/BlogpostImages/" + filename;
            }
            _blog.Title = myblog.Title;
            _blog.Content = myblog.Content;
            _blog.CategoryId = myblog.CategoryId;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var blog = _db.Blog.Find(id);
            if (blog == null)
                return HttpNotFound();
            _db.Blog.Remove(blog);
            try
            {
                System.IO.File.Delete(Server.MapPath(blog.PicURL));
            }
            catch
            {
                _db.SaveChanges();
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}