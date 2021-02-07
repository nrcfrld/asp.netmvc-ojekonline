using _190442010026.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _190442010026.Controllers
{
    public class PendapatanController : Controller
    {
        OjekOnlineEntities db = new OjekOnlineEntities();
        // GET: Pendapatan
        public ActionResult Index()
        {
            // Mengambil List Pendapatan
            ViewData["pendapatan"] = db.view_pendapatan.ToList();
            ViewData["pengemudi"] = db.pengemudis.ToList();

            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            try
            {
                pendapatan p = new pendapatan();
                p.id_pengemudi = Convert.ToInt32(form["pengemudi"]);
                p.tanggal = Convert.ToDateTime(form["tanggal"]);
                p.potongan = Convert.ToInt32(form["potongan"]);
                p.jumlah_jam_perbulan = Convert.ToInt32(form["jumlah_jam_perbulan"]);

                db.pendapatans.Add(p);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                var pendapatan = db.pendapatans.SingleOrDefault(d => d.id == id);
                if (pendapatan != null)
                {
                    db.pendapatans.Remove(pendapatan);
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var pendapatan = db.pendapatans.SingleOrDefault(d => d.id == id);
            ViewData["pengemudi"] = db.pengemudis.ToList();
            return View(pendapatan);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Update(FormCollection form, pendapatan r)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var p = db.pendapatans.SingleOrDefault(d => d.id == r.id);

                    if (p != null)
                    {
                        p.id_pengemudi = Convert.ToInt32(form["id_pengemudi"]);
                        p.tanggal = Convert.ToDateTime(form["tanggal"]);
                        p.potongan = Convert.ToDecimal(form["potongan"]);
                        p.jumlah_jam_perbulan = Convert.ToDecimal(form["jumlah_jam_perbulan"]);

                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult getPengemudi(int? id)
        {
            var pengemudi = db.pengemudis.SingleOrDefault(p => p.id == id);

            return Json( new { insentif_makan = pengemudi.insentif_makan, insentif_bensin = pengemudi.insentif_bensin, gaji_perjam = pengemudi.gaji_perjam }, JsonRequestBehavior.AllowGet);
        }
    }
}