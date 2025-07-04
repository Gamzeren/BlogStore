using BlogStore.BusinessLayer.Abstract;
using BlogStore.DataAccessLayer.Context;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.ViewComponents.ArticleDetailViewComponents
{
    public class _ArticleDetailMainCoverImageComponentPartial:ViewComponent
    {
        private readonly IArticleService _articleService;
        private readonly BlogContext _blogContext;

        public _ArticleDetailMainCoverImageComponentPartial(IArticleService articleService, BlogContext blogContext)
        {
            _articleService = articleService;
            _blogContext = blogContext;
        }

        public IViewComponentResult Invoke(int id)
        {
            var value=_articleService.TGetById(id);
            ViewBag.appuser=_blogContext.Users.Where(x=>x.Id==value.AppUserId).Select(y=>y.Name +" "+y.Surname).FirstOrDefault();
            ViewBag.appuserImage=_blogContext.Users.Where(x=>x.Id==value.AppUserId).Select(y=>y.ImageUrl).FirstOrDefault();
            return View(value);
        }
    }
}
