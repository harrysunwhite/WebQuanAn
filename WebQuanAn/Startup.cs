


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebQuanAn.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Http;
using WebQuanAn.Services;
using WebQuanAn.Interfaces;
using WebQuanAn.Helper;
using WebQuanAn.Areas.Identity.Data;

namespace WebQuanAn
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDistributedMemoryCache();           // Đăng ký dịch vụ lưu cache trong bộ nhớ (Session sẽ sử dụng nó)
            services.AddSession(option => { option.IdleTimeout = TimeSpan.FromMinutes(30); });
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = ".AspNetCore.Identity.Application";
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                options.SlidingExpiration = true;
            });

            services.AddDbContextPool<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DataContextConnection")));
            services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false).AddErrorDescriber<CustomErrorDescriber>()
                    .AddEntityFrameworkStores<DataContext>();
            services.AddRazorPages();
            services.AddTransient<IPhanLoai,PhanLoaisvc>();
            services.AddTransient<IThucDon, ThucDonsvc>();
            services.AddTransient<IAdminUser, AdminUsersvc>();
            services.AddTransient<IASMHelper, ASMHelper>();
            services.AddTransient<IKhachHang, KhachHangsvc>();
            services.AddTransient<IReport, Reportsvc>();
            services.AddTransient<IHome, Homesvc>();
            
            services.AddControllersWithViews();
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}


