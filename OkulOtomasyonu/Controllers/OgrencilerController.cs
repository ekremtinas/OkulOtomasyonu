using OkulOtomasyonu.Models.EntityFramework;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace OkulOtomasyonu.Controllers
{

    public class OgrencilerController : Controller
    {
        private SchoolDatabaseEntities2 db = new SchoolDatabaseEntities2();

        // GET: Ogrenciler

        public ActionResult Index(string sort)
        {
            var list = db.Ogrenciler.ToList();
            if (sort == "adi")
            {


                list = list.OrderBy(x => x.adi).ToList();
                TempData["tsort"] = "ida";


            }
            else if (sort == "ida")
            {
                list = list.OrderByDescending(x => x.adi).ToList();
            }
            else if (sort == "soyadi")
            {
                list = list.OrderBy(x => x.soyadi).ToList();
            }
            else if (sort == "tcno")
            {
                list = list.OrderBy(x => x.tc_no).ToList();
            }
            else if (sort == "ono")
            {
                list = list.OrderBy(x => x.o_no).ToList();
            }
            else if (sort == "dtarihi")
            {
                list = list.OrderBy(x => x.d_tarihi).ToList();
            }
            else if (sort == "adres")
            {
                list = list.OrderBy(x => x.adres).ToList();
            }



            return View(list);
        }
        [HttpPost]
        public ActionResult Index([Bind(Include = "adi,soyadi,tc_no,o_no,adres")] Ogrenciler ogrenciler)
        {

            var ogrenciler2 = db.Ogrenciler.ToList();

            if (ogrenciler.adi == null && ogrenciler.soyadi == null && ogrenciler.tc_no == null && ogrenciler.o_no == null)
            {
                ogrenciler2 = db.Ogrenciler.ToList();
                return RedirectToAction("Index/");
            }
            else if (ogrenciler.adi != null || ogrenciler.soyadi != null || ogrenciler.tc_no != null || ogrenciler.o_no != null)
            {
                var movies = from m in db.Ogrenciler
                             select m;
                ogrenciler2 = movies.Where(m => m.adi.Contains(ogrenciler.adi) || m.soyadi.Contains(ogrenciler.soyadi) || m.tc_no.Contains(ogrenciler.tc_no) || m.o_no.Contains(ogrenciler.o_no)).ToList();


            }
            if (ogrenciler2.Count() == 0)
            {
                TempData["searcherror"] = "Aradığınız bulunamamıştır.";
                return RedirectToAction("Index/", TempData["searcherror"]);
            }

            return View(ogrenciler2);


        }

        // GET: Ogrenciler/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogrenciler ogrenciler = db.Ogrenciler.Find(id);
            if (ogrenciler == null)
            {
                return HttpNotFound();
            }
            return View(ogrenciler);
        }

        // GET: Ogrenciler/Create
        public ActionResult Create()
        {


            var list = db.Ogrenciler.ToList();
            var list2 = list.Max(x => x.id);
            ViewBag.maxid = list2 + 1;

            return View();
        }

        // POST: Ogrenciler/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create([Bind(Include = "id,adi,soyadi,tc_no,o_no,d_tarihi,adres")] Ogrenciler ogrenciler)
        {
            var list = db.Ogrenciler.ToList();
            var list2 = list.Max(x => x.id);
            ViewBag.maxid = list2 + 1;

            if (ModelState.IsValid)
            {




                var ogrenciler2 = db.Ogrenciler.Where(m => m.id != ogrenciler.id && m.tc_no == ogrenciler.tc_no).ToList();
                var ogrenciler3 = db.Ogrenciler.Where(m => m.id != ogrenciler.id && m.o_no == ogrenciler.o_no).ToList();
                if (ogrenciler2.Count() != 0 && ogrenciler3.Count() != 0)
                {
                    TempData["Error3"] = "Bu TC Numarasında bir öğrenci vardır.";
                    TempData["Error4"] = "Bu Okul Numarasında bir öğrenci vardır.";
                    return RedirectToAction("Create");
                }
                else if (ogrenciler2.Count() != 0)
                {
                    TempData["Error3"] = "Bu TC Numarasında bir öğrenci vardır.";
                    return RedirectToAction("Create");
                }
                else if (ogrenciler3.Count() != 0)
                {
                    TempData["Error4"] = "Bu Okul Numarasında bir öğrenci vardır.";
                    return RedirectToAction("Create");
                }

                else
                {
                    TempData["create"] = ogrenciler.adi + " isimli öğrenci eklendi";
                    db.Ogrenciler.Add(ogrenciler);
                    db.SaveChanges();
                    return RedirectToAction("Index", TempData["create"]);
                }





            }

            return View(ogrenciler);
        }

        // GET: Ogrenciler/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogrenciler ogrenciler = db.Ogrenciler.Find(id);
            if (ogrenciler == null)
            {
                return HttpNotFound();
            }
            return View(ogrenciler);
        }

        // POST: Ogrenciler/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "id,adi,soyadi,tc_no,o_no,d_tarihi,adres")] Ogrenciler ogrenciler)
        {
            if (ModelState.IsValid)
            {
                var ogrenciler2 = db.Ogrenciler.Where(m => m.id != ogrenciler.id && m.tc_no == ogrenciler.tc_no).ToList();
                var ogrenciler3 = db.Ogrenciler.Where(m => m.id != ogrenciler.id && m.o_no == ogrenciler.o_no).ToList();

                if (ogrenciler2.Count() != 0 && ogrenciler3.Count() != 0)
                {
                    TempData["Error3"] = "Bu TC Numarasında bir öğrenci vardır.";
                    TempData["Error4"] = "Bu Okul Numarasında bir öğrenci vardır.";
                    return RedirectToAction("Edit/" + ogrenciler.id);
                }
                else if (ogrenciler2.Count() != 0)
                {
                    TempData["Error3"] = "Bu TC Numarasında bir öğrenci vardır.";
                    return RedirectToAction("Edit/" + ogrenciler.id);
                }
                else if (ogrenciler3.Count() != 0)
                {
                    TempData["Error4"] = "Bu Okul Numarasında bir öğrenci vardır.";
                    return RedirectToAction("Edit/" + ogrenciler.id);
                }
                else
                {
                    TempData["create"] = ogrenciler.adi + " isimli öğrenci güncellendi";
                    db.Entry(ogrenciler).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", TempData["create"]);
                }

            }
            return View(ogrenciler);
        }

        // GET: Ogrenciler/Delete/5

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogrenciler ogrenciler = db.Ogrenciler.Find(id);
            if (ogrenciler == null)
            {
                return HttpNotFound();
            }
            return View(ogrenciler);
        }

        // POST: Ogrenciler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Ogrenciler ogrenciler = db.Ogrenciler.Find(id);
            db.Ogrenciler.Remove(ogrenciler);
            db.SaveChanges();
            TempData["create"] = ogrenciler.adi + " isimli öğrenci silindi";
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

        public ActionResult Assignment(int? id)
        {
            ViewBag.id = id;
            var list = db.OgrenciDers.ToList();
            var list2 = list.Max(x => x.id);
            ViewBag.maxid = list2 + 1;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Assignment([Bind(Include = "id,ders_id2,ogrenci_id")] OgrenciDers ogrenciders)
        {


            if (ModelState.IsValid)
            {
                OgrenciDers ogrenciDers;
                var dersid = 1;
                // OgrenciDers ogrenciler = db.OgrenciDers.Where(m => m.ogrenci_id == ogrenciders.ogrenci_id).FirstOrDefault();
                OgrenciDers ogrenciler3 = (from y in db.OgrenciDers where y.ogrenci_id == ogrenciders.ogrenci_id select y).FirstOrDefault();
                var ogrenciler2 = (from y in db.OgrenciDers where y.ogrenci_id == ogrenciders.ogrenci_id select y).ToList();

                if (ogrenciler3 != null)
                {
                    for (var i = 0; i < ogrenciler2.Count(); i++)
                    {
                        OgrenciDers ogrenciler = (from y in db.OgrenciDers where y.ogrenci_id == ogrenciders.ogrenci_id select y).FirstOrDefault();
                        db.OgrenciDers.Remove(ogrenciler);
                        db.SaveChanges();
                    }
                }
                if (ogrenciders.ders_id2 != null)
                {
                    for (var i = 0; i < ogrenciders.ders_id2.Length; i++)
                    {
                        dersid = ogrenciders.ders_id2[i];
                        ogrenciDers = new OgrenciDers() { id = ogrenciders.id + i, ders_id = dersid, ogrenci_id = ogrenciders.ogrenci_id };
                        db.OgrenciDers.Add(ogrenciDers);

                    }
                    db.SaveChanges();
                }
                else
                {
                    for (var i = 0; i < ogrenciler2.Count(); i++)
                    {
                        OgrenciDers ogrenciler = (from y in db.OgrenciDers where y.ogrenci_id == ogrenciders.ogrenci_id select y).FirstOrDefault();
                        db.OgrenciDers.Remove(ogrenciler);
                        db.SaveChanges();
                    }

                }
                var list4 = db.Ogrenciler.Where(m => m.id == ogrenciders.ogrenci_id).ToList();
                TempData["create"] = list4[0].adi + " isimli öğrenciye " + ogrenciders.ders_id2.Count() + " tane ders atandı";
                return RedirectToAction("Index", TempData["create"]);
            }

            return View(ogrenciders);
        }


        public ActionResult AssignmentList(int? id)
        {
            ViewBag.id = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ogrenciler = db.OgrenciDers.Where(m => m.ogrenci_id == id);
            if (ogrenciler == null)
            {
                return HttpNotFound();
            }
            return View(ogrenciler);

        }



    }
}
