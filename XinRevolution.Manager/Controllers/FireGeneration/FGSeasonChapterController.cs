using Microsoft.AspNetCore.Mvc;
using XinRevolution.Manager.MetaDatas.FireGeneration;
using XinRevolution.Manager.Services.FireGeneration;

namespace XinRevolution.Manager.Controllers.FireGeneration
{
    public class FGSeasonChapterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}