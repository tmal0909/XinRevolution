using Microsoft.AspNetCore.Mvc;
using System.Linq;
using XinRevolution.Manager.MetaDatas.FireGeneration;
using XinRevolution.Manager.Services.FireGeneration;

namespace XinRevolution.Manager.Controllers.FireGeneration
{
    public class FGSeasonChapterController : Controller
    {
        private readonly FGSeasonService _seasonService;
        private readonly FGSeasonChapterService _chapterService;

        public FGSeasonChapterController(FGSeasonService seasonService, FGSeasonChapterService chapterService)
        {
            _seasonService = seasonService;
            _chapterService = chapterService;
        }

        public IActionResult Index(int seasonId)
        {
            var result = _chapterService.Find(seasonId);

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.Season = _seasonService.Find().Data.SingleOrDefault(x => x.Id == seasonId);

            return View(result.Data);
        }

        public IActionResult Create(int seasonId)
        {
            var result = _chapterService.FindMetaData();

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.Season = _seasonService.Find().Data.SingleOrDefault(x => x.Id == seasonId);

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FGSeasonChapterMD metaData)
        {
            var result = _chapterService.Create(metaData);

            if (!result.Status)
            {
                ViewBag.ErrorMessage = result.Message;
                ViewBag.Season = _seasonService.Find().Data.SingleOrDefault(x => x.Id == metaData.SeasonId );

                return View(result.Data);
            }

            return RedirectToAction("Index", "FGSeasonChapter", new { seasonId = metaData.SeasonId });
        }

        public IActionResult Update(int id)
        {
            var result = _chapterService.FindMetaData(id);

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.Season = _seasonService.Find().Data.SingleOrDefault(x => x.Id == result.Data.SeasonId);

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(FGSeasonChapterMD metaData)
        {
            var result = _chapterService.Update(metaData);

            if (!result.Status)
            {
                ViewBag.ErrorMessage = result.Message;
                ViewBag.Season = _seasonService.Find().Data.SingleOrDefault(x => x.Id == metaData.SeasonId);

                return View(result.Data);
            }

            return RedirectToAction("Index", "FGSeasonChapter", new { seasonId = metaData.SeasonId });
        }

        public IActionResult Delete(int id)
        {
            var result = _chapterService.FindMetaData(id);

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.Season = _seasonService.Find().Data.SingleOrDefault(x => x.Id == result.Data.SeasonId);

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(FGSeasonChapterMD metaData)
        {
            var result = _chapterService.Delete(metaData);

            if (!result.Status)
            {
                ViewBag.ErrorMessage = result.Message;
                ViewBag.Season = _seasonService.Find().Data.SingleOrDefault(x => x.Id == metaData.SeasonId);

                return View(result.Data);
            }

            return RedirectToAction("Index", "FGSeasonChapter", new { seasonId = metaData.SeasonId });
        }
    }
}