using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.Controllers
{
    public class AuthorLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
