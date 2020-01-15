using BlogSite.Models;
using System.Linq;
using System.Web.Mvc;

namespace BlogSite.Controllers
{
    [Authorize]
    public class AboutMeController : Controller
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(_db.AboutMe.SingleOrDefault());
        }
        public ActionResult Edit(int id)
        {
            var aboutme = _db.AboutMe.Where(x => x.AboutMeId == id).SingleOrDefault();
            if (aboutme == null)
                return HttpNotFound();
            return View(aboutme);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(AboutMe aboutme)
        {
            var _aboutme = _db.AboutMe.Where(x => x.AboutMeId == aboutme.AboutMeId).SingleOrDefault();
            _aboutme.Title = aboutme.Title;
            _aboutme.Explanation = aboutme.Explanation;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(AboutMe aboutme)
        {
            _db.AboutMe.Add(aboutme);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var about = _db.AboutMe.Find(id);
            if (about == null)
                return HttpNotFound();
            _db.AboutMe.Remove(about);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}