using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Cobra.Models;

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
                return HttpNotFound();
            }

            PreOrder preOrder = _context.PreOrder.Single(m => m.ID == id);
            if (preOrder == null)
            {
                return HttpNotFound();
            }

            return View(preOrder);
        }

        // GET: PreOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PreOrders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PreOrder preOrder)
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
                return HttpNotFound();
            }

            PreOrder preOrder = _context.PreOrder.Single(m => m.ID == id);
            if (preOrder == null)
            {
                return HttpNotFound();
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
                return HttpNotFound();
            }

            PreOrder preOrder = _context.PreOrder.Single(m => m.ID == id);
            if (preOrder == null)
            {
                return HttpNotFound();
            }

            return View(preOrder);
        }

        // POST: PreOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            PreOrder preOrder = _context.PreOrder.Single(m => m.ID == id);
            _context.PreOrder.Remove(preOrder);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
