using BlogStore.BusinessLayer.Abstract;
using BlogStore.DataAccessLayer.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogStore.PresentationLayer.Controllers
{
    public class ArticleController : Controller
    {
        private readonly BlogContext _blogContext;

        public ArticleController(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        [Route("Article/ArticleDetail/{slug}")]
        public IActionResult ArticleDetail(string slug)
        {
            var article = _blogContext.Articles.FirstOrDefault(x=>x.Slug==slug);
            if(article == null)
            {
                return NotFound();
            }
            ViewBag.i = article.ArticleId;
            return View(article);
        }
    }
}
