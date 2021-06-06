using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace WebQuanAn.Controllers
{
    public class PageHelperController : Controller
    {
        private IWebHostEnvironment hostingEnv;
        public PageHelperController(IWebHostEnvironment env)
        {
            this.hostingEnv = env;
        }    




        [HttpPost]

        public string CoppyImage()

        {

            string result = string.Empty;

            try

            {

                long size = 0;

                var file = Request.Form.Files;

                var filename = ContentDispositionHeaderValue

                                .Parse(file[0].ContentDisposition)

                                .FileName

                                .Trim('"');


                string filePath = hostingEnv.WebRootPath + @"\Images\" + DateTime.Now.ToString("ddmmyyyy_HHm") + "_" + $@"{ filename}";
             
                size += file[0].Length;

                using (FileStream fs = System.IO.File.Create(filePath))

                {

                    file[0].CopyTo(fs);

                    fs.Flush();

                }



                result = filePath;

            }

            catch (Exception ex)

            {

                result = ex.Message;

            }



            return result;

        }
    }
}
