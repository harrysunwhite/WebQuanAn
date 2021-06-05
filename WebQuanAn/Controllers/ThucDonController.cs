
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebQuanAn.Models;
using WebQuanAn.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;
using System.IO;

namespace WebQuanAn.Controllers
{
   
    public class ThucDonController : BaseController
    {
        private readonly IThucDon _service;
        private IWebHostEnvironment hostingEnv;
        public ThucDonController(IThucDon service, IWebHostEnvironment env)
        {
            this.hostingEnv = env;
            _service = service;
        }
        
        public IActionResult Index(ThucDonSearchModel model)
        {
           
           
            if (!model.Page.HasValue) model.Page = 1;
            var listPaged = _service.SearchByCondition(model);
            ViewBag.Names = listPaged;
            ViewBag.Data = model;
            return View();
        }
       
   
        [HttpGet]
        
        public ActionResult PageList(ThucDonSearchModel model)
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
         
        public IActionResult Add()
        {
            ViewData["MaLoai"] = new SelectList(_service.PhanloaiNav(), "Id", "TenLoai");

            return PartialView("_partialAdd");

        }
        [HttpPost]
       

        public IActionResult Add( ThucDon model)
        {
            
            
         if(ModelState.IsValid)
            {
                if(string.Compare(model.HinhAnh,"unchose.jpg",false)!=0)
                model.HinhAnh = DateTime.Now.ToString("ddmmyyyy_HHm") + "_" + model.HinhAnh;


                if (_service.Add(model)!=0)
               return Json(new { status = 1, title = "", text = "Thêm thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            else
               return Json(new { status = -2, title = "", text = "Thêm không thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            }
            return NoContent();
            
        } 
        [HttpGet]
       
        public ActionResult Edit(int id)
        {
            if (_service.Get(id) == null)
            {
                return NotFound();
            }
            else
            {
                ViewData["MaLoai"] = new SelectList(_service.PhanloaiNav(), "Id", "TenLoai", _service.Get(id).MaLoai);
                return PartialView("_partialedit", _service.Get(id));
            }    
                
        }

       
        [HttpPost]
       
        public ActionResult Edit( ThucDon model)
        {

           if(ModelState.IsValid)
            {
               
                    model.HinhAnh = DateTime.Now.ToString("ddmmyyyy_HHm") + "_" + model.HinhAnh;
                if (_service.Edit(model)!=0)
                return Json(new { status = 1, title = "", text = "Cập nhật thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            else
                return Json(new { status = -2, title = "", text = "Cập nhật không thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            }
            return NoContent();
           
        }

        [HttpPost]

        public ActionResult EditNoImage(ThucDon model)
        {

            if (ModelState.IsValid)
            {
               
                if (_service.Edit(model) != 0)
                    return Json(new { status = 1, title = "", text = "Cập nhật thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
                else
                    return Json(new { status = -2, title = "", text = "Cập nhật không thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            }
            return NoContent();

        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            ThucDon td = _service.Get(id);
            td.TrangThai = false;
            if (_service.Edit(td)!=0) 
                return Json(new { status = 1, title = "", text = "Thao tác thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            else
                return Json(new { status = -2, title = "", text = "Thao tác không thành công", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        [HttpPost]
        public ActionResult Restore(int id)
        {
            ThucDon td = _service.Get(id);
            td.TrangThai = true;
            if (_service.Edit(td) != 0)
                return Json(new { status = 1, title = "", text = "Thao tác thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            else
                return Json(new { status = -2, title = "", text = "Thao tác không thành công", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
        }    

    }
}


