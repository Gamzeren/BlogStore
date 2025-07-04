using BlogStore.BusinessLayer.Abstract;
using BlogStore.EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.ViewComponents
{
    public class _AuthorDashboardRecentCommentsComponentPartial:ViewComponent
    {
        private readonly ICommentService _commentService;
        private readonly UserManager<AppUser> _userManager;

        public _AuthorDashboardRecentCommentsComponentPartial(ICommentService commentService, UserManager<AppUser> userManager)
        {
            _commentService = commentService;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userName = HttpContext.User.Identity?.Name;
            if (string.IsNullOrEmpty(userName))
            {
                // Kullanıcı login değilse boş view dön
                return View();
            }
            var user = await _userManager.FindByNameAsync(userName);
            var comment = _commentService.TGetLast3CommentsByArticle(user.Id);
            return View(comment);
        }
    }
}
