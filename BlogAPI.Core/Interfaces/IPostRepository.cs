using BlogAPI.Core.Models;
namespace BlogAPI.Core.Interfaces;

public interface IPostRepository
{
    Task<Post> GetPostByIdAsync(int id);
    Task<IEnumerable<Post>> GetAllPostsAsync();
    Task<Post> CreatePostAsync(Post post);
    Task<Post> UpdatePostAsync(Post post);
    Task<bool> DeletePostAsync(int id);
}
