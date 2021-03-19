using OkulOtomasyonu.Models;
using OkulOtomasyonu.Models.EntityFramework;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace OkulOtomasyonu.Controllers
{
    public class AdminController : Controller
    {
        SchoolDatabaseEntities2 db = new SchoolDatabaseEntities2();

        // [AllowAnonymous]//Korumayı Kaldırmak
        public ActionResult Index()
        {
            return View();
        }



        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Admin adm)
        {
            if (adm.sifre == null && adm.k_adi == null)
            {
                ViewBag.mesaj = "Lütfen Kullanıcı Adı ve Şifre Giriniz";

                return View();
            }
            else if (adm.k_adi == null)
            {
                ViewBag.mesaj = "Lütfen Kullanıcı Adı Giriniz";
                return View();
            }
            else if (adm.sifre == null)
            {

                ViewBag.mesaj = "Lütfen Şifre Giriniz";
                return View();
            }

            else
            {
                var kullanici = db.Yoneticiler.FirstOrDefault(m => m.k_adi == adm.k_adi && m.sifre == adm.sifre);//K_adi ve şifre Varmı Yokmu

                if (kullanici != null)
                {
                    FormsAuthentication.SetAuthCookie(kullanici.id.ToString(), adm.benihatirla);//Çerezlerin Girilmesi
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.mesaj = "Geçersiz Kullanıcı Adı veya Şifre";
                    return View();
                }

            }

        }
        public ActionResult Logout(int id)//Çıkış İşlemi
        {
            var mevcut = db.Yoneticiler.Find(id);
            mevcut.songiristarihi = DateTime.Today;
            db.SaveChanges();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }













        [AllowAnonymous]
        public ActionResult Admins()//Yöneticiler
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Ask()//Soru - Cevap
        {

            return View();
        }




    }
}