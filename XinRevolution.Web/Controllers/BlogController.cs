using Microsoft.AspNetCore.Mvc;
using XinRevolution.Web.Constants;

namespace XinRevolution.Web.Controllers
{
    public class BlogController : Controller
    {
        public BlogController()
        {
            ViewBag.Animation = AnimationTypeConstant.Horizontal;
        }

        /// <summary>
        /// 部落格
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }
    }
}