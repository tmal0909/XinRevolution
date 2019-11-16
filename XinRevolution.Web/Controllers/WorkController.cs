using Microsoft.AspNetCore.Mvc;
using XinRevolution.Web.Constants;
using XinRevolution.Web.Services;

namespace XinRevolution.Web.Controllers
{
    public class WorkController : Controller
    {
        private readonly WorkService _service;

        public WorkController(WorkService service)
        {
            _service = service;

            ViewBag.Animation = AnimationTypeConstant.Horizontal;
        }

        /// <summary>
        /// 作品
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