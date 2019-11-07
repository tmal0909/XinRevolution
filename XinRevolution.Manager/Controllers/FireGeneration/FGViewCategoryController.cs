using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XinRevolution.Manager.MetaDatas.FireGeneration;
using XinRevolution.Manager.Services.FireGeneration;

namespace XinRevolution.Manager.Controllers.FireGeneration
{
    public class FGViewCategoryController : Controller
    {
        private readonly FGViewCategoryService _service;

        public FGViewCategoryController(FGViewCategoryService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var result = _service.Find();

            if(!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create()
        {
            var result = _service.FindMetaData();

            if(!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            return View(result.Data);
        }

        public IActionResult Update(int id)
        {
            var result = _service.FindMetaData(id);

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(FGViewCategoryMD metaData)
        {

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var result = _service.FindMetaData(id);

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            return View(result.Data);
        }
    }
}