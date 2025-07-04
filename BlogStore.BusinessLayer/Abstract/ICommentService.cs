using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogStore.EntityLayer.Entities;

namespace BlogStore.BusinessLayer.Abstract
{
    public interface ICommentService:IGenericService<Comment>
    {
        public List<Comment> TGetCommentsByArticle(int id);
        List<Comment> TGetLast3CommentsByArticle(string id);
        List<Comment> TGetAllComments();
    }
}
