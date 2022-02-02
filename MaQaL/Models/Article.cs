using System.ComponentModel.DataAnnotations;

namespace MaQaL.Models
{
    public class Article
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Title { get; set; }
        
        public string Text { get; set; }
        [MaxLength(100)]
        public string AuthorName { get; set; }
      
        public byte CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
