using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TranDucKhang_15DH110240MVC.Models;

namespace TranDucKhang_15DH110240MVC.Controllers
{
    public class NguoiDungController : Controller
    {
        // GET: NguoiDung
        dbQLBansachDataContext db = new dbQLBansachDataContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Dangky()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangky(FormCollection collection, KHACHHANG kh)
        {
            var hoten = collection["HotenKH"];
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            var matkhaunhaplai = collection["Matkhaunhaplai"];
            var diachi = collection["Diachi"];
            var email = collection["Email"];
            var dienthoai = collection["Dienthoai"];
            var gioitinh = collection["Gioitinh"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["Ngaysinh"]);
            if(String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên khách hàng không được để trống";
            }
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Phải nhập tên đăng nhập";
            }
            if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Phải nhập mật khẩu";
            }
            if (String.IsNullOrEmpty(matkhaunhaplai))
            {
                ViewData["Loi4"] = "Phải nhập lại mật khẩu";
            }
            if (String.IsNullOrEmpty(diachi))
            {
                ViewData["Loi5"] = "Địa chỉ không được để trống";
            }
            if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi6"] = "Email không được để trống";
            }
            if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi7"] = "Phải nhập điện thoại";
            }
            if(matkhaunhaplai!=matkhau)
            {
                ViewData["Loi4"] = "Mật khẩu nhập lại sai!";
            }
            else
            {
                kh.HoTenKH = hoten;
                kh.TenDN = tendn;
                kh.Matkhau = matkhau;
                kh.Email = email;
                kh.DiachiKH = diachi;
                kh.DienthoaiKH = dienthoai;
                kh.Ngaysinh = DateTime.Parse(ngaysinh);
                kh.Gioitinh = bool.Parse(gioitinh);
                db.KHACHHANGs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return RedirectToAction("Dangnhap");
            }
            return this.Dangky();
        }
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangnhap(FormCollection col)
        {
            var tendn = col["TenDN"];
            var matkhau = col["Matkhau"];
            if(String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if(String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.TenDN == tendn && n.Matkhau == matkhau);
                if (kh != null)
                {
                    ViewBag.Thongbao = "Chúc mừng đặng nhập thành công";
                    Session["TenDN"] = kh;
                    return RedirectToAction("Index", "BookStore", new { area = "" });
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
    }
}