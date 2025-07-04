using BlogStore.EntityLayer.Entities;
using BlogStore.PresentationLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> CreateUser(UserRegisterViewModel userRegisterViewModel)
        {
            AppUser appUser = new AppUser()
            {
                ImageUrl="Test",
                Description="Test",
                Name = userRegisterViewModel.Name,
                Email = userRegisterViewModel.Email,
                Surname = userRegisterViewModel.Surname,
                UserName = userRegisterViewModel.Username
            };
            await _userManager.CreateAsync(appUser, userRegisterViewModel.Password);
            return RedirectToAction("UserLogin", "Login");
        }
    }
}
