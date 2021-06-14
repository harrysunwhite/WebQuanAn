using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebQuanAn.Interfaces;
using WebQuanAn.Models;
using X.PagedList;

namespace WebQuanAn.Services
{
    public class Homesvc:IHome
    {
        private static int pageSize = 4;
        private readonly DataContext _context;

        public Homesvc(DataContext context)
        {
            _context = context;

        }
        public ThucDon Get(int id)
        {

            var item = _context.ThucDon.Find(id);

            if (item == null)
            {
                return null;
            }
            return item;


        }
        public IEnumerable<PhanLoai> PhanloaiNav()
        {
            return _context.PhanLoai.ToList();
        }


        public DonHang Add(DonHang donHang,List<CartItemModel> cartItems)
        {
            try
            {
                _context.Add(donHang);
                _context.SaveChanges();
                foreach (var item in cartItems)
                {
                    var TempData = new CTHD();
                    TempData.MaDh = donHang.MaDH;
                    TempData.MaTd = item.ThucDon.Id;
                    TempData.SoLuong = item.Quantity;
                    _context.Add(TempData);
                    _context.SaveChanges();
                }
                return donHang;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;

            }
        }

        public List<CTHD> GetCTHDs(string id)
        {
            var Listitem = _context.CTHD.Where(d => d.MaDh == id).Include(i => i.MaTdNavigation).Include(i => i.MaDhNavigation).ToList();

            if (Listitem == null)
            {
                return null;
            }
            return Listitem;
        }

        public IEnumerable<DonHang> GetDonHangs(string Makh)
        {
            return _context.DonHang.Where(x => x.MaKH == Makh).OrderByDescending(t=>t.ThoiGian).ToList();
        }
        public IPagedList<ThucDon> SearchByCondition(ThucDonSearchModel model)
        {

            IEnumerable<ThucDon> listUnpaged;
            listUnpaged = _context.ThucDon.Where(x=>x.TrangThai == true).Include(t => t.MaLoaiNavigation);

            if (!string.IsNullOrWhiteSpace(model.TenMon))

            {
                listUnpaged = listUnpaged.Where(x => x.TenMon.ToUpper().Contains(model.TenMon.ToUpper()));
            }




            if (model.Gia == 1) listUnpaged = listUnpaged.Where(x => x.Gia < 100000);
            if (model.Gia == 2) listUnpaged = listUnpaged.Where(x => x.Gia >= 100000 && x.Gia <= 300000);
            if (model.Gia == 3) listUnpaged = listUnpaged.Where(x => x.Gia > 300000);
            if (model.MaLoai != 0) listUnpaged = listUnpaged.Where(x => x.MaLoai == model.MaLoai);

          




            var listPaged = listUnpaged.ToPagedList(model.Page ?? 1, pageSize);


            if (listPaged.PageNumber != 1 && model.Page.HasValue && model.Page > listPaged.PageCount)
                return null;

            return listPaged;





        }

    }
}
