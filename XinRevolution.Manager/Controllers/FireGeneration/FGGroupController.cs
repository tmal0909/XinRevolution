using Microsoft.AspNetCore.Mvc;
using XinRevolution.Manager.MetaDatas.FireGeneration;
using XinRevolution.Manager.Services.FireGeneration;

namespace XinRevolution.Manager.Controllers.FireGeneration
{
    public class FGGroupController : Controller
    {
        private readonly FGGroupService _service;

        public FGGroupController(FGGroupService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var result = _service.Find();

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            return View(result.Data);
        }

        public IActionResult Create()
        {
            var result = _service.FindMetaData();

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FGGroupMD metaData)
        {
            var result = _service.Create(metaData);

            if (!result.Status)
            {
                ViewBag.ErrorMessage = result.Message;

                return View(metaData);
            }

            return RedirectToAction("Index", "FGGroup");
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
        public IActionResult Update(FGGroupMD metaData)
        {
            var result = _service.Update(metaData);

            if (!result.Status)
            {
                ViewBag.ErrorMessage = result.Message;

                return View(metaData);
            }

            return RedirectToAction("Index", "FGGroup");
        }

        public IActionResult Delete(int id)
        {
            var result = _service.FindMetaData(id);

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(FGGroupMD metaData)
        {
            var result = _service.Delete(metaData);

            if (!result.Status)
            {
                ViewBag.ErrorMessage = result.Message;

                return View(metaData);
            }

            return RedirectToAction("Index", "FGGroup");
        }
    }
}