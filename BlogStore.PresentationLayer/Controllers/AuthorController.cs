using BlogStore.BusinessLayer.Abstract;
using BlogStore.EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogStore.PresentationLayer.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<AppUser> _userManager;

        public AuthorController(IArticleService articleService, ICategoryService categoryService, UserManager<AppUser> userManager)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _userManager = userManager;
        }

        public async Task<IActionResult> GetProfile()
        {
            var userName = User.Identity?.Name;
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToAction("UserLogin", "Login");
            }

            var userProfile = await _userManager.FindByNameAsync(userName);
            if (userProfile == null)
            {
                return NotFound();
            }

            ViewBag.name = userProfile.Name;
            ViewBag.surname = userProfile.Surname;
            ViewBag.username = userProfile.UserName;
            ViewBag.imageurl = userProfile.ImageUrl;
            ViewBag.email = userProfile.Email;
            ViewBag.id = userProfile.Id;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(AppUser updateUser, string profileImageUrl, string newPassword)
        {
            var userName = User.Identity?.Name;
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToAction("UserLogin", "Login");
            }

            var user =await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return NotFound();
            }

            user.Name = updateUser.Name;
            user.Surname = updateUser.Surname;
            user.UserName = updateUser.UserName;
            user.Email = updateUser.Email;

            if (!string.IsNullOrEmpty(profileImageUrl))
            {
                user.ImageUrl = profileImageUrl;
            }

            if (!string.IsNullOrEmpty(newPassword))
            {
                var hasPassword=await _userManager.HasPasswordAsync(user);
                if (hasPassword)
                {
                    var removeResult=await _userManager.RemovePasswordAsync(user);
                    if (!removeResult.Succeeded)
                    {
                        foreach(var error in removeResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View("GetProfile", user);
                    }
                }
                var addResult = await _userManager.AddPasswordAsync(user, newPassword);
                if (!addResult.Succeeded)
                {
                    foreach(var error in addResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View("GetProfile", user);
                }
            }
            var result=await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Profil başarıyla güncellendi!";
                return RedirectToAction("UserLogin", "Login");
            }
            else
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View("GetProfile", user);
            }
        }

        [HttpGet]
        public IActionResult CreateArticle()
        {
            List<SelectListItem> values = (from x in _categoryService.TGetAll()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryId.ToString()
                                           }).ToList();
            ViewBag.v = values;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateArticle(Article article)
        {
            var userName = User.Identity?.Name;
            if(string.IsNullOrEmpty(userName))
            {
                return RedirectToAction("UserLogin", "Login");
            }

            var userProfile = await _userManager.FindByNameAsync(userName);
            if (userProfile == null)
            {
                return NotFound();
            }

            article.AppUserId = userProfile.Id;
            article.CreatedDate= DateTime.Now;

            _articleService.TInsert(article);
            return RedirectToAction("Index", "Default");
        }

        public async Task<IActionResult> MyArticleList()
        {
            var userName=User.Identity?.Name;
            if (string.IsNullOrEmpty(userName))
            {
                return RedirectToAction("UserLogin", "Login");
            }
            var userProfile = await _userManager.FindByNameAsync(userName);
            if (userProfile == null)
            {
                return NotFound();
            }
            var values = _articleService.TGetArticlesByAppUser(userProfile.Id);
            return View(values);
        }
    }
}
