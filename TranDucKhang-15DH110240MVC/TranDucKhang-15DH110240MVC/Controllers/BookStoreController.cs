using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TranDucKhang_15DH110240MVC.Models;
using PagedList;
using PagedList.Mvc;

namespace TranDucKhang_15DH110240MVC.Controllers
{
    public class BookStoreController : Controller
    {
        //
        // GET: /BookStore/
        dbQLBansachDataContext data = new dbQLBansachDataContext();

        private List<SACH> Laysachmoi(int count, string name)
        {
            //return data.SACHes.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
            if(name!=null && name!="")
            {
                return (from s in data.SACHes where s.Tensach.ToUpper().Contains(name.ToUpper()) orderby s.Ngaycapnhat descending select s).Take(count).ToList();
            }
            else
                return (from s in data.SACHes orderby s.Ngaycapnhat descending select s).Take(count).ToList();
        }

        public ActionResult Index(int? page, FormCollection fc)
        {
            ViewBag.TenDN = User.Identity.Name;
            int pageSize = 6;
            int pageNum = (page ?? 1);
            //var sach = from s in data.SACHes
            //           select s;
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    sach = sach.Where(s => s.Tensach.Contains(searchString));
            //}

            //var sachmoi = Laysachmoi(15);
            //return View(sachmoi.ToPagedList(pageNum, pageSize));
            string name = fc["SearchString"];
            var sachmoi = Laysachmoi(15, name);
            return View(sachmoi.ToPagedList(pageNum,pageSize));
        }

        public ActionResult ChuDe()
        {
            var chude = from cd in data.CHUDEs select cd;
            return PartialView(chude);
        }

        public ActionResult Nhaxuatban()
        {
            var nhaxuatban = from nxb in data.NHAXUATBANs select nxb;
            return PartialView(nhaxuatban);
        }

        public ActionResult SPTheochude(int id)
        {
            var sach = from s in data.SACHes where s.MaCD == id select s;
            return View(sach);
        }

        public ActionResult SPTheoNXB(int id)
        {
            var sach = from s in data.SACHes where s.MaNXB == id select s;
            return View(sach);
        }

        public ActionResult Details(int id)
        {
            var sach = from s in data.SACHes
                       where s.Masach == id
                       select s;
            return View(sach.Single());
        }
    }
}
