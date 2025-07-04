using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogStore.EntityLayer.Entities;

namespace BlogStore.DataAccessLayer.Abstract
{
    public interface IArticleDal:IGenericDal<Article>
    {
        List<Article> GetArticlesWithCategories();
        public AppUser GetAppUserByArticleId(int id);
        List<Article> GetTop3PopularArticles();
        List<Article> GetArticlesByAppUser(string id);
        public Article GetArticleBySlug(string slug);
        public Article GetArticleWithUser(int id);
        List<Article> GetArticlesByUserId(string id);
        List<Article> GetLast5ArticlesByUser(string id);
        public List<Article> GetArticlesByCategoryId(int id);
        List<(string CategoryName, int ArticleCount)> GetArticleCountByCategory();
    }
}
