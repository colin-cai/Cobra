using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cobra.Data;
using Cobra.Models;

namespace Cobra.Controllers
{
    public class FactorForPaperBagsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FactorForPaperBagsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: FactorForPaperBags
        public async Task<IActionResult> Index()
        {
            return View(await _context.FactorForPaperBag.ToListAsync());
        }

        // GET: FactorForPaperBags/Details/5
        public async Task<IActionResult> Details()
        {
            var factorForPaperBag = await _context.FactorForPaperBag.OrderByDescending(m=>m.CreateTime).FirstOrDefaultAsync();
            if (factorForPaperBag == null)
            {
                factorForPaperBag = new FactorForPaperBag();
                _context.FactorForPaperBag.Add(factorForPaperBag);
                await _context.SaveChangesAsync();
            }

            return View(factorForPaperBag);
        }

        //// GET: FactorForPaperBags/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var factorForPaperBag = await _context.FactorForPaperBag.SingleOrDefaultAsync(m => m.Id == id);
        //    if (factorForPaperBag == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(factorForPaperBag);
        //}

        // GET: FactorForPaperBags/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FactorForPaperBags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CoatedArtPaper,CreateTime,FoilPaper,Glitter,Glossy,GrosgrainRibbon,HotStamping,LaborOfFolding,LaborOfLargeSize,LaborOfMediumSize,LaborOfPrintOnePaper,LaborOfSmallSize,Matt,MetalisedPaper,NaturalKraftPaper,OrganzaRibbon,OtherEmbellishment,OtherHandle,PP,PaperTwist,PriceOfJHook,PriceOfLabel,PriceOfTag,SantinRibbon,SpotUV,TipOn,WhiteCardboard,WhiteKraftPaper")] FactorForPaperBag factorForPaperBag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(factorForPaperBag);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(factorForPaperBag);
        }

        // GET: FactorForPaperBags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factorForPaperBag = await _context.FactorForPaperBag.SingleOrDefaultAsync(m => m.Id == id);
            if (factorForPaperBag == null)
            {
                return NotFound();
            }
            return View(factorForPaperBag);
        }

        // POST: FactorForPaperBags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CoatedArtPaper,CreateTime,FoilPaper,Glitter,Glossy,GrosgrainRibbon,HotStamping,LaborOfFolding,LaborOfLargeSize,LaborOfMediumSize,LaborOfPrintOnePaper,LaborOfSmallSize,Matt,MetalisedPaper,NaturalKraftPaper,OrganzaRibbon,OtherEmbellishment,OtherHandle,PP,PaperTwist,PriceOfJHook,PriceOfLabel,PriceOfTag,SantinRibbon,SpotUV,TipOn,WhiteCardboard,WhiteKraftPaper")] FactorForPaperBag factorForPaperBag)
        {
            if (id != factorForPaperBag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    factorForPaperBag.CreateTime = DateTime.Now;
                    _context.Update(factorForPaperBag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FactorForPaperBagExists(factorForPaperBag.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(factorForPaperBag);
        }

        // GET: FactorForPaperBags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var factorForPaperBag = await _context.FactorForPaperBag.SingleOrDefaultAsync(m => m.Id == id);
            if (factorForPaperBag == null)
            {
                return NotFound();
            }

            return View(factorForPaperBag);
        }

        // POST: FactorForPaperBags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var factorForPaperBag = await _context.FactorForPaperBag.SingleOrDefaultAsync(m => m.Id == id);
            _context.FactorForPaperBag.Remove(factorForPaperBag);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool FactorForPaperBagExists(int id)
        {
            return _context.FactorForPaperBag.Any(e => e.Id == id);
        }
    }
}
