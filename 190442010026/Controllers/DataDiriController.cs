using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _190442010026.Models;

namespace _190442010026.Controllers
{
    public class DataDiriController : Controller
    {
        OjekOnlineEntities db = new OjekOnlineEntities();
        // GET: DataDiri
        public ActionResult Index()
        {
            // Mengambil List Data Diri
            ViewData["data_diri"] = db.data_diri.ToList();

            return View();
        }
        [ValidateInput(false)]
        public ActionResult Create(FormCollection form)
        {
            try
            {
                data_diri dd = new data_diri();
                dd.tanggal_daftar = Convert.ToDateTime(form["tanggal_daftar"]);
                dd.nama_pengemudi = form["nama_pengemudi"];
                dd.plat_nomor = form["plat_nomor"];
                dd.nomor_sim = form["nomor_sim"];
                dd.keterangan = form["keterangan"];
                dd.minimal_jam_kerja = Convert.ToDecimal(form["minimal_jam_kerja"]);

                // Untuk Gambar
                string path, filename, renameName = "";
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    path = Path.Combine(Server.MapPath("~/Content/images/"), "");
                    var pathSave = Path.Combine(Directory.GetCurrentDirectory(), path);

                    if (file != null && file.ContentLength > 0)
                    {
                        filename = Path.GetFileName(file.FileName);
                        renameName = form["id"] + "." + filename.Split('.').Last();
                        var fullPath = Path.Combine(pathSave, renameName);
                        file.SaveAs(fullPath);
                        dd.foto = "~/Content/images/" + renameName;
                    }
                }

                db.data_diri.Add(dd);
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
                var data_diri = db.data_diri.SingleOrDefault(d => d.id == id);
                if (data_diri != null)
                {
                    DataDiri dm = new DataDiri();
                    string filename = dm.GetFileGambar(id);
                    db.data_diri.Remove(data_diri);
                    db.SaveChanges();
                    if (System.IO.File.Exists(Server.MapPath(filename)))
                    {
                        System.IO.File.Delete(Server.MapPath(filename));
                    }
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
            var data_diri = db.data_diri.SingleOrDefault(d => d.id == id);
            return View(data_diri);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Update(FormCollection form, data_diri dd)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data_diri = db.data_diri.SingleOrDefault(d => d.id == dd.id);

                    if (data_diri != null)
                    {
                        data_diri.tanggal_daftar = Convert.ToDateTime(form["tanggal_daftar"]);
                        data_diri.nama_pengemudi = form["nama_pengemudi"];
                        data_diri.plat_nomor = form["plat_nomor"];
                        data_diri.nomor_sim = form["nomor_sim"];
                        data_diri.keterangan = form["keterangan"];
                        data_diri.minimal_jam_kerja = Convert.ToDecimal(form["minimal_jam_kerja"]);

                        // Untuk Gambar
                        string path, filename, renameName = "";
                        if (Request.Files.Count > 0)
                        {
                            // TODO : Cek File Gambar dan Hapus File Gambar Lama
                            var dm = new DataDiri();
                            filename = dm.GetFileGambar(dd.id);
                            if (System.IO.File.Exists(Server.MapPath(filename)))
                            {
                                System.IO.File.Delete(Server.MapPath(filename));
                            }

                            var file = Request.Files[0];
                            path = Path.Combine(Server.MapPath("~/Content/images/"), "");
                            var pathSave = Path.Combine(Directory.GetCurrentDirectory(), path);

                            if (file != null && file.ContentLength > 0)
                            {
                                filename = Path.GetFileName(file.FileName);
                                renameName = form["id"] + "." + filename.Split('.').Last();
                                var fullPath = Path.Combine(pathSave, renameName);
                                file.SaveAs(fullPath);
                                data_diri.foto = "~/Content/images/" + renameName;
                            }
                        }

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