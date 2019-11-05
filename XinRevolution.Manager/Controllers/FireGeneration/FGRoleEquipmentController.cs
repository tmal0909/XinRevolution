using System.Linq;
using Microsoft.AspNetCore.Mvc;
using XinRevolution.Manager.MetaDatas.FireGeneration;
using XinRevolution.Manager.Services.FireGeneration;

namespace XinRevolution.Manager.Controllers.FireGeneration
{
    public class FGRoleEquipmentController : Controller
    {
        private readonly FGGroupRoleService _roleService;
        private readonly FGRoleEquipmentService _equipmentService;

        public FGRoleEquipmentController(FGGroupRoleService roleService, FGRoleEquipmentService equipmentService)
        {
            _roleService = roleService;
            _equipmentService = equipmentService;
        }

        public IActionResult Index(int roleId)
        {
            var result = _equipmentService.Find(roleId);

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.Role = _roleService.Find().Data.SingleOrDefault(x => x.Id == roleId);

            return View(result.Data);
        }

        public IActionResult Create(int roleId)
        {
            var result = _equipmentService.FindMetaData();

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.Role = _roleService.Find().Data.SingleOrDefault(x => x.Id == roleId);

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FGRoleEquipmentMD metaData)
        {
            var result = _equipmentService.Create(metaData);

            if (!result.Status)
            {
                ViewBag.ErrorMessage = result.Message;
                ViewBag.Role = _roleService.Find().Data.SingleOrDefault(x => x.Id == metaData.RoleId);

                return View(metaData);
            }

            return RedirectToAction("Index", "FGRoleEquipment", new { roleId = result.Data.RoleId });
        }

        public IActionResult Update(int id)
        {
            var result = _equipmentService.FindMetaData(id);

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.Role = _roleService.Find().Data.SingleOrDefault(x => x.Id == result.Data.RoleId);

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(FGRoleEquipmentMD metaData)
        {
            var result = _equipmentService.Update(metaData);

            if (!result.Status)
            {
                ViewBag.ErrorMessage = result.Message;
                ViewBag.Role = _roleService.Find().Data.SingleOrDefault(x => x.Id == metaData.RoleId);

                return View(metaData);
            }

            return RedirectToAction("Index", "FGRoleEquipment", new { roleId = result.Data.RoleId });
        }

        public IActionResult Delete(int id)
        {
            var result = _equipmentService.FindMetaData(id);

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            ViewBag.Role = _roleService.Find().Data.SingleOrDefault(x => x.Id == result.Data.RoleId);

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(FGRoleEquipmentMD metaData)
        {
            var result = _equipmentService.Delete(metaData);

            if (!result.Status)
            {
                ViewBag.ErrorMessage = result.Message;
                ViewBag.Role = _roleService.Find().Data.SingleOrDefault(x => x.Id == metaData.RoleId);

                return View(metaData);
            }

            return RedirectToAction("Index", "FGRoleEquipment", new { roleId = result.Data.RoleId });
        }
    }
}