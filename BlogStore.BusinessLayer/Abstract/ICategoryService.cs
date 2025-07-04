using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogStore.DataAccessLayer.Dtos;
using BlogStore.EntityLayer.Entities;

namespace BlogStore.BusinessLayer.Abstract
{
    public interface ICategoryService:IGenericService<Category>
    {
        public List<CategoryWithArticleCountDto> TGetCategoryWithArticleCount();
    }
}
