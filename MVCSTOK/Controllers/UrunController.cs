using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCSTOK.Models.Entity;

namespace MVCSTOK.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        MvcDbStokEntities db=new MvcDbStokEntities();
        public ActionResult UrunList()
        {
            var degerler = db.TBLURUNLER.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList() // linq tanımlaması listeyi i ye atıyor
                                             select new SelectListItem
                                             {
                                                 Text=i.KATEGORIAD,
                                                 Value=i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler; // sorgudan gelen değerleri tutuyor.
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(TBLURUNLER p1)
        {
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p1.URUNKATEGORI).FirstOrDefault();
            p1.URUNKATEGORI = ktg.KATEGORIID;
            db.TBLURUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("UrunList");
        }
        public ActionResult Sil(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("UrunList");
        }
        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBLURUNLER.Find(id);

            List<SelectListItem> degerler = (from i in db.TBLKATEGORILER.ToList() // linq tanımlaması listeyi i ye atıyor
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler; // sorgudan gelen değerleri tutuyor.
            return View("UrunGetir", urun);
        }
        public ActionResult Guncelle(TBLURUNLER p1)
        {
            var urun = db.TBLURUNLER.Find(p1.URUNID);
            urun.URUNAD = p1.URUNAD;
            urun.MARKA = p1.MARKA;
            urun.FIYAT= p1.FIYAT;
            urun.STOK = p1.STOK;
            var ktg = db.TBLKATEGORILER.Where(m => m.KATEGORIID == p1.URUNKATEGORI).FirstOrDefault();
            urun.URUNKATEGORI = ktg.KATEGORIID;
            db.SaveChanges();
            return RedirectToAction("UrunList");
        }
    }
}