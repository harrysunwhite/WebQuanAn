using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebQuanAn.Areas.Identity.Data;
using WebQuanAn.Models;
using WebQuanAn.Interfaces;

namespace WebQuanAn.Areas.Identity.Pages.Account.Manage
{
    [Authorize]
    public class CartInfoModel : PageModel
    {
        private readonly IHome _service;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public CartInfoModel(IHome service, UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _service = service;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [BindProperty]
        public IEnumerable<DonHang> donHangs { get; set; } 
        public IActionResult OnGet()
        {
            var id = _userManager.GetUserId(User);
            donHangs = _service.GetDonHangs(id);
            return Page();
        }
    }
}
