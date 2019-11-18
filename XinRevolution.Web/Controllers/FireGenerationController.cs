using Microsoft.AspNetCore.Mvc;

namespace XinRevolution.Web.Controllers
{
    public class FireGenerationController : Controller
    {
        /// <summary>
        /// 焰世代首頁
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 角色介紹
        /// </summary>
        public IActionResult Character()
        {
            return View();
        }

        /// <summary>
        /// 世界觀
        /// </summary>
        public IActionResult WorldView()
        {
            return View();
        }
    }
}