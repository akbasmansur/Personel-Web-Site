using BlogSite.Models;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BlogSite.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        ApplicationDbContext _db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(_db.Project.ToList());
        }
        public ActionResult Edit(int id)
        {
            var project = _db.Project.Where(x => x.ProjectId == id).SingleOrDefault();
            if (project == null)
                return HttpNotFound();
            return View(project);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Project proje, HttpPostedFileBase PicURL)
        {
            var _proje = _db.Project.Where(x => x.ProjectId == proje.ProjectId).SingleOrDefault();
            if (PicURL != null)
            {
                if (System.IO.File.Exists(Server.MapPath(_proje.PicURL)))
                {
                    System.IO.File.Delete(Server.MapPath(_proje.PicURL));
                }
                WebImage webImage = new WebImage(PicURL.InputStream);
                string filename = PicURL.FileName;
                webImage.Resize(1200, 600);
                webImage.Save("~/Uploads/ProjectImages/" + filename);
                _proje.PicURL = "~/Uploads/ProjectImages/" + filename;
            }
            _proje.Name= proje.Name;
            _proje.Content = proje.Content;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Project proje, HttpPostedFileBase myPicURL)
        {
            if (myPicURL != null)
            {
                WebImage projimage = new WebImage(myPicURL.InputStream);

                string imgname = myPicURL.FileName;
                projimage.Resize(1200,600);
                projimage.Save("~/Uploads/ProjectImages/"+imgname);

                proje.PicURL = "~/Uploads/ProjectImages/" + imgname;
            }
            _db.Project.Add(proje);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var proje = _db.Project.Find(id);
            if (proje == null)
                return HttpNotFound();
            _db.Project.Remove(proje);
            try
            {
                System.IO.File.Delete(Server.MapPath(proje.PicURL));
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