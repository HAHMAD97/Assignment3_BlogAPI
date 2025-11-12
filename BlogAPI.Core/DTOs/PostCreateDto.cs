using System.ComponentModel.DataAnnotations;
using BlogAPI.Core.Models;
namespace BlogAPI.Core.DTOs

{
    public class PostCreateDTO
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Content cannot exceed 1000 characters")]
        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; } = string.Empty;

        public string Author { get; set; } = "Admin";

    }
}