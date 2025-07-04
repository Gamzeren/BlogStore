using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogStore.EntityLayer.Entities;

namespace BlogStore.BusinessLayer.Abstract
{
    public interface IArticleService:IGenericService<Article>
    {
        public List<Article> TGetArticlesWithCategories();
        public AppUser TGetAppUserByArticleId(int id);

        public List<Article> TGetTop3PopularArticles();
        public List<Article> TGetArticlesByAppUser(string id);
        public Article TGetArticleBySlug(string slug);
        public Article TGetArticleWithUser(int id);
        List<Article> TGetArticlesByUserId(string id);
        List<Article> TGetLast5ArticlesByUser(string id);
        public List<Article> TGetArticlesByCategoryId(int id);
        List<(string CategoryName, int ArticleCount)> TGetArticleCountByCategory();
    }
}
