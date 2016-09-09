using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Cobra.Models;
using Cobra.Data;

namespace Cobra.ViewComponents
{
    public class PaperBagViewComponent: ViewComponent
    {
        public PaperBagViewComponent(ApplicationDbContext dbConext)
        {
            DbContext = dbConext;
        }

        private ApplicationDbContext DbContext { get; }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var paperBag = await DbContext.PaperBag.ToAsyncEnumerable().FirstOrDefault(o=>o.Id == id);

            return View(paperBag);
        }
    }
}
