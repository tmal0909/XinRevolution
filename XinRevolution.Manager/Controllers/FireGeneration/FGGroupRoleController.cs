using Microsoft.AspNetCore.Mvc;
using XinRevolution.Manager.MetaDatas.FireGeneration;
using XinRevolution.Manager.Services.FireGeneration;

namespace XinRevolution.Manager.Controllers.FireGeneration
{
    public class FGGroupRoleController : Controller
    {
        private readonly FGGroupRoleService _service;

        public FGGroupRoleController(FGGroupRoleService service)
        {
            _service = service;
        }

        public IActionResult Index(int groupId)
        {
            var result = _service.Find(groupId);

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.GroupId = groupId;

            return View(result.Data);
        }

        public IActionResult Create(int groupId)
        {
            var result = _service.FindMetaData();

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.GroupId = groupId;

            return View(result.Data);
        }

        [HttpPost]
        public IActionResult Create(FGGroupRoleMD metaData)
        {
            var result = _service.Create(metaData);

            if (!result.Status)
            {
                ViewBag.ErrorMessage = result.Message;
                ViewBag.GroupId = result.Data.GroupId;

                return View(result.Data);
            }

            return RedirectToAction("Index", "FGGroupRole", new { groupId = metaData.GroupId });
        }

        public IActionResult Update(int id)
        {
            var result = _service.FindMetaData(id);

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.GroupId = result.Data.GroupId;

            return View(result.Data);
        }

        [HttpPost]
        public IActionResult Update(FGGroupRoleMD metaData)
        {
            var result = _service.Update(metaData);

            if (!result.Status)
            {
                ViewBag.ErrorMessage = result.Message;
                ViewBag.GroupId = result.Data.GroupId;

                return View(result.Data);
            }

            return RedirectToAction("Index", "FGGroupRole", new { groupId = metaData.GroupId });
        }

        public IActionResult Delete(int id)
        {
            var result = _service.FindMetaData(id);

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.GroupId = result.Data.GroupId;

            return View(result.Data);
        }

        [HttpPost]
        public IActionResult Delete(FGGroupRoleMD metaData)
        {
            var result = _service.Delete(metaData);

            if (!result.Status)
            {
                ViewBag.ErrorMessage = result.Message;
                ViewBag.GroupId = result.Data.GroupId;

                return View(result.Data);
            }

            return RedirectToAction("Index", "FGGroupRole", new { groupId = metaData.GroupId });
        }
    }
}