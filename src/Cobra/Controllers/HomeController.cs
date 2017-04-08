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
using Microsoft.AspNetCore.DataProtection;

namespace Cobra.Controllers
{
    //[RequireHttps]
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        private IHostingEnvironment _env;
        private IDataProtector _protector;

        public HomeController(ApplicationDbContext context, IHostingEnvironment env, IDataProtectionProvider provider)
        {
            _context = context;
            _env = env;
            _protector = provider.CreateProtector("Cobra.Controllers.PreOrdersController.V1");
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
                if (preOrder.DesignFiles != null)
                {
                    foreach (var file in preOrder.DesignFiles)
                    {
                        var filename = ContentDispositionHeaderValue
                                        .Parse(file.ContentDisposition)
                                        .FileName
                                        .Trim('"');
                        var uniqueDir = _env.WebRootPath + $@"\appdata\{Guid.NewGuid().ToString().Replace("-", "")}";
                        Directory.CreateDirectory(uniqueDir);
                        filename = $@"{uniqueDir}\{filename}";
                        using (FileStream fs = System.IO.File.Create(filename))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                        combinedFiles += $"{combinedFiles};";
                    }
                }
                ViewBag.Message = $"Your request has been received!";

                preOrder.CreateTime = DateTime.Now;
                preOrder.UpdateTime = DateTime.Now;
                preOrder.User = new ApplicationUser { UserName = HttpContext.User.Identity.Name };
                preOrder.UpdateUser = preOrder.User;
                preOrder.MappedFiles = combinedFiles;
                preOrder.Status = PreOrderState.Submited;
                _context.PreOrder.Add(preOrder);
                _context.SaveChanges();

                preOrder.ObscureId = _protector.Protect(preOrder.Id.ToString());
                return RedirectToAction(nameof(PreOrdersController.Edit), "PreOrders", new { obscureId = preOrder.ObscureId });
            }
            return View(preOrder);
        }

    }
}
