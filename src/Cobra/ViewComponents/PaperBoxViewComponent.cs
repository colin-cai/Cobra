using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Cobra.Models;

namespace Cobra.ViewComponents
{
    public class PaperBoxViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var instance = new PaperBox();
            return View();
        }
    }
}
