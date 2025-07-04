using BlogStore.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.ViewComponents
{
    public class _AuthorDashboardChartComponentPartial:ViewComponent
    {
        private readonly IArticleService _articleService;

        public _AuthorDashboardChartComponentPartial(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _articleService.TGetArticleCountByCategory();
            return View(values);
        }
    }
}
