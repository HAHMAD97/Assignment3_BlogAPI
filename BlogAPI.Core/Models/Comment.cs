using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace BlogAPI.Core.Models
{
    public class Comment
    {
    public int Id { get; set; }
    [Required(ErrorMessage = "PostId is required")]
    public int PostId { get; set; }

    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;

    [StringLength(150, ErrorMessage = "Email cannot exceed 150 characters")]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Content cannot exceed 1000 characters")]
    [Required(ErrorMessage = "Content is required")]
    public string Content { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    public Post Post { get; set; }
    }

}




