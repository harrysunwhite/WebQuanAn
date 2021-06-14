using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebQuanAn.Constants;
using WebQuanAn.Models;
using Microsoft.AspNetCore.Authorization;
using WebQuanAn.Interfaces;

namespace WebQuanAn.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private ILogger<CartController> _logger;

        private IHome _service;
        public CartController(ILogger<CartController> logger, IHome service)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]

        public List<CartItemModel> ListCart()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString(SessionKey.Cart.CartItem);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItemModel>>(jsoncart);
            }
            return new List<CartItemModel>();
        }
        [HttpGet]

        public int countQuantity()
        {
            var cart = ListCart();
            int count = 0;
            foreach (var item in cart)
            {
                count += item.Quantity;
            }
            return count;
        }
        void SaveCartSession(List<CartItemModel> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(SessionKey.Cart.CartItem, jsoncart);
        }
        [HttpGet]

        public IActionResult AddToCart([FromRoute] int id)
        {

            var thucDon = _service.Get(id);

            // Xử lý đưa vào Cart ...
            var cart = ListCart();
            var cartitem = cart.Find(p => p.ThucDon.Id == id);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.Quantity++;
            }
            else
            {
                //  Thêm mới
                cart.Add(new CartItemModel() { Quantity = 1, ThucDon = thucDon });
            }

            // Lưu cart vào Session
            SaveCartSession(cart);
            // Chuyển đến trang hiện thị Cart

            return PartialView("~/Views/Home/_CartPartial.cshtml", ListCart());
        }
        public IActionResult RemoveCart([FromRoute] int Id)
        {
            var cart = ListCart();
            var cartitem = cart.Find(p => p.ThucDon.Id == Id);
            if (cartitem != null)
            {

                cart.Remove(cartitem);
            }

            SaveCartSession(cart);
            return PartialView("~/Views/Home/_CartPartial.cshtml", ListCart());
        }

        public IActionResult EmptyCart()
        {
            return PartialView("~/Views/Home/_EmptyCart.cshtml");
        }





        [HttpPost]
        public IActionResult UpdateCart(int id, int quantity)
        {

            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = ListCart();
            var cartitem = cart.Find(p => p.ThucDon.Id == id);
            if (cartitem != null)
            {

                cartitem.Quantity = quantity;
            }
            SaveCartSession(cart);

            return View("~/Views/Home/_CartFinalPartial.cshtml");
        }

        [HttpPost]

        public IActionResult Create([Bind("MaKH,DiaChi,GhiChu")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                donHang.MaDH = DateTime.Now.ToString("TDddMMyyyy_HHmmss");
                donHang.ThoiGian = DateTime.Now;
                donHang.TrangThai = TrangThai._1;

                var liscart = ListCart();

                if (_service.Add(donHang, liscart) != null)
                {
                    HttpContext.Session.Remove(SessionKey.Cart.CartItem);
                    return Json(new { status = 1, title = "", text = "Gửi đơn hàng thành công!", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
                }

                else
                {
                    return Json(new { status = -2, title = "", text = "Đã xảy ra lỗi khi gửi đơn hàng", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
                }

            }
            return Json(donHang);
        }

        public IActionResult Cart()
        {
            return PartialView("~/Views/Home/_CartPartial.cshtml", ListCart());
        }

        public IActionResult CartFinal()
        {
            return View("~/Views/Home/_CartFinalPartial.cshtml");
        }

        public IActionResult CartDetail(string id)
        {

            var listTD = _service.GetCTHDs(id);
            return PartialView("~/Views/Home/_CartDetail.cshtml", listTD);




        }

    }
        
     
}
