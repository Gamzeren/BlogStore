using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogStore.DataAccessLayer.Abstract;
using BlogStore.DataAccessLayer.Context;
using BlogStore.DataAccessLayer.Repositories;
using BlogStore.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogStore.DataAccessLayer.EntityFramework
{
    public class EfArticleDal : GenericRepository<Article>, IArticleDal
    {
        private readonly BlogContext _context;
        public EfArticleDal(BlogContext context) : base(context)
        {
            _context = context;
        }

        public AppUser GetAppUserByArticleId(int id)
        {
            string userId=_context.Articles.Where(x=>x.ArticleId==id).Select(y=>y.AppUserId).FirstOrDefault();
            var userValue=_context.Users.Where(x=>x.Id==userId).FirstOrDefault();
            return userValue;
        }

        public Article GetArticleBySlug(string slug)
        {
            return _context.Articles
                                    .Include(x => x.AppUser)
                                    .Include(x => x.Category)
                                    .FirstOrDefault(x => x.Slug.ToLower()==slug.ToLower());
        }

        public List<(string CategoryName, int ArticleCount)> GetArticleCountByCategory()
        {
            var result=_context.Articles
                .Include(a=>a.Category)
                .GroupBy(a=>a.Category.CategoryName)
                .Select(g=>new ValueTuple<string, int>(g.Key, g.Count())).ToList();
            return result;
        }

        public List<Article> GetArticlesByAppUser(string id)
        {
            return _context.Articles.Where(x=>x.AppUserId==id).ToList();
        }

        public List<Article> GetArticlesByCategoryId(int id)
        {
            return _context.Articles
                    .Include(a=>a.Category)
                    .Include(a=>a.AppUser)
                    .Where(a=>a.CategoryId==id)
                    .ToList();
                    
        }

        public List<Article> GetArticlesByUserId(string id)
        {
            return _context.Articles
                .Include(x => x.AppUser)
                .Include(x => x.Category)
                .Where(x => x.AppUserId == id)
                .OrderByDescending(x => x.CreatedDate)
                .ToList();
        }

        public List<Article> GetArticlesWithCategories()
        {
            return _context.Articles.Include(x=>x.Category).ToList();
        }

        public Article GetArticleWithUser(int id)
        {
            return _context.Articles.Include(x=>x.AppUser).FirstOrDefault(x=>x.ArticleId==id);
        }

        public List<Article> GetLast5ArticlesByUser(string id)
        {
            return _context.Articles
                .Where(a=>a.AppUserId==id)
                .OrderByDescending(a=>a.CreatedDate)
                .Take(5)
                .ToList();
        }

        public List<Article> GetTop3PopularArticles()
        {
            var values=_context.Articles.OrderByDescending(x=>x.ArticleId).Take(3).ToList();
            return values;
        }
    }
}
