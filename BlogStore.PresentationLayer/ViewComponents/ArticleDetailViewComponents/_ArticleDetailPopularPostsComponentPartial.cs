using BlogStore.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.ViewComponents.ArticleDetailViewComponents
{
    public class _ArticleDetailPopularPostsComponentPartial:ViewComponent
    {
        private readonly IArticleService _articleService;

        public _ArticleDetailPopularPostsComponentPartial(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IViewComponentResult Invoke()
        {
            var value=_articleService.TGetTop3PopularArticles();
            return View(value);
        }
    }
}
