using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebQuanAn.Interfaces;
using WebQuanAn.Models;

namespace WebQuanAn.Controllers.HomeControl
{
    public class HomeController : Controller
    {
        private IHome _service;
        public HomeController(IHome service)
        {
            _service = service;
        }
        public IActionResult Index(ThucDonSearchModel model)
        {


            if (!model.Page.HasValue) model.Page = 1;
            var listPaged = _service.SearchByCondition(model);
            ViewBag.Names = listPaged;
            ViewBag.PhanLoai = _service.PhanloaiNav();
            ViewBag.Data = model;
            return View();
        }

   

        [HttpGet]

        public ActionResult PageList(ThucDonSearchModel model)
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
        [HttpGet]

        public ActionResult Detail(int id)
        {
            if (_service.Get(id) == null)
            {
                return NotFound();
            }
            else
            {
                ViewData["MaLoai"] = new SelectList(_service.PhanloaiNav(), "Id", "TenLoai", _service.Get(id).MaLoai);
                return PartialView("_partialDetail", _service.Get(id));
            }

        }

    }
}
