using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebReddkaApi.Data;
using WebReddkaApi.Data.Entities;
using WebReddkaApi.Models.Posts;

namespace WebReddkaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController(AppDbContext context, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPostsAsync()
    {
        var posts = await context.Posts
            .ProjectTo<PostItemModel>(mapper.ConfigurationProvider)
            .ToListAsync();
        return Ok(posts);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePostAsync([FromForm] PostCreateModel model)
    {
        var postEntity = mapper.Map<PostEntity>(model);
        if (model.Image != null)
        {
            
        }
        if (model.Video != null)
        {
            
        }
        context.Posts.Add(postEntity);
        await context.SaveChangesAsync();
        return Ok();
    }
}
