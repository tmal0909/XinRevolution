using Microsoft.AspNetCore.Mvc;
using XinRevolution.Web.Constants;

namespace XinRevolution.Web.Controllers
{
    public class WorkController : Controller
    {
        public WorkController()
        {
            ViewBag.Animation = AnimationTypeConstant.Horizontal;
        }

        /// <summary>
        /// 作品
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }
    }
}