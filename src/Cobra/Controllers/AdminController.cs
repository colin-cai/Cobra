using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Cobra.Data;
using Cobra.Models;

namespace Cobra.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FactorForPaperBags/Details/5
        public async Task<IActionResult> DetailsFactorForPaperBag()
        {
            var factorForPaperBag = await _context.FactorForPaperBag.OrderByDescending(m => m.CreateTime).FirstOrDefaultAsync();
            if (factorForPaperBag == null)
            {
                factorForPaperBag = new FactorForPaperBag();
                _context.FactorForPaperBag.Add(factorForPaperBag);
                await _context.SaveChangesAsync();
            }

            return View(factorForPaperBag);
        }

        // GET: FactorForPaperBags/Create
        public IActionResult CreateFactorForPaperBag()
        {
            return View();
        }

        // POST: FactorForPaperBags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFactorForPaperBag([Bind("Id,CoatedArtPaper,CreateTime,FoilPaper,Glitter,Glossy,GrosgrainRibbon,HotStamping,LaborOfFolding,LaborOfLargeSize,LaborOfMediumSize,LaborOfPrintOnePaper,LaborOfSmallSize,Matt,MetalisedPaper,NaturalKraftPaper,OrganzaRibbon,OtherEmbellishment,OtherHandle,PP,PaperTwist,PriceOfJHook,PriceOfLabel,PriceOfTag,SantinRibbon,SpotUV,TipOn,WhiteCardboard,WhiteKraftPaper")] FactorForPaperBag factorForPaperBag)
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
        public async Task<IActionResult> EditFactorForPaperBag(int? id)
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
        public async Task<IActionResult> EditFactorForPaperBag(int id, [Bind("Id,CoatedArtPaper,CreateTime,FoilPaper,Glitter,Glossy,GrosgrainRibbon,HotStamping,LaborOfFolding,LaborOfLargeSize,LaborOfMediumSize,LaborOfPrintOnePaper,LaborOfSmallSize,Matt,MetalisedPaper,NaturalKraftPaper,OrganzaRibbon,OtherEmbellishment,OtherHandle,PP,PaperTwist,PriceOfJHook,PriceOfLabel,PriceOfTag,SantinRibbon,SpotUV,TipOn,WhiteCardboard,WhiteKraftPaper")] FactorForPaperBag factorForPaperBag)
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

        private bool FactorForPaperBagExists(int id)
        {
            return _context.FactorForPaperBag.Any(e => e.Id == id);
        }
    }
}