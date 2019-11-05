using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XinRevolution.Manager.MetaDatas.FireGeneration;
using XinRevolution.Manager.Services.FireGeneration;

namespace XinRevolution.Manager.Controllers.FireGeneration
{
    public class FGRoleResourceController : Controller
    {
        private readonly FGGroupRoleService _roleService;
        private readonly FGRoleResourceService _resourceService;

        public FGRoleResourceController(FGGroupRoleService roleService, FGRoleResourceService resourceService)
        {
            _roleService = roleService;
            _resourceService = resourceService;
        }

        public IActionResult Index(int roleId)
        {
            var result = _resourceService.Find(roleId);

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.Role = _roleService.Find().Data.SingleOrDefault(x => x.Id == roleId);

            return View(result.Data);
        }

        public IActionResult Create(int roleId)
        {
            var result = _resourceService.FindMetaData();

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.Role = _roleService.Find().Data.SingleOrDefault(x => x.Id == roleId);

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FGRoleResourceMD metaData)
        {
            var result = _resourceService.Create(metaData);

            if (!result.Status)
            {
                result.Data.TypeOptions = _resourceService.GetOptions();
                ViewBag.ErrorMessage = result.Message;
                ViewBag.Role = _roleService.Find().Data.SingleOrDefault(x => x.Id == metaData.RoleId);

                return View(result.Data);
            }

            return RedirectToAction("Index", "FGRoleResource", new { roleId = metaData.RoleId });
        }

        public IActionResult Update(int id)
        {
            var result = _resourceService.FindMetaData(id);

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.Role = _roleService.Find().Data.SingleOrDefault(x => x.Id == result.Data.RoleId);

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(FGRoleResourceMD metaData)
        {
            var result = _resourceService.Update(metaData);

            if (!result.Status)
            {
                result.Data.TypeOptions = _resourceService.GetOptions();
                ViewBag.ErrorMessage = result.Message;
                ViewBag.Role = _roleService.Find().Data.SingleOrDefault(x => x.Id == metaData.RoleId);

                return View(result.Data);
            }

            return RedirectToAction("Index", "FGRoleResource", new { roleId = metaData.RoleId });
        }

        public IActionResult Delete(int id)
        {
            var result = _resourceService.FindMetaData(id);

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.Role = _roleService.Find().Data.SingleOrDefault(x => x.Id == result.Data.RoleId);

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(FGRoleResourceMD metaData)
        {
            var result = _resourceService.Delete(metaData);

            if (!result.Status)
            {
                result.Data.TypeOptions = _resourceService.GetOptions();
                ViewBag.ErrorMessage = result.Message;
                ViewBag.Role = _roleService.Find().Data.SingleOrDefault(x => x.Id == metaData.RoleId);

                return View(result.Data);
            }

            return RedirectToAction("Index", "FGRoleResource", new { roleId = metaData.RoleId });
        }
    }
}