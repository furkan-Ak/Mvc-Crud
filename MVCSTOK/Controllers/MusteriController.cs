using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSTOK.Models.Entity;
namespace MVCSTOK.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult MusteriList(string p)
        {
            var degerler = from d in db.TBLMUSTERILER select d; // d ile table da gez ger gezdiği d değerini değerlere at kısa foreach sorgusu
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.MUSTERIAD.Contains(p)); // contain içeren anlamında kullanılıyor, parametre(input içeriğinde yazan değerlere) yazıldıgında müşteri adında varda ona göre filtreleme yapacak
            }
            return View(degerler.ToList());
            //var degerler = db.TBLMUSTERILER.ToList();
            //return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View(); 
        }
        [HttpPost]
        public ActionResult YeniMusteri( TBLMUSTERILER p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            db.TBLMUSTERILER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("MusteriList");
        }
        public ActionResult Sil(int id)
        {
            var musteri=db.TBLMUSTERILER.Find(id);
            db.TBLMUSTERILER.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("MusteriList");
        }
        public ActionResult MusteriGetir(int id)
        {
            var mstr = db.TBLMUSTERILER.Find(id);
            return View("MusteriGetir",mstr);
        }
        public ActionResult Guncelle(TBLMUSTERILER p1)
        {
            var mstr = db.TBLMUSTERILER.Find(p1.MUSTERIID);
            mstr.MUSTERIAD = p1.MUSTERIAD;
            mstr.MUSTERISOYAD = p1.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("MusteriList");
        }
    }
}