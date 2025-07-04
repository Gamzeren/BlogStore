using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.Controllers
{
    public class AuthorDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
