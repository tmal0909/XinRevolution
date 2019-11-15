using Microsoft.AspNetCore.Mvc;
using XinRevolution.Web.Constants;
using XinRevolution.Web.Services;

namespace XinRevolution.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogService _service;

        public BlogController(BlogService service)
        {
            _service = service;
        }

        /// <summary>
        /// 部落格
        /// </summary>
        public IActionResult Index()
        {
            var result = _service.Find();

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            return View(result.Data);
        }
    }
}