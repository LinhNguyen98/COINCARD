using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using COINCARD.Models;

using System.Net;
using Microsoft.AspNet.Identity;
namespace COINCARD.Controllers
{
    public class NguoidungController : Controller
    {
        // GET: Nguoidung
        QLBanGiayDataContext data = new QLBanGiayDataContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dangky()
        {
            return View();
        }
        [HttpPost]
        public ActionResult dangky(FormCollection collection, KHACHHANG kh)
        {
            var hoten = collection["Hoten"];
            var tendn = collection["Taikhoan"];
            var matkhau = collection["Matkhau"];
            var nhaplaimatkhau = collection["Nhaplaimatkhau"];
            var diachi = collection["Diachi"];
            var email = collection["Email"];
            var dienthoai = collection["Dienthoai"];
            var ngaysinh = collection["Ngaysinh"];
            if (string.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên không được để trống";
            }
            else if (string.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Nhập tên đăng nhập";
            }
            else if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Nhập mật khẩu";
            }
            else if (string.IsNullOrEmpty(diachi))
            {
                ViewData["Loi5"] = "Nhập địa chỉ";
            }
            else if (string.IsNullOrEmpty(email))
            {
                ViewData["Loi6"] = "Nhập Email";
            }
            else if (string.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi7"] = "Nhập số điện thoại";
            }
            else if (kh.Matkhau != kh.Nhaplaimatkhau)
            {
                ViewData["Loi4"] = "Mật khẩu nhập lại không khớp";
            }
            else if (ngaysinh == string.Empty)
            {
                ViewData["Loi8"] = "Phải nhập ngày sinh";
            }

            else
            {
                ViewBag.Thongbao = "Đăng kí thành công";
                kh.Hoten = hoten;
                kh.Taikhoan = tendn;
                kh.Matkhau = matkhau;
                kh.Nhaplaimatkhau = nhaplaimatkhau;
                kh.Email = email;
                kh.Diachi = diachi;
                kh.Dienthoai = dienthoai;
                if (ngaysinh != string.Empty)
                {
                    kh.Ngaysinh = DateTime.Parse(ngaysinh);
                }


                data.KHACHHANGs.InsertOnSubmit(kh);
                data.SubmitChanges();

                return RedirectToAction("Dangnhap");

            }
            return this.Dangky();
        }
        public ActionResult Dangnhap(FormCollection collection)
        {
            var tendn = collection["Taikhoan"];
            var matkhau = collection["Matkhau"];
            if (string.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.Taikhoan == tendn && n.Matkhau == matkhau);
                if (kh != null)
                {
                    Session["Taikhoan"] = kh;
                    return RedirectToAction("Giohang", "Giohang");
                }
                else
                    ViewBag.Thongbao = "Sai tên đăng nhập hoặc mật khẩu";
            }
            return View();
        }
        public ActionResult Dangxuat(FormCollection collection)
        {
            var tendn = collection["Taikhoan"];
            var matkhau = collection["Matkhau"];
            KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.Taikhoan == tendn && n.Matkhau == matkhau);

            Session["Taikhoan"] = null;
            return RedirectToAction("Index", "COINCARD");
        }
    }
}