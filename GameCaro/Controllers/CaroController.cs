using GameCaro.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameCaro.Controllers
{
    public class CaroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Caro3x3()
        {
            ViewBag.number = ConnectedUser.Ids.Count();
            return View();
        }
        [HttpPost]
        public IActionResult Caro3x3(string user, string idGroup)
        {
            ViewBag.user = user;
            ViewBag.idGroup = idGroup;

            return View();
        }
    }
}
