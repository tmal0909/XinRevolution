using Microsoft.AspNetCore.Mvc;
using XinRevolution.Manager.MetaDatas.FireGeneration;
using XinRevolution.Manager.Services.FireGeneration;

namespace XinRevolution.Manager.Controllers.FireGeneration
{
    public class FGChapterComicController : Controller
    {
        private readonly FGSeasonChapterService _chapterService;
        private readonly FGChapterComicService _comicService;

        public FGChapterComicController(FGSeasonChapterService chapterService, FGChapterComicService comicService)
        {
            _chapterService = chapterService;
            _comicService = comicService;
        }

        public IActionResult Index(int chapterId)
        {
            var result = _comicService.Find(chapterId);

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.Chapter = _chapterService.FindDetail(chapterId).Data;

            return View(result.Data);
        }

        public IActionResult Create(int chapterId)
        {
            var result = _comicService.FindMetaData();

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.Chapter = _chapterService.FindDetail(chapterId).Data;

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FGChapterComicMD metaData)
        {
            var result = _comicService.Create(metaData);

            if (!result.Status)
            {
                ViewBag.ErrorMessage = result.Message;
                ViewBag.Chapter = _chapterService.FindDetail(metaData.ChapterId).Data;

                return View(result.Data);
            }

            return RedirectToAction("Index", "FGChapterComic", new { chapterId = metaData.ChapterId });
        }

        public IActionResult Update(int id)
        {
            var result = _comicService.FindMetaData(id);

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.Chapter = _chapterService.FindDetail(result.Data.ChapterId).Data;

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(FGChapterComicMD metaData)
        {
            var result = _comicService.Update(metaData);

            if (!result.Status)
            {
                ViewBag.ErrorMessage = result.Message;
                ViewBag.Chapter = _chapterService.FindDetail(metaData.ChapterId).Data;

                return View(result.Data);
            }

            return RedirectToAction("Index", "FGChapterComic", new { chapterId = metaData.ChapterId });
        }

        public IActionResult Delete(int id)
        {
            var result = _comicService.FindMetaData(id);

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.Chapter = _chapterService.FindDetail(result.Data.ChapterId).Data;

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(FGChapterComicMD metaData)
        {
            var result = _comicService.Delete(metaData);

            if (!result.Status)
            {
                ViewBag.ErrorMessage = result.Message;
                ViewBag.Chapter = _chapterService.FindDetail(metaData.ChapterId).Data;

                return View(result.Data);
            }

            return RedirectToAction("Index", "FGChapterComic", new { chapterId = metaData.ChapterId });
        }
    }
}