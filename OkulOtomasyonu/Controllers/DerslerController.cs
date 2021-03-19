using OkulOtomasyonu.Models.EntityFramework;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace OkulOtomasyonu.Controllers
{
    public class DerslerController : Controller
    {
        private SchoolDatabaseEntities2 db = new SchoolDatabaseEntities2();

        // GET: Dersler
        public ActionResult Index(string sort)
        {
            var list = db.Dersler.ToList();
            if (sort == "dersno")
            {
                list = list.OrderBy(x => x.ders_no).ToList();
            }
            else if (sort == "dersadi")
            {
                list = list.OrderBy(x => x.ders_adi).ToList();
            }
            else if (sort == "dershocasi")
            {
                list = list.OrderBy(x => x.ders_hocasi).ToList();
            }

            return View(list);
        }
        //POST:Dersler
        [HttpPost]
        public ActionResult Index([Bind(Include = "ders_adi,ders_hocasi")] Dersler dersler)
        {

            var dersler2 = db.Dersler.ToList();

            if (dersler.ders_adi == null && dersler.ders_hocasi == null)
            {
                dersler2 = db.Dersler.ToList();
                return RedirectToAction("Index/");
            }
            else if (dersler.ders_adi != null || dersler.ders_hocasi != null)
            {
                var admin = from m in db.Dersler
                            select m;
                dersler2 = admin.Where(m => m.ders_adi.Contains(dersler.ders_adi) || m.ders_hocasi.Contains(dersler.ders_hocasi)).ToList();


            }
            if (dersler2.Count() == 0)
            {
                TempData["searcherror"] = "Aradığınız bulunamamıştır.";
                return RedirectToAction("Index/", TempData["searcherror"]);
            }

            return View(dersler2);


        }

        // GET: Dersler/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dersler dersler = db.Dersler.Find(id);
            if (dersler == null)
            {
                return HttpNotFound();
            }
            return View(dersler);
        }

        // GET: Dersler/Create
        public ActionResult Create()
        {
            var list = db.Dersler.ToList();
            var list2 = list.Max(x => x.id);
            ViewBag.maxid = list2 + 1;
            var list3 = list.Max(x => x.ders_no);
            ViewBag.maxdersno = list3 + 1;
            return View();
        }

        // POST: Dersler/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,ders_no,ders_adi,ders_hocasi")] Dersler dersler)
        {
            var list = db.Dersler.ToList();
            var list2 = list.Max(x => x.id);
            ViewBag.maxid = list2 + 1;
            var list3 = list.Max(x => x.ders_no);
            ViewBag.maxdersno = list3 + 1;
            if (ModelState.IsValid)
            {
                var dersler2 = db.Dersler.Where(m => m.id != dersler.id && m.ders_no == dersler.ders_no).ToList();
                var dersler3 = db.Dersler.Where(m => m.id != dersler.id && m.ders_adi == dersler.ders_adi).ToList();

                if (dersler2.Count() != 0 && dersler3.Count() != 0)
                {
                    TempData["Error3"] = "Bu Ders Numarası başka bir derse aittir.";
                    TempData["Error4"] = "Bu Ders Adı başka bir derse aittir.";
                    return RedirectToAction("Create");
                }
                else if (dersler2.Count() != 0)
                {
                    TempData["Error3"] = "Bu Ders Numarası başka bir derse aittir.";
                    return RedirectToAction("Create");
                }
                else if (dersler3.Count() != 0)
                {
                    TempData["Error4"] = "Bu Ders Adı başka bir derse aittir.";
                    return RedirectToAction("Create");
                }
                else
                {
                    db.Dersler.Add(dersler);
                    db.SaveChanges();
                    TempData["create"] = dersler.ders_adi + " dersi eklendi";
                    return RedirectToAction("Index", TempData["create"]);
                }


            }

            return View(dersler);
        }

        // GET: Dersler/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dersler dersler = db.Dersler.Find(id);
            if (dersler == null)
            {
                return HttpNotFound();
            }
            return View(dersler);
        }

        // POST: Dersler/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,ders_no,ders_adi,ders_hocasi")] Dersler dersler)
        {
            if (ModelState.IsValid)
            {

                var dersler2 = db.Dersler.Where(m => m.id != dersler.id && m.ders_no == dersler.ders_no).ToList();
                var dersler3 = db.Dersler.Where(m => m.id != dersler.id && m.ders_adi == dersler.ders_adi).ToList();

                if (dersler2.Count() != 0 && dersler3.Count() != 0)
                {
                    TempData["Error3"] = "Bu Ders Numarası başka bir derse aittir.";
                    TempData["Error4"] = "Bu Ders Adı başka bir derse aittir.";
                    return RedirectToAction("Edit/" + dersler.id);
                }
                else if (dersler2.Count() != 0)
                {
                    TempData["Error3"] = "Bu Ders Numarası başka bir derse aittir.";
                    return RedirectToAction("Edit/" + dersler.id);
                }
                else if (dersler3.Count() != 0)
                {
                    TempData["Error4"] = "Bu Ders Adı başka bir derse aittir.";
                    return RedirectToAction("Edit/" + dersler.id);
                }


                else
                {
                    db.Entry(dersler).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["create"] = dersler.ders_adi + " dersi güncellendi";
                    return RedirectToAction("Index", TempData["create"]);
                }

            }
            return View(dersler);
        }

        // GET: Dersler/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dersler dersler = db.Dersler.Find(id);
            if (dersler == null)
            {
                return HttpNotFound();
            }
            return View(dersler);
        }

        // POST: Dersler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dersler dersler = db.Dersler.Find(id);
            db.Dersler.Remove(dersler);
            db.SaveChanges();
            TempData["create"] = dersler.ders_adi + " dersi silindi";
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
        public ActionResult DersList(int? id)
        {
            ViewBag.id = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ogrenciler = db.OgrenciDers.Where(m => m.ders_id == id);
            if (ogrenciler == null)
            {
                return HttpNotFound();
            }
            return View(ogrenciler);

        }
    }
}
