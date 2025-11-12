using BlogAPI.Core.Models;
namespace BlogAPI.Core.Interfaces;

public interface ICommentRepository
{
    Task<Comment?> GetCommentByIdAsync(int id);
    Task<IEnumerable<Comment>> GetAllCommentsAsync();
    Task<IEnumerable<Comment>> GetAllCommentsByPostIdAsync(int postId);
    Task<Comment> CreateCommentAsync(Comment comment);
    Task<Comment?> UpdateCommentAsync(Comment comment);
    Task<bool> DeleteCommentAsync(int id);
}
