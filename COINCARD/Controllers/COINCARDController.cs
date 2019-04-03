using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using COINCARD.Models;

namespace COINCARD.Controllers
{
    public class COINCARDController : Controller
    {
        // GET: COINCARD
        QLBanGiayDataContext data = new QLBanGiayDataContext();
        
        public ActionResult Index( )
        {
          
            return View();
        }
        public ActionResult Lienhe()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Thuonghieu()
        {
            var thuonghieu = from th in data.THUONGHIEUs select th;
            return PartialView(thuonghieu);
        }
        public ActionResult SPtheothuonghieu(int id)
        {
            
           
            var giay = from s in data.GIAYs where s.MaTH == id select s;


            return View(giay);



        }
        public ActionResult Details(int id)
        {
            var giay = from s in data.GIAYs
                       where s.Magiay == id
                       select s;
            return View(giay.Single());
        }
    }
}