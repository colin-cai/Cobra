using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cobra.Models;
using Cobra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Cobra.Controllers
{
    [Authorize]
    public class PreOrdersController : Controller
    {
        private ApplicationDbContext _context;
        private IHostingEnvironment _env;
        private IDataProtector _protector;
        private IDataProtector _protectorBag;
        private readonly UserManager<ApplicationUser> _userManager;

        public PreOrdersController(ApplicationDbContext context, 
            IHostingEnvironment env, 
            IDataProtectionProvider provider,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _env = env;
            _protector = provider.CreateProtector("Cobra.Controllers.PreOrdersController.V1");
            _protectorBag = provider.CreateProtector("Cobra.Controllers.PreOrdersController.Bag.V1");
            _userManager = userManager;
        }

        // GET: PreOrders
        public IActionResult Index()
        {
            var userName = HttpContext.User.Identity;
            var orders = _context.PreOrder.Where(o => o.User.UserName == HttpContext.User.Identity.Name);
            foreach(var order in orders)
            {
                order.ObscureId = _protector.Protect(order.Id.ToString());
            }
            //_context.PreOrder.
            return View(orders.ToList());
        }

        public IActionResult Details(string obscureId)
        {
            if (obscureId == null)
            {
                return NotFound();
            }

            string unId = _protector.Unprotect(obscureId.ToString());
            int iid = int.Parse(unId);

            PreOrder preOrder = _context.PreOrder.Single(m => m.Id == iid);
            if (preOrder != null)
            {
                preOrder.ObscureId = obscureId;
                preOrder.PaperBags = _context.PaperBag.Where(o => o.PreOrderId == preOrder.Id).ToList();

                foreach (var bag in preOrder.PaperBags)
                {
                    bag.ObscureId = _protectorBag.Protect(bag.Id.ToString());
                    bag.ObscurePreOrderId = obscureId;
                }
            }
            else
            {
                return NotFound();
            }

            return View(preOrder);
        }

        // GET: PreOrders/Create
        public IActionResult Create(String product)
        {
            ViewBag.ProductTypes = Enum.GetNames(typeof(ProductType)).Select(o => new SelectListItem() { Text = o, Value = o });
            ViewBag.ProductType = product;


            //ProductType productType = (ProductType)Enum.Parse(typeof(ProductType), product);
            //PreOrder order = new PreOrder()
            //{
            //    ProductType = productType,
            //    Product = Product.Create(productType)

            //};

            //var paperbag = order.Items[0].Product as PaperBag;
            //paperbag.HasJHooks = true;

            PreOrder order = new PreOrder();
            return View(order);
            //return View();
        } 

        // POST: PreOrders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PreOrder preOrder)
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
                        combinedFiles += $"{filename};";
                    }
                }
                preOrder.MappedFiles = combinedFiles;

                preOrder.CreateTime = DateTime.Now;
                preOrder.UpdateTime = DateTime.Now;
                preOrder.User = await GetCurrentUserAsync();
                preOrder.UpdateUser = preOrder.User;
                preOrder.Status = PreOrderState.Submited;
                _context.PreOrder.Add(preOrder);
                _context.SaveChanges();

                preOrder.ObscureId = _protector.Protect(preOrder.Id.ToString());
                return RedirectToAction("Edit", new { obscureId = preOrder.ObscureId });
            }
            return View(preOrder);
        }

        // GET: PreOrders/Edit/5 
        public IActionResult Edit(string obscureId)
        {
            if (obscureId == null)
            {
                return NotFound();
            }

            string unId = _protector.Unprotect(obscureId.ToString());
            int iid = int.Parse(unId);

            PreOrder preOrder = _context.PreOrder.Single(m => m.Id == iid);
            if (preOrder != null)
            {
                preOrder.ObscureId = obscureId;
                preOrder.PaperBags = _context.PaperBag.Where(o => o.PreOrderId == preOrder.Id).ToList();

                foreach (var bag in preOrder.PaperBags)
                {
                    bag.ObscureId = _protectorBag.Protect(bag.Id.ToString());
                    bag.ObscurePreOrderId = unId;
                }
            }
            else
            {
                return NotFound();
            }

            return View(preOrder);
        }

        // POST: PreOrders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PreOrder preOrder)
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
                        combinedFiles += $"{filename};";
                    }
                }
                preOrder.MappedFiles = combinedFiles;

                preOrder.UpdateTime = DateTime.Now;
                preOrder.Status = PreOrderState.Submited;
                _context.Update(preOrder);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(preOrder);
        }

        // GET: PreOrders/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(string obscureId)
        {
            if (obscureId == null)
            {
                return NotFound();
            }

            string unId = _protector.Unprotect(obscureId);
            int iid = int.Parse(unId);

            PreOrder preOrder = _context.PreOrder.Single(m => m.Id == iid);
            preOrder.ObscureId = obscureId;
            if (preOrder == null)
            {
                return NotFound();
            }

            return View(preOrder);
        }

        // POST: PreOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string obscureId)
        {
            if (obscureId == null)
            {
                return NotFound();
            }

            string unId = _protector.Unprotect(obscureId);
            int iid = int.Parse(unId);

            var item = await _context.PreOrder.SingleOrDefaultAsync(m => m.Id == iid);
            _context.PreOrder.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #region Paper Bag
        // GET: PreOrders/Create
        public IActionResult CreatePaperBag(string obscureId)
        {
            ViewData["PreOrderId"] = obscureId;
            return View();
        }

        // POST: PreOrders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePaperBag(PaperBag paperBag)
        {
            if (ModelState.IsValid)
            {
                var factor = _context.FactorForPaperBag.FirstOrDefault();
                paperBag.EvaluatedPrice = paperBag.EvaluatePrice(factor);
                var first = _context.PaperBag.OrderByDescending(o => o.Id).FirstOrDefault();
                if (first != null)
                {
                    paperBag.Id = first.Id + 1;
                }
                else
                {
                    paperBag.Id = 10001;
                }

                if (paperBag.ObscurePreOrderId == null)
                {
                    return NotFound();
                }

                string unId = _protector.Unprotect(paperBag.ObscurePreOrderId);
                int iid = int.Parse(unId);
                paperBag.PreOrderId = iid;

                PreOrder preOrder = _context.PreOrder.Single(m => m.Id == iid);
                if (preOrder == null)
                {
                    preOrder = await CreateDefaultPreOrder();
                    _context.Add(preOrder);
                    paperBag.PreOrderId = preOrder.Id;
                }

                _context.Add(paperBag);
                _context.SaveChanges();

                preOrder.ObscureId = _protector.Protect(preOrder.Id.ToString());
                return RedirectToAction("Edit", new { obscureId = preOrder.ObscureId });
            }
            return View(paperBag);
        }

        // GET: PreOrders/Edit/5 
        public IActionResult EditPaperBag(String obscureId)
        {
            if (obscureId == null)
            {
                return NotFound();
            }

            string unId = _protectorBag.Unprotect(obscureId.ToString());
            int iid = int.Parse(unId);

            var paperBag = _context.PaperBag.Single(m => m.Id == iid);
            if (paperBag == null)
            {
                return NotFound();
            }
            else
            {
                paperBag.ObscureId = obscureId;
                paperBag.ObscurePreOrderId = _protector.Protect(paperBag.PreOrderId.ToString());
            }

            return View(paperBag);
        }

        // POST: PreOrders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPaperBag(PaperBag paperBag)
        {
            if (ModelState.IsValid)
            {
                var factor = _context.FactorForPaperBag.FirstOrDefault();
                paperBag.EvaluatedPrice = paperBag.EvaluatePrice(factor);
                _context.Update(paperBag);
                _context.SaveChanges();
                return RedirectToAction("Edit", new { obscureId = _protector.Protect(paperBag.PreOrderId.ToString()) });
            }

            return View(paperBag);
        }

        // GET: PreOrders/Details/5
        public IActionResult DetailsPaperBag(String obscureId)
        {
            if (obscureId == null)
            {
                return NotFound();
            }

            string unId = _protectorBag.Unprotect(obscureId.ToString());
            int iid = int.Parse(unId);

            var paperBag = _context.PaperBag.Single(m => m.Id == iid);
            if (paperBag == null)
            {
                return NotFound();
            }
            else
            {
                paperBag.ObscureId = _protectorBag.Protect(paperBag.Id.ToString());
                paperBag.ObscurePreOrderId = _protector.Protect(paperBag.PreOrderId.ToString());
            }

            return View(paperBag);
        }

        public IActionResult DeletePaperBag(String obscureId)
        {
            if (obscureId == null)
            {
                return NotFound();
            }

            string unId = _protectorBag.Unprotect(obscureId.ToString());
            int iid = int.Parse(unId);

            var paperBag = _context.PaperBag.Single(m => m.Id == iid);
            if (paperBag == null)
            {
                return NotFound();
            }
            paperBag.ObscureId = obscureId;
            paperBag.ObscurePreOrderId = _protector.Protect(paperBag.PreOrderId.ToString());

            return View(paperBag);
        }

        [HttpPost, ActionName("DeletePaperBag")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePaperBagConfirmed(string obscureId)
        {
            if (obscureId == null)
            {
                return NotFound();
            }

            string unId = _protectorBag.Unprotect(obscureId.ToString());
            int iid = int.Parse(unId);

            var paperBag = _context.PaperBag.Single(m => m.Id == iid);
            if (paperBag == null)
            {
                return NotFound();
            }

            PreOrder preOrder = _context.PreOrder.Single(m => m.Id == paperBag.PreOrderId);
            if (preOrder == null)
            {
                return NotFound();
            }

            if (preOrder.PaperBags != null)
            {
                preOrder.PaperBags.Remove(paperBag);
            }

            _context.PaperBag.Remove(paperBag);
            await _context.SaveChangesAsync();

            preOrder.ObscureId = _protector.Protect(preOrder.Id.ToString());
            return RedirectToAction("Edit", new { obscureId = preOrder.ObscureId });
        }
        #endregion

        private async Task<PreOrder> CreateDefaultPreOrder()
        {
            var preOrder = new PreOrder();
            preOrder.CreateTime = DateTime.Now;
            preOrder.UpdateTime = DateTime.Now;
            preOrder.User = await GetCurrentUserAsync();
            preOrder.UpdateUser = preOrder.User;
            preOrder.Email = preOrder.User.UserName;
            preOrder.Phone = preOrder.User.PhoneNumber;
            preOrder.Status = PreOrderState.New;

            return preOrder;
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

    }
}
