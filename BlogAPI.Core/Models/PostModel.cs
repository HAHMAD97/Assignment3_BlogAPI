using System;
using System.Net;
namespace BlogAPI.Core.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}