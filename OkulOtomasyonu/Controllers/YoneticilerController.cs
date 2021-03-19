using OkulOtomasyonu.Models.EntityFramework;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace OkulOtomasyonu.Controllers
{
    public class YoneticilerController : Controller
    {
        private SchoolDatabaseEntities2 db = new SchoolDatabaseEntities2();

        // GET: Yoneticiler
        public ActionResult Index(string sort)
        {
            var list = db.Yoneticiler.ToList();
            if (sort == "kadi")
            {
                list = list.OrderBy(x => x.k_adi).ToList();
            }
            else if (sort == "email")
            {
                list = list.OrderBy(x => x.email).ToList();
            }
            else if (sort == "sgt")
            {
                list = list.OrderBy(x => x.songiristarihi).ToList();
            }

            return View(list);
        }
        [HttpPost]
        public ActionResult Index([Bind(Include = "k_adi,email")] Yoneticiler yoneticiler)
        {

            var yoneticiler2 = db.Yoneticiler.ToList();

            if (yoneticiler.k_adi == null && yoneticiler.email == null)
            {
                yoneticiler2 = db.Yoneticiler.ToList();
                return RedirectToAction("Index/");
            }
            else if (yoneticiler.k_adi != null || yoneticiler.email != null)
            {
                var movies = from m in db.Yoneticiler
                             select m;
                yoneticiler2 = movies.Where(m => m.k_adi.Contains(yoneticiler.k_adi) || m.email.Contains(yoneticiler.email)).ToList();


            }
            if (yoneticiler2.Count() == 0)
            {
                TempData["searcherror"] = "Aradığınız bulunamamıştır.";
                return RedirectToAction("Index/", TempData["searcherror"]);
            }

            return View(yoneticiler2);


        }
        // GET: Yoneticiler/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yoneticiler yoneticiler = db.Yoneticiler.Find(id);
            if (yoneticiler == null)
            {
                return HttpNotFound();
            }
            return View(yoneticiler);
        }

        // GET: Yoneticiler/Create
        public ActionResult Create()
        {
            var list = db.Yoneticiler.ToList();
            var list2 = list.Max(x => x.id);
            ViewBag.maxid = list2 + 1;
            return View();
        }

        // POST: Yoneticiler/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,k_adi,sifre,sifre2,email,gizlisoru,gizlicevap,songiristarihi")] Yoneticiler yoneticiler)
        {
            var list = db.Yoneticiler.ToList();
            var list2 = list.Max(x => x.id);
            ViewBag.maxid = list2 + 1;
            if (ModelState.IsValid)
            {

                var yoneticiler2 = db.Yoneticiler.Where(m => m.id != yoneticiler.id && m.k_adi == yoneticiler.k_adi).ToList();
                var yoneticiler3 = db.Yoneticiler.Where(m => m.id != yoneticiler.id && m.email == yoneticiler.email).ToList();
                if (yoneticiler2.Count() != 0 && yoneticiler3.Count() != 0)
                {
                    TempData["Error3"] = "Bu Kullanıcı adında bir yönetici vardır.";
                    TempData["Error4"] = "Bu E-Mail'e sahip bir yönetici vardır.";
                    return RedirectToAction("Create");
                }
                else if (yoneticiler2.Count() != 0)
                {
                    TempData["Error3"] = "Bu Kullanıcı adında bir yönetici vardır.";
                    return RedirectToAction("Create");
                }
                else if (yoneticiler3.Count() != 0)
                {
                    TempData["Error4"] = "Bu E-Mail'e sahip bir yönetici vardır.";
                    return RedirectToAction("Create");
                }

                else
                {
                    db.Yoneticiler.Add(yoneticiler);
                    db.SaveChanges();
                    TempData["create"] = yoneticiler.k_adi + " kullanıcı adlı yönetici eklendi";
                    return RedirectToAction("Index", TempData["create"]);
                }

            }

            return View(yoneticiler);
        }

        // GET: Yoneticiler/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yoneticiler yoneticiler = db.Yoneticiler.Find(id);
            if (yoneticiler == null)
            {
                return HttpNotFound();
            }
            return View(yoneticiler);
        }

        // POST: Yoneticiler/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,k_adi,sifre,sifre2,email,gizlisoru,gizlicevap,songiristarihi")] Yoneticiler yoneticiler)
        {
            if (ModelState.IsValid)
            {
                var yoneticiler2 = db.Yoneticiler.Where(m => m.id != yoneticiler.id && m.k_adi == yoneticiler.k_adi).ToList();
                var yoneticiler3 = db.Yoneticiler.Where(m => m.id != yoneticiler.id && m.email == yoneticiler.email).ToList();
                if (yoneticiler2.Count() != 0 && yoneticiler3.Count() != 0)
                {
                    TempData["Error3"] = "Bu Kullanıcı adında bir yönetici vardır.";
                    TempData["Error4"] = "Bu E-Mail'e sahip bir yönetici vardır.";
                    return RedirectToAction("Edit/" + yoneticiler.id);
                }
                else if (yoneticiler2.Count() != 0)
                {
                    TempData["Error3"] = "Bu Kullanıcı adında bir yönetici vardır.";
                    return RedirectToAction("Edit/" + yoneticiler.id);
                }
                else if (yoneticiler3.Count() != 0)
                {
                    TempData["Error4"] = "Bu E-Mail'e sahip bir yönetici vardır.";
                    return RedirectToAction("Edit/" + yoneticiler.id);
                }

                else
                {
                    db.Entry(yoneticiler).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["create"] = yoneticiler.k_adi + " kullanıcı adlı yönetici güncellendi";
                    return RedirectToAction("Index", TempData["create"]);
                }

            }
            return View(yoneticiler);
        }

        // GET: Yoneticiler/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Yoneticiler yoneticiler = db.Yoneticiler.Find(id);
            if (yoneticiler == null)
            {
                return HttpNotFound();
            }
            return View(yoneticiler);
        }

        // POST: Yoneticiler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Yoneticiler yoneticiler = db.Yoneticiler.Find(id);
            db.Yoneticiler.Remove(yoneticiler);
            db.SaveChanges();
            TempData["create"] = yoneticiler.k_adi + " kullanıcı adlı yönetici silindi";
            return RedirectToAction("Index", TempData["create"]);
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
