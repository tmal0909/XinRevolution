using Microsoft.AspNetCore.Mvc;
using XinRevolution.Manager.MetaDatas;
using XinRevolution.Manager.Services;

namespace XinRevolution.Manager.Controllers
{
    public class BlogPostController : Controller
    {
        private readonly BlogPostService _service;

        public BlogPostController(BlogPostService service)
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
        public IActionResult Create(BlogPostMD metaData)
        {
            var result = _service.Create(metaData);

            if (!result.Status)
            {
                result.Data.ReferenceTypeOptions = _service.GetOptions();
                ViewBag.ErrorMessage = result.Message;
                ViewBag.BlogId = metaData.BlogId;

                return View(metaData);
            }

            return RedirectToAction("Index", "BlogPost", new { blogId = metaData.BlogId });
        }

        public IActionResult Update(int id)
        {
            var result = _service.FindMetaData(id);

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.BlogId = result.Data.BlogId;

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(BlogPostMD metaData)
        {
            var result = _service.Update(metaData);

            if (!result.Status)
            {
                result.Data.ReferenceTypeOptions = _service.GetOptions();
                ViewBag.ErrorMessage = result.Message;
                ViewBag.BlogId = metaData.BlogId;

                return View(metaData);
            }

            return RedirectToAction("Index", "BlogPost", new { blogId = metaData.BlogId });
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
        public IActionResult Delete(BlogPostMD metaData)
        {
            var result = _service.Delete(metaData);

            if (!result.Status)
            {
                result.Data.ReferenceTypeOptions = _service.GetOptions();
                ViewBag.ErrorMessage = result.Message;
                ViewBag.BlogId = metaData.BlogId;

                return View(metaData);
            }

            return RedirectToAction("Index", "BlogPost", new { blogId = metaData.BlogId });
        }
    }
}