using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XinRevolution.Manager.MetaDatas.FireGeneration;
using XinRevolution.Manager.Services.FireGeneration;

namespace XinRevolution.Manager.Controllers.FireGeneration
{
    public class FGViewCategoryEventController : Controller
    {
        private readonly FGViewCategoryService _categoryService;
        private readonly FGViewCategoryEventService _categoryEventService;

        public FGViewCategoryEventController(FGViewCategoryService categoryService, FGViewCategoryEventService categoryEventService)
        {
            _categoryService = categoryService;
            _categoryEventService = categoryEventService;
        }

        public IActionResult Index(int categoryId)
        {
            var result = _categoryEventService.Find(categoryId);

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.Category = _categoryService.Find().Data.Single(x => x.Id == categoryId);

            return View(result.Data);
        }

        public IActionResult Create(int categoryId)
        {
            var result = _categoryEventService.FindMetaData();

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.Category = _categoryService.Find().Data.Single(x => x.Id == categoryId);

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FGViewCategoryEventMD metaData)
        {
            var result = _categoryEventService.Create(metaData);

            if (!result.Status)
            {
                ViewBag.ErrorMessage = result.Message;
                ViewBag.Category = _categoryService.Find().Data.Single(x => x.Id == result.Data.CategoryId);

                return View(result.Data);
            }

            return RedirectToAction("Index", "FGViewCategoryEvent", new { categoryId = result.Data.CategoryId });
        }

        public IActionResult Update(int id)
        {
            var result = _categoryEventService.FindMetaData(id);

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.Category = _categoryService.Find().Data.Single(x => x.Id == result.Data.CategoryId);

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(FGViewCategoryEventMD metaData)
        {
            var result = _categoryEventService.Update(metaData);

            if (!result.Status)
            {
                ViewBag.ErrorMessage = result.Message;
                ViewBag.Category = _categoryService.Find().Data.Single(x => x.Id == result.Data.CategoryId);

                return View(result.Data);
            }

            return RedirectToAction("Index", "FGViewCategoryEvent", new { categoryId = result.Data.CategoryId });
        }

        public IActionResult Delete(int id)
        {
            var result = _categoryEventService.FindMetaData(id);

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.Category = _categoryService.Find().Data.Single(x => x.Id == result.Data.CategoryId);

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(FGViewCategoryEventMD metaData)
        {
            var result = _categoryEventService.Delete(metaData);

            if (!result.Status)
            {
                ViewBag.ErrorMessage = result.Message;
                ViewBag.Category = _categoryService.Find().Data.Single(x => x.Id == result.Data.CategoryId);

                return View(result.Data);
            }

            return RedirectToAction("Index", "FGViewCategoryEvent", new { categoryId = result.Data.CategoryId });
        }
    }
}