using BlogStore.EntityLayer.Entities;

namespace BlogStore.PresentationLayer.Models
{
    public class CategoryWithArticlesViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Article> Articles { get; set; }
        public int? SelectedCategoryId { get; set; }
    }
}
