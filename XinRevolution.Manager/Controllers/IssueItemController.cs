using Microsoft.AspNetCore.Mvc;
using XinRevolution.Manager.MetaDatas;
using XinRevolution.Manager.Services;

namespace XinRevolution.Manager.Controllers
{
    public class IssueItemController : Controller
    {
        private readonly IssueItemService _service;

        public IssueItemController(IssueItemService service)
        {
            _service = service;
        }

        public IActionResult Index(int issueId)
        {
            var result = _service.Find();

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.IssueId = issueId;

            return View(result.Data);
        }

        public IActionResult Create(int issueId)
        {
            var result = _service.FindMetaData();

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.IssueId = issueId;

            return View(result.Data);
        }

        [HttpPost]
        public IActionResult Create(IssueItemMD metaData)
        {
            var result = _service.Create(metaData);

            if (!result.Status)
            {
                ViewBag.ErrorMessage = result.Message;

                return View(result.Data);
            }

            return RedirectToAction("Index", "IssueItem", new { issueId = metaData.IssueId });
        }

        public IActionResult Update(int id)
        {
            var result = _service.FindMetaData(id);

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.IssueId = result.Data.IssueId;

            return View(result.Data);
        }

        [HttpPost]
        public IActionResult Update(IssueItemMD metaData)
        {
            var result = _service.Update(metaData);

            if (!result.Status)
            {
                ViewBag.ErrorMessage = result.Message;

                return View(result.Data);
            }

            return RedirectToAction("Index", "IssueItem", new { issueId = metaData.IssueId });
        }

        public IActionResult Delete(int id)
        {
            var result = _service.FindMetaData(id);

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.IssueId = result.Data.IssueId;

            return View(result.Data);
        }

        [HttpPost]
        public IActionResult Delete(IssueItemMD metaData)
        {
            var result = _service.Delete(metaData);

            if (!result.Status)
            {
                ViewBag.ErrorMessage = result.Message;

                return View(result.Data);
            }

            return RedirectToAction("Index", "IssueItem", new { issueId = metaData.IssueId });
        }
    }
}