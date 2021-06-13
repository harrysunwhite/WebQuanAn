
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebQuanAn.Models;
using WebQuanAn.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace WebQuanAn.Controllers
{
   
    public class KhachHangController : BaseController
    {
        private readonly IKhachHang _service;
        public KhachHangController(IKhachHang service)
        {
            _service = service;
        }

        public IActionResult Index(KhachHangSearchModel model)
        {
           
            if (!model.Page.HasValue) model.Page = 1;
            var listPaged = _service.SearchByCondition(model);
                                        ViewBag.Names = listPaged;
            ViewBag.Data = model;
            return View();
        }
       
      
        [HttpGet]
       
        public ActionResult PageList(KhachHangSearchModel model)
        {
            
          if  (_service.SearchByCondition(model).Count()>0)
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

        public ActionResult Detail(string id)
        {

            if (_service.Get(id) == null)
            {
                return NotFound();
            }
            else
                return PartialView("_partialDetail", _service.Get(id));
        }

        //public IActionResult Add()
        //{

        //    return PartialView("_partialAdd");

        //}
        //[HttpPost]


        //public IActionResult Add( KhachHang model)
        //{
        // if(ModelState.IsValid)
        //    {
        //                     if (_service.Add(model)!=null)
        //       return Json(new { status = 1, title = "", text = "Thêm thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
        //    else
        //       return Json(new { status = -2, title = "", text = "Thêm không thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
        //    }
        //    return Json(model);

        //} 
        //[HttpGet]

        //public ActionResult Edit(Int32 id)
        //{
        //    if (_service.Get(id) == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {

        //      return PartialView("_partialedit", _service.Get(id));
        //    }

        //}


        //[HttpPost]

        //public ActionResult Edit( KhachHang model)
        //{
        //   if(ModelState.IsValid)
        //    {
        //         if (_service.Edit(model)!=null)
        //        return Json(new { status = 1, title = "", text = "Cập nhật thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
        //    else
        //        return Json(new { status = -2, title = "", text = "Cập nhật không thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
        //    }
        //    return Json(model);

        //}

        //[HttpPost]
        //public ActionResult Delete(Int32 id)
        //{
        //    if (_service.Delete(id)) 
        //        return Json(new { status = 1, title = "", text = "Xoá thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
        //    else
        //        return Json(new { status = -2, title = "", text = "Xoá không thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
        //}
    }
}


