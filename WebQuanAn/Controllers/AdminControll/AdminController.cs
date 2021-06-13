using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebQuanAn.Interfaces;
using WebQuanAn.Constants;
using WebQuanAn.Models.ViewModels;
using WebQuanAn.Models;
using Newtonsoft.Json;

namespace WebQuanAn.Controllers
{
    public class AdminController : Controller
    {
        private IAdminUser _adminUser;
        public AdminController(IAdminUser adminUser)
        {
            _adminUser = adminUser;
        }
        public IActionResult Login(string returnUrl)
        {
            string userName = HttpContext.Session.GetString(SessionKey.Nguoidung.UserName);
            if(userName!=null&&userName!="")
            {
                return RedirectToAction("phanloai", "index");
            }
            ViewLogin login = new ViewLogin();
            login.ReturnUrl = returnUrl;
            return View(login);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(ViewLogin viewLogin)
        {
            if (ModelState.IsValid)
            {
                AdminUser nguoidung = _adminUser.UserLogin(viewLogin);
                if (nguoidung != null)
                {
                    HttpContext.Session.SetString(SessionKey.Nguoidung.UserName, nguoidung.Email);
                    HttpContext.Session.SetString(SessionKey.Nguoidung.FullName, nguoidung.Ten);
                    HttpContext.Session.SetString(SessionKey.Nguoidung.Pass, nguoidung.MatKhau);
                    HttpContext.Session.SetString(SessionKey.Nguoidung.Role, nguoidung.Role.ToString());
                    HttpContext.Session.SetString(SessionKey.Nguoidung.NguoidungContext,JsonConvert.SerializeObject(nguoidung));

                    return RedirectToAction("index","thucdon");
                   
                }
            }
            return View(viewLogin);
        }

       

        public IActionResult Logout()
        {
            HttpContext.Session.Remove(SessionKey.Nguoidung.UserName);
            HttpContext.Session.Remove(SessionKey.Nguoidung.FullName);
            HttpContext.Session.Remove(SessionKey.Nguoidung.Pass);
            HttpContext.Session.Remove(SessionKey.Nguoidung.Role);
            HttpContext.Session.Remove(SessionKey.Nguoidung.NguoidungContext);

            return RedirectToAction("login", "admin");
        }

      


    }
}
