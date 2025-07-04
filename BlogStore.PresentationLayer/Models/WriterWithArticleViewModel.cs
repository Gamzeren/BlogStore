using BlogStore.EntityLayer.Entities;

namespace BlogStore.PresentationLayer.Models
{
    public class WriterWithArticleViewModel
    {
        public List<AppUser> Writers { get; set; }
        public List<Article> Articles { get; set; }
        public string SelectedAuthorId { get; set; }
    }
}
