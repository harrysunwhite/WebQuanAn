
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebQuanAn.Models;
using WebQuanAn.Interfaces;


namespace WebQuanAn.Controllers
{
   
    public class AdminUserController : Controller
    {
        private readonly IAdminUser _service;
        public AdminUserController(IAdminUser service)
        {
            _service = service;
        }

        public IActionResult Index(AdminUserSearchModel model)
        {
           
            if (!model.Page.HasValue) model.Page = 1;
            var listPaged = _service.SearchByCondition(model);
            ViewBag.Names = listPaged;
            ViewBag.Data = model;
            return View();
        }
       
        // GET: DepartmentController
        [HttpGet]
        
        public ActionResult PageList(AdminUserSearchModel model)
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
          
            
            return PartialView("_partialAdd");

        }
        [HttpPost]
       

        public IActionResult Add( AdminUser model)
        {

         if(ModelState.IsValid)
            {
                if (string.Compare(model.Hinh, "unchoseUser.png", false) != 0)
                    model.Hinh = DateTime.Now.ToString("ddmmyyyy_HHm") + "_" + model.Hinh;

               
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
                return PartialView("_partialedit", _service.Get(id));
        }

       
        [HttpPost]
       
        public ActionResult Edit( AdminUser model)
        {
            model.MatkhauCF = model.MatKhau;
          
                model.Hinh = DateTime.Now.ToString("ddmmyyyy_HHm") + "_" + model.Hinh;
                if (_service.Edit(model)!=0)
                return Json(new { status = 1, title = "", text = "Cập nhật thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            else
                return Json(new { status = -2, title = "", text = "Cập nhật không thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
           
           
        }


        [HttpPost]

        public ActionResult EditNoImage(AdminUser model)
        {

            model.MatkhauCF = model.MatKhau;

            if (_service.Edit(model) != 0)
                    return Json(new { status = 1, title = "", text = "Cập nhật thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
                else
                    return Json(new { status = -2, title = "", text = "Cập nhật không thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
           
            
            

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (_service.Delete(id)) 
                return Json(new { status = 1, title = "", text = "Xoá thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            else
                return Json(new { status = -2, title = "", text = "Xoá không thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
        }
    }
}


