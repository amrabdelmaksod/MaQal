using System.ComponentModel.DataAnnotations;

namespace MaQaL.DTOs
{
    public class CategoryDTO
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
