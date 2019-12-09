using Microsoft.AspNetCore.Mvc;
using XinRevolution.Manager.MetaDatas;
using XinRevolution.Manager.Services;

namespace XinRevolution.Manager.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserService _service;

        public HomeController(UserService service)
        {
            _service = service;
        }

        public IActionResult Login()
        {
            var result = _service.FindMetaData();

            if (!result.Status)
                return RedirectToAction("Error", "Home", new { errorMessage = result.Message });

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(UserMD metaData)
        {
            var result = _service.Login(metaData);

            if (!result.Status)
            {
                ViewBag.ErrorMessage = result.Message;

                return View(result.Data);
            }

            // TODO : Set Authentication

            return RedirectToAction("Index", "User");

        }

        public IActionResult Logout()
        {
            // TODO : Clear Authentication

            return RedirectToAction("Login", "Home");
        }

        public IActionResult Error(string errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage;

            return View();
        }
    }
}
