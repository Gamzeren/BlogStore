using BlogStore.EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.ViewComponents
{
    public class _AuthorLayoutNavbarComponentPartial:ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public _AuthorLayoutNavbarComponentPartial(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userName = HttpContext.User.Identity?.Name;
            if (string.IsNullOrEmpty(userName))
            {
                //kullanıcı giriş yapmamışsa
                return View();
            }
            var user = await _userManager.FindByNameAsync(userName);
            ViewBag.image = user?.ImageUrl;
            return View();
        }
    }
}
