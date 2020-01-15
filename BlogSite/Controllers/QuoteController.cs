using BlogSite.Models;
using System.Linq;
using System.Web.Mvc;

namespace BlogSite.Controllers
{
    [Authorize]
    public class QuoteController : Controller
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(_db.Quote.ToList());
        }
        public ActionResult Edit(int id)
        {
            var soz = _db.Quote.Where(x => x.QuoteId == id).SingleOrDefault();
            if (soz == null)
                return HttpNotFound();
            return View(soz);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Quote quote)
        {
            var soz = _db.Quote.Where(x => x.QuoteId == quote.QuoteId).SingleOrDefault();
            soz.TheQuote = quote.TheQuote;
            soz.QuoteOwner = quote.QuoteOwner;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Quote quote)
        {
            _db.Quote.Add(quote);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var soz = _db.Quote.Find(id);
            if (soz == null)
                return HttpNotFound();
            _db.Quote.Remove(soz);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}