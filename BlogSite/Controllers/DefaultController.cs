using BlogSite.Models;
using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;

namespace BlogSite.Controllers
{
    public class DefaultController : Controller
    {
        ApplicationDbContext _db = new ApplicationDbContext();

        [Route("")]
        [Route("anasayfa")]
        public ActionResult Index()
        {
            VisitingCountInc();
            var siraliblog = _db.Blog.Include("Category").Include("Comments").OrderByDescending(x => x.BlogId).ToList();
            var sonblog = siraliblog.FirstOrDefault();
            int elementCount = siraliblog.Count;
            if (elementCount != 0)
            {
                ViewBag.YorumSayisi = sonblog.Comments.Count(c => c.Confirmation == true);
                return View(sonblog);
            }
            return View("Error");
        }
        [Route("sozler")]
        public ActionResult Quote()
        {
            VisitingCountInc();
            var soz = _db.Quote.ToList();
            return View(soz);
        }
        [Route("hakkimda")]
        public ActionResult AboutMe()
        {
            VisitingCountInc();
            return View(_db.AboutMe.SingleOrDefault());
        }
        [Route("projeler")]
        public ActionResult Project()
        {
            VisitingCountInc();
            return View(_db.Project.ToList());
        }
        [Route("blog")]
        public ActionResult BlogPost(int? page)
        {
            VisitingCountInc();
            var nesne = _db.Blog.Include("Category").Include("Comments").OrderByDescending(x => x.BlogId).ToList().ToPagedList(page ?? 1, 3);
            return View(nesne);
        }
        [Route("blog/{CategoryName}/{secim:int}")]
        public ActionResult BlogPostByCategory(int secim)
        {
            var nesne = _db.Blog.Include("Category").Include("Comments")
                .Where(x=>x.CategoryId == secim).OrderByDescending(x => x.BlogId).ToList();
            return View(nesne);
        }
        [Route("blog/{Title}-{id:int}")]
        public ActionResult BlogDetails(int id)
        {
            VisitingCountInc();
            var myblog = _db.Blog.Include("Category").Include("Comments").Where(x => x.BlogId == id).FirstOrDefault();
            return View(myblog);
        }
        public ActionResult BlogKategoriPartial()
        {
            return PartialView(_db.Category.Include("Blogs").ToList().OrderBy(x => x.CategoryName));
        }
        [HttpPost]
        public JsonResult DoComment(string adsoyad, string eposta, string icerik, int blogid)
        {
            var yrm = new Comment { Name = adsoyad, E_mail = eposta, BlogId = blogid, Content = icerik, Confirmation = false };
            _db.Comment.Add(yrm);
            _db.SaveChanges();
            return Json(false);
        }
        public void VisitingCountInc(int id = 2)
        {
            var count = _db.WebSiteHit.Where(x => x.WebSiteHitId == id).SingleOrDefault();
            count.HitCount++;
            _db.SaveChanges();
        }
    }
}