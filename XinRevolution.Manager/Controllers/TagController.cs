using Microsoft.AspNetCore.Mvc;
using XinRevolution.Manager.MetaDatas;
using XinRevolution.Manager.Services;

namespace XinRevolution.Manager.Controllers
{
    public class TagController : Controller
    {
        private readonly TagService _service;

        public TagController(TagService service)
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
        public IActionResult Create(TagMD metaData)
        {
            var result = _service.Create(metaData);

            if (!result.Status)
            {
                result.Data.StatusOptions = _service.GetOptions();
                ViewBag.ErrorMessage = result.Message;

                return View(result.Data);
            }

            return RedirectToAction("Index", "Tag");
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
        public IActionResult Update(TagMD metaData)
        {
            var result = _service.Update(metaData);

            if (!result.Status)
            {
                result.Data.StatusOptions = _service.GetOptions();
                ViewBag.ErrorMessage = result.Message;

                return View(result.Data);
            }

            return RedirectToAction("Index", "Tag");
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
        public IActionResult Delete(TagMD metaData)
        {
            var result = _service.Delete(metaData);

            if (!result.Status)
            {
                result.Data.StatusOptions = _service.GetOptions();
                ViewBag.ErrorMessage = result.Message;

                return View(result.Data);
            }

            return RedirectToAction("Index", "Tag");
        }
    }
}