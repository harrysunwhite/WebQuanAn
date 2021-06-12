using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebQuanAn.Interfaces;
using WebQuanAn.Models;

namespace WebQuanAn.Controllers
{
    public class ReportController : Controller
    {
        private IReport _service;
        public ReportController(IReport service)
        {
            _service = service;
        }
        public ActionResult Index(DonHangSearchModel model)
        {
            if (!model.Page.HasValue) model.Page = 1;
            var listPaged = _service.SearchByCondition(model);
            ViewBag.Names = listPaged;
            ViewBag.Data = model;
            return View();
            
        }

        [HttpGet]

        public ActionResult PageList(DonHangSearchModel model)
        {
           
            if (_service.SearchByCondition(model).Count() > 0)
            {

                if (!model.Page.HasValue) model.Page = 1;



                var listmodel = _service.SearchByCondition(model);
                ViewBag.Names = listmodel;
                ViewBag.Data = model;

                return PartialView("_NameListPartial", ViewBag.Names);
            }
            else
            {

                return Json(new { status = -2, title = "", text = "Không tìm thấy", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            }

        }

        public IActionResult CartDetail(int id)
        {

               var listTD = _service.GetCTHDs(id);
                return PartialView("_CartDetail", listTD);
           

          

        }
        [HttpPost]
        public IActionResult Update(int id,int trangThai)
        {
            var donHang = _service.Get(id);
            donHang.TrangThai = (TrangThai)trangThai;
                if (_service.Edit(donHang) != null)
                    return Json(new { status = 1, title = "", text = "Cập nhật thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
                else
                    return Json(new { status = -2, title = "", text = "Cập nhật không thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            
            
        }


        public ActionResult Details(int id)
        {
            return View();
        }

       
    }
}
