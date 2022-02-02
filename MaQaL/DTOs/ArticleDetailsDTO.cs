namespace MaQaL.DTOs
{
    public class ArticleDetailsDTO
    {
        public int Id { get; set; }
       
        public string Title { get; set; }

        public string Text { get; set; }
       
        public string AuthorName { get; set; }

        public byte CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
