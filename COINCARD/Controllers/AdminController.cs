using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using COINCARD.Models;
using System.IO;

namespace COINCARD.Controllers
{
    public class AdminController : Controller
    {
        QLBanGiayDataContext data = new QLBanGiayDataContext();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Giay()
        {

            return View(data.GIAYs.ToList());
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            // Gán các giá trị người dùng nhập liệu cho các biến 
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
                //Gán giá trị cho đối tượng được tạo mới (ad)        

                Admin ad = data.Admins.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);
                if (ad != null)
                {
                    //ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["Taikhoanadmin"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
        [HttpGet]
        public ActionResult ThemmoiGiay()
        {
            ViewBag.MaTH = new SelectList(data.THUONGHIEUs.ToList().OrderBy(n => n.TenThuongHieu), "MaTH", "TenThuongHieu");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemmoiGiay(GIAY giay, HttpPostedFileBase fileUpload)
        {
            //Dua du lieu vao dropdownload
            ViewBag.MaTH = new SelectList(data.THUONGHIEUs.ToList().OrderBy(n => n.TenThuongHieu), "MaTH", "TenThuongHieu");
            //Kiem tra duong dan file
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            //Them vao CSDL
            else
            {
                if (ModelState.IsValid)
                {
                    //Luu ten fie, luu y bo sung thu vien using System.IO;
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    //Luu duong dan cua file
                    var path = Path.Combine(Server.MapPath("~/img"), fileName);
                    //Kiem tra hình anh ton tai chua?
                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {
                        //Luu hinh anh vao duong dan
                        fileUpload.SaveAs(path);
                    }
                    giay.Anhbia = fileName;
                    //Luu vao CSDL
                    data.GIAYs.InsertOnSubmit(giay);
                    data.SubmitChanges();
                }
                return RedirectToAction("Giay");
            }
        }
        public ActionResult Chitietgiay(int id)
        {
            //Lay ra doi tuong sach theo ma
            GIAY giay = data.GIAYs.SingleOrDefault(n => n.Magiay == id);
            ViewBag.Magiay = giay.Magiay;
            if (giay == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(giay);
        }
        [HttpGet]
        public ActionResult Xoagiay(int id)
        {
            //Lay ra doi tuong sach can xoa theo ma
            GIAY giay = data.GIAYs.SingleOrDefault(n => n.Magiay == id);
            ViewBag.Magiay = giay.Magiay;
            if (giay == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(giay);
        }

        [HttpPost, ActionName("Xoagiay")]
        public ActionResult Xacnhanxoa(int id)
        {
            //Lay ra doi tuong sach can xoa theo ma
            GIAY giay = data.GIAYs.SingleOrDefault(n => n.Magiay == id);
            ViewBag.Magiay = giay.Magiay;
            if (giay == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.GIAYs.DeleteOnSubmit(giay);
            data.SubmitChanges();
            return RedirectToAction("Giay");
        }
        [HttpGet]


        public ActionResult Suagiay(int id)
        {
            GIAY giay =data.GIAYs.SingleOrDefault(n => n.Magiay == id);
            if (giay == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Dua du lieu vao dropdownList
            //Lay ds tu tabke chu de, sắp xep tang dan trheo ten chu de, chon lay gia tri Ma CD, hien thi thi Tenchude
            ViewBag.MaTH = new SelectList(data.THUONGHIEUs.ToList().OrderBy(n => n.TenThuongHieu), "MaTH", "TenThuongHieu", giay.MaTH);
            return View(giay);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Suagiay(int? id, HttpPostedFileBase fileUpload)
        {
            GIAY giay = data.GIAYs.Where(m => m.Magiay == id).SingleOrDefault();
            UpdateModel(giay);
            //Dua du lieu vao dropdownload
            ViewBag.MaTH = new SelectList(data.THUONGHIEUs.ToList().OrderBy(n => n.TenThuongHieu), "MaTH", "TenThuongHieu", giay.MaTH);
            //Kiem tra duong dan file
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            //Them vao CSDL
            else
            {
                if (ModelState.IsValid)
                {
                    //Luu ten fie, luu y bo sung thu vien using System.IO;
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    //Luu duong dan cua file
                    var path = Path.Combine(Server.MapPath("~/img"), fileName);
                    //Kiem tra hình anh ton tai chua?
                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    else
                    {
                        //Luu hinh anh vao duong dan
                        fileUpload.SaveAs(path);
                    }
                    giay.Anhbia = fileName;
                    //Luu vao CSDL   

                    data.SubmitChanges();

                }
                return RedirectToAction("Giay");
            }
        }







        //public ActionResult Suagiay(int id)
        //{
        //    //Lay ra doi tuong sach theo ma
        //    GIAY giay = data.GIAYs.SingleOrDefault(n => n.Magiay == id);
        //    ViewBag.Magiay = giay.Magiay;
        //    if (giay == null)
        //    {
        //        Response.StatusCode = 404;
        //        return null;
        //    }
        //    //Dua du lieu vao dropdownList
        //    //Lay ds tu tabke chu de, sắp xep tang dan trheo ten chu de, chon lay gia tri Ma CD, hien thi thi Tenchude
        //    ViewBag.MaTH = new SelectList(data.THUONGHIEUs.ToList().OrderBy(n => n.TenThuongHieu), "MaTH", "TenThuongHieu", giay.MaTH);
        //    return View(giay);
        //}
        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult Suasach(GIAY giay, HttpPostedFileBase fileUpload)
        //{
        //    //Dua du lieu vao dropdownload
        //    ViewBag.MaTH = new SelectList(data.THUONGHIEUs.ToList().OrderBy(n => n.TenThuongHieu), "MaTH", "TenThuongHieu");

        //    //Kiem tra duong dan file
        //    if (fileUpload == null)
        //    {
        //        ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
        //        return View();
        //    }
        //    //Them vao CSDL
        //    else
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            //Luu ten fie, luu y bo sung thu vien using System.IO;
        //            var fileName = Path.GetFileName(fileUpload.FileName);
        //            //Luu duong dan cua file
        //            var path = Path.Combine(Server.MapPath("~/img/"), fileName);
        //            //Kiem tra hình anh ton tai chua?
        //            if (System.IO.File.Exists(path))
        //                ViewBag.Thongbao = "Hình ảnh đã tồn tại";
        //            else
        //            {
        //                //Luu hinh anh vao duong dan
        //                fileUpload.SaveAs(path);
        //            }
        //            giay.Anhbia = fileName;
        //            //Luu vao CSDL   
        //            UpdateModel(giay);
        //            data.SubmitChanges();

        //        }
        //        return RedirectToAction("Giay");
        //    }
        //}

        public ActionResult Dangxuatadmin(FormCollection collection)
        {
            var tendn = collection["username"];
            var matkhau = collection["password"];
            Admin ad = data.Admins.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);
            Session["Taikhoanadmin"] = null;


            return RedirectToAction("Index", "COINCARD");


        }
        public ActionResult Quanlydonhang()
        {
            return View(data.DONDATHANGs.ToList());
        }
        //public ActionResult Chitietdonhang(int id)
        //{
        //    //Lay ra doi tuong sach theo ma
        //    DONDATHANG ct = data.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
        //    ViewBag.Magiay = ct.MaDonHang;
        //    if (ct == null)
        //    {
        //        Response.StatusCode = 404;
        //        return null;
        //    }
        //    return View(ct);
        //}
        public ActionResult XoaDH(int id)
        {
            //Lay ra doi tuong sach can xoa theo ma
            DONDATHANG dh = data.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            ViewBag.Magiay = dh.MaDonHang;
            if (dh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dh);
        }

        [HttpPost, ActionName("XoaDH")]
        public ActionResult XacnhanxoaDH(int id)
        {
            //Lay ra doi tuong sach can xoa theo ma
            DONDATHANG dh = data.DONDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            ViewBag.Magiay = dh.MaDonHang;
            if (dh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.DONDATHANGs.DeleteOnSubmit(dh);
            data.SubmitChanges();
            return RedirectToAction("Quanlydonhang");
        }
        public ActionResult Quanlyuser()
        {
            return View(data.KHACHHANGs.ToList());

        }
        public ActionResult Xoauser(int id)
        {
            //Lay ra doi tuong sach can xoa theo ma
            KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            ViewBag.MaKH = kh.MaKH;
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kh);
        }

        [HttpPost, ActionName("Xoauser")]
        public ActionResult XacnhanXoauser(int id)
        {
            //Lay ra doi tuong sach can xoa theo ma
            KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            ViewBag.MaKH = kh.MaKH;
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.KHACHHANGs.DeleteOnSubmit(kh);
            data.SubmitChanges();
            return RedirectToAction("Quanlyuser");
        }
    }
}