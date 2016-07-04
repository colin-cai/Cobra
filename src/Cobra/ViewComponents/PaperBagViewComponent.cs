using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Cobra.Models;

namespace Cobra.ViewComponents
{
    public class PaperBagViewComponent: ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync(Product product)
        {
            //if (this.ViewData.Model == null)
            //{
            //    this.ViewData.Model = new PreOrder();
            //}

            //return View((this.ViewData.Model as PreOrder).Product);
            ViewData["product"] = product;

            return View(product);

        }
    }
}
