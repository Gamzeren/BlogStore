using BlogStore.BusinessLayer.Abstract;
using BlogStore.PresentationLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogStore.PresentationLayer.ViewComponents
{
    public class _CategoryListComponentPartial:ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;

        public _CategoryListComponentPartial(ICategoryService categoryService, IArticleService articleService)
        {
            _categoryService = categoryService;
            _articleService = articleService;
        }

        public IViewComponentResult Invoke(int? id)
        {
            var model = new CategoryWithArticlesViewModel();
            model.Categories = _categoryService.TGetAll();

            if (id.HasValue)
            {
                model.SelectedCategoryId = id;
                model.Articles=_articleService.TGetArticlesByCategoryId(id.Value);
            }
            return View(model);
        }
    }
}
