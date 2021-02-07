using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _190442010026.Models;

namespace _190442010026.Controllers
{
    public class PengemudiController : Controller
    {
        OjekOnlineEntities db = new OjekOnlineEntities();

        // GET: DataDiri
        public ActionResult Index()
        {
            // Mengambil List Data Diri
            ViewData["data_diri"] = db.pengemudis.ToList();

            return View();
        }
        [ValidateInput(false)]
        public ActionResult Create(FormCollection form)
        {
            try
            {
                pengemudi p = new pengemudi();
                p.nama_pengemudi = form["nama_pengemudi"];
                p.plat_nomor = form["plat_nomor"];
                p.nomor_sim = form["nomor_sim"];
                p.gaji_perjam = Convert.ToDecimal(form["gaji_perjam"]);
                p.insentif_bensin = Convert.ToDecimal(form["insentif_bensin"]);
                p.insentif_makan = Convert.ToDecimal(form["insentif_makan"]);

                db.pengemudis.Add(p);
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
                var pengemudi = db.pengemudis.SingleOrDefault(d => d.id == id);
                if (pengemudi != null)
                {
                    db.pengemudis.Remove(pengemudi);
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
            var data_diri = db.pengemudis.SingleOrDefault(d => d.id == id);
            return View(data_diri);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Update(FormCollection form, pengemudi r)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var p = db.pengemudis.SingleOrDefault(d => d.id == r.id);

                    if (p != null)
                    {
                        p.nama_pengemudi = form["nama_pengemudi"];
                        p.plat_nomor = form["plat_nomor"];
                        p.nomor_sim = form["nomor_sim"];
                        p.gaji_perjam = Convert.ToDecimal(form["gaji_perjam"]);
                        p.insentif_bensin = Convert.ToDecimal(form["insentif_bensin"]);
                        p.insentif_makan = Convert.ToDecimal(form["insentif_makan"]);

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
    }
}