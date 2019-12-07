using Microsoft.AspNetCore.Mvc;
using XinRevolution.Web.Constants;
using XinRevolution.Web.Services;

namespace XinRevolution.Web.Controllers
{
    public class FireGenerationController : Controller
    {
        private readonly FireGenerationService _service;

        public FireGenerationController(FireGenerationService service)
        {
            _service = service;

            ViewBag.Animation = AnimationTypeConstant.Horizontal;
        }

        /// <summary>
        /// 簡介
        /// </summary>
        public IActionResult Intro()
        {
            var result = _service.FindIntro(ControllerContext.RouteData.Values["controller"].ToString());

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            return View(result.Data);
        }

        /// <summary>
        /// 焰世代首頁
        /// </summary>
        public IActionResult Index()
        {
            var result = _service.FindRoleGroup();

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            return View(result.Data);
        }

        /// <summary>
        /// 角色介紹
        /// </summary>
        public IActionResult Role(int roleId)
        {
            var result = _service.FindRole(roleId);

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            return View(result.Data);
        }

        /// <summary>
        /// 時間軸
        /// </summary>
        public IActionResult StoryLine()
        {
            var result = _service.FindStoryLine();

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            return View(result.Data);
        }

        /// <summary>
        /// 時間軸 - 季
        /// </summary>
        public IActionResult Season(int seasonId)
        {
            var result = _service.FindSeason(seasonId);

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            return View(result.Data);
        }

        /// <summary>
        /// 時間軸 - 章 (漫畫)
        /// </summary>
        /// <param name="chapterId"></param>
        /// <returns></returns>
        public IActionResult Chapter(int chapterId)
        {
            var result = _service.FindChapter(chapterId);

            if (!result.Status)
                return null;

            return PartialView("_Chapter",result.Data);
        }

        /// <summary>
        /// 世界觀
        /// </summary>
        public IActionResult ViewCategory()
        {
            var result = _service.FindCategory();

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            return View(result.Data);
        }

        /// <summary>
        /// 世界觀 - 內容
        /// </summary>
        public IActionResult ViewCategoryEvent(int eventId)
        {
            var result = _service.FindCategoryEvent(eventId);

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            return PartialView("_ViewCategoryEvent", result.Data);
        }
    }
}