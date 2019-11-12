using Microsoft.AspNetCore.Mvc;
using XinRevolution.Web.Constants;

namespace XinRevolution.Web.Controllers
{
    public class IssueController : Controller
    {
        public IssueController()
        {
            ViewBag.Animation = AnimationTypeConstant.Horizontal;
        }

        /// <summary>
        /// 追蹤議題
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }
    }
}