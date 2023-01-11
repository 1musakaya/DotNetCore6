using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationCoreLogin.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
