using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TranDucKhang_15DH110240MVC.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace TranDucKhang_15DH110240MVC.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        dbQLBansachDataContext db = new dbQLBansachDataContext();
        public ActionResult Index()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login");
            return View();
        }

        public ActionResult Sach(int? page)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login");
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            return View(db.SACHes.ToList().OrderBy(n => n.Masach).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Themmoisach()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login");
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemmoiSach(SACH sach, HttpPostedFileBase fileupload)
        {
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            if (fileupload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var filename = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Hinhsanpham"), filename);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileupload.SaveAs(path);
                    }
                    sach.Hinhminhhoa = filename;
                    db.SACHes.InsertOnSubmit(sach);
                    db.SubmitChanges();
                }
                return RedirectToAction("Sach");
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendn = collection["username"];
            var matkhau = collection["password"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                Admin ad = db.Admins.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);
                if (ad != null)
                {
                    Session["Taikhoanadmin"] = ad;
                    Session["TenDN"] = tendn;
                    return RedirectToAction("Index", "Admin");
                }
                else
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }

        public ActionResult Chitietsach(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login");
            SACH sach = db.SACHes.SingleOrDefault(n => n.Masach == id);
            ViewBag.Masach = sach.Masach;
            if(sach==null)
            {
                Response.StatusCode = 404;
            }
            return View(sach);
        }
        [HttpGet]
        public ActionResult Xoasach(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login");
            SACH sach = db.SACHes.SingleOrDefault(n => n.Masach == id);
            ViewBag.Masach = sach.Masach;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }

        [HttpPost,ActionName("Xoasach")]
        public ActionResult Xacnhanxoa(int id)
        {
            SACH sach = db.SACHes.SingleOrDefault(n => n.Masach == id);
            ViewBag.Masach = sach.Masach;
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.SACHes.DeleteOnSubmit(sach);
            db.SubmitChanges();
            return RedirectToAction("Sach");
        }

        [HttpGet]
        public ActionResult Suasach(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login");
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login");
            SACH sach = db.SACHes.SingleOrDefault(n => n.Masach == id);
            if(sach==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            return View(sach);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Suasach(SACH sach, HttpPostedFileBase fileupload)
        {
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NHAXUATBANs.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            if (fileupload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var filename = Path.GetFileName(fileupload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Hinhsanpham"), filename);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fileupload.SaveAs(path);
                    }

                    SACH s = db.SACHes.Where(x => x.Masach == sach.Masach).Single<SACH>();
                    s.Tensach = sach.Tensach;
                    s.Dongia = sach.Dongia;
                    s.MaCD = sach.MaCD;
                    s.MaNXB = sach.MaNXB;
                    s.Ngaycapnhat = sach.Ngaycapnhat;
                    s.Soluongban = sach.Soluongban;
                    s.Mota = sach.Mota;
                    s.Hinhminhhoa = filename;
                    db.SubmitChanges();
                }
                return RedirectToAction("Sach");
            }
        }
        [HttpPost]
        public ActionResult Logout()
        {
            Session["Tentaikhoan"] = null;
            Session["TenDN"] = null;
            return RedirectToAction("Login");
        }
    }
}
