﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TranDucKhang_15DH110240MVC.Models
{
    public class Giohang
    {
        dbQLBansachDataContext data = new dbQLBansachDataContext();
        public int iMasach { set; get; }
        public string sTensach { get; set; }
        public string sAnhbia { get; set; }
        public Double dDongia { get; set; }
        public int iSoluong { get; set; }
        public Double dThanhtien
        {
            get { return iSoluong * dDongia; }
        }

        public Giohang (int Masach)
        {
            iMasach = Masach;
            SACH sach = data.SACHes.Single(n => n.Masach == iMasach);
            sTensach = sach.Tensach;
            sAnhbia = sach.Hinhminhhoa;
            dDongia = double.Parse(sach.Dongia.ToString());
            iSoluong = 1;
        }

    }
}