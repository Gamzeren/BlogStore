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
    public class EfCommentDal : GenericRepository<Comment>, ICommentDal
    {
        private readonly BlogContext _context;
        public EfCommentDal(BlogContext context) : base(context)
        {
            _context = context;
        }

        public List<Comment> GetAllComments()
        {
            return _context.Comments.ToList();
        }

        public List<Comment> GetCommentsByArticle(int id)
        {
            var values=_context.Comments.Include(x=>x.AppUser).Include(y=>y.Article).Where(z=>z.ArticleId==id).ToList();
            return values;
        }

        public List<Comment> GetLast3CommentsByArticle(string id)
        {
            var values=_context.Comments
                                .Include(x=>x.AppUser)
                                .Include(x=>x.Article)
                                .Where(x=>x.Article.AppUserId==id)
                                .OrderByDescending(x=>x.CommentDate)
                                .Take(5)
                                .ToList();
            return values;
        }
    }
}
