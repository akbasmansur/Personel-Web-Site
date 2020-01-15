using BlogSite.Models;
using System.Linq;
using System.Web.Mvc;

namespace BlogSite.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(_db.Category.ToList());
        }
        public ActionResult Edit(int id)
        {
            var category = _db.Category.Where(x => x.CategoryId == id).SingleOrDefault();
            if (category == null)
                return HttpNotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Category _category)
        {
            var category = _db.Category.Where(x => x.CategoryId == _category.CategoryId).SingleOrDefault();
            category.CategoryName = _category.CategoryName;
            category.Description = _category.Description;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Category category)
        {
            _db.Category.Add(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var category = _db.Category.Find(id);
            if (category == null)
                return HttpNotFound();
            _db.Category.Remove(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}