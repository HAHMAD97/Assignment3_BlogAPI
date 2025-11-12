using Microsoft.AspNetCore.JsonPatch;
using BlogAPI.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BlogAPI.Infrastructure.Data;

namespace BlogAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogPostsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BlogPostsController(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
        