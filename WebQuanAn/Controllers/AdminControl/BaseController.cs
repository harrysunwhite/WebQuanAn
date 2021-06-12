using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebQuanAn.Constants;
using WebQuanAn.Filters;

namespace WebQuanAn.Controllers
{


    [AuthenticationFilter]
    public abstract class BaseController : Controller
    {
        public BaseController()
        {
        }

        protected string GetUserName()
        {
            return HttpContext.Session.GetString(SessionKey.Nguoidung.UserName);
        }
        protected string GetFullName()
        {
            return HttpContext.Session.GetString(SessionKey.Nguoidung.FullName);
        }


    }


}
