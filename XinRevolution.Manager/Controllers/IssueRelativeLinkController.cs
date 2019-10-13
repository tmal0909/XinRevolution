using Microsoft.AspNetCore.Mvc;
using XinRevolution.Manager.MetaDatas;
using XinRevolution.Manager.Services;

namespace XinRevolution.Manager.Controllers
{
    public class IssueRelativeLinkController : Controller
    {
        private readonly IssueRelativeLinkService _service;

        public IssueRelativeLinkController(IssueRelativeLinkService service)
        {
            _service = service;
        }

        public IActionResult Index(int issueId)
        {
            var result = _service.Find(issueId);

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
        public IActionResult Create(IssueRelativeLinkMD metaData)
        {
            var result = _service.Create(metaData);

            if (!result.Status)
            {
                ViewBag.ErrorMessage = result.Message;

                return View(result.Data);
            }

            return RedirectToAction("Index", "IssueRelativeLink", new { issueId = metaData.IssueId });
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
        public IActionResult Update(IssueRelativeLinkMD metaData)
        {
            var result = _service.Update(metaData);

            if (!result.Status)
            {
                ViewBag.ErrorMessage = result.Message;

                return View(result.Data);
            }

            return RedirectToAction("Index", "IssueRelativeLink", new { issueId = metaData.IssueId });
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
        public IActionResult Delete(IssueRelativeLinkMD metaData)
        {
            var result = _service.Delete(metaData);

            if (!result.Status)
            {
                ViewBag.ErrorMessage = result.Message;

                return View(result.Data);
            }

            return RedirectToAction("Index", "IssueRelativeLink", new { issueId = metaData.IssueId });
        }
    }
}