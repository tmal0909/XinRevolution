using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

        public IActionResult Create(int groupId)
        {
            return View();
        }

        public IActionResult Update(int id)
        {
            return View();
        }

        public IActionResult Delete(int id)
        {
            return View();
        }
    }
}