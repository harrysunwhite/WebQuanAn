
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebQuanAn.Models;
using WebQuanAn.Interfaces;
using WebQuanAn.Constants;

namespace WebQuanAn.Controllers
{
   
    public class AdminUserController : BaseController
    {
        private readonly IAdminUser _service;
        private static bool isUpdate;
        public AdminUserController(IAdminUser service)
        {
            _service = service;
        }

        public IActionResult Index(AdminUserSearchModel model)
        {
            isUpdate = false;
           
            if (!model.Page.HasValue) model.Page = 1;
            var listPaged = _service.SearchByCondition(model);
            ViewBag.Names = listPaged;
            ViewBag.Data = model;
            return View();
        }
       
      
        [HttpGet]
        
        public ActionResult PageList(AdminUserSearchModel model)
        {
            isUpdate = false;
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
            isUpdate = false;
            
            return PartialView("_partialAdd");

        }
        [HttpPost]
       

        public IActionResult Add( AdminUser model)
        {
            isUpdate = false;
         if(ModelState.IsValid)
            {
                
                    

                int Id = _service.Add(model);
                if (Id!=0)
               return Json(new { status = Id, title = "", text = "Thêm thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            else
               return Json(new { status = -2, title = "", text = "Thêm không thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            }
            return Json(model); ;
            
        } 
        [HttpGet]
       
        public ActionResult Edit(int id)
        {
            isUpdate = true;
            if (_service.Get(id) == null)
            {
                return NotFound();
            }
            else
                return PartialView("_partialedit", _service.GetUpdate(id));
        }

        public ActionResult Detail(int id)
        {
            
            if (_service.Get(id) == null)
            {
                return NotFound();
            }
            else
                return PartialView("_partialDetail", _service.Get(id));
        }


        [HttpPost]
       
        public ActionResult Edit( UpdateModel model)
        {
            

            
            
            if (ModelState.IsValid)
            {
                int id = _service.Edit(model);

               
                if (id != 0)
                    return Json(new { status = id, title = "", text = "Cập nhật thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
                else
                    return Json(new { status = -2, title = "", text = "Cập nhật không thành công.", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());

            }
            else
                return Json(model);
        }


       

        [HttpPost]
        public ActionResult Delete(int id)
        {
            UpdateModel adminUser = _service.GetUpdate(id);
            adminUser.uTrangThai = false;
            if (_service.Edit(adminUser)!=0) 
                return Json(new { status = 1, title = "", text = "Thao tác thành công ", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            else
                return Json(new { status = -2, title = "", text = "Thao tác không thành công", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
        }
        [HttpPost]
        public ActionResult Restore(int id)
        {
            UpdateModel adminUser = _service.GetUpdate(id);
            adminUser.uTrangThai = true;
            if (_service.Edit(adminUser) != 0)
                return Json(new { status = 1, title = "", text = "Thao tác thành công ", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
            else
                return Json(new { status = -2, title = "", text = "Thao tác không thành công", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
        }

        public IActionResult ValidateEmail(string email)
        {
            if(isUpdate )
            {
                return Json(data: true);
            }    
            if (!_service.CheckEmail(email))
                return Json(data: "Địa chỉ mail đã tồn tại");

                return Json(data: true);

        }

        public IActionResult ChangePass()
        {
            Console.WriteLine("hello");
            return PartialView("_changePass"); ;
        }

        [HttpPost]
        public IActionResult ChangePass(ChangePassModel changePass)
        {
            if (ModelState.IsValid)
            {
                if(_service.CheckNewPass(changePass.UserEmail,changePass.Oldpass))
                {
                    if (_service.ChangePass(changePass))
                    {
                        HttpContext.Session.Clear();
                        return Json(new { status = 1, title = "", text = "Thao tác thành công ", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
                    }

                    else
                        return Json(new { status = -2, title = "", text = "Thao tác không thành công", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());
                }    
                return Json(new { status = -3, title = "", text = "Mật khẩu cũ không đúng", obj = "" }, new Newtonsoft.Json.JsonSerializerSettings());


            }
            return Json(changePass);
        }


        public IActionResult ValidateNewPass(string Oldpass)
        {
            string email = HttpContext.Session.GetString(SessionKey.Nguoidung.UserName);
            Console.WriteLine(_service.CheckNewPass(email, Oldpass));
            if (!_service.CheckNewPass(email, Oldpass))
                return Json(data: "Mật khẩu cũ không đúng");

            return Json(data: true);
        }

        
    }
}


