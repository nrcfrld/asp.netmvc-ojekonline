using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _190442010026.Models
{
    public class DataDiri
    {
        OjekOnlineEntities db = new OjekOnlineEntities();

        public string GetFileGambar(int id)
        {
            string foto = "";
            var cari = db.data_diri.Where(a => a.id == id).FirstOrDefault();

            if (cari != null)
            {
                foto = cari.foto;
            }
            return foto;
        }
    }
}