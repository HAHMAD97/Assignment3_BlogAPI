using BlogAPI.Core.Interfaces;
using BlogAPI.Core.Models;
using BlogAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace BlogAPI.Infrastructure.Repositories;
public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _context;

    public CommentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Comment?> GetCommentByIdAsync(int id)
    {
        return await _context.Comments.FindAsync(id);
    }

    public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
    {
        return await _context.Comments.ToListAsync();
    }

    public async Task<IEnumerable<Comment>> GetAllCommentsByPostIdAsync(int postId)
    {
        return await _context.Comments.Where(c => c.PostId == postId).ToListAsync();
    }

    public async Task<Comment> CreateCommentAsync(Comment comment)
    {
        comment.CreatedDate = DateTime.UtcNow;
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> UpdateCommentAsync(Comment comment)
    {
        var existingComment = await _context.Comments.FindAsync(comment.Id);
        if (existingComment == null)
        {
            return null;
        }
        existingComment.Name = comment.Name;
        existingComment.Email = comment.Email;
        existingComment.Content = comment.Content;
        await _context.SaveChangesAsync();
        return existingComment;
    }
    public async Task<bool> DeleteCommentAsync(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
        {
            return false;
        }
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> ExistsCommentAsync(int id)
    {
        return await _context.Comments.AnyAsync(c => c.Id == id);
    }
}