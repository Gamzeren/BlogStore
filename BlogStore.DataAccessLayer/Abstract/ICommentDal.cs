using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogStore.EntityLayer.Entities;

namespace BlogStore.DataAccessLayer.Abstract
{
    public interface ICommentDal:IGenericDal<Comment>
    {
        List<Comment> GetCommentsByArticle(int id);
        List<Comment> GetLast3CommentsByArticle(string id);
        List<Comment> GetAllComments();
    }
}
