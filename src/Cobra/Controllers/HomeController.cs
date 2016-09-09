using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cobra.Models;
using Cobra.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Cobra.Controllers
{
    //[RequireHttps]
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        private IHostingEnvironment _env;

        public HomeController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        // POST: PreOrders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateQuote(PreOrder preOrder)
        {
            if (ModelState.IsValid)
            {
                var combinedFiles = string.Empty;
                foreach (var file in preOrder.DesignFiles)
                {
                    var filename = ContentDispositionHeaderValue
                                    .Parse(file.ContentDisposition)
                                    .FileName
                                    .Trim('"');
                    var uniqueDir = _env.WebRootPath + $@"\appdata\{Guid.NewGuid().ToString().Replace("-","")}";
                    Directory.CreateDirectory(uniqueDir);
                    filename = $@"{uniqueDir}\{filename}";
                    using (FileStream fs = System.IO.File.Create(filename))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    combinedFiles += $"{combinedFiles};";
                }
                ViewBag.Message = $"Your request has been received!";
    

                preOrder.CreateTime = DateTime.Now;
                preOrder.UpdateTime = DateTime.Now;
                preOrder.MappedFiles = combinedFiles;
                preOrder.Status = PreOrderState.Submited;
                _context.PreOrder.Add(preOrder);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(preOrder);
        }

    }
}
