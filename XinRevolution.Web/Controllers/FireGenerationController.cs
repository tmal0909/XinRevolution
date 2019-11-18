﻿using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

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