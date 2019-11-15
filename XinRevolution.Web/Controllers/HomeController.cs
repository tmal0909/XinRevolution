using Microsoft.AspNetCore.Mvc;
using XinRevolution.Web.Constants;

namespace XinRevolution.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            ViewBag.Animation = AnimationTypeConstant.Horizontal;
        }
        
        /// <summary>
        /// 新革命首頁
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error(string errorMessage = "")
        {
            ViewBag.ErrorMessage = errorMessage;

            return View();
        }
    }
}
