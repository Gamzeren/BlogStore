using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogStore.DataAccessLayer.Abstract;
using BlogStore.DataAccessLayer.Context;
using BlogStore.DataAccessLayer.Repositories;
using BlogStore.EntityLayer.Entities;

namespace BlogStore.DataAccessLayer.EntityFramework
{
    public class EfTagDal : GenericRepository<Tag>, ITagDal
    {
        public EfTagDal(BlogContext context) : base(context)
        {
        }
    }
}
