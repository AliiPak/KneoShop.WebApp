using KenoShop.WebApp.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace KenoShop.WebApp.Areas.Controller
{
    public class HomeController : AdminBaseController
    {
        public IActionResult index()
        {
            return View();
        }

    }
}
