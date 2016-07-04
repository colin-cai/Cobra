using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cobra.Models;
using Cobra.Data;

namespace Cobra.Controllers
{
    public class PreOrdersController : Controller
    {
        private ApplicationDbContext _context;

        public PreOrdersController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: PreOrders
        public IActionResult Index()
        {
            return View(_context.PreOrder.ToList());
        }

        // GET: PreOrders/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PreOrder preOrder = _context.PreOrder.Single(m => m.Id == id);
            if (preOrder != null)
            {
                preOrder.PaperBags = _context.PaperBag.Where(o => o.PreOrderId == preOrder.Id).ToList();
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
        public IActionResult Create(PreOrder preOrder)
        {
            if (ModelState.IsValid)
            {
                preOrder.CreateTime = DateTime.Now;
                preOrder.UpdateTime = DateTime.Now;
                preOrder.Status = PreOrderState.Submited;
                _context.PreOrder.Add(preOrder);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(preOrder);
        }

        // GET: PreOrders/Create
        public IActionResult CreatePaperBag(int id)
        {
            ViewData["PreOrderId"] = id;
            return View();
        }

        // POST: PreOrders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePaperBag(PaperBag paperBag)
        {
            if (ModelState.IsValid)
            {
                //PreOrder preOrder = _context.PreOrder.Single(m => m.Id == paperBag.PreOrderId);
                //if (preOrder == null)
                //{
                //    return NotFound();
                //}

                //if(preOrder.PaperBags == null)
                //{
                //    preOrder.PaperBags = new System.Collections.Generic.List<PaperBag>();
                //}

                //preOrder.PaperBags.Add(paperBag);
                var first = _context.PaperBag.OrderByDescending(o => o.Id).FirstOrDefault();
                if (first != null)
                {
                    paperBag.Id = first.Id + 1;
                }
                _context.Add(paperBag);

                _context.SaveChanges();

                return RedirectToAction("Details", new { id = paperBag.PreOrderId });
            }
            return View(paperBag);
        }

        // GET: PreOrders/Create
        public IActionResult DeletePaperBag(int? orderId, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PaperBag paperBag = _context.PaperBag.Single(m => m.Id == id);
            if (paperBag == null)
            {
                return NotFound();
            }
            paperBag.PreOrder = _context.PreOrder.Single(m => m.Id == paperBag.PreOrderId);

            return View(paperBag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePaperBag(int id)
        {
            PaperBag paperBag = _context.PaperBag.Single(m => m.Id == id);

            if (paperBag != null)
            {
                PreOrder preOrder = _context.PreOrder.Single(m => m.Id == paperBag.PreOrderId);
                if (preOrder != null && preOrder.PaperBags != null)
                {
                    preOrder.PaperBags.Remove(paperBag);
                }
            }
            _context.PaperBag.Remove(paperBag);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: PreOrders/Create
        public IActionResult CreatePaperBox()
        {
            ViewBag.ProductTypes = Enum.GetNames(typeof(ProductType)).Select(o => new SelectListItem() { Text = o, Value = o });

            PreOrder order = new PreOrder();
            return View(order);
        }

        // POST: PreOrders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePaperBox(PreOrder preOrder)
        {
            if (ModelState.IsValid)
            {
                _context.PreOrder.Add(preOrder);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(preOrder);
        }

        // GET: PreOrders/Edit/5 
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PreOrder preOrder = _context.PreOrder.Single(m => m.Id == id);
            if (preOrder != null)
            {
                preOrder.PaperBags = _context.PaperBag.Where(o => o.PreOrderId == preOrder.Id).ToList();
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
                preOrder.Status = PreOrderState.Submited;
                _context.Update(preOrder);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(preOrder);
        }

        // GET: PreOrders/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PreOrder preOrder = _context.PreOrder.Single(m => m.Id == id);
            if (preOrder == null)
            {
                return NotFound();
            }

            return View(preOrder);
        }

        // POST: PreOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            PreOrder preOrder = _context.PreOrder.Single(m => m.Id == id);
            _context.PreOrder.Remove(preOrder);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
