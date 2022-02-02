using System.ComponentModel.DataAnnotations;
using MaQaL.Models;

namespace MaQaL.DTOs
{
    public class ArticleDTO
    {
        public string Title { get; set; }
        
        public string Text { get; set; }
        [MaxLength(100)]
        public string AuthorName { get; set; }

        public byte CategoryId { get; set; }
       
    }
}
