using Microsoft.AspNetCore.Mvc;
using XinRevolution.Manager.MetaDatas;
using XinRevolution.Manager.Services;

namespace XinRevolution.Manager.Controllers
{
    public class BlogTagController : Controller
    {
        private readonly BlogTagService _service;

        public BlogTagController(BlogTagService service)
        {
            _service = service;
        }

        public IActionResult Index(int blogId)
        {
            var result = _service.Find(blogId);

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.BlogId = blogId;

            return View(result.Data);
        }

        public IActionResult Create(int blogId)
        {
            var result = _service.FindMetaData();

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.BlogId = blogId;

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BlogTagMD metaData)
        {
            var result = _service.Create(metaData);

            if (!result.Status)
            {
                result.Data.TagOptions = _service.GetOptions();
                ViewBag.ErrorMessage = result.Message;
                ViewBag.BlogId = result.Data.BlogId;

                return View(result.Data);
            }

            return RedirectToAction("Index", "BlogTag", new { blogId = metaData.BlogId });
        }

        public IActionResult Delete(int id)
        {
            var result = _service.FindMetaData(id);

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.BlogId = result.Data.BlogId;

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(BlogTagMD metaData)
        {
            var result = _service.Delete(metaData);

            if (!result.Status)
            {
                result.Data.TagOptions = _service.GetOptions();
                ViewBag.ErrorMessage = result.Message;
                ViewBag.BlogId = result.Data.BlogId;

                return View(result.Data);
            }

            return RedirectToAction("Index", "BlogTag", new { blogId = metaData.BlogId });
        }
    }
}