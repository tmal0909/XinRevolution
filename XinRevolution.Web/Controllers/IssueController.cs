using Microsoft.AspNetCore.Mvc;
using XinRevolution.Web.Constants;
using XinRevolution.Web.Services;

namespace XinRevolution.Web.Controllers
{
    public class IssueController : Controller
    {
        private readonly IssueService _service;

        public IssueController(IssueService service)
        {
            _service = service;

            ViewBag.Animation = AnimationTypeConstant.Horizontal;
        }

        /// <summary>
        /// 追蹤議題
        /// </summary>
        public IActionResult Index()
        {
            var result = _service.Find();

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            return View(result.Data);
        }

        /// <summary>
        /// 追蹤議題 - 詳細資料
        /// </summary>
        public IActionResult Detail(int issueId)
        {
            var result = _service.FindDetail(issueId);

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            return View(result.Data);
        }
    }
}