using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using BlogAPI.Core.DTOs;
using BlogAPI.Core.Interfaces;
using BlogAPI.Core.Models;
namespace BlogAPI.API.Controllers;


[ApiController]
[Route("api")]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    public CommentsController(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    [HttpGet("[controller]")]
    public async Task<IActionResult> GetAllComments()
    {
        var comments = await _commentRepository.GetAllCommentsAsync();
        return Ok(comments);
    }

    [HttpGet("[controller]/{id}")]
    public async Task<IActionResult> GetCommentById(int id)
    {
        var comment = await _commentRepository.GetCommentByIdAsync(id);
        if (comment == null)
        {
            return NotFound(new { message = $"Comment with ID {id} not found" });
        }
        return Ok(comment);
    }

    [HttpGet("posts/{postId}/[controller]")]
    public async Task<IActionResult> GetAllCommentsByPostId(int postId)
    {
        var comments = await _commentRepository.GetAllCommentsByPostIdAsync(postId);
        if (comments == null || !comments.Any())
        {
            return NotFound(new { message = $"No comments found for post ID {postId}" });
        }
        return Ok(comments);
    }

    [HttpPost("posts/{postId}/[controller]")]
    public async Task<IActionResult> CreateComment(int postId, [FromBody] CommentCreateDto commentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var comment = new Comment
        {
            PostId = postId,
            Name = commentDto.Name,
            Email = commentDto.Email,
            Content = commentDto.Content
        };

        var createdComment = await _commentRepository.CreateCommentAsync(comment);

        return CreatedAtAction
            (nameof(GetCommentById),
            new { id = createdComment.Id },
            createdComment
        );
    }

    [HttpPut("[controller]/{id}")]
    public async Task<IActionResult> UpdateComment(int id, [FromBody] CommentUpdateDto commentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var exists = await _commentRepository.ExistsCommentAsync(id);
        if (!exists)
        {
            return NotFound(new { message = $"Comment with ID {id} not found" });
        }

        var comment = new Comment
        {
            Id = id,
            Name = commentDto.Name,
            Email = commentDto.Email,
            Content = commentDto.Content
        };

        var updatedComment = await _commentRepository.UpdateCommentAsync(comment);
        return Ok(updatedComment);
    }
    

    [HttpPatch("[controller]/{id}")]
    public async Task<IActionResult> PatchComment(int id, [FromBody] JsonPatchDocument<Comment> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest(new { message = "Patch document is null" });
        }

        var comment = await _commentRepository.GetCommentByIdAsync(id);
        if (comment == null)
        {
            return NotFound(new { message = $"Comment with ID {id} not found" });
        }

        patchDoc.ApplyTo(comment, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        await _commentRepository.UpdateCommentAsync(comment);

        return Ok(comment);
    }


    [HttpDelete("[controller]/{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var deleted = await _commentRepository.DeleteCommentAsync(id);
        if (!deleted)
        {
            return NotFound(new { message = $"Comment with ID {id} not found" });
        }

        return NoContent();
    }
}