using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COINCARD.Models
{
    public class Giohang
    {
        QLBanGiayDataContext data = new QLBanGiayDataContext();
        public int iMagiay { get; set; }
        public string sTengiay { get; set; }
        public string iSize { get; set; }
        public string sAnhbia { get; set; }
        public Double dDongia { get; set; }
        public int iSoluong { set; get; }
        public Double dThanhtien
        {
            get { return iSoluong * dDongia; }

        }
        public Giohang(int Magiay)
        {
            iMagiay = Magiay;
            GIAY giay = data.GIAYs.Single(n => n.Magiay == iMagiay);
            iSize = giay.Size;
            sTengiay = giay.Tengiay;
            sAnhbia = giay.Anhbia;
            dDongia = double.Parse(giay.Giaban.ToString());
            iSoluong = 1;
        }

    }
}