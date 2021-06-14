using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebQuanAn.Models;
using X.PagedList;

namespace WebQuanAn.Interfaces
{
    public interface IHome
    {
        ThucDon Get(int id); 
        IEnumerable<PhanLoai> PhanloaiNav();
        IEnumerable<DonHang> GetDonHangs(string Makh);
        public List<CTHD> GetCTHDs(string id);
       
        IPagedList<ThucDon> SearchByCondition(ThucDonSearchModel model);
        public DonHang Add(DonHang donHang, List<CartItemModel> cartItems);
    }
}
