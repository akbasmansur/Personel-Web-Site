using BlogSite.Models;
using System.Linq;
using System.Web.Mvc;

namespace BlogSite.Controllers
{
    [RoutePrefix("blogadmin")]
    [Authorize]
    public class HomeController : Controller
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        [Route("index")]
        public ActionResult Index()
        {
            ViewBag.BlogSayisi = _db.Blog.ToList().Count;
            ViewBag.YorumSayisi = _db.Comment.ToList().Count;
            ViewBag.KategoriSayisi = _db.Category.ToList().Count;
            ViewBag.ZiyaretSayisi = _db.WebSiteHit.Where(x => x.WebSiteHitId == 2).SingleOrDefault().HitCount;
            return View(_db.Comment.ToList());
        }
    }
}