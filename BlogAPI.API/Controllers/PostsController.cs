using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using BlogAPI.Core.DTOs;
using BlogAPI.Core.Interfaces;
using BlogAPI.Core.Models;
namespace BlogAPI.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostRepository _postRepository;
    public PostsController(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllPosts()
    {
        var posts = await _postRepository.GetAllPostsAsync();
        
        return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostById(int id)
    {
        var post = await _postRepository.GetPostByIdAsync(id);
        if (post == null)
        {
            return NotFound(new { message = $"Post with ID {id} not found" });
        }
        
        return Ok(post);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] PostCreateDto postDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var post = new Post
        {
            Title = postDto.Title,
            Content = postDto.Content,
            Author = postDto.Author
        };

        var createdPost = await _postRepository.CreatePostAsync(post);

        return CreatedAtAction
            (nameof(GetPostById),
            new { id = createdPost.Id },
            createdPost
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(int id, [FromBody] PostUpdateDto postDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var exists = await _postRepository.ExistsPostAsync(id);
        if (!exists)
        {
            return NotFound(new { message = $"Post with ID {id} not found" });
        }

        var post = new Post
        {
            Id = id,
            Title = postDto.Title,
            Content = postDto.Content,
            Author = postDto.Author
        };

        var updatedPost = await _postRepository.UpdatePostAsync(post);
        
        return Ok(updatedPost);

    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchPost(int id, [FromBody] JsonPatchDocument<Post> patchDoc)
    {
        if (patchDoc == null)
        {
            return BadRequest(new { message = "Patch document is null" });
        }

        var post = await _postRepository.GetPostByIdAsync(id);
        if (post == null)
        {
            return NotFound(new { message = $"Post with ID {id} not found" });
        }


        patchDoc.ApplyTo(post, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        post.UpdatedDate = DateTime.UtcNow;
        await _postRepository.UpdatePostAsync(post);

        return Ok(post);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var deleted = await _postRepository.DeletePostAsync(id);
        if (!deleted)
        {
            return NotFound(new { message = $"Post with ID {id} not found" });
        }

        return NoContent();
    }
}